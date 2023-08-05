using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class UrgentScheduleChangeBranchAudit
	{
		public static AuditCollection Audit(UrgentScheduleChangeBranch urgentschedulechangebranch,UrgentScheduleChangeBranch urgentschedulechangebranchOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (urgentschedulechangebranch.mUrgentScheduleChangeId != urgentschedulechangebranchOld.mUrgentScheduleChangeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechangebranch);
				audit.mField = "urgent_schedule_change_id";
				audit.mOldValue = urgentschedulechangebranchOld.mUrgentScheduleChangeId.ToString();
				audit.mNewValue = urgentschedulechangebranch.mUrgentScheduleChangeId.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechangebranch.mBranchId != urgentschedulechangebranchOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechangebranch);
				audit.mField = "branch_id";
				audit.mOldValue = urgentschedulechangebranchOld.mBranchId.ToString();
				audit.mNewValue = urgentschedulechangebranch.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechangebranch.mUserId != urgentschedulechangebranchOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechangebranch);
				audit.mField = "user_id";
				audit.mOldValue = urgentschedulechangebranchOld.mUserId.ToString();
				audit.mNewValue = urgentschedulechangebranch.mUserId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, UrgentScheduleChangeBranch urgentschedulechangebranch)
		{
			audit.mUserFullName = urgentschedulechangebranch.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_UrgentScheduleChangeBranch);
			audit.mRowId = urgentschedulechangebranch.mId;
			audit.mAction = 2;
		}
	}
}