using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class BranchCashAudit
	{
		public static AuditCollection Audit(BranchCash branchcash,BranchCash branchcashOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (branchcash.mBranchId != branchcashOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcash);
				audit.mField = "branch_id";
				audit.mOldValue = branchcashOld.mBranchId.ToString();
				audit.mNewValue = branchcash.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (branchcash.mSalesDate != branchcashOld.mSalesDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcash);
				audit.mField = "sales_date";
				audit.mOldValue = branchcashOld.mSalesDate.ToString();
				audit.mNewValue = branchcash.mSalesDate.ToString();
				audit_collection.Add(audit);
			}

			if (branchcash.mTotalAmount != branchcashOld.mTotalAmount)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcash);
				audit.mField = "total_amount";
				audit.mOldValue = branchcashOld.mTotalAmount.ToString();
				audit.mNewValue = branchcash.mTotalAmount.ToString();
				audit_collection.Add(audit);
			}

			if (branchcash.mIsDeposited != branchcashOld.mIsDeposited)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcash);
				audit.mField = "is_deposited";
				audit.mOldValue = branchcashOld.mIsDeposited.ToString();
				audit.mNewValue = branchcash.mIsDeposited.ToString();
				audit_collection.Add(audit);
			}

			if (branchcash.mCashExplanation != branchcashOld.mCashExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcash);
				audit.mField = "cash_explanation";
				audit.mOldValue = branchcashOld.mCashExplanation;
				audit.mNewValue = branchcash.mCashExplanation;
				audit_collection.Add(audit);
			}

			if (branchcash.mEmployeeId != branchcashOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcash);
				audit.mField = "employee_id";
				audit.mOldValue = branchcashOld.mEmployeeId.ToString();
				audit.mNewValue = branchcash.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (branchcash.mDepositedById != branchcashOld.mDepositedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcash);
				audit.mField = "deposited_by_id";
				audit.mOldValue = branchcashOld.mDepositedById.ToString();
				audit.mNewValue = branchcash.mDepositedById.ToString();
				audit_collection.Add(audit);
			}

			if (branchcash.mDepositDate != branchcashOld.mDepositDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcash);
				audit.mField = "deposit_date";
				audit.mOldValue = branchcashOld.mDepositDate.ToString();
				audit.mNewValue = branchcash.mDepositDate.ToString();
				audit_collection.Add(audit);
			}

			if (branchcash.mDepositValidated != branchcashOld.mDepositValidated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcash);
				audit.mField = "deposit_validated";
				audit.mOldValue = branchcashOld.mDepositValidated.ToString();
				audit.mNewValue = branchcash.mDepositValidated.ToString();
				audit_collection.Add(audit);
			}

			if (branchcash.mDatestamp != branchcashOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcash);
				audit.mField = "datestamp";
				audit.mOldValue = branchcashOld.mDatestamp.ToString();
				audit.mNewValue = branchcash.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, BranchCash branchcash)
		{
			audit.mUserFullName = branchcash.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_BranchCash);
			audit.mRowId = branchcash.mId;
			audit.mAction = 2;
		}
	}
}