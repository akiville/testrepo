using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class AnnouncementAudit
	{
		public static AuditCollection Audit(Announcement announcement,Announcement announcementOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (announcement.mMessage != announcementOld.mMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, announcement);
				audit.mField = "message";
				audit.mOldValue = announcementOld.mMessage;
				audit.mNewValue = announcement.mMessage;
				audit_collection.Add(audit);
			}

			if (announcement.mHeader != announcementOld.mHeader)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, announcement);
				audit.mField = "header";
				audit.mOldValue = announcementOld.mHeader;
				audit.mNewValue = announcement.mHeader;
				audit_collection.Add(audit);
			}

			if (announcement.mFooter != announcementOld.mFooter)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, announcement);
				audit.mField = "footer";
				audit.mOldValue = announcementOld.mFooter;
				audit.mNewValue = announcement.mFooter;
				audit_collection.Add(audit);
			}

			if (announcement.mDateSent != announcementOld.mDateSent)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, announcement);
				audit.mField = "date_sent";
				audit.mOldValue = announcementOld.mDateSent.ToString();
				audit.mNewValue = announcement.mDateSent.ToString();
				audit_collection.Add(audit);
			}

			if (announcement.mUserId != announcementOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, announcement);
				audit.mField = "user_id";
				audit.mOldValue = announcementOld.mUserId.ToString();
				audit.mNewValue = announcement.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (announcement.mDatetime != announcementOld.mDatetime)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, announcement);
				audit.mField = "datetime";
				audit.mOldValue = announcementOld.mDatetime.ToString();
				audit.mNewValue = announcement.mDatetime.ToString();
				audit_collection.Add(audit);
			}

			if (announcement.mIsPosted != announcementOld.mIsPosted)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, announcement);
				audit.mField = "is_posted";
				audit.mOldValue = announcementOld.mIsPosted.ToString();
				audit.mNewValue = announcement.mIsPosted.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Announcement announcement)
		{
			audit.mUserFullName = announcement.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Announcement);
			audit.mRowId = announcement.mId;
			audit.mAction = 2;
		}
	}
}