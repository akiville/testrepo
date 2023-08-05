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
	public static class UrgentScheduleChangeMessageManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static UrgentScheduleChangeMessageCollection GetList()
		{
			UrgentScheduleChangeMessageCriteria urgentschedulechangemessage = new UrgentScheduleChangeMessageCriteria();
			return GetList(urgentschedulechangemessage, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeMessageCollection GetList(string sortExpression)
		{
			UrgentScheduleChangeMessageCriteria urgentschedulechangemessage = new UrgentScheduleChangeMessageCriteria();
			return GetList(urgentschedulechangemessage, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeMessageCollection GetList(int startRowIndex, int maximumRows)
		{
			UrgentScheduleChangeMessageCriteria urgentschedulechangemessage = new UrgentScheduleChangeMessageCriteria();
			return GetList(urgentschedulechangemessage, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeMessageCollection GetList(UrgentScheduleChangeMessageCriteria urgentschedulechangemessageCriteria)
		{
			return GetList(urgentschedulechangemessageCriteria, string.Empty, -1, -1);
		}

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static UrgentScheduleChangeMessageCollection GetListByScheduleId(UrgentScheduleChangeMessageCriteria urgentschedulechangemessageCriteria)
        {
            return GetListByScheduleId(urgentschedulechangemessageCriteria, string.Empty, -1, -1);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static UrgentScheduleChangeMessageCollection GetListByScheduleId(UrgentScheduleChangeMessageCriteria urgentschedulechangemessageCriteria, string sortExpression, int startRowIndex, int maximumRows)
        {
            UrgentScheduleChangeMessageCollection myCollection = UrgentScheduleChangeMessageDB.GetListByScheduleId(urgentschedulechangemessageCriteria);
            if (!string.IsNullOrEmpty(sortExpression))
            {
                myCollection.Sort(new UrgentScheduleChangeMessageComparer(sortExpression));
            }
            if (startRowIndex >= 0 && maximumRows > 0)
            {
                return new UrgentScheduleChangeMessageCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
            }
            return myCollection;
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeMessageCollection GetList(UrgentScheduleChangeMessageCriteria urgentschedulechangemessageCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			UrgentScheduleChangeMessageCollection myCollection = UrgentScheduleChangeMessageDB.GetList(urgentschedulechangemessageCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new UrgentScheduleChangeMessageComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new UrgentScheduleChangeMessageCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(UrgentScheduleChangeMessageCriteria urgentschedulechangemessageCriteria)
		{
			return UrgentScheduleChangeMessageDB.SelectCountForGetList(urgentschedulechangemessageCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeMessage GetItem(int id)
		{
			UrgentScheduleChangeMessage urgentschedulechangemessage = UrgentScheduleChangeMessageDB.GetItem(id);
			return urgentschedulechangemessage;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(UrgentScheduleChangeMessage myUrgentScheduleChangeMessage)
		{
			if (!myUrgentScheduleChangeMessage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid urgentschedulechangemessage. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myUrgentScheduleChangeMessage.mId != 0)
					AuditUpdate(myUrgentScheduleChangeMessage);

				int id = UrgentScheduleChangeMessageDB.Save(myUrgentScheduleChangeMessage);
				if(myUrgentScheduleChangeMessage.mId == 0)
					AuditInsert(myUrgentScheduleChangeMessage, id);

				myUrgentScheduleChangeMessage.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(UrgentScheduleChangeMessage myUrgentScheduleChangeMessage)
		{
			if (UrgentScheduleChangeMessageDB.Delete(myUrgentScheduleChangeMessage.mId))
			{
				AuditDelete(myUrgentScheduleChangeMessage);
				return myUrgentScheduleChangeMessage.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(UrgentScheduleChangeMessage myUrgentScheduleChangeMessage, int id)
		{
			AuditManager.AuditInsert(false, myUrgentScheduleChangeMessage.mUserFullName,(int)(Tables.ptApi_UrgentScheduleChangeMessage),id,"Insert");
		}
		private static void AuditDelete( UrgentScheduleChangeMessage myUrgentScheduleChangeMessage)
		{
			AuditManager.AuditDelete(false, myUrgentScheduleChangeMessage.mUserFullName,(int)(Tables.ptApi_UrgentScheduleChangeMessage),myUrgentScheduleChangeMessage.mId,"Delete");
		}
		private static void AuditUpdate( UrgentScheduleChangeMessage myUrgentScheduleChangeMessage)
		{
			UrgentScheduleChangeMessage old_urgentschedulechangemessage = GetItem(myUrgentScheduleChangeMessage.mId);
			AuditCollection audit_collection = UrgentScheduleChangeMessageAudit.Audit(myUrgentScheduleChangeMessage, old_urgentschedulechangemessage);
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
		private class UrgentScheduleChangeMessageComparer : IComparer < UrgentScheduleChangeMessage >
		{
			private string _sortColumn;
			private bool _reverse;
			public UrgentScheduleChangeMessageComparer(string sortExpression)
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

			public int Compare(UrgentScheduleChangeMessage x, UrgentScheduleChangeMessage y)
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