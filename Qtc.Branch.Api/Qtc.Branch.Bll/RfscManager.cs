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
	public static class RfscManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RfscCollection GetList()
		{
			RfscCriteria rfsc = new RfscCriteria();
			return GetList(rfsc, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RfscCollection GetList(string sortExpression)
		{
			RfscCriteria rfsc = new RfscCriteria();
			return GetList(rfsc, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RfscCollection GetList(int startRowIndex, int maximumRows)
		{
			RfscCriteria rfsc = new RfscCriteria();
			return GetList(rfsc, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RfscCollection GetList(RfscCriteria rfscCriteria)
		{
			return GetList(rfscCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RfscCollection GetList(RfscCriteria rfscCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RfscCollection myCollection = RfscDB.GetList(rfscCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RfscComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RfscCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RfscCriteria rfscCriteria)
		{
			return RfscDB.SelectCountForGetList(rfscCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Rfsc GetItem(int id)
		{
			Rfsc rfsc = RfscDB.GetItem(id);
			return rfsc;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Rfsc myRfsc)
		{
			if (!myRfsc.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rfsc. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRfsc.mId != 0)
					AuditUpdate(myRfsc);

				int id = RfscDB.Save(myRfsc);
				if(myRfsc.mId == 0)
					AuditInsert(myRfsc, id);

				myRfsc.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Rfsc myRfsc)
		{
			if (RfscDB.Delete(myRfsc.mId))
			{
				AuditDelete(myRfsc);
				return myRfsc.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Rfsc myRfsc, int id)
		{
			AuditManager.AuditInsert(false, myRfsc.mUserFullName,(int)(Tables.ptApi_Rfsc),id,"Insert");
		}
		private static void AuditDelete( Rfsc myRfsc)
		{
			AuditManager.AuditDelete(false, myRfsc.mUserFullName,(int)(Tables.ptApi_Rfsc),myRfsc.mId,"Delete");
		}
		private static void AuditUpdate( Rfsc myRfsc)
		{
			Rfsc old_rfsc = GetItem(myRfsc.mId);
			AuditCollection audit_collection = RfscAudit.Audit(myRfsc, old_rfsc);
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
		private class RfscComparer : IComparer < Rfsc >
		{
			private string _sortColumn;
			private bool _reverse;
			public RfscComparer(string sortExpression)
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

			public int Compare(Rfsc x, Rfsc y)
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