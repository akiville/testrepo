using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class LmmAttendanceScheduleTypeAudit
	{
		public static AuditCollection Audit(LmmAttendanceScheduleType lmmattendancescheduletype,LmmAttendanceScheduleType lmmattendancescheduletypeOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (lmmattendancescheduletype.mName != lmmattendancescheduletypeOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendancescheduletype);
				audit.mField = "name";
				audit.mOldValue = lmmattendancescheduletypeOld.mName;
				audit.mNewValue = lmmattendancescheduletype.mName;
				audit_collection.Add(audit);
			}

			if (lmmattendancescheduletype.mIsPresent != lmmattendancescheduletypeOld.mIsPresent)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendancescheduletype);
				audit.mField = "is_present";
				audit.mOldValue = lmmattendancescheduletypeOld.mIsPresent.ToString();
				audit.mNewValue = lmmattendancescheduletype.mIsPresent.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendancescheduletype.mBackColor != lmmattendancescheduletypeOld.mBackColor)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendancescheduletype);
				audit.mField = "back_color";
				audit.mOldValue = lmmattendancescheduletypeOld.mBackColor;
				audit.mNewValue = lmmattendancescheduletype.mBackColor;
				audit_collection.Add(audit);
			}

			if (lmmattendancescheduletype.mForeColor != lmmattendancescheduletypeOld.mForeColor)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendancescheduletype);
				audit.mField = "fore_color";
				audit.mOldValue = lmmattendancescheduletypeOld.mForeColor;
				audit.mNewValue = lmmattendancescheduletype.mForeColor;
				audit_collection.Add(audit);
			}

			if (lmmattendancescheduletype.mSmsCode != lmmattendancescheduletypeOld.mSmsCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendancescheduletype);
				audit.mField = "sms_code";
				audit.mOldValue = lmmattendancescheduletypeOld.mSmsCode;
				audit.mNewValue = lmmattendancescheduletype.mSmsCode;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, LmmAttendanceScheduleType lmmattendancescheduletype)
		{
			audit.mUserFullName = lmmattendancescheduletype.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_LmmAttendanceScheduleType);
			audit.mRowId = lmmattendancescheduletype.mId;
			audit.mAction = 2;
		}
	}
}