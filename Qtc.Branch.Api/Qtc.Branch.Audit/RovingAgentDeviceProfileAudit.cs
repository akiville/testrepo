using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingAgentDeviceProfileAudit
	{
		public static AuditCollection Audit(RovingAgentDeviceProfile rovingagentdeviceprofile,RovingAgentDeviceProfile rovingagentdeviceprofileOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingagentdeviceprofile.mDeviceId != rovingagentdeviceprofileOld.mDeviceId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentdeviceprofile);
				audit.mField = "device_id";
				audit.mOldValue = rovingagentdeviceprofileOld.mDeviceId;
				audit.mNewValue = rovingagentdeviceprofile.mDeviceId;
				audit_collection.Add(audit);
			}

			if (rovingagentdeviceprofile.mKioskMode != rovingagentdeviceprofileOld.mKioskMode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentdeviceprofile);
				audit.mField = "kiosk_mode";
				audit.mOldValue = rovingagentdeviceprofileOld.mKioskMode.ToString();
				audit.mNewValue = rovingagentdeviceprofile.mKioskMode.ToString();
				audit_collection.Add(audit);
			}

			if (rovingagentdeviceprofile.mIsLocked != rovingagentdeviceprofileOld.mIsLocked)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentdeviceprofile);
				audit.mField = "is_locked";
				audit.mOldValue = rovingagentdeviceprofileOld.mIsLocked.ToString();
				audit.mNewValue = rovingagentdeviceprofile.mIsLocked.ToString();
				audit_collection.Add(audit);
			}

			if (rovingagentdeviceprofile.mMobileNo != rovingagentdeviceprofileOld.mMobileNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentdeviceprofile);
				audit.mField = "mobile_no";
				audit.mOldValue = rovingagentdeviceprofileOld.mMobileNo;
				audit.mNewValue = rovingagentdeviceprofile.mMobileNo;
				audit_collection.Add(audit);
			}

			if (rovingagentdeviceprofile.mUserId != rovingagentdeviceprofileOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentdeviceprofile);
				audit.mField = "user_id";
				audit.mOldValue = rovingagentdeviceprofileOld.mUserId.ToString();
				audit.mNewValue = rovingagentdeviceprofile.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingagentdeviceprofile.mLastKnowLocation != rovingagentdeviceprofileOld.mLastKnowLocation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingagentdeviceprofile);
				audit.mField = "last_know_location";
				audit.mOldValue = rovingagentdeviceprofileOld.mLastKnowLocation;
				audit.mNewValue = rovingagentdeviceprofile.mLastKnowLocation;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingAgentDeviceProfile rovingagentdeviceprofile)
		{
			audit.mUserFullName = rovingagentdeviceprofile.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingAgentDeviceProfile);
			audit.mRowId = rovingagentdeviceprofile.mId;
			audit.mAction = 2;
		}
	}
}