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
	public static class MessengerManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static MessengerCollection GetList()
		{
			MessengerCriteria messenger = new MessengerCriteria();
			return GetList(messenger, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerCollection GetList(string sortExpression)
		{
			MessengerCriteria messenger = new MessengerCriteria();
			return GetList(messenger, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerCollection GetList(int startRowIndex, int maximumRows)
		{
			MessengerCriteria messenger = new MessengerCriteria();
			return GetList(messenger, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerCollection GetList(MessengerCriteria messengerCriteria)
		{
			return GetList(messengerCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerCollection GetList(MessengerCriteria messengerCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			MessengerCollection myCollection = MessengerDB.GetList(messengerCriteria);
            if(myCollection != null)
            {
                foreach (Messenger messenger in myCollection)
                {
                    MessengerDetailCollection messengerDetail_list = new MessengerDetailCollection();
                    MessengerDetailCriteria messenger_detail_criteria = new MessengerDetailCriteria();
                    messenger_detail_criteria.mMessengerId = messenger.mId;
                    messenger_detail_criteria.mEmployeeId = 0;
                    messengerDetail_list = MessengerDetailManager.GetList();
                    messenger.mMessengerDetailCollection = messengerDetail_list;

                    MessengerParticipantCollection messenger_participant_list = new MessengerParticipantCollection();
                    MessengerParticipantCriteria messenger_participant_criteria = new MessengerParticipantCriteria();
                    messenger_participant_criteria.mMessengerId = messenger.mId;
                    messenger_participant_criteria.mEmployeeId = 0;
                    messenger_participant_list = MessengerParticipantManager.GetList(messenger_participant_criteria);
                    messenger.mMessengerParticipantCollection = messenger_participant_list;
                }
                
            }
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new MessengerComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new MessengerCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(MessengerCriteria messengerCriteria)
		{
			return MessengerDB.SelectCountForGetList(messengerCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Messenger GetItem(int id)
		{
			Messenger messenger = MessengerDB.GetItem(id);
			return messenger;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Messenger myMessenger)
		{
			if (!myMessenger.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid messenger. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myMessenger.mId != 0)
					AuditUpdate(myMessenger);

				int id = MessengerDB.Save(myMessenger);
				if(myMessenger.mId == 0)
					AuditInsert(myMessenger, id);

                if(id > 0)
                {
                    if (myMessenger.mMessengerDetailCollection != null)
                    {
                        foreach (MessengerDetail messenger_detail in myMessenger.mMessengerDetailCollection)
                        {
                            messenger_detail.mMessengerId = id;
                            messenger_detail.mUserFullName = "";
                            MessengerDetailManager.Save(messenger_detail);
                        }
                    }

                    if (myMessenger.mMessengerParticipantCollection != null)
                    {
                        foreach (MessengerParticipant messenger_participant in myMessenger.mMessengerParticipantCollection)
                        {
                            messenger_participant.mMessengerId = id;
                            MessengerParticipantManager.Save(messenger_participant);
                        }
                    }
                }
				myMessenger.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Messenger myMessenger)
		{
			if (MessengerDB.Delete(myMessenger.mId))
			{
				AuditDelete(myMessenger);
				return myMessenger.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Messenger myMessenger, int id)
		{
			AuditManager.AuditInsert(false, myMessenger.mUserFullName,(int)(Tables.ptApi_Messenger),id,"Insert");
		}
		private static void AuditDelete( Messenger myMessenger)
		{
			AuditManager.AuditDelete(false, myMessenger.mUserFullName,(int)(Tables.ptApi_Messenger),myMessenger.mId,"Delete");
		}
		private static void AuditUpdate( Messenger myMessenger)
		{
			Messenger old_messenger = GetItem(myMessenger.mId);
			AuditCollection audit_collection = MessengerAudit.Audit(myMessenger, old_messenger);
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
		private class MessengerComparer : IComparer < Messenger >
		{
			private string _sortColumn;
			private bool _reverse;
			public MessengerComparer(string sortExpression)
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

			public int Compare(Messenger x, Messenger y)
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