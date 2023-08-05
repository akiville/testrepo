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
	public static class MessengerDetailManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static MessengerDetailCollection GetList()
		{
			MessengerDetailCriteria messengerdetail = new MessengerDetailCriteria();
			return GetList(messengerdetail, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerDetailCollection GetList(string sortExpression)
		{
			MessengerDetailCriteria messengerdetail = new MessengerDetailCriteria();
			return GetList(messengerdetail, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerDetailCollection GetList(int startRowIndex, int maximumRows)
		{
			MessengerDetailCriteria messengerdetail = new MessengerDetailCriteria();
			return GetList(messengerdetail, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerDetailCollection GetList(MessengerDetailCriteria messengerdetailCriteria)
		{
			return GetList(messengerdetailCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerDetailCollection GetList(MessengerDetailCriteria messengerdetailCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			MessengerDetailCollection myCollection = MessengerDetailDB.GetList(messengerdetailCriteria);
            if(myCollection.Count > 0)
            {
                foreach (MessengerDetail messenger_detail in myCollection)
                {
                    MessengerStatusCollection messenger_status_list = new MessengerStatusCollection();
                    MessengerStatusCriteria messenger_status_criteria = new MessengerStatusCriteria();
                    messenger_status_criteria.mMessengerDetailId = messenger_detail.mId;
                    messenger_status_list = MessengerStatusManager.GetList(messenger_status_criteria);
                    messenger_detail.mMessengerStatus = messenger_status_list;
                }
             
            }
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new MessengerDetailComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new MessengerDetailCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(MessengerDetailCriteria messengerdetailCriteria)
		{
			return MessengerDetailDB.SelectCountForGetList(messengerdetailCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerDetail GetItem(int id)
		{
			MessengerDetail messengerdetail = MessengerDetailDB.GetItem(id);
			return messengerdetail;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(MessengerDetail myMessengerDetail)
		{
			if (!myMessengerDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid messengerdetail. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myMessengerDetail.mId != 0)
					AuditUpdate(myMessengerDetail);

				int id = MessengerDetailDB.Save(myMessengerDetail);
				if(myMessengerDetail.mId == 0)
					AuditInsert(myMessengerDetail, id);

                if (myMessengerDetail.mMessengerStatus != null)
                {
                    foreach(MessengerStatus messenger_status in myMessengerDetail.mMessengerStatus)
                    {
                        messenger_status.mMessengerDetailId = id;
                        MessengerStatusManager.Save(messenger_status);
                    }
                }
				myMessengerDetail.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(MessengerDetail myMessengerDetail)
		{
			if (MessengerDetailDB.Delete(myMessengerDetail.mId))
			{
				AuditDelete(myMessengerDetail);
				return myMessengerDetail.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(MessengerDetail myMessengerDetail, int id)
		{
			AuditManager.AuditInsert(false, myMessengerDetail.mUserFullName,(int)(Tables.ptApi_MessengerDetail),id,"Insert");
		}
		private static void AuditDelete( MessengerDetail myMessengerDetail)
		{
			AuditManager.AuditDelete(false, myMessengerDetail.mUserFullName,(int)(Tables.ptApi_MessengerDetail),myMessengerDetail.mId,"Delete");
		}
		private static void AuditUpdate( MessengerDetail myMessengerDetail)
		{
			MessengerDetail old_messengerdetail = GetItem(myMessengerDetail.mId);
			AuditCollection audit_collection = MessengerDetailAudit.Audit(myMessengerDetail, old_messengerdetail);
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
		private class MessengerDetailComparer : IComparer < MessengerDetail >
		{
			private string _sortColumn;
			private bool _reverse;
			public MessengerDetailComparer(string sortExpression)
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

			public int Compare(MessengerDetail x, MessengerDetail y)
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