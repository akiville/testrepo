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
	public static class MessengerParticipantManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static MessengerParticipantCollection GetList()
		{
			MessengerParticipantCriteria messengerparticipant = new MessengerParticipantCriteria();
			return GetList(messengerparticipant, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerParticipantCollection GetList(string sortExpression)
		{
			MessengerParticipantCriteria messengerparticipant = new MessengerParticipantCriteria();
			return GetList(messengerparticipant, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerParticipantCollection GetList(int startRowIndex, int maximumRows)
		{
			MessengerParticipantCriteria messengerparticipant = new MessengerParticipantCriteria();
			return GetList(messengerparticipant, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerParticipantCollection GetList(MessengerParticipantCriteria messengerparticipantCriteria)
		{
			return GetList(messengerparticipantCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerParticipantCollection GetList(MessengerParticipantCriteria messengerparticipantCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			MessengerParticipantCollection myCollection = MessengerParticipantDB.GetList(messengerparticipantCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new MessengerParticipantComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new MessengerParticipantCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(MessengerParticipantCriteria messengerparticipantCriteria)
		{
			return MessengerParticipantDB.SelectCountForGetList(messengerparticipantCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerParticipant GetItem(int id)
		{
			MessengerParticipant messengerparticipant = MessengerParticipantDB.GetItem(id);
			return messengerparticipant;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(MessengerParticipant myMessengerParticipant)
		{
			if (!myMessengerParticipant.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid messengerparticipant. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myMessengerParticipant.mId != 0)
					AuditUpdate(myMessengerParticipant);

				int id = MessengerParticipantDB.Save(myMessengerParticipant);
				if(myMessengerParticipant.mId == 0)
					AuditInsert(myMessengerParticipant, id);

				myMessengerParticipant.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(MessengerParticipant myMessengerParticipant)
		{
			if (MessengerParticipantDB.Delete(myMessengerParticipant.mId))
			{
				AuditDelete(myMessengerParticipant);
				return myMessengerParticipant.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(MessengerParticipant myMessengerParticipant, int id)
		{
			AuditManager.AuditInsert(false, myMessengerParticipant.mUserFullName,(int)(Tables.ptApi_MessengerParticipant),id,"Insert");
		}
		private static void AuditDelete( MessengerParticipant myMessengerParticipant)
		{
			AuditManager.AuditDelete(false, myMessengerParticipant.mUserFullName,(int)(Tables.ptApi_MessengerParticipant),myMessengerParticipant.mId,"Delete");
		}
		private static void AuditUpdate( MessengerParticipant myMessengerParticipant)
		{
			MessengerParticipant old_messengerparticipant = GetItem(myMessengerParticipant.mId);
			AuditCollection audit_collection = MessengerParticipantAudit.Audit(myMessengerParticipant, old_messengerparticipant);
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
		private class MessengerParticipantComparer : IComparer < MessengerParticipant >
		{
			private string _sortColumn;
			private bool _reverse;
			public MessengerParticipantComparer(string sortExpression)
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

			public int Compare(MessengerParticipant x, MessengerParticipant y)
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