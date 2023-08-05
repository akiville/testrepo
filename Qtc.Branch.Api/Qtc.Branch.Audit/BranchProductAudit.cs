using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class BranchProductAudit
	{
		public static AuditCollection Audit(BranchProduct branchproduct,BranchProduct branchproductOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (branchproduct.mBranchId != branchproductOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchproduct);
				audit.mField = "branch_id";
				audit.mOldValue = branchproductOld.mBranchId.ToString();
				audit.mNewValue = branchproduct.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (branchproduct.mProductId != branchproductOld.mProductId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchproduct);
				audit.mField = "product_id";
				audit.mOldValue = branchproductOld.mProductId.ToString();
				audit.mNewValue = branchproduct.mProductId.ToString();
				audit_collection.Add(audit);
			}

			if (branchproduct.mUserId != branchproductOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchproduct);
				audit.mField = "user_id";
				audit.mOldValue = branchproductOld.mUserId.ToString();
				audit.mNewValue = branchproduct.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (branchproduct.mDatestamp != branchproductOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchproduct);
				audit.mField = "datestamp";
				audit.mOldValue = branchproductOld.mDatestamp.ToString();
				audit.mNewValue = branchproduct.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, BranchProduct branchproduct)
		{
			audit.mUserFullName = branchproduct.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_BranchProduct);
			audit.mRowId = branchproduct.mId;
			audit.mAction = 2;
		}
	}
}