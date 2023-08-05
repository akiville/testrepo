using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class LmmAudit
	{
		public static AuditCollection Audit(Lmm lmm,Lmm lmmOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (lmm.mBranchId != lmmOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmm);
				audit.mField = "branch_id";
				audit.mOldValue = lmmOld.mBranchId.ToString();
				audit.mNewValue = lmm.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (lmm.mLmmId != lmmOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmm);
				audit.mField = "lmm_id";
				audit.mOldValue = lmmOld.mLmmId.ToString();
				audit.mNewValue = lmm.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (lmm.mSalesDate != lmmOld.mSalesDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmm);
				audit.mField = "sales_date";
				audit.mOldValue = lmmOld.mSalesDate.ToString();
				audit.mNewValue = lmm.mSalesDate.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Lmm lmm)
		{
			audit.mUserFullName = lmm.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Lmm);
			audit.mRowId = lmm.mId;
			audit.mAction = 2;
		}
	}
}