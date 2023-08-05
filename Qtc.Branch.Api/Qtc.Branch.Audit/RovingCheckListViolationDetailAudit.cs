using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingCheckListViolationDetailAudit
	{
		public static AuditCollection Audit(RovingCheckListViolationDetail rovingchecklistviolationdetail,RovingCheckListViolationDetail rovingchecklistviolationdetailOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingchecklistviolationdetail.mRovingChecklistViolationId != rovingchecklistviolationdetailOld.mRovingChecklistViolationId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolationdetail);
				audit.mField = "roving_checklist_violation_id";
				audit.mOldValue = rovingchecklistviolationdetailOld.mRovingChecklistViolationId.ToString();
				audit.mNewValue = rovingchecklistviolationdetail.mRovingChecklistViolationId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingchecklistviolationdetail.mRpsId != rovingchecklistviolationdetailOld.mRpsId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolationdetail);
				audit.mField = "rps_id";
				audit.mOldValue = rovingchecklistviolationdetailOld.mRpsId.ToString();
				audit.mNewValue = rovingchecklistviolationdetail.mRpsId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingchecklistviolationdetail.mRpsChklistId != rovingchecklistviolationdetailOld.mRpsChklistId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolationdetail);
				audit.mField = "rps_chklist_id";
				audit.mOldValue = rovingchecklistviolationdetailOld.mRpsChklistId.ToString();
				audit.mNewValue = rovingchecklistviolationdetail.mRpsChklistId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingchecklistviolationdetail.mViolationId != rovingchecklistviolationdetailOld.mViolationId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolationdetail);
				audit.mField = "violation_id";
				audit.mOldValue = rovingchecklistviolationdetailOld.mViolationId.ToString();
				audit.mNewValue = rovingchecklistviolationdetail.mViolationId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingchecklistviolationdetail.mImageFileName != rovingchecklistviolationdetailOld.mImageFileName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolationdetail);
				audit.mField = "image_file_name";
				audit.mOldValue = rovingchecklistviolationdetailOld.mImageFileName;
				audit.mNewValue = rovingchecklistviolationdetail.mImageFileName;
				audit_collection.Add(audit);
			}

			if (rovingchecklistviolationdetail.mRemarks != rovingchecklistviolationdetailOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolationdetail);
				audit.mField = "remarks";
				audit.mOldValue = rovingchecklistviolationdetailOld.mRemarks;
				audit.mNewValue = rovingchecklistviolationdetail.mRemarks;
				audit_collection.Add(audit);
			}

			if (rovingchecklistviolationdetail.mRecordId != rovingchecklistviolationdetailOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolationdetail);
				audit.mField = "record_id";
				audit.mOldValue = rovingchecklistviolationdetailOld.mRecordId.ToString();
				audit.mNewValue = rovingchecklistviolationdetail.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingCheckListViolationDetail rovingchecklistviolationdetail)
		{
			audit.mUserFullName = rovingchecklistviolationdetail.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingCheckListViolationDetail);
			audit.mRowId = rovingchecklistviolationdetail.mId;
			audit.mAction = 2;
		}
	}
}