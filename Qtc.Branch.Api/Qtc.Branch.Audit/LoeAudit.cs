using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class LoeAudit
	{
		public static AuditCollection Audit(Loe loe,Loe loeOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (loe.mLoeId != loeOld.mLoeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "loe_id";
				audit.mOldValue = loeOld.mLoeId.ToString();
				audit.mNewValue = loe.mLoeId.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mSaleDate != loeOld.mSaleDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "sale_date";
				audit.mOldValue = loeOld.mSaleDate.ToString();
				audit.mNewValue = loe.mSaleDate.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mNumber != loeOld.mNumber)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "number";
				audit.mOldValue = loeOld.mNumber.ToString();
				audit.mNewValue = loe.mNumber.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mAuditNumber != loeOld.mAuditNumber)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "audit_number";
				audit.mOldValue = loeOld.mAuditNumber.ToString();
				audit.mNewValue = loe.mAuditNumber.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mRflNo != loeOld.mRflNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "rfl_no";
				audit.mOldValue = loeOld.mRflNo.ToString();
				audit.mNewValue = loe.mRflNo.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mBranchId != loeOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "branch_id";
				audit.mOldValue = loeOld.mBranchId.ToString();
				audit.mNewValue = loe.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mType != loeOld.mType)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "type";
				audit.mOldValue = loeOld.mType.ToString();
				audit.mNewValue = loe.mType.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mMcId != loeOld.mMcId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "mc_id";
				audit.mOldValue = loeOld.mMcId.ToString();
				audit.mNewValue = loe.mMcId.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mRequestedById != loeOld.mRequestedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "requested_by_id";
				audit.mOldValue = loeOld.mRequestedById.ToString();
				audit.mNewValue = loe.mRequestedById.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mTransactedById != loeOld.mTransactedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "transacted_by_id";
				audit.mOldValue = loeOld.mTransactedById.ToString();
				audit.mNewValue = loe.mTransactedById.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mApprovedById != loeOld.mApprovedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "approved_by_id";
				audit.mOldValue = loeOld.mApprovedById.ToString();
				audit.mNewValue = loe.mApprovedById.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mCodeId != loeOld.mCodeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "code_id";
				audit.mOldValue = loeOld.mCodeId.ToString();
				audit.mNewValue = loe.mCodeId.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mAuditedById != loeOld.mAuditedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "audited_by_id";
				audit.mOldValue = loeOld.mAuditedById.ToString();
				audit.mNewValue = loe.mAuditedById.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mRemarks != loeOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "remarks";
				audit.mOldValue = loeOld.mRemarks;
				audit.mNewValue = loe.mRemarks;
				audit_collection.Add(audit);
			}

			if (loe.mComment != loeOld.mComment)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "comment";
				audit.mOldValue = loeOld.mComment;
				audit.mNewValue = loe.mComment;
				audit_collection.Add(audit);
			}

			if (loe.mDisApproved != loeOld.mDisApproved)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "dis_approved";
				audit.mOldValue = loeOld.mDisApproved.ToString();
				audit.mNewValue = loe.mDisApproved.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mDisApprovedReason != loeOld.mDisApprovedReason)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "dis_approved_reason";
				audit.mOldValue = loeOld.mDisApprovedReason;
				audit.mNewValue = loe.mDisApprovedReason;
				audit_collection.Add(audit);
			}

			if (loe.mWitnessNo != loeOld.mWitnessNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "witness_no";
				audit.mOldValue = loeOld.mWitnessNo.ToString();
				audit.mNewValue = loe.mWitnessNo.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mWitnessName != loeOld.mWitnessName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "witness_name";
				audit.mOldValue = loeOld.mWitnessName;
				audit.mNewValue = loe.mWitnessName;
				audit_collection.Add(audit);
			}

			if (loe.mAmount != loeOld.mAmount)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "amount";
				audit.mOldValue = loeOld.mAmount.ToString();
				audit.mNewValue = loe.mAmount.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mLoeNoMcBag != loeOld.mLoeNoMcBag)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "loe_no_mc_bag";
				audit.mOldValue = loeOld.mLoeNoMcBag;
				audit.mNewValue = loe.mLoeNoMcBag;
				audit_collection.Add(audit);
			}

			if (loe.mReasonId != loeOld.mReasonId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "reason_id";
				audit.mOldValue = loeOld.mReasonId.ToString();
				audit.mNewValue = loe.mReasonId.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mReconcillingFormNo != loeOld.mReconcillingFormNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "reconcilling_form_no";
				audit.mOldValue = loeOld.mReconcillingFormNo;
				audit.mNewValue = loe.mReconcillingFormNo;
				audit_collection.Add(audit);
			}

			if (loe.mAloe != loeOld.mAloe)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "aloe";
				audit.mOldValue = loeOld.mAloe.ToString();
				audit.mNewValue = loe.mAloe.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mXaloe != loeOld.mXaloe)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "xaloe";
				audit.mOldValue = loeOld.mXaloe.ToString();
				audit.mNewValue = loe.mXaloe.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mReject != loeOld.mReject)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "reject";
				audit.mOldValue = loeOld.mReject.ToString();
				audit.mNewValue = loe.mReject.ToString();
				audit_collection.Add(audit);
			}

			if (loe.mGuestCount != loeOld.mGuestCount)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loe);
				audit.mField = "guest_count";
				audit.mOldValue = loeOld.mGuestCount.ToString();
				audit.mNewValue = loe.mGuestCount.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Loe loe)
		{
			audit.mUserFullName = loe.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Loe);
			audit.mRowId = loe.mId;
			audit.mAction = 2;
		}
	}
}