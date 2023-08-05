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
	public static class IbwManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static IbwCollection GetList()
		{
			IbwCriteria ibw = new IbwCriteria();
			return GetList(ibw, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static IbwCollection GetList(string sortExpression)
		{
			IbwCriteria ibw = new IbwCriteria();
			return GetList(ibw, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static IbwCollection GetList(int startRowIndex, int maximumRows)
		{
			IbwCriteria ibw = new IbwCriteria();
			return GetList(ibw, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static IbwCollection GetList(IbwCriteria ibwCriteria)
		{
			return GetList(ibwCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static IbwCollection GetList(IbwCriteria ibwCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			IbwCollection myCollection = IbwDB.GetList(ibwCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new IbwComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new IbwCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(IbwCriteria ibwCriteria)
		{
			return IbwDB.SelectCountForGetList(ibwCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Ibw GetItem(int id)
		{
			Ibw ibw = IbwDB.GetItem(id);
			return ibw;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Ibw myIbw)
		{
			if (!myIbw.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid ibw. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myIbw.mId != 0)
					AuditUpdate(myIbw);

				int id = IbwDB.Save(myIbw);
				if(myIbw.mId == 0)
					AuditInsert(myIbw, id);

				myIbw.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Ibw myIbw)
		{
			if (IbwDB.Delete(myIbw.mId))
			{
				AuditDelete(myIbw);
				return myIbw.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Ibw myIbw, int id)
		{
			AuditManager.AuditInsert(false, myIbw.mUserFullName,(int)(Tables.ptApi_Ibw),id,"Insert");
		}
		private static void AuditDelete( Ibw myIbw)
		{
			AuditManager.AuditDelete(false, myIbw.mUserFullName,(int)(Tables.ptApi_Ibw),myIbw.mId,"Delete");
		}
		private static void AuditUpdate( Ibw myIbw)
		{
			Ibw old_ibw = GetItem(myIbw.mId);
			AuditCollection audit_collection = IbwAudit.Audit(myIbw, old_ibw);
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
		private class IbwComparer : IComparer < Ibw >
		{
			private string _sortColumn;
			private bool _reverse;
			public IbwComparer(string sortExpression)
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

			public int Compare(Ibw x, Ibw y)
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