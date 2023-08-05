using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DisseminatedLetterSmsReplyAudit
	{
		public static AuditCollection Audit(DisseminatedLetterSmsReply disseminatedlettersmsreply,DisseminatedLetterSmsReply disseminatedlettersmsreplyOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (disseminatedlettersmsreply.mHrLetterId != disseminatedlettersmsreplyOld.mHrLetterId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedlettersmsreply);
				audit.mField = "hr_letter_id";
				audit.mOldValue = disseminatedlettersmsreplyOld.mHrLetterId.ToString();
				audit.mNewValue = disseminatedlettersmsreply.mHrLetterId.ToString();
				audit_collection.Add(audit);
			}

			if (disseminatedlettersmsreply.mActualExpirationDate != disseminatedlettersmsreplyOld.mActualExpirationDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedlettersmsreply);
				audit.mField = "actual_expiration_date";
				audit.mOldValue = disseminatedlettersmsreplyOld.mActualExpirationDate.ToString();
				audit.mNewValue = disseminatedlettersmsreply.mActualExpirationDate.ToString();
				audit_collection.Add(audit);
			}

			if (disseminatedlettersmsreply.mResponseDate != disseminatedlettersmsreplyOld.mResponseDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedlettersmsreply);
				audit.mField = "response_date";
				audit.mOldValue = disseminatedlettersmsreplyOld.mResponseDate.ToString();
				audit.mNewValue = disseminatedlettersmsreply.mResponseDate.ToString();
				audit_collection.Add(audit);
			}

			if (disseminatedlettersmsreply.mLmmId != disseminatedlettersmsreplyOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedlettersmsreply);
				audit.mField = "lmm_id";
				audit.mOldValue = disseminatedlettersmsreplyOld.mLmmId.ToString();
				audit.mNewValue = disseminatedlettersmsreply.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, DisseminatedLetterSmsReply disseminatedlettersmsreply)
		{
			audit.mUserFullName = disseminatedlettersmsreply.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_DisseminatedLetterSmsReply);
			audit.mRowId = disseminatedlettersmsreply.mId;
			audit.mAction = 2;
		}
	}
}