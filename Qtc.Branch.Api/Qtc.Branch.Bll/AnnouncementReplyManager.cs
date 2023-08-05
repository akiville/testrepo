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
	public static class AnnouncementReplyManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static AnnouncementReplyCollection GetList()
		{
			AnnouncementReplyCriteria announcementreply = new AnnouncementReplyCriteria();
			return GetList(announcementreply, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnnouncementReplyCollection GetList(string sortExpression)
		{
			AnnouncementReplyCriteria announcementreply = new AnnouncementReplyCriteria();
			return GetList(announcementreply, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnnouncementReplyCollection GetList(int startRowIndex, int maximumRows)
		{
			AnnouncementReplyCriteria announcementreply = new AnnouncementReplyCriteria();
			return GetList(announcementreply, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnnouncementReplyCollection GetList(AnnouncementReplyCriteria announcementreplyCriteria)
		{
			return GetList(announcementreplyCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnnouncementReplyCollection GetList(AnnouncementReplyCriteria announcementreplyCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			AnnouncementReplyCollection myCollection = AnnouncementReplyDB.GetList(announcementreplyCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new AnnouncementReplyComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new AnnouncementReplyCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(AnnouncementReplyCriteria announcementreplyCriteria)
		{
			return AnnouncementReplyDB.SelectCountForGetList(announcementreplyCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AnnouncementReply GetItem(int id)
		{
			AnnouncementReply announcementreply = AnnouncementReplyDB.GetItem(id);
			return announcementreply;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(AnnouncementReply myAnnouncementReply)
		{
			if (!myAnnouncementReply.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid announcementreply. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myAnnouncementReply.mId != 0)
					AuditUpdate(myAnnouncementReply);

				int id = AnnouncementReplyDB.Save(myAnnouncementReply);
				if(myAnnouncementReply.mId == 0)
					AuditInsert(myAnnouncementReply, id);

				myAnnouncementReply.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(AnnouncementReply myAnnouncementReply)
		{
			if (AnnouncementReplyDB.Delete(myAnnouncementReply.mId))
			{
				AuditDelete(myAnnouncementReply);
				return myAnnouncementReply.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(AnnouncementReply myAnnouncementReply, int id)
		{
			AuditManager.AuditInsert(false, myAnnouncementReply.mUserFullName,(int)(Tables.ptApi_AnnouncementReply),id,"Insert");
		}
		private static void AuditDelete( AnnouncementReply myAnnouncementReply)
		{
			AuditManager.AuditDelete(false, myAnnouncementReply.mUserFullName,(int)(Tables.ptApi_AnnouncementReply),myAnnouncementReply.mId,"Delete");
		}
		private static void AuditUpdate( AnnouncementReply myAnnouncementReply)
		{
			AnnouncementReply old_announcementreply = GetItem(myAnnouncementReply.mId);
			AuditCollection audit_collection = AnnouncementReplyAudit.Audit(myAnnouncementReply, old_announcementreply);
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
		private class AnnouncementReplyComparer : IComparer < AnnouncementReply >
		{
			private string _sortColumn;
			private bool _reverse;
			public AnnouncementReplyComparer(string sortExpression)
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

			public int Compare(AnnouncementReply x, AnnouncementReply y)
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