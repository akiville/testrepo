using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class SalesSchedulingUpdatesAudit
	{
		public static AuditCollection Audit(SalesSchedulingUpdates salesschedulingupdates,SalesSchedulingUpdates salesschedulingupdatesOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (salesschedulingupdates.mSalesSchedulingId != salesschedulingupdatesOld.mSalesSchedulingId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "sales_scheduling_id";
				audit.mOldValue = salesschedulingupdatesOld.mSalesSchedulingId.ToString();
				audit.mNewValue = salesschedulingupdates.mSalesSchedulingId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mCutoffId != salesschedulingupdatesOld.mCutoffId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "cutoff_id";
				audit.mOldValue = salesschedulingupdatesOld.mCutoffId.ToString();
				audit.mNewValue = salesschedulingupdates.mCutoffId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mBranch != salesschedulingupdatesOld.mBranch)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "branch";
				audit.mOldValue = salesschedulingupdatesOld.mBranch.ToString();
				audit.mNewValue = salesschedulingupdates.mBranch.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mLmm != salesschedulingupdatesOld.mLmm)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "lmm";
				audit.mOldValue = salesschedulingupdatesOld.mLmm;
				audit.mNewValue = salesschedulingupdates.mLmm;
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mType != salesschedulingupdatesOld.mType)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "type";
				audit.mOldValue = salesschedulingupdatesOld.mType;
				audit.mNewValue = salesschedulingupdates.mType;
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mBranchArea != salesschedulingupdatesOld.mBranchArea)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "branch_area";
				audit.mOldValue = salesschedulingupdatesOld.mBranchArea;
				audit.mNewValue = salesschedulingupdates.mBranchArea;
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mAgency != salesschedulingupdatesOld.mAgency)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "agency";
				audit.mOldValue = salesschedulingupdatesOld.mAgency;
				audit.mNewValue = salesschedulingupdates.mAgency;
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mRate != salesschedulingupdatesOld.mRate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "rate";
				audit.mOldValue = salesschedulingupdatesOld.mRate.ToString();
				audit.mNewValue = salesschedulingupdates.mRate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mAmount != salesschedulingupdatesOld.mAmount)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "amount";
				audit.mOldValue = salesschedulingupdatesOld.mAmount.ToString();
				audit.mNewValue = salesschedulingupdates.mAmount.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mCurrentBranchId != salesschedulingupdatesOld.mCurrentBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "current_branch_id";
				audit.mOldValue = salesschedulingupdatesOld.mCurrentBranchId;
				audit.mNewValue = salesschedulingupdates.mCurrentBranchId;
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mIdExpired != salesschedulingupdatesOld.mIdExpired)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "id_expired";
				audit.mOldValue = salesschedulingupdatesOld.mIdExpired;
				audit.mNewValue = salesschedulingupdates.mIdExpired;
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mMondayDate != salesschedulingupdatesOld.mMondayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "monday_date";
				audit.mOldValue = salesschedulingupdatesOld.mMondayDate.ToString();
				audit.mNewValue = salesschedulingupdates.mMondayDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mMonday != salesschedulingupdatesOld.mMonday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "monday";
				audit.mOldValue = salesschedulingupdatesOld.mMonday.ToString();
				audit.mNewValue = salesschedulingupdates.mMonday.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mMondaySchedule != salesschedulingupdatesOld.mMondaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "monday_schedule";
				audit.mOldValue = salesschedulingupdatesOld.mMondaySchedule.ToString();
				audit.mNewValue = salesschedulingupdates.mMondaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mMondayHasId != salesschedulingupdatesOld.mMondayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "monday_has_id";
				audit.mOldValue = salesschedulingupdatesOld.mMondayHasId.ToString();
				audit.mNewValue = salesschedulingupdates.mMondayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mMondayExpiredId != salesschedulingupdatesOld.mMondayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "monday_expired_id";
				audit.mOldValue = salesschedulingupdatesOld.mMondayExpiredId.ToString();
				audit.mNewValue = salesschedulingupdates.mMondayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mTuesdayDate != salesschedulingupdatesOld.mTuesdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "tuesday_date";
				audit.mOldValue = salesschedulingupdatesOld.mTuesdayDate.ToString();
				audit.mNewValue = salesschedulingupdates.mTuesdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mTuesday != salesschedulingupdatesOld.mTuesday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "tuesday";
				audit.mOldValue = salesschedulingupdatesOld.mTuesday.ToString();
				audit.mNewValue = salesschedulingupdates.mTuesday.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mTuesdaySchedule != salesschedulingupdatesOld.mTuesdaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "tuesday_schedule";
				audit.mOldValue = salesschedulingupdatesOld.mTuesdaySchedule.ToString();
				audit.mNewValue = salesschedulingupdates.mTuesdaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mTuesdayHasId != salesschedulingupdatesOld.mTuesdayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "tuesday_has_id";
				audit.mOldValue = salesschedulingupdatesOld.mTuesdayHasId.ToString();
				audit.mNewValue = salesschedulingupdates.mTuesdayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mTuesdayExpiredId != salesschedulingupdatesOld.mTuesdayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "tuesday_expired_id";
				audit.mOldValue = salesschedulingupdatesOld.mTuesdayExpiredId.ToString();
				audit.mNewValue = salesschedulingupdates.mTuesdayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mWednesdayDate != salesschedulingupdatesOld.mWednesdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "wednesday_date";
				audit.mOldValue = salesschedulingupdatesOld.mWednesdayDate.ToString();
				audit.mNewValue = salesschedulingupdates.mWednesdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mWednesday != salesschedulingupdatesOld.mWednesday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "wednesday";
				audit.mOldValue = salesschedulingupdatesOld.mWednesday.ToString();
				audit.mNewValue = salesschedulingupdates.mWednesday.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mWednesdaySchedule != salesschedulingupdatesOld.mWednesdaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "wednesday_schedule";
				audit.mOldValue = salesschedulingupdatesOld.mWednesdaySchedule.ToString();
				audit.mNewValue = salesschedulingupdates.mWednesdaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mWednesdayHasId != salesschedulingupdatesOld.mWednesdayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "wednesday_has_id";
				audit.mOldValue = salesschedulingupdatesOld.mWednesdayHasId.ToString();
				audit.mNewValue = salesschedulingupdates.mWednesdayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mWednesdayExpiredId != salesschedulingupdatesOld.mWednesdayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "wednesday_expired_id";
				audit.mOldValue = salesschedulingupdatesOld.mWednesdayExpiredId.ToString();
				audit.mNewValue = salesschedulingupdates.mWednesdayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mThursdayDate != salesschedulingupdatesOld.mThursdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "thursday_date";
				audit.mOldValue = salesschedulingupdatesOld.mThursdayDate.ToString();
				audit.mNewValue = salesschedulingupdates.mThursdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mThursday != salesschedulingupdatesOld.mThursday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "thursday";
				audit.mOldValue = salesschedulingupdatesOld.mThursday.ToString();
				audit.mNewValue = salesschedulingupdates.mThursday.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mThursdaySchedule != salesschedulingupdatesOld.mThursdaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "thursday_schedule";
				audit.mOldValue = salesschedulingupdatesOld.mThursdaySchedule.ToString();
				audit.mNewValue = salesschedulingupdates.mThursdaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mThursdayHasId != salesschedulingupdatesOld.mThursdayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "thursday_has_id";
				audit.mOldValue = salesschedulingupdatesOld.mThursdayHasId.ToString();
				audit.mNewValue = salesschedulingupdates.mThursdayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mThursdayExpiredId != salesschedulingupdatesOld.mThursdayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "thursday_expired_id";
				audit.mOldValue = salesschedulingupdatesOld.mThursdayExpiredId.ToString();
				audit.mNewValue = salesschedulingupdates.mThursdayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mFridayDate != salesschedulingupdatesOld.mFridayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "friday_date";
				audit.mOldValue = salesschedulingupdatesOld.mFridayDate.ToString();
				audit.mNewValue = salesschedulingupdates.mFridayDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mFriday != salesschedulingupdatesOld.mFriday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "friday";
				audit.mOldValue = salesschedulingupdatesOld.mFriday.ToString();
				audit.mNewValue = salesschedulingupdates.mFriday.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mFridaySchedule != salesschedulingupdatesOld.mFridaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "friday_schedule";
				audit.mOldValue = salesschedulingupdatesOld.mFridaySchedule.ToString();
				audit.mNewValue = salesschedulingupdates.mFridaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mFridayHasId != salesschedulingupdatesOld.mFridayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "friday_has_id";
				audit.mOldValue = salesschedulingupdatesOld.mFridayHasId.ToString();
				audit.mNewValue = salesschedulingupdates.mFridayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mFridayExpiredId != salesschedulingupdatesOld.mFridayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "friday_expired_id";
				audit.mOldValue = salesschedulingupdatesOld.mFridayExpiredId.ToString();
				audit.mNewValue = salesschedulingupdates.mFridayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mSaturdayDate != salesschedulingupdatesOld.mSaturdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "saturday_date";
				audit.mOldValue = salesschedulingupdatesOld.mSaturdayDate.ToString();
				audit.mNewValue = salesschedulingupdates.mSaturdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mSaturday != salesschedulingupdatesOld.mSaturday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "saturday";
				audit.mOldValue = salesschedulingupdatesOld.mSaturday.ToString();
				audit.mNewValue = salesschedulingupdates.mSaturday.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mSaturdaySchedule != salesschedulingupdatesOld.mSaturdaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "saturday_schedule";
				audit.mOldValue = salesschedulingupdatesOld.mSaturdaySchedule.ToString();
				audit.mNewValue = salesschedulingupdates.mSaturdaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mSaturdayHasId != salesschedulingupdatesOld.mSaturdayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "saturday_has_id";
				audit.mOldValue = salesschedulingupdatesOld.mSaturdayHasId.ToString();
				audit.mNewValue = salesschedulingupdates.mSaturdayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mSaturdayExpiredId != salesschedulingupdatesOld.mSaturdayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "saturday_expired_id";
				audit.mOldValue = salesschedulingupdatesOld.mSaturdayExpiredId.ToString();
				audit.mNewValue = salesschedulingupdates.mSaturdayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mSundayDate != salesschedulingupdatesOld.mSundayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "sunday_date";
				audit.mOldValue = salesschedulingupdatesOld.mSundayDate.ToString();
				audit.mNewValue = salesschedulingupdates.mSundayDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mSunday != salesschedulingupdatesOld.mSunday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "sunday";
				audit.mOldValue = salesschedulingupdatesOld.mSunday.ToString();
				audit.mNewValue = salesschedulingupdates.mSunday.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mSundaySchedule != salesschedulingupdatesOld.mSundaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "sunday_schedule";
				audit.mOldValue = salesschedulingupdatesOld.mSundaySchedule.ToString();
				audit.mNewValue = salesschedulingupdates.mSundaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mSundayHasId != salesschedulingupdatesOld.mSundayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "sunday_has_id";
				audit.mOldValue = salesschedulingupdatesOld.mSundayHasId.ToString();
				audit.mNewValue = salesschedulingupdates.mSundayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mSundayExpiredId != salesschedulingupdatesOld.mSundayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "sunday_expired_id";
				audit.mOldValue = salesschedulingupdatesOld.mSundayExpiredId.ToString();
				audit.mNewValue = salesschedulingupdates.mSundayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mEmployeeId != salesschedulingupdatesOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "employee_id";
				audit.mOldValue = salesschedulingupdatesOld.mEmployeeId.ToString();
				audit.mNewValue = salesschedulingupdates.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mPositionScheduleId != salesschedulingupdatesOld.mPositionScheduleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "position_schedule_id";
				audit.mOldValue = salesschedulingupdatesOld.mPositionScheduleId.ToString();
				audit.mNewValue = salesschedulingupdates.mPositionScheduleId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mTimeIn != salesschedulingupdatesOld.mTimeIn)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "time_in";
				audit.mOldValue = salesschedulingupdatesOld.mTimeIn;
				audit.mNewValue = salesschedulingupdates.mTimeIn;
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mBreakOut != salesschedulingupdatesOld.mBreakOut)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "break_out";
				audit.mOldValue = salesschedulingupdatesOld.mBreakOut;
				audit.mNewValue = salesschedulingupdates.mBreakOut;
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mBreakIn != salesschedulingupdatesOld.mBreakIn)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "break_in";
				audit.mOldValue = salesschedulingupdatesOld.mBreakIn;
				audit.mNewValue = salesschedulingupdates.mBreakIn;
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mTimeOut != salesschedulingupdatesOld.mTimeOut)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "time_out";
				audit.mOldValue = salesschedulingupdatesOld.mTimeOut;
				audit.mNewValue = salesschedulingupdates.mTimeOut;
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mAgencyId != salesschedulingupdatesOld.mAgencyId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "agency_id";
				audit.mOldValue = salesschedulingupdatesOld.mAgencyId.ToString();
				audit.mNewValue = salesschedulingupdates.mAgencyId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mPosition != salesschedulingupdatesOld.mPosition)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "position";
				audit.mOldValue = salesschedulingupdatesOld.mPosition;
				audit.mNewValue = salesschedulingupdates.mPosition;
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mPositionId != salesschedulingupdatesOld.mPositionId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "position_id";
				audit.mOldValue = salesschedulingupdatesOld.mPositionId.ToString();
				audit.mNewValue = salesschedulingupdates.mPositionId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mBranchWorkable != salesschedulingupdatesOld.mBranchWorkable)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "branch_workable";
				audit.mOldValue = salesschedulingupdatesOld.mBranchWorkable.ToString();
				audit.mNewValue = salesschedulingupdates.mBranchWorkable.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mContinueToNextweek != salesschedulingupdatesOld.mContinueToNextweek)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "continue_to_nextweek";
				audit.mOldValue = salesschedulingupdatesOld.mContinueToNextweek.ToString();
				audit.mNewValue = salesschedulingupdates.mContinueToNextweek.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mAssignTo != salesschedulingupdatesOld.mAssignTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "assign_to";
				audit.mOldValue = salesschedulingupdatesOld.mAssignTo.ToString();
				audit.mNewValue = salesschedulingupdates.mAssignTo.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mAsIs != salesschedulingupdatesOld.mAsIs)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "as_is";
				audit.mOldValue = salesschedulingupdatesOld.mAsIs.ToString();
				audit.mNewValue = salesschedulingupdates.mAsIs.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mStartDate != salesschedulingupdatesOld.mStartDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "start_date";
				audit.mOldValue = salesschedulingupdatesOld.mStartDate.ToString();
				audit.mNewValue = salesschedulingupdates.mStartDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingupdates.mEndDate != salesschedulingupdatesOld.mEndDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingupdates);
				audit.mField = "end_date";
				audit.mOldValue = salesschedulingupdatesOld.mEndDate.ToString();
				audit.mNewValue = salesschedulingupdates.mEndDate.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, SalesSchedulingUpdates salesschedulingupdates)
		{
			audit.mUserFullName = salesschedulingupdates.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_SalesSchedulingUpdates);
			audit.mRowId = salesschedulingupdates.mId;
			audit.mAction = 2;
		}
	}
}