using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class NonComplianceAudit
	{
		public static AuditCollection Audit(NonCompliance noncompliance,NonCompliance noncomplianceOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (noncompliance.mName != noncomplianceOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, noncompliance);
				audit.mField = "name";
				audit.mOldValue = noncomplianceOld.mName;
				audit.mNewValue = noncompliance.mName;
				audit_collection.Add(audit);
			}

			if (noncompliance.mRemarks != noncomplianceOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, noncompliance);
				audit.mField = "remarks";
				audit.mOldValue = noncomplianceOld.mRemarks;
				audit.mNewValue = noncompliance.mRemarks;
				audit_collection.Add(audit);
			}

			if (noncompliance.mRequiredDetails != noncomplianceOld.mRequiredDetails)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, noncompliance);
				audit.mField = "required_details";
				audit.mOldValue = noncomplianceOld.mRequiredDetails.ToString();
				audit.mNewValue = noncompliance.mRequiredDetails.ToString();
				audit_collection.Add(audit);
			}

			if (noncompliance.mRequiredExplanation != noncomplianceOld.mRequiredExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, noncompliance);
				audit.mField = "required_explanation";
				audit.mOldValue = noncomplianceOld.mRequiredExplanation.ToString();
				audit.mNewValue = noncompliance.mRequiredExplanation.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, NonCompliance noncompliance)
		{
			audit.mUserFullName = noncompliance.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_NonCompliance);
			audit.mRowId = noncompliance.mId;
			audit.mAction = 2;
		}
	}
}