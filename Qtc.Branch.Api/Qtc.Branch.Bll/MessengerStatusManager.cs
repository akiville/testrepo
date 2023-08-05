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
	public static class MessengerStatusManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static MessengerStatusCollection GetList()
		{
			MessengerStatusCriteria messengerstatus = new MessengerStatusCriteria();
			return GetList(messengerstatus, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerStatusCollection GetList(string sortExpression)
		{
			MessengerStatusCriteria messengerstatus = new MessengerStatusCriteria();
			return GetList(messengerstatus, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerStatusCollection GetList(int startRowIndex, int maximumRows)
		{
			MessengerStatusCriteria messengerstatus = new MessengerStatusCriteria();
			return GetList(messengerstatus, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerStatusCollection GetList(MessengerStatusCriteria messengerstatusCriteria)
		{
			return GetList(messengerstatusCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerStatusCollection GetList(MessengerStatusCriteria messengerstatusCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			MessengerStatusCollection myCollection = MessengerStatusDB.GetList(messengerstatusCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new MessengerStatusComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new MessengerStatusCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(MessengerStatusCriteria messengerstatusCriteria)
		{
			return MessengerStatusDB.SelectCountForGetList(messengerstatusCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerStatus GetItem(int id)
		{
			MessengerStatus messengerstatus = MessengerStatusDB.GetItem(id);
			return messengerstatus;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(MessengerStatus myMessengerStatus)
		{
			if (!myMessengerStatus.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid messengerstatus. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myMessengerStatus.mId != 0)
					AuditUpdate(myMessengerStatus);

				int id = MessengerStatusDB.Save(myMessengerStatus);
				if(myMessengerStatus.mId == 0)
					AuditInsert(myMessengerStatus, id);

				myMessengerStatus.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(MessengerStatus myMessengerStatus)
		{
			if (MessengerStatusDB.Delete(myMessengerStatus.mId))
			{
				AuditDelete(myMessengerStatus);
				return myMessengerStatus.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(MessengerStatus myMessengerStatus, int id)
		{
			AuditManager.AuditInsert(false, myMessengerStatus.mUserFullName,(int)(Tables.ptApi_MessengerStatus),id,"Insert");
		}
		private static void AuditDelete( MessengerStatus myMessengerStatus)
		{
			AuditManager.AuditDelete(false, myMessengerStatus.mUserFullName,(int)(Tables.ptApi_MessengerStatus),myMessengerStatus.mId,"Delete");
		}
		private static void AuditUpdate( MessengerStatus myMessengerStatus)
		{
			MessengerStatus old_messengerstatus = GetItem(myMessengerStatus.mId);
			AuditCollection audit_collection = MessengerStatusAudit.Audit(myMessengerStatus, old_messengerstatus);
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
		private class MessengerStatusComparer : IComparer < MessengerStatus >
		{
			private string _sortColumn;
			private bool _reverse;
			public MessengerStatusComparer(string sortExpression)
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

			public int Compare(MessengerStatus x, MessengerStatus y)
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