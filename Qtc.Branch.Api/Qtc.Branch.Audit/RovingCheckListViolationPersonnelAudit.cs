using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingCheckListViolationPersonnelAudit
	{
		public static AuditCollection Audit(RovingCheckListViolationPersonnel rovingchecklistviolationpersonnel,RovingCheckListViolationPersonnel rovingchecklistviolationpersonnelOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingchecklistviolationpersonnel.mRpsId != rovingchecklistviolationpersonnelOld.mRpsId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolationpersonnel);
				audit.mField = "rps_id";
				audit.mOldValue = rovingchecklistviolationpersonnelOld.mRpsId.ToString();
				audit.mNewValue = rovingchecklistviolationpersonnel.mRpsId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingchecklistviolationpersonnel.mRpsChklistId != rovingchecklistviolationpersonnelOld.mRpsChklistId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolationpersonnel);
				audit.mField = "rps_chklist_id";
				audit.mOldValue = rovingchecklistviolationpersonnelOld.mRpsChklistId.ToString();
				audit.mNewValue = rovingchecklistviolationpersonnel.mRpsChklistId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingchecklistviolationpersonnel.mViolationId != rovingchecklistviolationpersonnelOld.mViolationId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolationpersonnel);
				audit.mField = "violation_id";
				audit.mOldValue = rovingchecklistviolationpersonnelOld.mViolationId.ToString();
				audit.mNewValue = rovingchecklistviolationpersonnel.mViolationId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingchecklistviolationpersonnel.mRclvdDetailId != rovingchecklistviolationpersonnelOld.mRclvdDetailId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolationpersonnel);
				audit.mField = "rclvd_detail_id";
				audit.mOldValue = rovingchecklistviolationpersonnelOld.mRclvdDetailId.ToString();
				audit.mNewValue = rovingchecklistviolationpersonnel.mRclvdDetailId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingchecklistviolationpersonnel.mRovingChecklistViolationId != rovingchecklistviolationpersonnelOld.mRovingChecklistViolationId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolationpersonnel);
				audit.mField = "roving_checklist_violation_id";
				audit.mOldValue = rovingchecklistviolationpersonnelOld.mRovingChecklistViolationId.ToString();
				audit.mNewValue = rovingchecklistviolationpersonnel.mRovingChecklistViolationId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingchecklistviolationpersonnel.mEmployeeId != rovingchecklistviolationpersonnelOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolationpersonnel);
				audit.mField = "employee_id";
				audit.mOldValue = rovingchecklistviolationpersonnelOld.mEmployeeId.ToString();
				audit.mNewValue = rovingchecklistviolationpersonnel.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingCheckListViolationPersonnel rovingchecklistviolationpersonnel)
		{
			audit.mUserFullName = rovingchecklistviolationpersonnel.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingCheckListViolationPersonnel);
			audit.mRowId = rovingchecklistviolationpersonnel.mId;
			audit.mAction = 2;
		}
	}
}