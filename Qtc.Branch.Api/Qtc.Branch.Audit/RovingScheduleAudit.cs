using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingScheduleAudit
	{
		public static AuditCollection Audit(RovingSchedule rovingschedule,RovingSchedule rovingscheduleOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingschedule.mRecordId != rovingscheduleOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingschedule);
				audit.mField = "record_id";
				audit.mOldValue = rovingscheduleOld.mRecordId.ToString();
				audit.mNewValue = rovingschedule.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingschedule.mName != rovingscheduleOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingschedule);
				audit.mField = "name";
				audit.mOldValue = rovingscheduleOld.mName;
				audit.mNewValue = rovingschedule.mName;
				audit_collection.Add(audit);
			}

			if (rovingschedule.mRemarks != rovingscheduleOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingschedule);
				audit.mField = "remarks";
				audit.mOldValue = rovingscheduleOld.mRemarks;
				audit.mNewValue = rovingschedule.mRemarks;
				audit_collection.Add(audit);
			}

			if (rovingschedule.mCode != rovingscheduleOld.mCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingschedule);
				audit.mField = "code";
				audit.mOldValue = rovingscheduleOld.mCode;
				audit.mNewValue = rovingschedule.mCode;
				audit_collection.Add(audit);
			}

			if (rovingschedule.mBackColor != rovingscheduleOld.mBackColor)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingschedule);
				audit.mField = "back_color";
				audit.mOldValue = rovingscheduleOld.mBackColor;
				audit.mNewValue = rovingschedule.mBackColor;
				audit_collection.Add(audit);
			}

			if (rovingschedule.mForeColor != rovingscheduleOld.mForeColor)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingschedule);
				audit.mField = "fore_color";
				audit.mOldValue = rovingscheduleOld.mForeColor;
				audit.mNewValue = rovingschedule.mForeColor;
				audit_collection.Add(audit);
			}

			if (rovingschedule.mBlueSlipNo != rovingscheduleOld.mBlueSlipNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingschedule);
				audit.mField = "blue_slip_no";
				audit.mOldValue = rovingscheduleOld.mBlueSlipNo.ToString();
				audit.mNewValue = rovingschedule.mBlueSlipNo.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingSchedule rovingschedule)
		{
			audit.mUserFullName = rovingschedule.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingSchedule);
			audit.mRowId = rovingschedule.mId;
			audit.mAction = 2;
		}
	}
}