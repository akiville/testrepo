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
	public static class MessengerGpsManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static MessengerGpsCollection GetList()
		{
			MessengerGpsCriteria messengergps = new MessengerGpsCriteria();
			return GetList(messengergps, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerGpsCollection GetList(string sortExpression)
		{
			MessengerGpsCriteria messengergps = new MessengerGpsCriteria();
			return GetList(messengergps, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerGpsCollection GetList(int startRowIndex, int maximumRows)
		{
			MessengerGpsCriteria messengergps = new MessengerGpsCriteria();
			return GetList(messengergps, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerGpsCollection GetList(MessengerGpsCriteria messengergpsCriteria)
		{
			return GetList(messengergpsCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerGpsCollection GetList(MessengerGpsCriteria messengergpsCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			MessengerGpsCollection myCollection = MessengerGpsDB.GetList(messengergpsCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new MessengerGpsComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new MessengerGpsCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(MessengerGpsCriteria messengergpsCriteria)
		{
			return MessengerGpsDB.SelectCountForGetList(messengergpsCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static MessengerGps GetItem(int id)
		{
			MessengerGps messengergps = MessengerGpsDB.GetItem(id);
			return messengergps;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(MessengerGps myMessengerGps)
		{
			if (!myMessengerGps.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid messengergps. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myMessengerGps.mId != 0)
					AuditUpdate(myMessengerGps);

				int id = MessengerGpsDB.Save(myMessengerGps);
				if(myMessengerGps.mId == 0)
					AuditInsert(myMessengerGps, id);

				myMessengerGps.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(MessengerGps myMessengerGps)
		{
			if (MessengerGpsDB.Delete(myMessengerGps.mId))
			{
				AuditDelete(myMessengerGps);
				return myMessengerGps.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(MessengerGps myMessengerGps, int id)
		{
			AuditManager.AuditInsert(false, myMessengerGps.mUserFullName,(int)(Tables.ptApi_MessengerGps),id,"Insert");
		}
		private static void AuditDelete( MessengerGps myMessengerGps)
		{
			AuditManager.AuditDelete(false, myMessengerGps.mUserFullName,(int)(Tables.ptApi_MessengerGps),myMessengerGps.mId,"Delete");
		}
		private static void AuditUpdate( MessengerGps myMessengerGps)
		{
			MessengerGps old_messengergps = GetItem(myMessengerGps.mId);
			AuditCollection audit_collection = MessengerGpsAudit.Audit(myMessengerGps, old_messengergps);
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
		private class MessengerGpsComparer : IComparer < MessengerGps >
		{
			private string _sortColumn;
			private bool _reverse;
			public MessengerGpsComparer(string sortExpression)
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

			public int Compare(MessengerGps x, MessengerGps y)
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