using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class AuditMessageDetailAudit
	{
		public static AuditCollection Audit(AuditMessageDetail auditmessagedetail,AuditMessageDetail auditmessagedetailOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (auditmessagedetail.mAuditMessageId != auditmessagedetailOld.mAuditMessageId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessagedetail);
				audit.mField = "audit_message_id";
				audit.mOldValue = auditmessagedetailOld.mAuditMessageId.ToString();
				audit.mNewValue = auditmessagedetail.mAuditMessageId.ToString();
				audit_collection.Add(audit);
			}

			if (auditmessagedetail.mMessage != auditmessagedetailOld.mMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessagedetail);
				audit.mField = "message";
				audit.mOldValue = auditmessagedetailOld.mMessage;
				audit.mNewValue = auditmessagedetail.mMessage;
				audit_collection.Add(audit);
			}

			if (auditmessagedetail.mStatus != auditmessagedetailOld.mStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessagedetail);
				audit.mField = "status";
				audit.mOldValue = auditmessagedetailOld.mStatus;
				audit.mNewValue = auditmessagedetail.mStatus;
				audit_collection.Add(audit);
			}

			if (auditmessagedetail.mUserId != auditmessagedetailOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessagedetail);
				audit.mField = "user_id";
				audit.mOldValue = auditmessagedetailOld.mUserId.ToString();
				audit.mNewValue = auditmessagedetail.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (auditmessagedetail.mMessageDate != auditmessagedetailOld.mMessageDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessagedetail);
				audit.mField = "message_date";
				audit.mOldValue = auditmessagedetailOld.mMessageDate.ToString();
				audit.mNewValue = auditmessagedetail.mMessageDate.ToString();
				audit_collection.Add(audit);
			}

			if (auditmessagedetail.mDatestamp != auditmessagedetailOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessagedetail);
				audit.mField = "datestamp";
				audit.mOldValue = auditmessagedetailOld.mDatestamp.ToString();
				audit.mNewValue = auditmessagedetail.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, AuditMessageDetail auditmessagedetail)
		{
			audit.mUserFullName = auditmessagedetail.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_AuditMessageDetail);
			audit.mRowId = auditmessagedetail.mId;
			audit.mAction = 2;
		}
	}
}