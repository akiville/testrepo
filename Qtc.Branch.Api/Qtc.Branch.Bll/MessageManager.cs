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
	public static class MessageManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static MessageCollection GetList()
		{
			MessageCriteria message = new MessageCriteria();
			return GetList(message, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessageCollection GetList(string sortExpression)
		{
			MessageCriteria message = new MessageCriteria();
			return GetList(message, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessageCollection GetList(int startRowIndex, int maximumRows)
		{
			MessageCriteria message = new MessageCriteria();
			return GetList(message, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessageCollection GetList(MessageCriteria messageCriteria)
		{
			return GetList(messageCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessageCollection GetList(MessageCriteria messageCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			MessageCollection myCollection = MessageDB.GetList(messageCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new MessageComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new MessageCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(MessageCriteria messageCriteria)
		{
			return MessageDB.SelectCountForGetList(messageCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Message GetItem(int id)
		{
			Message message = MessageDB.GetItem(id);
			return message;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Message myMessage)
		{
			if (!myMessage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid message. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myMessage.mId != 0)
					AuditUpdate(myMessage);

				int id = MessageDB.Save(myMessage);
				if(myMessage.mId == 0)
					AuditInsert(myMessage, id);

				myMessage.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Message myMessage)
		{
			if (MessageDB.Delete(myMessage.mId))
			{
				AuditDelete(myMessage);
				return myMessage.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Message myMessage, int id)
		{
			AuditManager.AuditInsert(false, myMessage.mUserFullName,(int)(Tables.ptApi_Message),id,"Insert");
		}
		private static void AuditDelete( Message myMessage)
		{
			AuditManager.AuditDelete(false, myMessage.mUserFullName,(int)(Tables.ptApi_Message),myMessage.mId,"Delete");
		}
		private static void AuditUpdate( Message myMessage)
		{
			Message old_message = GetItem(myMessage.mId);
			AuditCollection audit_collection = MessageAudit.Audit(myMessage, old_message);
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
		private class MessageComparer : IComparer < Message >
		{
			private string _sortColumn;
			private bool _reverse;
			public MessageComparer(string sortExpression)
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

			public int Compare(Message x, Message y)
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