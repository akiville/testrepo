using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class AnnouncementReplyAudit
	{
		public static AuditCollection Audit(AnnouncementReply announcementreply,AnnouncementReply announcementreplyOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (announcementreply.mAnnouncementId != announcementreplyOld.mAnnouncementId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, announcementreply);
				audit.mField = "announcement_id";
				audit.mOldValue = announcementreplyOld.mAnnouncementId.ToString();
				audit.mNewValue = announcementreply.mAnnouncementId.ToString();
				audit_collection.Add(audit);
			}

			if (announcementreply.mEmployeeId != announcementreplyOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, announcementreply);
				audit.mField = "employee_id";
				audit.mOldValue = announcementreplyOld.mEmployeeId.ToString();
				audit.mNewValue = announcementreply.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (announcementreply.mIsAcknowledge != announcementreplyOld.mIsAcknowledge)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, announcementreply);
				audit.mField = "is_acknowledge";
				audit.mOldValue = announcementreplyOld.mIsAcknowledge.ToString();
				audit.mNewValue = announcementreply.mIsAcknowledge.ToString();
				audit_collection.Add(audit);
			}

			if (announcementreply.mRemarks != announcementreplyOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, announcementreply);
				audit.mField = "remarks";
				audit.mOldValue = announcementreplyOld.mRemarks;
				audit.mNewValue = announcementreply.mRemarks;
				audit_collection.Add(audit);
			}

			if (announcementreply.mAknowledgementDate != announcementreplyOld.mAknowledgementDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, announcementreply);
				audit.mField = "aknowledgement_date";
				audit.mOldValue = announcementreplyOld.mAknowledgementDate.ToString();
				audit.mNewValue = announcementreply.mAknowledgementDate.ToString();
				audit_collection.Add(audit);
			}

			if (announcementreply.mDatetime != announcementreplyOld.mDatetime)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, announcementreply);
				audit.mField = "datetime";
				audit.mOldValue = announcementreplyOld.mDatetime.ToString();
				audit.mNewValue = announcementreply.mDatetime.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, AnnouncementReply announcementreply)
		{
			audit.mUserFullName = announcementreply.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_AnnouncementReply);
			audit.mRowId = announcementreply.mId;
			audit.mAction = 2;
		}
	}
}