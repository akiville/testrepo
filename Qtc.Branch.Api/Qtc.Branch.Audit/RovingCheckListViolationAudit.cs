using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingCheckListViolationAudit
	{
		public static AuditCollection Audit(RovingCheckListViolation rovingchecklistviolation,RovingCheckListViolation rovingchecklistviolationOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingchecklistviolation.mRovingChecklistId != rovingchecklistviolationOld.mRovingChecklistId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolation);
				audit.mField = "roving_checklist_id";
				audit.mOldValue = rovingchecklistviolationOld.mRovingChecklistId.ToString();
				audit.mNewValue = rovingchecklistviolation.mRovingChecklistId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingchecklistviolation.mTypeOfViolationId != rovingchecklistviolationOld.mTypeOfViolationId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklistviolation);
				audit.mField = "type_of_violation_id";
				audit.mOldValue = rovingchecklistviolationOld.mTypeOfViolationId.ToString();
				audit.mNewValue = rovingchecklistviolation.mTypeOfViolationId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingCheckListViolation rovingchecklistviolation)
		{
			audit.mUserFullName = rovingchecklistviolation.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingCheckListViolation);
			audit.mRowId = rovingchecklistviolation.mId;
			audit.mAction = 2;
		}
	}
}