using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class SalesScheduleAudit
	{
		public static AuditCollection Audit(SalesSchedule salesschedule,SalesSchedule salesscheduleOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (salesschedule.mScheduleId != salesscheduleOld.mScheduleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedule);
				audit.mField = "schedule_id";
				audit.mOldValue = salesscheduleOld.mScheduleId.ToString();
				audit.mNewValue = salesschedule.mScheduleId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedule.mBranchId != salesscheduleOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedule);
				audit.mField = "branch_id";
				audit.mOldValue = salesscheduleOld.mBranchId.ToString();
				audit.mNewValue = salesschedule.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedule.mEmployeeId != salesscheduleOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedule);
				audit.mField = "employee_id";
				audit.mOldValue = salesscheduleOld.mEmployeeId.ToString();
				audit.mNewValue = salesschedule.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedule.mDate != salesscheduleOld.mDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedule);
				audit.mField = "date";
				audit.mOldValue = salesscheduleOld.mDate.ToString();
				audit.mNewValue = salesschedule.mDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedule.mHasId != salesscheduleOld.mHasId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedule);
				audit.mField = "has_id";
				audit.mOldValue = salesscheduleOld.mHasId.ToString();
				audit.mNewValue = salesschedule.mHasId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedule.mExpiredId != salesscheduleOld.mExpiredId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedule);
				audit.mField = "expired_id";
				audit.mOldValue = salesscheduleOld.mExpiredId.ToString();
				audit.mNewValue = salesschedule.mExpiredId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedule.mSchedule != salesscheduleOld.mSchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedule);
				audit.mField = "schedule";
				audit.mOldValue = salesscheduleOld.mSchedule.ToString();
				audit.mNewValue = salesschedule.mSchedule.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedule.mTimeIn != salesscheduleOld.mTimeIn)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedule);
				audit.mField = "time_in";
				audit.mOldValue = salesscheduleOld.mTimeIn;
				audit.mNewValue = salesschedule.mTimeIn;
				audit_collection.Add(audit);
			}

			if (salesschedule.mBreakout != salesscheduleOld.mBreakout)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedule);
				audit.mField = "breakout";
				audit.mOldValue = salesscheduleOld.mBreakout;
				audit.mNewValue = salesschedule.mBreakout;
				audit_collection.Add(audit);
			}

			if (salesschedule.mBreakin != salesscheduleOld.mBreakin)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedule);
				audit.mField = "breakin";
				audit.mOldValue = salesscheduleOld.mBreakin;
				audit.mNewValue = salesschedule.mBreakin;
				audit_collection.Add(audit);
			}

			if (salesschedule.mTimeOut != salesscheduleOld.mTimeOut)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedule);
				audit.mField = "time_out";
				audit.mOldValue = salesscheduleOld.mTimeOut;
				audit.mNewValue = salesschedule.mTimeOut;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, SalesSchedule salesschedule)
		{
			audit.mUserFullName = salesschedule.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_SalesSchedule);
			audit.mRowId = salesschedule.mId;
			audit.mAction = 2;
		}
	}
}