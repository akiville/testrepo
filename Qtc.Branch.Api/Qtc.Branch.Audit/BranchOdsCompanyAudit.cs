using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class BranchOdsCompanyAudit
	{
		public static AuditCollection Audit(BranchOdsCompany branchodscompany,BranchOdsCompany branchodscompanyOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (branchodscompany.mRecordId != branchodscompanyOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchodscompany);
				audit.mField = "record_id";
				audit.mOldValue = branchodscompanyOld.mRecordId.ToString();
				audit.mNewValue = branchodscompany.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (branchodscompany.mBranchId != branchodscompanyOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchodscompany);
				audit.mField = "branch_id";
				audit.mOldValue = branchodscompanyOld.mBranchId.ToString();
				audit.mNewValue = branchodscompany.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (branchodscompany.mOdcCompanyId != branchodscompanyOld.mOdcCompanyId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchodscompany);
				audit.mField = "odc_company_id";
				audit.mOldValue = branchodscompanyOld.mOdcCompanyId.ToString();
				audit.mNewValue = branchodscompany.mOdcCompanyId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, BranchOdsCompany branchodscompany)
		{
			audit.mUserFullName = branchodscompany.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_BranchOdsCompany);
			audit.mRowId = branchodscompany.mId;
			audit.mAction = 2;
		}
	}
}