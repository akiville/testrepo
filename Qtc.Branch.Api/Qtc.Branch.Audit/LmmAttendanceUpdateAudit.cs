using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class LmmAttendanceUpdateAudit
	{
		public static AuditCollection Audit(LmmAttendanceUpdate lmmattendanceupdate,LmmAttendanceUpdate lmmattendanceupdateOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (lmmattendanceupdate.mSsuId != lmmattendanceupdateOld.mSsuId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "ssu_id";
				audit.mOldValue = lmmattendanceupdateOld.mSsuId.ToString();
				audit.mNewValue = lmmattendanceupdate.mSsuId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mMondayDate != lmmattendanceupdateOld.mMondayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "monday_date";
				audit.mOldValue = lmmattendanceupdateOld.mMondayDate.ToString();
				audit.mNewValue = lmmattendanceupdate.mMondayDate.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mMondayScheduleId != lmmattendanceupdateOld.mMondayScheduleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "monday_schedule_id";
				audit.mOldValue = lmmattendanceupdateOld.mMondayScheduleId.ToString();
				audit.mNewValue = lmmattendanceupdate.mMondayScheduleId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mTuesdayDate != lmmattendanceupdateOld.mTuesdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "tuesday_date";
				audit.mOldValue = lmmattendanceupdateOld.mTuesdayDate.ToString();
				audit.mNewValue = lmmattendanceupdate.mTuesdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mTuesdayScheduleId != lmmattendanceupdateOld.mTuesdayScheduleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "tuesday_schedule_id";
				audit.mOldValue = lmmattendanceupdateOld.mTuesdayScheduleId.ToString();
				audit.mNewValue = lmmattendanceupdate.mTuesdayScheduleId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mWednesdayDate != lmmattendanceupdateOld.mWednesdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "wednesday_date";
				audit.mOldValue = lmmattendanceupdateOld.mWednesdayDate.ToString();
				audit.mNewValue = lmmattendanceupdate.mWednesdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mWednesdayScheduleId != lmmattendanceupdateOld.mWednesdayScheduleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "wednesday_schedule_id";
				audit.mOldValue = lmmattendanceupdateOld.mWednesdayScheduleId.ToString();
				audit.mNewValue = lmmattendanceupdate.mWednesdayScheduleId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mThursdayDate != lmmattendanceupdateOld.mThursdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "thursday_date";
				audit.mOldValue = lmmattendanceupdateOld.mThursdayDate.ToString();
				audit.mNewValue = lmmattendanceupdate.mThursdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mThursdayScheduleId != lmmattendanceupdateOld.mThursdayScheduleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "thursday_schedule_id";
				audit.mOldValue = lmmattendanceupdateOld.mThursdayScheduleId.ToString();
				audit.mNewValue = lmmattendanceupdate.mThursdayScheduleId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mFridayDate != lmmattendanceupdateOld.mFridayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "friday_date";
				audit.mOldValue = lmmattendanceupdateOld.mFridayDate.ToString();
				audit.mNewValue = lmmattendanceupdate.mFridayDate.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mFridayScheduleId != lmmattendanceupdateOld.mFridayScheduleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "friday_schedule_id";
				audit.mOldValue = lmmattendanceupdateOld.mFridayScheduleId.ToString();
				audit.mNewValue = lmmattendanceupdate.mFridayScheduleId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mSaturdayDate != lmmattendanceupdateOld.mSaturdayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "saturday_date";
				audit.mOldValue = lmmattendanceupdateOld.mSaturdayDate.ToString();
				audit.mNewValue = lmmattendanceupdate.mSaturdayDate.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mSaturdayScheduleId != lmmattendanceupdateOld.mSaturdayScheduleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "saturday_schedule_id";
				audit.mOldValue = lmmattendanceupdateOld.mSaturdayScheduleId.ToString();
				audit.mNewValue = lmmattendanceupdate.mSaturdayScheduleId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mSundayDate != lmmattendanceupdateOld.mSundayDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "sunday_date";
				audit.mOldValue = lmmattendanceupdateOld.mSundayDate.ToString();
				audit.mNewValue = lmmattendanceupdate.mSundayDate.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mSundayScheduleId != lmmattendanceupdateOld.mSundayScheduleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "sunday_schedule_id";
				audit.mOldValue = lmmattendanceupdateOld.mSundayScheduleId.ToString();
				audit.mNewValue = lmmattendanceupdate.mSundayScheduleId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mEmployeeId != lmmattendanceupdateOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "employee_id";
				audit.mOldValue = lmmattendanceupdateOld.mEmployeeId.ToString();
				audit.mNewValue = lmmattendanceupdate.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mCutoffId != lmmattendanceupdateOld.mCutoffId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "cutoff_id";
				audit.mOldValue = lmmattendanceupdateOld.mCutoffId.ToString();
				audit.mNewValue = lmmattendanceupdate.mCutoffId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mLmmId != lmmattendanceupdateOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "lmm_id";
				audit.mOldValue = lmmattendanceupdateOld.mLmmId.ToString();
				audit.mNewValue = lmmattendanceupdate.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendanceupdate.mRecordId != lmmattendanceupdateOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendanceupdate);
				audit.mField = "record_id";
				audit.mOldValue = lmmattendanceupdateOld.mRecordId.ToString();
				audit.mNewValue = lmmattendanceupdate.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, LmmAttendanceUpdate lmmattendanceupdate)
		{
			audit.mUserFullName = lmmattendanceupdate.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_LmmAttendanceUpdate);
			audit.mRowId = lmmattendanceupdate.mId;
			audit.mAction = 2;
		}
	}
}