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
	public static class RequestMessageManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RequestMessageCollection GetList()
		{
			RequestMessageCriteria requestmessage = new RequestMessageCriteria();
			return GetList(requestmessage, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestMessageCollection GetList(string sortExpression)
		{
			RequestMessageCriteria requestmessage = new RequestMessageCriteria();
			return GetList(requestmessage, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestMessageCollection GetList(int startRowIndex, int maximumRows)
		{
			RequestMessageCriteria requestmessage = new RequestMessageCriteria();
			return GetList(requestmessage, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestMessageCollection GetList(RequestMessageCriteria requestmessageCriteria)
		{
			return GetList(requestmessageCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestMessageCollection GetList(RequestMessageCriteria requestmessageCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RequestMessageCollection myCollection = RequestMessageDB.GetList(requestmessageCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RequestMessageComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RequestMessageCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RequestMessageCriteria requestmessageCriteria)
		{
			return RequestMessageDB.SelectCountForGetList(requestmessageCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestMessage GetItem(int id)
		{
			RequestMessage requestmessage = RequestMessageDB.GetItem(id);
			return requestmessage;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RequestMessage myRequestMessage)
		{
			if (!myRequestMessage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid requestmessage. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRequestMessage.mId != 0)
					AuditUpdate(myRequestMessage);

				int id = RequestMessageDB.Save(myRequestMessage);
				if(myRequestMessage.mId == 0)
					AuditInsert(myRequestMessage, id);

				myRequestMessage.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RequestMessage myRequestMessage)
		{
			if (RequestMessageDB.Delete(myRequestMessage.mId))
			{
				AuditDelete(myRequestMessage);
				return myRequestMessage.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RequestMessage myRequestMessage, int id)
		{
			AuditManager.AuditInsert(false, myRequestMessage.mUserFullName,(int)(Tables.ptApi_RequestMessage),id,"Insert");
		}
		private static void AuditDelete( RequestMessage myRequestMessage)
		{
			AuditManager.AuditDelete(false, myRequestMessage.mUserFullName,(int)(Tables.ptApi_RequestMessage),myRequestMessage.mId,"Delete");
		}
		private static void AuditUpdate( RequestMessage myRequestMessage)
		{
			RequestMessage old_requestmessage = GetItem(myRequestMessage.mId);
			AuditCollection audit_collection = RequestMessageAudit.Audit(myRequestMessage, old_requestmessage);
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
		private class RequestMessageComparer : IComparer < RequestMessage >
		{
			private string _sortColumn;
			private bool _reverse;
			public RequestMessageComparer(string sortExpression)
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

			public int Compare(RequestMessage x, RequestMessage y)
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