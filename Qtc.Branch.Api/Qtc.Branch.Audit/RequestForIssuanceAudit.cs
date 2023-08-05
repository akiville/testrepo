using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RequestForIssuanceAudit
	{
		public static AuditCollection Audit(RequestForIssuance requestforissuance,RequestForIssuance requestforissuanceOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (requestforissuance.mIssuanceId != requestforissuanceOld.mIssuanceId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "issuance_id";
				audit.mOldValue = requestforissuanceOld.mIssuanceId.ToString();
				audit.mNewValue = requestforissuance.mIssuanceId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mDateRequested != requestforissuanceOld.mDateRequested)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "date_requested";
				audit.mOldValue = requestforissuanceOld.mDateRequested.ToString();
				audit.mNewValue = requestforissuance.mDateRequested.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mDateNeeded != requestforissuanceOld.mDateNeeded)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "date_needed";
				audit.mOldValue = requestforissuanceOld.mDateNeeded.ToString();
				audit.mNewValue = requestforissuance.mDateNeeded.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mBranchId != requestforissuanceOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "branch_id";
				audit.mOldValue = requestforissuanceOld.mBranchId.ToString();
				audit.mNewValue = requestforissuance.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mNumber != requestforissuanceOld.mNumber)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "number";
				audit.mOldValue = requestforissuanceOld.mNumber.ToString();
				audit.mNewValue = requestforissuance.mNumber.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mRequestedById != requestforissuanceOld.mRequestedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "requested_by_id";
				audit.mOldValue = requestforissuanceOld.mRequestedById.ToString();
				audit.mNewValue = requestforissuance.mRequestedById.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mProcessedById != requestforissuanceOld.mProcessedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "processed_by_id";
				audit.mOldValue = requestforissuanceOld.mProcessedById.ToString();
				audit.mNewValue = requestforissuance.mProcessedById.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mDepartmentId != requestforissuanceOld.mDepartmentId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "department_id";
				audit.mOldValue = requestforissuanceOld.mDepartmentId.ToString();
				audit.mNewValue = requestforissuance.mDepartmentId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mPurposeId != requestforissuanceOld.mPurposeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "purpose_id";
				audit.mOldValue = requestforissuanceOld.mPurposeId.ToString();
				audit.mNewValue = requestforissuance.mPurposeId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mExplanation != requestforissuanceOld.mExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "explanation";
				audit.mOldValue = requestforissuanceOld.mExplanation;
				audit.mNewValue = requestforissuance.mExplanation;
				audit_collection.Add(audit);
			}

			if (requestforissuance.mProductGroupId != requestforissuanceOld.mProductGroupId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "product_group_id";
				audit.mOldValue = requestforissuanceOld.mProductGroupId.ToString();
				audit.mNewValue = requestforissuance.mProductGroupId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mPlanned != requestforissuanceOld.mPlanned)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "planned";
				audit.mOldValue = requestforissuanceOld.mPlanned.ToString();
				audit.mNewValue = requestforissuance.mPlanned.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mCancelled != requestforissuanceOld.mCancelled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "cancelled";
				audit.mOldValue = requestforissuanceOld.mCancelled.ToString();
				audit.mNewValue = requestforissuance.mCancelled.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mCancelledReason != requestforissuanceOld.mCancelledReason)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "cancelled_reason";
				audit.mOldValue = requestforissuanceOld.mCancelledReason;
				audit.mNewValue = requestforissuance.mCancelledReason;
				audit_collection.Add(audit);
			}

			if (requestforissuance.mIssuedToId != requestforissuanceOld.mIssuedToId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "issued_to_id";
				audit.mOldValue = requestforissuanceOld.mIssuedToId.ToString();
				audit.mNewValue = requestforissuance.mIssuedToId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mRequestForRepairId != requestforissuanceOld.mRequestForRepairId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "request_for_repair_id";
				audit.mOldValue = requestforissuanceOld.mRequestForRepairId.ToString();
				audit.mNewValue = requestforissuance.mRequestForRepairId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuance.mDatestamp != requestforissuanceOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuance);
				audit.mField = "datestamp";
				audit.mOldValue = requestforissuanceOld.mDatestamp.ToString();
				audit.mNewValue = requestforissuance.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RequestForIssuance requestforissuance)
		{
			audit.mUserFullName = requestforissuance.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RequestForIssuance);
			audit.mRowId = requestforissuance.mId;
			audit.mAction = 2;
		}
	}
}