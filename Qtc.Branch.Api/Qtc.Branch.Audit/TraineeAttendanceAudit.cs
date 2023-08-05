using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class TraineeAttendanceAudit
	{
		public static AuditCollection Audit(TraineeAttendance traineeattendance,TraineeAttendance traineeattendanceOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (traineeattendance.mLmmId != traineeattendanceOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "lmm_id";
				audit.mOldValue = traineeattendanceOld.mLmmId.ToString();
				audit.mNewValue = traineeattendance.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (traineeattendance.mEmployeeId != traineeattendanceOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "employee_id";
				audit.mOldValue = traineeattendanceOld.mEmployeeId.ToString();
				audit.mNewValue = traineeattendance.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (traineeattendance.mBranchId != traineeattendanceOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "branch_id";
				audit.mOldValue = traineeattendanceOld.mBranchId.ToString();
				audit.mNewValue = traineeattendance.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (traineeattendance.mMondayDate != traineeattendanceOld.mMondayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "monday_date";
				audit.mOldValue = traineeattendanceOld.mMondayDate.ToString();
				audit.mNewValue = traineeattendance.mMondayDate.ToString();
				audit_collection.Add(audit);
			}

			if (traineeattendance.mMondayStatus != traineeattendanceOld.mMondayStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "monday_status";
				audit.mOldValue = traineeattendanceOld.mMondayStatus;
				audit.mNewValue = traineeattendance.mMondayStatus;
				audit_collection.Add(audit);
			}

			if (traineeattendance.mTuesdayDate != traineeattendanceOld.mTuesdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "tuesday_date";
				audit.mOldValue = traineeattendanceOld.mTuesdayDate.ToString();
				audit.mNewValue = traineeattendance.mTuesdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (traineeattendance.mTuesdayStatus != traineeattendanceOld.mTuesdayStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "tuesday_status";
				audit.mOldValue = traineeattendanceOld.mTuesdayStatus.ToString();
				audit.mNewValue = traineeattendance.mTuesdayStatus.ToString();
				audit_collection.Add(audit);
			}

			if (traineeattendance.mWednesdayDate != traineeattendanceOld.mWednesdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "wednesday_date";
				audit.mOldValue = traineeattendanceOld.mWednesdayDate.ToString();
				audit.mNewValue = traineeattendance.mWednesdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (traineeattendance.mWednesdayStatus != traineeattendanceOld.mWednesdayStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "wednesday_status";
				audit.mOldValue = traineeattendanceOld.mWednesdayStatus;
				audit.mNewValue = traineeattendance.mWednesdayStatus;
				audit_collection.Add(audit);
			}

			if (traineeattendance.mThursdayDate != traineeattendanceOld.mThursdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "thursday_date";
				audit.mOldValue = traineeattendanceOld.mThursdayDate.ToString();
				audit.mNewValue = traineeattendance.mThursdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (traineeattendance.mThursdayStatus != traineeattendanceOld.mThursdayStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "thursday_status";
				audit.mOldValue = traineeattendanceOld.mThursdayStatus;
				audit.mNewValue = traineeattendance.mThursdayStatus;
				audit_collection.Add(audit);
			}

			if (traineeattendance.mFridayDate != traineeattendanceOld.mFridayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "friday_date";
				audit.mOldValue = traineeattendanceOld.mFridayDate.ToString();
				audit.mNewValue = traineeattendance.mFridayDate.ToString();
				audit_collection.Add(audit);
			}

			if (traineeattendance.mFridayStatus != traineeattendanceOld.mFridayStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "friday_status";
				audit.mOldValue = traineeattendanceOld.mFridayStatus;
				audit.mNewValue = traineeattendance.mFridayStatus;
				audit_collection.Add(audit);
			}

			if (traineeattendance.mSaturdayDate != traineeattendanceOld.mSaturdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "saturday_date";
				audit.mOldValue = traineeattendanceOld.mSaturdayDate.ToString();
				audit.mNewValue = traineeattendance.mSaturdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (traineeattendance.mSaturdayStatus != traineeattendanceOld.mSaturdayStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "saturday_status";
				audit.mOldValue = traineeattendanceOld.mSaturdayStatus;
				audit.mNewValue = traineeattendance.mSaturdayStatus;
				audit_collection.Add(audit);
			}

			if (traineeattendance.mSundayDate != traineeattendanceOld.mSundayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "sunday_date";
				audit.mOldValue = traineeattendanceOld.mSundayDate.ToString();
				audit.mNewValue = traineeattendance.mSundayDate.ToString();
				audit_collection.Add(audit);
			}

			if (traineeattendance.mSundayStatus != traineeattendanceOld.mSundayStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "sunday_status";
				audit.mOldValue = traineeattendanceOld.mSundayStatus;
				audit.mNewValue = traineeattendance.mSundayStatus;
				audit_collection.Add(audit);
			}

			if (traineeattendance.mSalesScheduleId != traineeattendanceOld.mSalesScheduleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "sales_schedule_id";
				audit.mOldValue = traineeattendanceOld.mSalesScheduleId.ToString();
				audit.mNewValue = traineeattendance.mSalesScheduleId.ToString();
				audit_collection.Add(audit);
			}

			if (traineeattendance.mRemarks != traineeattendanceOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "remarks";
				audit.mOldValue = traineeattendanceOld.mRemarks;
				audit.mNewValue = traineeattendance.mRemarks;
				audit_collection.Add(audit);
			}

			if (traineeattendance.mDatestamp != traineeattendanceOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, traineeattendance);
				audit.mField = "datestamp";
				audit.mOldValue = traineeattendanceOld.mDatestamp.ToString();
				audit.mNewValue = traineeattendance.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, TraineeAttendance traineeattendance)
		{
			audit.mUserFullName = traineeattendance.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_TraineeAttendance);
			audit.mRowId = traineeattendance.mId;
			audit.mAction = 2;
		}
	}
}