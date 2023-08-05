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
	public static class UrgentScheduleChangeManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static UrgentScheduleChangeCollection GetList()
		{
			UrgentScheduleChangeCriteria urgentschedulechange = new UrgentScheduleChangeCriteria();
			return GetList(urgentschedulechange, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeCollection GetList(string sortExpression)
		{
			UrgentScheduleChangeCriteria urgentschedulechange = new UrgentScheduleChangeCriteria();
			return GetList(urgentschedulechange, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeCollection GetList(int startRowIndex, int maximumRows)
		{
			UrgentScheduleChangeCriteria urgentschedulechange = new UrgentScheduleChangeCriteria();
			return GetList(urgentschedulechange, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeCollection GetList(UrgentScheduleChangeCriteria urgentschedulechangeCriteria)
		{
			return GetList(urgentschedulechangeCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeCollection GetList(UrgentScheduleChangeCriteria urgentschedulechangeCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			UrgentScheduleChangeCollection myCollection = UrgentScheduleChangeDB.GetList(urgentschedulechangeCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new UrgentScheduleChangeComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new UrgentScheduleChangeCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(UrgentScheduleChangeCriteria urgentschedulechangeCriteria)
		{
			return UrgentScheduleChangeDB.SelectCountForGetList(urgentschedulechangeCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChange GetItem(int id)
		{
			UrgentScheduleChange urgentschedulechange = UrgentScheduleChangeDB.GetItem(id);

            //UrgentScheduleChangeMessageCriteria urgentScheduleChangeMessageCriteria = new UrgentScheduleChangeMessageCriteria();
            //urgentScheduleChangeMessageCriteria.mUrgentScheduleChangeId = id;
            //urgentScheduleChangeMessageCriteria.mToLmmId = urgentschedulechange.mToLmmId;

            //urgentschedulechange.mUrgentScheduleChangeMessageCollection = UrgentScheduleChangeMessageManager.GetList(urgentScheduleChangeMessageCriteria);

            UrgentScheduleChangeBranchCriteria urgent_schedule_change_criteria = new UrgentScheduleChangeBranchCriteria();

            urgent_schedule_change_criteria.mUrgentScheduleChangeId = id;
            urgentschedulechange.mUrgentScheduleChangeBranchCollection = UrgentScheduleChangeBranchManager.GetList(urgent_schedule_change_criteria);

            return urgentschedulechange;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(UrgentScheduleChange myUrgentScheduleChange)
		{
			if (!myUrgentScheduleChange.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid urgentschedulechange. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myUrgentScheduleChange.mId != 0)
					AuditUpdate(myUrgentScheduleChange);

				int id = UrgentScheduleChangeDB.Save(myUrgentScheduleChange);
				if(myUrgentScheduleChange.mId == 0)
					AuditInsert(myUrgentScheduleChange, id);

				myUrgentScheduleChange.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(UrgentScheduleChange myUrgentScheduleChange)
		{
			if (UrgentScheduleChangeDB.Delete(myUrgentScheduleChange.mId))
			{
				AuditDelete(myUrgentScheduleChange);
				return myUrgentScheduleChange.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(UrgentScheduleChange myUrgentScheduleChange, int id)
		{
			AuditManager.AuditInsert(false, myUrgentScheduleChange.mUserFullName,(int)(Tables.ptApi_UrgentScheduleChange),id,"Insert");
		}
		private static void AuditDelete( UrgentScheduleChange myUrgentScheduleChange)
		{
			AuditManager.AuditDelete(false, myUrgentScheduleChange.mUserFullName,(int)(Tables.ptApi_UrgentScheduleChange),myUrgentScheduleChange.mId,"Delete");
		}
		private static void AuditUpdate( UrgentScheduleChange myUrgentScheduleChange)
		{
			UrgentScheduleChange old_urgentschedulechange = GetItem(myUrgentScheduleChange.mId);
			AuditCollection audit_collection = UrgentScheduleChangeAudit.Audit(myUrgentScheduleChange, old_urgentschedulechange);
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
		private class UrgentScheduleChangeComparer : IComparer < UrgentScheduleChange >
		{
			private string _sortColumn;
			private bool _reverse;
			public UrgentScheduleChangeComparer(string sortExpression)
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

			public int Compare(UrgentScheduleChange x, UrgentScheduleChange y)
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