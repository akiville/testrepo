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
	public static class RequestTopicManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RequestTopicCollection GetList()
		{
			RequestTopicCriteria requesttopic = new RequestTopicCriteria();
			return GetList(requesttopic, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestTopicCollection GetList(string sortExpression)
		{
			RequestTopicCriteria requesttopic = new RequestTopicCriteria();
			return GetList(requesttopic, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestTopicCollection GetList(int startRowIndex, int maximumRows)
		{
			RequestTopicCriteria requesttopic = new RequestTopicCriteria();
			return GetList(requesttopic, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestTopicCollection GetList(RequestTopicCriteria requesttopicCriteria)
		{
			return GetList(requesttopicCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestTopicCollection GetList(RequestTopicCriteria requesttopicCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RequestTopicCollection myCollection = RequestTopicDB.GetList(requesttopicCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RequestTopicComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RequestTopicCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RequestTopicCriteria requesttopicCriteria)
		{
			return RequestTopicDB.SelectCountForGetList(requesttopicCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestTopic GetItem(int id)
		{
			RequestTopic requesttopic = RequestTopicDB.GetItem(id);
			return requesttopic;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RequestTopic myRequestTopic)
		{
			if (!myRequestTopic.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid requesttopic. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRequestTopic.mId != 0)
					AuditUpdate(myRequestTopic);

				int id = RequestTopicDB.Save(myRequestTopic);
				if(myRequestTopic.mId == 0)
					AuditInsert(myRequestTopic, id);

				myRequestTopic.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RequestTopic myRequestTopic)
		{
			if (RequestTopicDB.Delete(myRequestTopic.mId))
			{
				AuditDelete(myRequestTopic);
				return myRequestTopic.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RequestTopic myRequestTopic, int id)
		{
			AuditManager.AuditInsert(false, myRequestTopic.mUserFullName,(int)(Tables.ptApi_RequestTopic),id,"Insert");
		}
		private static void AuditDelete( RequestTopic myRequestTopic)
		{
			AuditManager.AuditDelete(false, myRequestTopic.mUserFullName,(int)(Tables.ptApi_RequestTopic),myRequestTopic.mId,"Delete");
		}
		private static void AuditUpdate( RequestTopic myRequestTopic)
		{
			RequestTopic old_requesttopic = GetItem(myRequestTopic.mId);
			AuditCollection audit_collection = RequestTopicAudit.Audit(myRequestTopic, old_requesttopic);
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
		private class RequestTopicComparer : IComparer < RequestTopic >
		{
			private string _sortColumn;
			private bool _reverse;
			public RequestTopicComparer(string sortExpression)
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

			public int Compare(RequestTopic x, RequestTopic y)
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