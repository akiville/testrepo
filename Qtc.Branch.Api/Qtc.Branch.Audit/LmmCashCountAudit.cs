using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class LmmCashCountAudit
	{
		public static AuditCollection Audit(LmmCashCount lmmcashcount,LmmCashCount lmmcashcountOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (lmmcashcount.mBranchId != lmmcashcountOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmcashcount);
				audit.mField = "branch_id";
				audit.mOldValue = lmmcashcountOld.mBranchId.ToString();
				audit.mNewValue = lmmcashcount.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmcashcount.mSalesDate != lmmcashcountOld.mSalesDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmcashcount);
				audit.mField = "sales_date";
				audit.mOldValue = lmmcashcountOld.mSalesDate.ToString();
				audit.mNewValue = lmmcashcount.mSalesDate.ToString();
				audit_collection.Add(audit);
			}

			if (lmmcashcount.mCash != lmmcashcountOld.mCash)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmcashcount);
				audit.mField = "cash";
				audit.mOldValue = lmmcashcountOld.mCash.ToString();
				audit.mNewValue = lmmcashcount.mCash.ToString();
				audit_collection.Add(audit);
			}

			if (lmmcashcount.mGrabFood != lmmcashcountOld.mGrabFood)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmcashcount);
				audit.mField = "grab_food";
				audit.mOldValue = lmmcashcountOld.mGrabFood.ToString();
				audit.mNewValue = lmmcashcount.mGrabFood.ToString();
				audit_collection.Add(audit);
			}

			if (lmmcashcount.mFoodpanda != lmmcashcountOld.mFoodpanda)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmcashcount);
				audit.mField = "foodpanda";
				audit.mOldValue = lmmcashcountOld.mFoodpanda.ToString();
				audit.mNewValue = lmmcashcount.mFoodpanda.ToString();
				audit_collection.Add(audit);
			}

			if (lmmcashcount.mTotalCash != lmmcashcountOld.mTotalCash)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmcashcount);
				audit.mField = "total_cash";
				audit.mOldValue = lmmcashcountOld.mTotalCash.ToString();
				audit.mNewValue = lmmcashcount.mTotalCash.ToString();
				audit_collection.Add(audit);
			}

			if (lmmcashcount.mUserId != lmmcashcountOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmcashcount);
				audit.mField = "user_id";
				audit.mOldValue = lmmcashcountOld.mUserId.ToString();
				audit.mNewValue = lmmcashcount.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmcashcount.mRemarks != lmmcashcountOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmcashcount);
				audit.mField = "remarks";
				audit.mOldValue = lmmcashcountOld.mRemarks;
				audit.mNewValue = lmmcashcount.mRemarks;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, LmmCashCount lmmcashcount)
		{
			audit.mUserFullName = lmmcashcount.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_LmmCashCount);
			audit.mRowId = lmmcashcount.mId;
			audit.mAction = 2;
		}
	}
}