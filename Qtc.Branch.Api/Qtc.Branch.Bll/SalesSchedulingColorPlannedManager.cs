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
	public static class SalesSchedulingColorPlannedManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static SalesSchedulingColorPlannedCollection GetList()
		{
			SalesSchedulingColorPlannedCriteria salesschedulingcolorplanned = new SalesSchedulingColorPlannedCriteria();
			return GetList(salesschedulingcolorplanned, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingColorPlannedCollection GetList(string sortExpression)
		{
			SalesSchedulingColorPlannedCriteria salesschedulingcolorplanned = new SalesSchedulingColorPlannedCriteria();
			return GetList(salesschedulingcolorplanned, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingColorPlannedCollection GetList(int startRowIndex, int maximumRows)
		{
			SalesSchedulingColorPlannedCriteria salesschedulingcolorplanned = new SalesSchedulingColorPlannedCriteria();
			return GetList(salesschedulingcolorplanned, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingColorPlannedCollection GetList(SalesSchedulingColorPlannedCriteria salesschedulingcolorplannedCriteria)
		{
			return GetList(salesschedulingcolorplannedCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingColorPlannedCollection GetList(SalesSchedulingColorPlannedCriteria salesschedulingcolorplannedCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			SalesSchedulingColorPlannedCollection myCollection = SalesSchedulingColorPlannedDB.GetList(salesschedulingcolorplannedCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new SalesSchedulingColorPlannedComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new SalesSchedulingColorPlannedCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(SalesSchedulingColorPlannedCriteria salesschedulingcolorplannedCriteria)
		{
			return SalesSchedulingColorPlannedDB.SelectCountForGetList(salesschedulingcolorplannedCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingColorPlanned GetItem(int id)
		{
			SalesSchedulingColorPlanned salesschedulingcolorplanned = SalesSchedulingColorPlannedDB.GetItem(id);
			return salesschedulingcolorplanned;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(SalesSchedulingColorPlanned mySalesSchedulingColorPlanned)
		{
			if (!mySalesSchedulingColorPlanned.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid salesschedulingcolorplanned. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				//if(mySalesSchedulingColorPlanned.mId != 0)
				//	AuditUpdate(mySalesSchedulingColorPlanned);

				int id = SalesSchedulingColorPlannedDB.Save(mySalesSchedulingColorPlanned);
				//if(mySalesSchedulingColorPlanned.mId == 0)
				//	AuditInsert(mySalesSchedulingColorPlanned, id);

				mySalesSchedulingColorPlanned.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(SalesSchedulingColorPlanned mySalesSchedulingColorPlanned)
		{
			if (SalesSchedulingColorPlannedDB.Delete(mySalesSchedulingColorPlanned.mId))
			{
				AuditDelete(mySalesSchedulingColorPlanned);
				return mySalesSchedulingColorPlanned.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(SalesSchedulingColorPlanned mySalesSchedulingColorPlanned, int id)
		{
			AuditManager.AuditInsert(false, mySalesSchedulingColorPlanned.mUserFullName,(int)(Tables.ptApi_SalesSchedulingColorPlanned),id,"Insert");
		}
		private static void AuditDelete( SalesSchedulingColorPlanned mySalesSchedulingColorPlanned)
		{
			AuditManager.AuditDelete(false, mySalesSchedulingColorPlanned.mUserFullName,(int)(Tables.ptApi_SalesSchedulingColorPlanned),mySalesSchedulingColorPlanned.mId,"Delete");
		}
		private static void AuditUpdate( SalesSchedulingColorPlanned mySalesSchedulingColorPlanned)
		{
			SalesSchedulingColorPlanned old_salesschedulingcolorplanned = GetItem(mySalesSchedulingColorPlanned.mId);
			AuditCollection audit_collection = SalesSchedulingColorPlannedAudit.Audit(mySalesSchedulingColorPlanned, old_salesschedulingcolorplanned);
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
		private class SalesSchedulingColorPlannedComparer : IComparer < SalesSchedulingColorPlanned >
		{
			private string _sortColumn;
			private bool _reverse;
			public SalesSchedulingColorPlannedComparer(string sortExpression)
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

			public int Compare(SalesSchedulingColorPlanned x, SalesSchedulingColorPlanned y)
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