using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Transactions;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Dal;
using Qtc.Branch.Validation;
using Qtc.Branch.Audit;

namespace Qtc.Branch.Bll
{
	[DataObjectAttribute()]
	public static class LmmManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static LmmCollection GetList()
		{
			LmmCriteria lmm = new LmmCriteria();
			return GetList(lmm, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmCollection GetList(string sortExpression)
		{
			LmmCriteria lmm = new LmmCriteria();
			return GetList(lmm, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmCollection GetList(int startRowIndex, int maximumRows)
		{
			LmmCriteria lmm = new LmmCriteria();
			return GetList(lmm, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmCollection GetList(LmmCriteria lmmCriteria)
		{
			return GetList(lmmCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmCollection GetList(LmmCriteria lmmCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			LmmCollection myCollection = LmmDB.GetList(lmmCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new LmmComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new LmmCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(LmmCriteria lmmCriteria)
		{
			return LmmDB.SelectCountForGetList(lmmCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Lmm GetItem(int id)
		{
			Lmm lmm = LmmDB.GetItem(id);
			return lmm;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Lmm myLmm)
		{
			if (!myLmm.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid lmm. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myLmm.mId != 0)
					AuditUpdate(myLmm);

				int id = LmmDB.Save(myLmm);
				if(myLmm.mId == 0)
					AuditInsert(myLmm, id);

				myLmm.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Lmm myLmm)
		{
			if (LmmDB.Delete(myLmm.mId))
			{
				AuditDelete(myLmm);
				return myLmm.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Lmm myLmm, int id)
		{
			AuditManager.AuditInsert(false, myLmm.mUserFullName,(int)(Tables.ptApi_Lmm),id,"Insert");
		}
		private static void AuditDelete( Lmm myLmm)
		{
			AuditManager.AuditDelete(false, myLmm.mUserFullName,(int)(Tables.ptApi_Lmm),myLmm.mId,"Delete");
		}
		private static void AuditUpdate( Lmm myLmm)
		{
			Lmm old_lmm = GetItem(myLmm.mId);
			AuditCollection audit_collection = LmmAudit.Audit(myLmm, old_lmm);
			if (audit_collection != null)
			{
				foreach (BusinessEntities.Audit audit in audit_collection)
				{
					AuditManager.Save(audit);
				}
			}
		}
		#endregion

		#region IComparable
		private class LmmComparer : IComparer < Lmm >
		{
			private string _sortColumn;
			private bool _reverse;
			public LmmComparer(string sortExpression)
			{
				if (string.IsNullOrEmpty(sortExpression))
				{
					sortExpression = "field_name desc";
				}
				_reverse = sortExpression.ToUpperInvariant().EndsWith(" desc", StringComparison.OrdinalIgnoreCase);
				if (_reverse)
				{
					_sortColumn = sortExpression.Substring(0, sortExpression.Length - 5);
				}
				else
				{
					_sortColumn = sortExpression;
				}
			}

			public int Compare(Lmm x, Lmm y)
			{
				int retVal = 0;
				switch (_sortColumn.ToUpperInvariant())
				{
					case "FIELD":
						retVal = string.Compare(x.mRemarks, y.mRemarks, StringComparison.OrdinalIgnoreCase);
						break;
				}
				int _reverseInt = 1;
				if ((_reverse))
				{
					_reverseInt = -1;
				}
				return (retVal * _reverseInt);
			}
		}
		#endregion
	}
}