using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class UrgentScheduleChangeMessageAudit
	{
		public static AuditCollection Audit(UrgentScheduleChangeMessage urgentschedulechangemessage,UrgentScheduleChangeMessage urgentschedulechangemessageOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (urgentschedulechangemessage.mUrgentScheduleChangeId != urgentschedulechangemessageOld.mUrgentScheduleChangeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechangemessage);
				audit.mField = "urgent_schedule_change_id";
				audit.mOldValue = urgentschedulechangemessageOld.mUrgentScheduleChangeId.ToString();
				audit.mNewValue = urgentschedulechangemessage.mUrgentScheduleChangeId.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechangemessage.mLmmId != urgentschedulechangemessageOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechangemessage);
				audit.mField = "lmm_id";
				audit.mOldValue = urgentschedulechangemessageOld.mLmmId.ToString();
				audit.mNewValue = urgentschedulechangemessage.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechangemessage.mBranchId != urgentschedulechangemessageOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechangemessage);
				audit.mField = "branch_id";
				audit.mOldValue = urgentschedulechangemessageOld.mBranchId.ToString();
				audit.mNewValue = urgentschedulechangemessage.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechangemessage.mStartDate != urgentschedulechangemessageOld.mStartDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechangemessage);
				audit.mField = "start_date";
				audit.mOldValue = urgentschedulechangemessageOld.mStartDate.ToString();
				audit.mNewValue = urgentschedulechangemessage.mStartDate.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechangemessage.mEndDate != urgentschedulechangemessageOld.mEndDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechangemessage);
				audit.mField = "end_date";
				audit.mOldValue = urgentschedulechangemessageOld.mEndDate.ToString();
				audit.mNewValue = urgentschedulechangemessage.mEndDate.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechangemessage.mToLmmId != urgentschedulechangemessageOld.mToLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechangemessage);
				audit.mField = "to_lmm_id";
				audit.mOldValue = urgentschedulechangemessageOld.mToLmmId.ToString();
				audit.mNewValue = urgentschedulechangemessage.mToLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechangemessage.mStatus != urgentschedulechangemessageOld.mStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechangemessage);
				audit.mField = "status";
				audit.mOldValue = urgentschedulechangemessageOld.mStatus;
				audit.mNewValue = urgentschedulechangemessage.mStatus;
				audit_collection.Add(audit);
			}

			if (urgentschedulechangemessage.mPersonnelId != urgentschedulechangemessageOld.mPersonnelId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechangemessage);
				audit.mField = "personnel_id";
				audit.mOldValue = urgentschedulechangemessageOld.mPersonnelId.ToString();
				audit.mNewValue = urgentschedulechangemessage.mPersonnelId.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechangemessage.mPersonnelName != urgentschedulechangemessageOld.mPersonnelName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechangemessage);
				audit.mField = "personnel_name";
				audit.mOldValue = urgentschedulechangemessageOld.mPersonnelName;
				audit.mNewValue = urgentschedulechangemessage.mPersonnelName;
				audit_collection.Add(audit);
			}

			if (urgentschedulechangemessage.mDatetime != urgentschedulechangemessageOld.mDatetime)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechangemessage);
				audit.mField = "datetime";
				audit.mOldValue = urgentschedulechangemessageOld.mDatetime.ToString();
				audit.mNewValue = urgentschedulechangemessage.mDatetime.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, UrgentScheduleChangeMessage urgentschedulechangemessage)
		{
			audit.mUserFullName = urgentschedulechangemessage.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_UrgentScheduleChangeMessage);
			audit.mRowId = urgentschedulechangemessage.mId;
			audit.mAction = 2;
		}
	}
}