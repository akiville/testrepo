using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class TrainingAttendance2Audit
	{
		public static AuditCollection Audit(TrainingAttendance2 trainingattendance2,TrainingAttendance2 trainingattendance2Old)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (trainingattendance2.mSalesSchedulingId != trainingattendance2Old.mSalesSchedulingId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "sales_scheduling_id";
				audit.mOldValue = trainingattendance2Old.mSalesSchedulingId.ToString();
				audit.mNewValue = trainingattendance2.mSalesSchedulingId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mCutoffId != trainingattendance2Old.mCutoffId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "cutoff_id";
				audit.mOldValue = trainingattendance2Old.mCutoffId.ToString();
				audit.mNewValue = trainingattendance2.mCutoffId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mBranch != trainingattendance2Old.mBranch)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "branch";
				audit.mOldValue = trainingattendance2Old.mBranch.ToString();
				audit.mNewValue = trainingattendance2.mBranch.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mLmm != trainingattendance2Old.mLmm)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "lmm";
				audit.mOldValue = trainingattendance2Old.mLmm;
				audit.mNewValue = trainingattendance2.mLmm;
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mType != trainingattendance2Old.mType)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "type";
				audit.mOldValue = trainingattendance2Old.mType;
				audit.mNewValue = trainingattendance2.mType;
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mBranchArea != trainingattendance2Old.mBranchArea)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "branch_area";
				audit.mOldValue = trainingattendance2Old.mBranchArea;
				audit.mNewValue = trainingattendance2.mBranchArea;
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mAgency != trainingattendance2Old.mAgency)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "agency";
				audit.mOldValue = trainingattendance2Old.mAgency;
				audit.mNewValue = trainingattendance2.mAgency;
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mRate != trainingattendance2Old.mRate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "rate";
				audit.mOldValue = trainingattendance2Old.mRate.ToString();
				audit.mNewValue = trainingattendance2.mRate.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mAmount != trainingattendance2Old.mAmount)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "amount";
				audit.mOldValue = trainingattendance2Old.mAmount.ToString();
				audit.mNewValue = trainingattendance2.mAmount.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mCurrentBranchId != trainingattendance2Old.mCurrentBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "current_branch_id";
				audit.mOldValue = trainingattendance2Old.mCurrentBranchId;
				audit.mNewValue = trainingattendance2.mCurrentBranchId;
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mIdExpired != trainingattendance2Old.mIdExpired)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "id_expired";
				audit.mOldValue = trainingattendance2Old.mIdExpired;
				audit.mNewValue = trainingattendance2.mIdExpired;
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mMondayDate != trainingattendance2Old.mMondayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "monday_date";
				audit.mOldValue = trainingattendance2Old.mMondayDate.ToString();
				audit.mNewValue = trainingattendance2.mMondayDate.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mMonday != trainingattendance2Old.mMonday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "monday";
				audit.mOldValue = trainingattendance2Old.mMonday.ToString();
				audit.mNewValue = trainingattendance2.mMonday.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mMondaySchedule != trainingattendance2Old.mMondaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "monday_schedule";
				audit.mOldValue = trainingattendance2Old.mMondaySchedule.ToString();
				audit.mNewValue = trainingattendance2.mMondaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mMondayHasId != trainingattendance2Old.mMondayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "monday_has_id";
				audit.mOldValue = trainingattendance2Old.mMondayHasId.ToString();
				audit.mNewValue = trainingattendance2.mMondayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mMondayExpiredId != trainingattendance2Old.mMondayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "monday_expired_id";
				audit.mOldValue = trainingattendance2Old.mMondayExpiredId.ToString();
				audit.mNewValue = trainingattendance2.mMondayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mTuesdayDate != trainingattendance2Old.mTuesdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "tuesday_date";
				audit.mOldValue = trainingattendance2Old.mTuesdayDate.ToString();
				audit.mNewValue = trainingattendance2.mTuesdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mTuesday != trainingattendance2Old.mTuesday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "tuesday";
				audit.mOldValue = trainingattendance2Old.mTuesday.ToString();
				audit.mNewValue = trainingattendance2.mTuesday.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mTuesdaySchedule != trainingattendance2Old.mTuesdaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "tuesday_schedule";
				audit.mOldValue = trainingattendance2Old.mTuesdaySchedule.ToString();
				audit.mNewValue = trainingattendance2.mTuesdaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mTuesdayHasId != trainingattendance2Old.mTuesdayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "tuesday_has_id";
				audit.mOldValue = trainingattendance2Old.mTuesdayHasId.ToString();
				audit.mNewValue = trainingattendance2.mTuesdayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mTuesdayExpiredId != trainingattendance2Old.mTuesdayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "tuesday_expired_id";
				audit.mOldValue = trainingattendance2Old.mTuesdayExpiredId.ToString();
				audit.mNewValue = trainingattendance2.mTuesdayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mWednesdayDate != trainingattendance2Old.mWednesdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "wednesday_date";
				audit.mOldValue = trainingattendance2Old.mWednesdayDate.ToString();
				audit.mNewValue = trainingattendance2.mWednesdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mWednesday != trainingattendance2Old.mWednesday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "wednesday";
				audit.mOldValue = trainingattendance2Old.mWednesday.ToString();
				audit.mNewValue = trainingattendance2.mWednesday.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mWednesdaySchedule != trainingattendance2Old.mWednesdaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "wednesday_schedule";
				audit.mOldValue = trainingattendance2Old.mWednesdaySchedule.ToString();
				audit.mNewValue = trainingattendance2.mWednesdaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mWednesdayHasId != trainingattendance2Old.mWednesdayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "wednesday_has_id";
				audit.mOldValue = trainingattendance2Old.mWednesdayHasId.ToString();
				audit.mNewValue = trainingattendance2.mWednesdayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mWednesdayExpiredId != trainingattendance2Old.mWednesdayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "wednesday_expired_id";
				audit.mOldValue = trainingattendance2Old.mWednesdayExpiredId.ToString();
				audit.mNewValue = trainingattendance2.mWednesdayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mThursdayDate != trainingattendance2Old.mThursdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "thursday_date";
				audit.mOldValue = trainingattendance2Old.mThursdayDate.ToString();
				audit.mNewValue = trainingattendance2.mThursdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mThursday != trainingattendance2Old.mThursday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "thursday";
				audit.mOldValue = trainingattendance2Old.mThursday.ToString();
				audit.mNewValue = trainingattendance2.mThursday.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mThursdaySchedule != trainingattendance2Old.mThursdaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "thursday_schedule";
				audit.mOldValue = trainingattendance2Old.mThursdaySchedule.ToString();
				audit.mNewValue = trainingattendance2.mThursdaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mThursdayHasId != trainingattendance2Old.mThursdayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "thursday_has_id";
				audit.mOldValue = trainingattendance2Old.mThursdayHasId.ToString();
				audit.mNewValue = trainingattendance2.mThursdayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mThursdayExpiredId != trainingattendance2Old.mThursdayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "thursday_expired_id";
				audit.mOldValue = trainingattendance2Old.mThursdayExpiredId.ToString();
				audit.mNewValue = trainingattendance2.mThursdayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mFridayDate != trainingattendance2Old.mFridayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "friday_date";
				audit.mOldValue = trainingattendance2Old.mFridayDate.ToString();
				audit.mNewValue = trainingattendance2.mFridayDate.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mFriday != trainingattendance2Old.mFriday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "friday";
				audit.mOldValue = trainingattendance2Old.mFriday.ToString();
				audit.mNewValue = trainingattendance2.mFriday.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mFridaySchedule != trainingattendance2Old.mFridaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "friday_schedule";
				audit.mOldValue = trainingattendance2Old.mFridaySchedule.ToString();
				audit.mNewValue = trainingattendance2.mFridaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mFridayHasId != trainingattendance2Old.mFridayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "friday_has_id";
				audit.mOldValue = trainingattendance2Old.mFridayHasId.ToString();
				audit.mNewValue = trainingattendance2.mFridayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mFridayExpiredId != trainingattendance2Old.mFridayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "friday_expired_id";
				audit.mOldValue = trainingattendance2Old.mFridayExpiredId.ToString();
				audit.mNewValue = trainingattendance2.mFridayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mSaturdayDate != trainingattendance2Old.mSaturdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "saturday_date";
				audit.mOldValue = trainingattendance2Old.mSaturdayDate.ToString();
				audit.mNewValue = trainingattendance2.mSaturdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mSaturday != trainingattendance2Old.mSaturday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "saturday";
				audit.mOldValue = trainingattendance2Old.mSaturday.ToString();
				audit.mNewValue = trainingattendance2.mSaturday.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mSaturdaySchedule != trainingattendance2Old.mSaturdaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "saturday_schedule";
				audit.mOldValue = trainingattendance2Old.mSaturdaySchedule.ToString();
				audit.mNewValue = trainingattendance2.mSaturdaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mSaturdayHasId != trainingattendance2Old.mSaturdayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "saturday_has_id";
				audit.mOldValue = trainingattendance2Old.mSaturdayHasId.ToString();
				audit.mNewValue = trainingattendance2.mSaturdayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mSaturdayExpiredId != trainingattendance2Old.mSaturdayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "saturday_expired_id";
				audit.mOldValue = trainingattendance2Old.mSaturdayExpiredId.ToString();
				audit.mNewValue = trainingattendance2.mSaturdayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mSundayDate != trainingattendance2Old.mSundayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "sunday_date";
				audit.mOldValue = trainingattendance2Old.mSundayDate.ToString();
				audit.mNewValue = trainingattendance2.mSundayDate.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mSunday != trainingattendance2Old.mSunday)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "sunday";
				audit.mOldValue = trainingattendance2Old.mSunday.ToString();
				audit.mNewValue = trainingattendance2.mSunday.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mSundaySchedule != trainingattendance2Old.mSundaySchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "sunday_schedule";
				audit.mOldValue = trainingattendance2Old.mSundaySchedule.ToString();
				audit.mNewValue = trainingattendance2.mSundaySchedule.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mSundayHasId != trainingattendance2Old.mSundayHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "sunday_has_id";
				audit.mOldValue = trainingattendance2Old.mSundayHasId.ToString();
				audit.mNewValue = trainingattendance2.mSundayHasId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mSundayExpiredId != trainingattendance2Old.mSundayExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "sunday_expired_id";
				audit.mOldValue = trainingattendance2Old.mSundayExpiredId.ToString();
				audit.mNewValue = trainingattendance2.mSundayExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mEmployeeId != trainingattendance2Old.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "employee_id";
				audit.mOldValue = trainingattendance2Old.mEmployeeId.ToString();
				audit.mNewValue = trainingattendance2.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mPositionScheduleId != trainingattendance2Old.mPositionScheduleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "position_schedule_id";
				audit.mOldValue = trainingattendance2Old.mPositionScheduleId.ToString();
				audit.mNewValue = trainingattendance2.mPositionScheduleId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mTimeIn != trainingattendance2Old.mTimeIn)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "time_in";
				audit.mOldValue = trainingattendance2Old.mTimeIn;
				audit.mNewValue = trainingattendance2.mTimeIn;
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mBreakOut != trainingattendance2Old.mBreakOut)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "break_out";
				audit.mOldValue = trainingattendance2Old.mBreakOut;
				audit.mNewValue = trainingattendance2.mBreakOut;
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mBreakIn != trainingattendance2Old.mBreakIn)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "break_in";
				audit.mOldValue = trainingattendance2Old.mBreakIn;
				audit.mNewValue = trainingattendance2.mBreakIn;
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mTimeOut != trainingattendance2Old.mTimeOut)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "time_out";
				audit.mOldValue = trainingattendance2Old.mTimeOut;
				audit.mNewValue = trainingattendance2.mTimeOut;
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mAgencyId != trainingattendance2Old.mAgencyId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "agency_id";
				audit.mOldValue = trainingattendance2Old.mAgencyId.ToString();
				audit.mNewValue = trainingattendance2.mAgencyId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mPosition != trainingattendance2Old.mPosition)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "position";
				audit.mOldValue = trainingattendance2Old.mPosition;
				audit.mNewValue = trainingattendance2.mPosition;
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mPositionId != trainingattendance2Old.mPositionId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "position_id";
				audit.mOldValue = trainingattendance2Old.mPositionId.ToString();
				audit.mNewValue = trainingattendance2.mPositionId.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mBranchWorkable != trainingattendance2Old.mBranchWorkable)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "branch_workable";
				audit.mOldValue = trainingattendance2Old.mBranchWorkable.ToString();
				audit.mNewValue = trainingattendance2.mBranchWorkable.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mContinueToNextweek != trainingattendance2Old.mContinueToNextweek)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "continue_to_nextweek";
				audit.mOldValue = trainingattendance2Old.mContinueToNextweek.ToString();
				audit.mNewValue = trainingattendance2.mContinueToNextweek.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mAssignTo != trainingattendance2Old.mAssignTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "assign_to";
				audit.mOldValue = trainingattendance2Old.mAssignTo.ToString();
				audit.mNewValue = trainingattendance2.mAssignTo.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mAsIs != trainingattendance2Old.mAsIs)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "as_is";
				audit.mOldValue = trainingattendance2Old.mAsIs.ToString();
				audit.mNewValue = trainingattendance2.mAsIs.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mStartDate != trainingattendance2Old.mStartDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "start_date";
				audit.mOldValue = trainingattendance2Old.mStartDate.ToString();
				audit.mNewValue = trainingattendance2.mStartDate.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mEndDate != trainingattendance2Old.mEndDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "end_date";
				audit.mOldValue = trainingattendance2Old.mEndDate.ToString();
				audit.mNewValue = trainingattendance2.mEndDate.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mIsConfirmed != trainingattendance2Old.mIsConfirmed)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "is_confirmed";
				audit.mOldValue = trainingattendance2Old.mIsConfirmed.ToString();
				audit.mNewValue = trainingattendance2.mIsConfirmed.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mConfirmedBy != trainingattendance2Old.mConfirmedBy)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "confirmed_by";
				audit.mOldValue = trainingattendance2Old.mConfirmedBy.ToString();
				audit.mNewValue = trainingattendance2.mConfirmedBy.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mConfirmationDate != trainingattendance2Old.mConfirmationDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "confirmation_date";
				audit.mOldValue = trainingattendance2Old.mConfirmationDate.ToString();
				audit.mNewValue = trainingattendance2.mConfirmationDate.ToString();
				audit_collection.Add(audit);
			}

			if (trainingattendance2.mConfirmationRemarks != trainingattendance2Old.mConfirmationRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, trainingattendance2);
				audit.mField = "confirmation_remarks";
				audit.mOldValue = trainingattendance2Old.mConfirmationRemarks;
				audit.mNewValue = trainingattendance2.mConfirmationRemarks;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, TrainingAttendance2 trainingattendance2)
		{
			audit.mUserFullName = trainingattendance2.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_TrainingAttendance2);
			audit.mRowId = trainingattendance2.mId;
			audit.mAction = 2;
		}
	}
}