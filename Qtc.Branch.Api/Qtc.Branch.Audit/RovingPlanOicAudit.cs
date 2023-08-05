using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingPlanOicAudit
	{
		public static AuditCollection Audit(RovingPlanOic rovingplanoic,RovingPlanOic rovingplanoicOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingplanoic.mRecordId != rovingplanoicOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanoic);
				audit.mField = "record_id";
				audit.mOldValue = rovingplanoicOld.mRecordId.ToString();
				audit.mNewValue = rovingplanoic.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanoic.mRovingPlanId != rovingplanoicOld.mRovingPlanId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanoic);
				audit.mField = "roving_plan_id";
				audit.mOldValue = rovingplanoicOld.mRovingPlanId.ToString();
				audit.mNewValue = rovingplanoic.mRovingPlanId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanoic.mEmployeeId != rovingplanoicOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanoic);
				audit.mField = "employee_id";
				audit.mOldValue = rovingplanoicOld.mEmployeeId.ToString();
				audit.mNewValue = rovingplanoic.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingPlanOic rovingplanoic)
		{
			audit.mUserFullName = rovingplanoic.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingPlanOic);
			audit.mRowId = rovingplanoic.mId;
			audit.mAction = 2;
		}
	}
}