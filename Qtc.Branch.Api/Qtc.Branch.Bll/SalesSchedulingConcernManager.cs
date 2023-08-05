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
	public static class SalesSchedulingConcernManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static SalesSchedulingConcernCollection GetList()
		{
			SalesSchedulingConcernCriteria salesschedulingconcern = new SalesSchedulingConcernCriteria();
			return GetList(salesschedulingconcern, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingConcernCollection GetList(string sortExpression)
		{
			SalesSchedulingConcernCriteria salesschedulingconcern = new SalesSchedulingConcernCriteria();
			return GetList(salesschedulingconcern, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingConcernCollection GetList(int startRowIndex, int maximumRows)
		{
			SalesSchedulingConcernCriteria salesschedulingconcern = new SalesSchedulingConcernCriteria();
			return GetList(salesschedulingconcern, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingConcernCollection GetList(SalesSchedulingConcernCriteria salesschedulingconcernCriteria)
		{
			return GetList(salesschedulingconcernCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingConcernCollection GetList(SalesSchedulingConcernCriteria salesschedulingconcernCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			SalesSchedulingConcernCollection myCollection = SalesSchedulingConcernDB.GetList(salesschedulingconcernCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new SalesSchedulingConcernComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new SalesSchedulingConcernCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(SalesSchedulingConcernCriteria salesschedulingconcernCriteria)
		{
			return SalesSchedulingConcernDB.SelectCountForGetList(salesschedulingconcernCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingConcern GetItem(int id)
		{
			SalesSchedulingConcern salesschedulingconcern = SalesSchedulingConcernDB.GetItem(id);
			return salesschedulingconcern;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(SalesSchedulingConcern mySalesSchedulingConcern)
		{
			if (!mySalesSchedulingConcern.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid salesschedulingconcern. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(mySalesSchedulingConcern.mId != 0)
					AuditUpdate(mySalesSchedulingConcern);

				int id = SalesSchedulingConcernDB.Save(mySalesSchedulingConcern);
				if(mySalesSchedulingConcern.mId == 0)
					AuditInsert(mySalesSchedulingConcern, id);

				mySalesSchedulingConcern.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(SalesSchedulingConcern mySalesSchedulingConcern)
		{
			if (SalesSchedulingConcernDB.Delete(mySalesSchedulingConcern.mId))
			{
				AuditDelete(mySalesSchedulingConcern);
				return mySalesSchedulingConcern.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(SalesSchedulingConcern mySalesSchedulingConcern, int id)
		{
			AuditManager.AuditInsert(false, mySalesSchedulingConcern.mUserFullName,(int)(Tables.ptApi_SalesSchedulingConcern),id,"Insert");
		}
		private static void AuditDelete( SalesSchedulingConcern mySalesSchedulingConcern)
		{
			AuditManager.AuditDelete(false, mySalesSchedulingConcern.mUserFullName,(int)(Tables.ptApi_SalesSchedulingConcern),mySalesSchedulingConcern.mId,"Delete");
		}
		private static void AuditUpdate( SalesSchedulingConcern mySalesSchedulingConcern)
		{
			SalesSchedulingConcern old_salesschedulingconcern = GetItem(mySalesSchedulingConcern.mId);
			AuditCollection audit_collection = SalesSchedulingConcernAudit.Audit(mySalesSchedulingConcern, old_salesschedulingconcern);
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
		private class SalesSchedulingConcernComparer : IComparer < SalesSchedulingConcern >
		{
			private string _sortColumn;
			private bool _reverse;
			public SalesSchedulingConcernComparer(string sortExpression)
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

			public int Compare(SalesSchedulingConcern x, SalesSchedulingConcern y)
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