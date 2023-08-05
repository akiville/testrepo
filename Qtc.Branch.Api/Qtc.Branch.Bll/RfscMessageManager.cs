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
	public static class RfscMessageManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RfscMessageCollection GetList()
		{
			RfscMessageCriteria rfscmessage = new RfscMessageCriteria();
			return GetList(rfscmessage, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RfscMessageCollection GetList(string sortExpression)
		{
			RfscMessageCriteria rfscmessage = new RfscMessageCriteria();
			return GetList(rfscmessage, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RfscMessageCollection GetList(int startRowIndex, int maximumRows)
		{
			RfscMessageCriteria rfscmessage = new RfscMessageCriteria();
			return GetList(rfscmessage, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RfscMessageCollection GetList(RfscMessageCriteria rfscmessageCriteria)
		{
			return GetList(rfscmessageCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RfscMessageCollection GetList(RfscMessageCriteria rfscmessageCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RfscMessageCollection myCollection = RfscMessageDB.GetList(rfscmessageCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RfscMessageComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RfscMessageCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RfscMessageCriteria rfscmessageCriteria)
		{
			return RfscMessageDB.SelectCountForGetList(rfscmessageCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RfscMessage GetItem(int id)
		{
			RfscMessage rfscmessage = RfscMessageDB.GetItem(id);
			return rfscmessage;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RfscMessage myRfscMessage)
		{
			if (!myRfscMessage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rfscmessage. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRfscMessage.mId != 0)
					AuditUpdate(myRfscMessage);

				int id = RfscMessageDB.Save(myRfscMessage);
				if(myRfscMessage.mId == 0)
					AuditInsert(myRfscMessage, id);

				myRfscMessage.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RfscMessage myRfscMessage)
		{
			if (RfscMessageDB.Delete(myRfscMessage.mId))
			{
				AuditDelete(myRfscMessage);
				return myRfscMessage.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RfscMessage myRfscMessage, int id)
		{
			AuditManager.AuditInsert(false, myRfscMessage.mUserFullName,(int)(Tables.ptApi_RfscMessage),id,"Insert");
		}
		private static void AuditDelete( RfscMessage myRfscMessage)
		{
			AuditManager.AuditDelete(false, myRfscMessage.mUserFullName,(int)(Tables.ptApi_RfscMessage),myRfscMessage.mId,"Delete");
		}
		private static void AuditUpdate( RfscMessage myRfscMessage)
		{
			RfscMessage old_rfscmessage = GetItem(myRfscMessage.mId);
			AuditCollection audit_collection = RfscMessageAudit.Audit(myRfscMessage, old_rfscmessage);
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
		private class RfscMessageComparer : IComparer < RfscMessage >
		{
			private string _sortColumn;
			private bool _reverse;
			public RfscMessageComparer(string sortExpression)
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

			public int Compare(RfscMessage x, RfscMessage y)
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