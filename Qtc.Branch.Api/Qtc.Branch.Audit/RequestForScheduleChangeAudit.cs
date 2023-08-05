using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RequestForScheduleChangeAudit
	{
		public static AuditCollection Audit(RequestForScheduleChange requestforschedulechange,RequestForScheduleChange requestforschedulechangeOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (requestforschedulechange.mRequestForScheduleChangeId != requestforschedulechangeOld.mRequestForScheduleChangeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "request_for_schedule_change_id";
				audit.mOldValue = requestforschedulechangeOld.mRequestForScheduleChangeId.ToString();
				audit.mNewValue = requestforschedulechange.mRequestForScheduleChangeId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mIsApproved != requestforschedulechangeOld.mIsApproved)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "is_approved";
				audit.mOldValue = requestforschedulechangeOld.mIsApproved.ToString();
				audit.mNewValue = requestforschedulechange.mIsApproved.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mIsCancelled != requestforschedulechangeOld.mIsCancelled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "is_cancelled";
				audit.mOldValue = requestforschedulechangeOld.mIsCancelled.ToString();
				audit.mNewValue = requestforschedulechange.mIsCancelled.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mIsPostponed != requestforschedulechangeOld.mIsPostponed)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "is_postponed";
				audit.mOldValue = requestforschedulechangeOld.mIsPostponed.ToString();
				audit.mNewValue = requestforschedulechange.mIsPostponed.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mRequestedById != requestforschedulechangeOld.mRequestedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "requested_by_id";
				audit.mOldValue = requestforschedulechangeOld.mRequestedById.ToString();
				audit.mNewValue = requestforschedulechange.mRequestedById.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mEmployeeId != requestforschedulechangeOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "employee_id";
				audit.mOldValue = requestforschedulechangeOld.mEmployeeId.ToString();
				audit.mNewValue = requestforschedulechange.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mAgency != requestforschedulechangeOld.mAgency)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "agency";
				audit.mOldValue = requestforschedulechangeOld.mAgency;
				audit.mNewValue = requestforschedulechange.mAgency;
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mBranch != requestforschedulechangeOld.mBranch)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "branch";
				audit.mOldValue = requestforschedulechangeOld.mBranch;
				audit.mNewValue = requestforschedulechange.mBranch;
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mPosition != requestforschedulechangeOld.mPosition)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "position";
				audit.mOldValue = requestforschedulechangeOld.mPosition;
				audit.mNewValue = requestforschedulechange.mPosition;
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mArea != requestforschedulechangeOld.mArea)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "area";
				audit.mOldValue = requestforschedulechangeOld.mArea;
				audit.mNewValue = requestforschedulechange.mArea;
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mLmmId != requestforschedulechangeOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "lmm_id";
				audit.mOldValue = requestforschedulechangeOld.mLmmId.ToString();
				audit.mNewValue = requestforschedulechange.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mLmm != requestforschedulechangeOld.mLmm)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "lmm";
				audit.mOldValue = requestforschedulechangeOld.mLmm;
				audit.mNewValue = requestforschedulechange.mLmm;
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mStartDate != requestforschedulechangeOld.mStartDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "start_date";
				audit.mOldValue = requestforschedulechangeOld.mStartDate.ToString();
				audit.mNewValue = requestforschedulechange.mStartDate.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mEndDate != requestforschedulechangeOld.mEndDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "end_date";
				audit.mOldValue = requestforschedulechangeOld.mEndDate.ToString();
				audit.mNewValue = requestforschedulechange.mEndDate.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mReasonId != requestforschedulechangeOld.mReasonId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "reason_id";
				audit.mOldValue = requestforschedulechangeOld.mReasonId.ToString();
				audit.mNewValue = requestforschedulechange.mReasonId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mRemarks != requestforschedulechangeOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "remarks";
				audit.mOldValue = requestforschedulechangeOld.mRemarks;
				audit.mNewValue = requestforschedulechange.mRemarks;
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mPersonToRelieveId != requestforschedulechangeOld.mPersonToRelieveId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "person_to_relieve_id";
				audit.mOldValue = requestforschedulechangeOld.mPersonToRelieveId.ToString();
				audit.mNewValue = requestforschedulechange.mPersonToRelieveId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mRelieverBranch != requestforschedulechangeOld.mRelieverBranch)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "reliever_branch";
				audit.mOldValue = requestforschedulechangeOld.mRelieverBranch;
				audit.mNewValue = requestforschedulechange.mRelieverBranch;
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mReplacementId != requestforschedulechangeOld.mReplacementId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "replacement_id";
				audit.mOldValue = requestforschedulechangeOld.mReplacementId.ToString();
				audit.mNewValue = requestforschedulechange.mReplacementId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mPostponedTo != requestforschedulechangeOld.mPostponedTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "postponed_to";
				audit.mOldValue = requestforschedulechangeOld.mPostponedTo.ToString();
				audit.mNewValue = requestforschedulechange.mPostponedTo.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mReconNumber != requestforschedulechangeOld.mReconNumber)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "recon_number";
				audit.mOldValue = requestforschedulechangeOld.mReconNumber;
				audit.mNewValue = requestforschedulechange.mReconNumber;
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mReason != requestforschedulechangeOld.mReason)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "reason";
				audit.mOldValue = requestforschedulechangeOld.mReason;
				audit.mNewValue = requestforschedulechange.mReason;
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mExplanation != requestforschedulechangeOld.mExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "explanation";
				audit.mOldValue = requestforschedulechangeOld.mExplanation;
				audit.mNewValue = requestforschedulechange.mExplanation;
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mChangeWith != requestforschedulechangeOld.mChangeWith)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "change_with";
				audit.mOldValue = requestforschedulechangeOld.mChangeWith.ToString();
				audit.mNewValue = requestforschedulechange.mChangeWith.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mBranchFrom != requestforschedulechangeOld.mBranchFrom)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "branch_from";
				audit.mOldValue = requestforschedulechangeOld.mBranchFrom.ToString();
				audit.mNewValue = requestforschedulechange.mBranchFrom.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mBranchTo != requestforschedulechangeOld.mBranchTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "branch_to";
				audit.mOldValue = requestforschedulechangeOld.mBranchTo.ToString();
				audit.mNewValue = requestforschedulechange.mBranchTo.ToString();
				audit_collection.Add(audit);
			}

			if (requestforschedulechange.mIsPlanned != requestforschedulechangeOld.mIsPlanned)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforschedulechange);
				audit.mField = "is_planned";
				audit.mOldValue = requestforschedulechangeOld.mIsPlanned.ToString();
				audit.mNewValue = requestforschedulechange.mIsPlanned.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RequestForScheduleChange requestforschedulechange)
		{
			audit.mUserFullName = requestforschedulechange.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RequestForScheduleChange);
			audit.mRowId = requestforschedulechange.mId;
			audit.mAction = 2;
		}
	}
}