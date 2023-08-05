using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class AuditMessageAudit
	{
		public static AuditCollection Audit(AuditMessage auditmessage,AuditMessage auditmessageOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (auditmessage.mMessage != auditmessageOld.mMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessage);
				audit.mField = "message";
				audit.mOldValue = auditmessageOld.mMessage;
				audit.mNewValue = auditmessage.mMessage;
				audit_collection.Add(audit);
			}

			if (auditmessage.mRemarks != auditmessageOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessage);
				audit.mField = "remarks";
				audit.mOldValue = auditmessageOld.mRemarks;
				audit.mNewValue = auditmessage.mRemarks;
				audit_collection.Add(audit);
			}

			if (auditmessage.mDateCreated != auditmessageOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessage);
				audit.mField = "date_created";
				audit.mOldValue = auditmessageOld.mDateCreated.ToString();
				audit.mNewValue = auditmessage.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (auditmessage.mUserId != auditmessageOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessage);
				audit.mField = "user_id";
				audit.mOldValue = auditmessageOld.mUserId.ToString();
				audit.mNewValue = auditmessage.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (auditmessage.mStatus != auditmessageOld.mStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessage);
				audit.mField = "status";
				audit.mOldValue = auditmessageOld.mStatus;
				audit.mNewValue = auditmessage.mStatus;
				audit_collection.Add(audit);
			}

			if (auditmessage.mDatestamp != auditmessageOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, auditmessage);
				audit.mField = "datestamp";
				audit.mOldValue = auditmessageOld.mDatestamp.ToString();
				audit.mNewValue = auditmessage.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, AuditMessage auditmessage)
		{
			audit.mUserFullName = auditmessage.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_AuditMessage);
			audit.mRowId = auditmessage.mId;
			audit.mAction = 2;
		}
	}
}