using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class IbwAudit
	{
		public static AuditCollection Audit(Ibw ibw,Ibw ibwOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (ibw.mIbwId != ibwOld.mIbwId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "ibw_id";
				audit.mOldValue = ibwOld.mIbwId.ToString();
				audit.mNewValue = ibw.mIbwId.ToString();
				audit_collection.Add(audit);
			}

			if (ibw.mDate != ibwOld.mDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "date";
				audit.mOldValue = ibwOld.mDate.ToString();
				audit.mNewValue = ibw.mDate.ToString();
				audit_collection.Add(audit);
			}

			if (ibw.mTransactionDate != ibwOld.mTransactionDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "transaction_date";
				audit.mOldValue = ibwOld.mTransactionDate.ToString();
				audit.mNewValue = ibw.mTransactionDate.ToString();
				audit_collection.Add(audit);
			}

			if (ibw.mNumber != ibwOld.mNumber)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "number";
				audit.mOldValue = ibwOld.mNumber.ToString();
				audit.mNewValue = ibw.mNumber.ToString();
				audit_collection.Add(audit);
			}

			if (ibw.mPlannerId != ibwOld.mPlannerId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "planner_id";
				audit.mOldValue = ibwOld.mPlannerId.ToString();
				audit.mNewValue = ibw.mPlannerId.ToString();
				audit_collection.Add(audit);
			}

			if (ibw.mMcId != ibwOld.mMcId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "mc_id";
				audit.mOldValue = ibwOld.mMcId.ToString();
				audit.mNewValue = ibw.mMcId.ToString();
				audit_collection.Add(audit);
			}

			if (ibw.mBranchId != ibwOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "branch_id";
				audit.mOldValue = ibwOld.mBranchId.ToString();
				audit.mNewValue = ibw.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (ibw.mToBranchId != ibwOld.mToBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "to_branch_id";
				audit.mOldValue = ibwOld.mToBranchId.ToString();
				audit.mNewValue = ibw.mToBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (ibw.mRequestedById != ibwOld.mRequestedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "requested_by_id";
				audit.mOldValue = ibwOld.mRequestedById.ToString();
				audit.mNewValue = ibw.mRequestedById.ToString();
				audit_collection.Add(audit);
			}

			if (ibw.mApprovedById != ibwOld.mApprovedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "approved_by_id";
				audit.mOldValue = ibwOld.mApprovedById.ToString();
				audit.mNewValue = ibw.mApprovedById.ToString();
				audit_collection.Add(audit);
			}

			if (ibw.mReasonId != ibwOld.mReasonId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "reason_id";
				audit.mOldValue = ibwOld.mReasonId.ToString();
				audit.mNewValue = ibw.mReasonId.ToString();
				audit_collection.Add(audit);
			}

			if (ibw.mNovNo != ibwOld.mNovNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "nov_no";
				audit.mOldValue = ibwOld.mNovNo;
				audit.mNewValue = ibw.mNovNo;
				audit_collection.Add(audit);
			}

			if (ibw.mRemarks != ibwOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "remarks";
				audit.mOldValue = ibwOld.mRemarks;
				audit.mNewValue = ibw.mRemarks;
				audit_collection.Add(audit);
			}

			if (ibw.mCodeId != ibwOld.mCodeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, ibw);
				audit.mField = "code_id";
				audit.mOldValue = ibwOld.mCodeId.ToString();
				audit.mNewValue = ibw.mCodeId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Ibw ibw)
		{
			audit.mUserFullName = ibw.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Ibw);
			audit.mRowId = ibw.mId;
			audit.mAction = 2;
		}
	}
}