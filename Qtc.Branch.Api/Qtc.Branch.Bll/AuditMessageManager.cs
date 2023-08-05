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
	public static class AuditMessageManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static AuditMessageCollection GetList()
		{
			AuditMessageCriteria auditmessage = new AuditMessageCriteria();
			return GetList(auditmessage, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditMessageCollection GetList(string sortExpression)
		{
			AuditMessageCriteria auditmessage = new AuditMessageCriteria();
			return GetList(auditmessage, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditMessageCollection GetList(int startRowIndex, int maximumRows)
		{
			AuditMessageCriteria auditmessage = new AuditMessageCriteria();
			return GetList(auditmessage, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditMessageCollection GetList(AuditMessageCriteria auditmessageCriteria)
		{
			return GetList(auditmessageCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditMessageCollection GetList(AuditMessageCriteria auditmessageCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			AuditMessageCollection myCollection = AuditMessageDB.GetList(auditmessageCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new AuditMessageComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new AuditMessageCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(AuditMessageCriteria auditmessageCriteria)
		{
			return AuditMessageDB.SelectCountForGetList(auditmessageCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditMessage GetItem(int id)
		{
			AuditMessage auditmessage = AuditMessageDB.GetItem(id);
			return auditmessage;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(AuditMessage myAuditMessage)
		{
			if (!myAuditMessage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid auditmessage. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myAuditMessage.mId != 0)
					AuditUpdate(myAuditMessage);

				int id = AuditMessageDB.Save(myAuditMessage);
				if(myAuditMessage.mId == 0)
					AuditInsert(myAuditMessage, id);

				myAuditMessage.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(AuditMessage myAuditMessage)
		{
			if (AuditMessageDB.Delete(myAuditMessage.mId))
			{
				AuditDelete(myAuditMessage);
				return myAuditMessage.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(AuditMessage myAuditMessage, int id)
		{
			AuditManager.AuditInsert(false, myAuditMessage.mUserFullName,(int)(Tables.ptApi_AuditMessage),id,"Insert");
		}
		private static void AuditDelete( AuditMessage myAuditMessage)
		{
			AuditManager.AuditDelete(false, myAuditMessage.mUserFullName,(int)(Tables.ptApi_AuditMessage),myAuditMessage.mId,"Delete");
		}
		private static void AuditUpdate( AuditMessage myAuditMessage)
		{
			AuditMessage old_auditmessage = GetItem(myAuditMessage.mId);
			AuditCollection audit_collection = AuditMessageAudit.Audit(myAuditMessage, old_auditmessage);
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
		private class AuditMessageComparer : IComparer < AuditMessage >
		{
			private string _sortColumn;
			private bool _reverse;
			public AuditMessageComparer(string sortExpression)
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

			public int Compare(AuditMessage x, AuditMessage y)
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