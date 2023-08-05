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
	public static class SalesScheduleManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static SalesScheduleCollection GetList()
		{
			SalesScheduleCriteria salesschedule = new SalesScheduleCriteria();
			return GetList(salesschedule, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesScheduleCollection GetList(string sortExpression)
		{
			SalesScheduleCriteria salesschedule = new SalesScheduleCriteria();
			return GetList(salesschedule, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesScheduleCollection GetList(int startRowIndex, int maximumRows)
		{
			SalesScheduleCriteria salesschedule = new SalesScheduleCriteria();
			return GetList(salesschedule, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesScheduleCollection GetList(SalesScheduleCriteria salesscheduleCriteria)
		{
			return GetList(salesscheduleCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesScheduleCollection GetList(SalesScheduleCriteria salesscheduleCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			SalesScheduleCollection myCollection = SalesScheduleDB.GetList(salesscheduleCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new SalesScheduleComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new SalesScheduleCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(SalesScheduleCriteria salesscheduleCriteria)
		{
			return SalesScheduleDB.SelectCountForGetList(salesscheduleCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedule GetItem(int id)
		{
			SalesSchedule salesschedule = SalesScheduleDB.GetItem(id);
			return salesschedule;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(SalesSchedule mySalesSchedule)
		{
			if (!mySalesSchedule.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid salesschedule. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(mySalesSchedule.mId != 0)
					AuditUpdate(mySalesSchedule);

				int id = SalesScheduleDB.Save(mySalesSchedule);
				if(mySalesSchedule.mId == 0)
					AuditInsert(mySalesSchedule, id);

				mySalesSchedule.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(SalesSchedule mySalesSchedule)
		{
			if (SalesScheduleDB.Delete(mySalesSchedule.mId))
			{
				AuditDelete(mySalesSchedule);
				return mySalesSchedule.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(SalesSchedule mySalesSchedule, int id)
		{
			AuditManager.AuditInsert(false, mySalesSchedule.mUserFullName,(int)(Tables.ptApi_SalesSchedule),id,"Insert");
		}
		private static void AuditDelete( SalesSchedule mySalesSchedule)
		{
			AuditManager.AuditDelete(false, mySalesSchedule.mUserFullName,(int)(Tables.ptApi_SalesSchedule),mySalesSchedule.mId,"Delete");
		}
		private static void AuditUpdate( SalesSchedule mySalesSchedule)
		{
			SalesSchedule old_salesschedule = GetItem(mySalesSchedule.mId);
			AuditCollection audit_collection = SalesScheduleAudit.Audit(mySalesSchedule, old_salesschedule);
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
		private class SalesScheduleComparer : IComparer < SalesSchedule >
		{
			private string _sortColumn;
			private bool _reverse;
			public SalesScheduleComparer(string sortExpression)
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

			public int Compare(SalesSchedule x, SalesSchedule y)
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