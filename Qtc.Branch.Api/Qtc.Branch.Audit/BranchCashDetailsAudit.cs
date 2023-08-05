using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class BranchCashDetailsAudit
	{
		public static AuditCollection Audit(BranchCashDetails branchcashdetails,BranchCashDetails branchcashdetailsOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (branchcashdetails.mSalesDate != branchcashdetailsOld.mSalesDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcashdetails);
				audit.mField = "sales_date";
				audit.mOldValue = branchcashdetailsOld.mSalesDate.ToString();
				audit.mNewValue = branchcashdetails.mSalesDate.ToString();
				audit_collection.Add(audit);
			}

			if (branchcashdetails.mDenominationId != branchcashdetailsOld.mDenominationId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcashdetails);
				audit.mField = "denomination_id";
				audit.mOldValue = branchcashdetailsOld.mDenominationId.ToString();
				audit.mNewValue = branchcashdetails.mDenominationId.ToString();
				audit_collection.Add(audit);
			}

			if (branchcashdetails.mQty != branchcashdetailsOld.mQty)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcashdetails);
				audit.mField = "qty";
				audit.mOldValue = branchcashdetailsOld.mQty.ToString();
				audit.mNewValue = branchcashdetails.mQty.ToString();
				audit_collection.Add(audit);
			}

			if (branchcashdetails.mTotalAmount != branchcashdetailsOld.mTotalAmount)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcashdetails);
				audit.mField = "total_amount";
				audit.mOldValue = branchcashdetailsOld.mTotalAmount.ToString();
				audit.mNewValue = branchcashdetails.mTotalAmount.ToString();
				audit_collection.Add(audit);
			}

			if (branchcashdetails.mCashExplanation != branchcashdetailsOld.mCashExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcashdetails);
				audit.mField = "cash_explanation";
				audit.mOldValue = branchcashdetailsOld.mCashExplanation;
				audit.mNewValue = branchcashdetails.mCashExplanation;
				audit_collection.Add(audit);
			}

			if (branchcashdetails.mBranchCashId != branchcashdetailsOld.mBranchCashId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcashdetails);
				audit.mField = "branch_cash_id";
				audit.mOldValue = branchcashdetailsOld.mBranchCashId.ToString();
				audit.mNewValue = branchcashdetails.mBranchCashId.ToString();
				audit_collection.Add(audit);
			}

			if (branchcashdetails.mUserId != branchcashdetailsOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcashdetails);
				audit.mField = "user_id";
				audit.mOldValue = branchcashdetailsOld.mUserId.ToString();
				audit.mNewValue = branchcashdetails.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (branchcashdetails.mUploadDate != branchcashdetailsOld.mUploadDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchcashdetails);
				audit.mField = "upload_date";
				audit.mOldValue = branchcashdetailsOld.mUploadDate.ToString();
				audit.mNewValue = branchcashdetails.mUploadDate.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, BranchCashDetails branchcashdetails)
		{
			audit.mUserFullName = branchcashdetails.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_BranchCashDetails);
			audit.mRowId = branchcashdetails.mId;
			audit.mAction = 2;
		}
	}
}