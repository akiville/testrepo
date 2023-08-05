using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class UrgentScheduleChangeAudit
	{
		public static AuditCollection Audit(UrgentScheduleChange urgentschedulechange,UrgentScheduleChange urgentschedulechangeOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (urgentschedulechange.mLmmId != urgentschedulechangeOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechange);
				audit.mField = "lmm_id";
				audit.mOldValue = urgentschedulechangeOld.mLmmId.ToString();
				audit.mNewValue = urgentschedulechange.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechange.mDateFiled != urgentschedulechangeOld.mDateFiled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechange);
				audit.mField = "date_filed";
				audit.mOldValue = urgentschedulechangeOld.mDateFiled.ToString();
				audit.mNewValue = urgentschedulechange.mDateFiled.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechange.mIncidentDate != urgentschedulechangeOld.mIncidentDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechange);
				audit.mField = "incident_date";
				audit.mOldValue = urgentschedulechangeOld.mIncidentDate.ToString();
				audit.mNewValue = urgentschedulechange.mIncidentDate.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechange.mEmployeeId != urgentschedulechangeOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechange);
				audit.mField = "employee_id";
				audit.mOldValue = urgentschedulechangeOld.mEmployeeId.ToString();
				audit.mNewValue = urgentschedulechange.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechange.mConcern != urgentschedulechangeOld.mConcern)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechange);
				audit.mField = "concern";
				audit.mOldValue = urgentschedulechangeOld.mConcern;
				audit.mNewValue = urgentschedulechange.mConcern;
				audit_collection.Add(audit);
			}

			if (urgentschedulechange.mLmmAttendanceScheduleTypeId != urgentschedulechangeOld.mLmmAttendanceScheduleTypeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechange);
				audit.mField = "lmm_attendance_schedule_type_id";
				audit.mOldValue = urgentschedulechangeOld.mLmmAttendanceScheduleTypeId.ToString();
				audit.mNewValue = urgentschedulechange.mLmmAttendanceScheduleTypeId.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechange.mExplanation != urgentschedulechangeOld.mExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechange);
				audit.mField = "explanation";
				audit.mOldValue = urgentschedulechangeOld.mExplanation;
				audit.mNewValue = urgentschedulechange.mExplanation;
				audit_collection.Add(audit);
			}

			if (urgentschedulechange.mReasonCodeId != urgentschedulechangeOld.mReasonCodeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechange);
				audit.mField = "reason_code_id";
				audit.mOldValue = urgentschedulechangeOld.mReasonCodeId.ToString();
				audit.mNewValue = urgentschedulechange.mReasonCodeId.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechange.mAffectedPersonnelId != urgentschedulechangeOld.mAffectedPersonnelId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechange);
				audit.mField = "affected_persnnel_id";
				audit.mOldValue = urgentschedulechangeOld.mAffectedPersonnelId.ToString();
				audit.mNewValue = urgentschedulechange.mAffectedPersonnelId.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechange.mAffectedBranchId != urgentschedulechangeOld.mAffectedBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechange);
				audit.mField = "affected_branch_id";
				audit.mOldValue = urgentschedulechangeOld.mAffectedBranchId.ToString();
				audit.mNewValue = urgentschedulechange.mAffectedBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechange.mDatestamp != urgentschedulechangeOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechange);
				audit.mField = "datestamp";
				audit.mOldValue = urgentschedulechangeOld.mDatestamp.ToString();
				audit.mNewValue = urgentschedulechange.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			if (urgentschedulechange.mUserId != urgentschedulechangeOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, urgentschedulechange);
				audit.mField = "user_id";
				audit.mOldValue = urgentschedulechangeOld.mUserId.ToString();
				audit.mNewValue = urgentschedulechange.mUserId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, UrgentScheduleChange urgentschedulechange)
		{
			audit.mUserFullName = urgentschedulechange.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_UrgentScheduleChange);
			audit.mRowId = urgentschedulechange.mId;
			audit.mAction = 2;
		}
	}
}