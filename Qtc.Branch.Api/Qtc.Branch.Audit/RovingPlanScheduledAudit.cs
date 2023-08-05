using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingPlanScheduledAudit
	{
		public static AuditCollection Audit(RovingPlanScheduled rovingplanscheduled,RovingPlanScheduled rovingplanscheduledOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingplanscheduled.mRecordId != rovingplanscheduledOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "record_id";
				audit.mOldValue = rovingplanscheduledOld.mRecordId.ToString();
				audit.mNewValue = rovingplanscheduled.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mDateCreated != rovingplanscheduledOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "date_created";
				audit.mOldValue = rovingplanscheduledOld.mDateCreated.ToString();
				audit.mNewValue = rovingplanscheduled.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mRovingRequestId != rovingplanscheduledOld.mRovingRequestId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "roving_request_id";
				audit.mOldValue = rovingplanscheduledOld.mRovingRequestId.ToString();
				audit.mNewValue = rovingplanscheduled.mRovingRequestId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mRovingPlanId != rovingplanscheduledOld.mRovingPlanId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "roving_plan_id";
				audit.mOldValue = rovingplanscheduledOld.mRovingPlanId.ToString();
				audit.mNewValue = rovingplanscheduled.mRovingPlanId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mRovingPlanDatesId != rovingplanscheduledOld.mRovingPlanDatesId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "roving_plan_dates_id";
				audit.mOldValue = rovingplanscheduledOld.mRovingPlanDatesId.ToString();
				audit.mNewValue = rovingplanscheduled.mRovingPlanDatesId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mRovingPlanOicId != rovingplanscheduledOld.mRovingPlanOicId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "roving_plan_oic_id";
				audit.mOldValue = rovingplanscheduledOld.mRovingPlanOicId.ToString();
				audit.mNewValue = rovingplanscheduled.mRovingPlanOicId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mRovingPlanScheduledId != rovingplanscheduledOld.mRovingPlanScheduledId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "roving_plan_schedule_id";
				audit.mOldValue = rovingplanscheduledOld.mRovingPlanScheduledId.ToString();
				audit.mNewValue = rovingplanscheduled.mRovingPlanScheduledId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mBranchAreaId != rovingplanscheduledOld.mBranchAreaId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "branch_area_id";
				audit.mOldValue = rovingplanscheduledOld.mBranchAreaId.ToString();
				audit.mNewValue = rovingplanscheduled.mBranchAreaId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mBranchId != rovingplanscheduledOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "branch_id";
				audit.mOldValue = rovingplanscheduledOld.mBranchId.ToString();
				audit.mNewValue = rovingplanscheduled.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mStartDate != rovingplanscheduledOld.mStartDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "start_date";
				audit.mOldValue = rovingplanscheduledOld.mStartDate.ToString();
				audit.mNewValue = rovingplanscheduled.mStartDate.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mEndDate != rovingplanscheduledOld.mEndDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "end_date";
				audit.mOldValue = rovingplanscheduledOld.mEndDate.ToString();
				audit.mNewValue = rovingplanscheduled.mEndDate.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mRemarks != rovingplanscheduledOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "remarks";
				audit.mOldValue = rovingplanscheduledOld.mRemarks;
				audit.mNewValue = rovingplanscheduled.mRemarks;
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mVisited != rovingplanscheduledOld.mVisited)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "visited";
				audit.mOldValue = rovingplanscheduledOld.mVisited.ToString();
				audit.mNewValue = rovingplanscheduled.mVisited.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mVisitedRemarks != rovingplanscheduledOld.mVisitedRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "visited_remarks";
				audit.mOldValue = rovingplanscheduledOld.mVisitedRemarks;
				audit.mNewValue = rovingplanscheduled.mVisitedRemarks;
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mPost != rovingplanscheduledOld.mPost)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "post";
				audit.mOldValue = rovingplanscheduledOld.mPost.ToString();
				audit.mNewValue = rovingplanscheduled.mPost.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduled.mAlreadyEgress != rovingplanscheduledOld.mAlreadyEgress)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduled);
				audit.mField = "already_egress";
				audit.mOldValue = rovingplanscheduledOld.mAlreadyEgress.ToString();
				audit.mNewValue = rovingplanscheduled.mAlreadyEgress.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingPlanScheduled rovingplanscheduled)
		{
			audit.mUserFullName = rovingplanscheduled.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingPlanScheduled);
			audit.mRowId = rovingplanscheduled.mId;
			audit.mAction = 2;
		}
	}
}