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
	public static class AnnouncementManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static AnnouncementCollection GetList()
		{
			AnnouncementCriteria announcement = new AnnouncementCriteria();
			return GetList(announcement, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnnouncementCollection GetList(string sortExpression)
		{
			AnnouncementCriteria announcement = new AnnouncementCriteria();
			return GetList(announcement, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnnouncementCollection GetList(int startRowIndex, int maximumRows)
		{
			AnnouncementCriteria announcement = new AnnouncementCriteria();
			return GetList(announcement, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnnouncementCollection GetList(AnnouncementCriteria announcementCriteria)
		{
			return GetList(announcementCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnnouncementCollection GetList(AnnouncementCriteria announcementCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			AnnouncementCollection myCollection = AnnouncementDB.GetList(announcementCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new AnnouncementComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new AnnouncementCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(AnnouncementCriteria announcementCriteria)
		{
			return AnnouncementDB.SelectCountForGetList(announcementCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Announcement GetItem(int id)
		{
			Announcement announcement = AnnouncementDB.GetItem(id);
			return announcement;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Announcement myAnnouncement)
		{
			if (!myAnnouncement.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid announcement. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myAnnouncement.mId != 0)
					AuditUpdate(myAnnouncement);

				int id = AnnouncementDB.Save(myAnnouncement);
				if(myAnnouncement.mId == 0)
					AuditInsert(myAnnouncement, id);

				myAnnouncement.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Announcement myAnnouncement)
		{
			if (AnnouncementDB.Delete(myAnnouncement.mId))
			{
				AuditDelete(myAnnouncement);
				return myAnnouncement.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Announcement myAnnouncement, int id)
		{
			AuditManager.AuditInsert(false, myAnnouncement.mUserFullName,(int)(Tables.ptApi_Announcement),id,"Insert");
		}
		private static void AuditDelete( Announcement myAnnouncement)
		{
			AuditManager.AuditDelete(false, myAnnouncement.mUserFullName,(int)(Tables.ptApi_Announcement),myAnnouncement.mId,"Delete");
		}
		private static void AuditUpdate( Announcement myAnnouncement)
		{
			Announcement old_announcement = GetItem(myAnnouncement.mId);
			AuditCollection audit_collection = AnnouncementAudit.Audit(myAnnouncement, old_announcement);
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
		private class AnnouncementComparer : IComparer < Announcement >
		{
			private string _sortColumn;
			private bool _reverse;
			public AnnouncementComparer(string sortExpression)
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

			public int Compare(Announcement x, Announcement y)
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