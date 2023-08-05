using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DisseminatedLetterExtensionRequestAudit
	{
		public static AuditCollection Audit(DisseminatedLetterExtensionRequest disseminatedletterextensionrequest,DisseminatedLetterExtensionRequest disseminatedletterextensionrequestOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (disseminatedletterextensionrequest.mDisseminatedLetterId != disseminatedletterextensionrequestOld.mDisseminatedLetterId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletterextensionrequest);
				audit.mField = "disseminated_letter_id";
				audit.mOldValue = disseminatedletterextensionrequestOld.mDisseminatedLetterId.ToString();
				audit.mNewValue = disseminatedletterextensionrequest.mDisseminatedLetterId.ToString();
				audit_collection.Add(audit);
			}

			if (disseminatedletterextensionrequest.mLmmId != disseminatedletterextensionrequestOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletterextensionrequest);
				audit.mField = "lmm_id";
				audit.mOldValue = disseminatedletterextensionrequestOld.mLmmId.ToString();
				audit.mNewValue = disseminatedletterextensionrequest.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (disseminatedletterextensionrequest.mRemarks != disseminatedletterextensionrequestOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletterextensionrequest);
				audit.mField = "remarks";
				audit.mOldValue = disseminatedletterextensionrequestOld.mRemarks;
				audit.mNewValue = disseminatedletterextensionrequest.mRemarks;
				audit_collection.Add(audit);
			}

			if (disseminatedletterextensionrequest.mRequestDate != disseminatedletterextensionrequestOld.mRequestDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, disseminatedletterextensionrequest);
				audit.mField = "request_date";
				audit.mOldValue = disseminatedletterextensionrequestOld.mRequestDate.ToString();
				audit.mNewValue = disseminatedletterextensionrequest.mRequestDate.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, DisseminatedLetterExtensionRequest disseminatedletterextensionrequest)
		{
			audit.mUserFullName = disseminatedletterextensionrequest.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_DisseminatedLetterExtensionRequest);
			audit.mRowId = disseminatedletterextensionrequest.mId;
			audit.mAction = 2;
		}
	}
}