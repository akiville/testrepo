using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DeviceLoginAudit
	{
		public static AuditCollection Audit(DeviceLogin devicelogin,DeviceLogin deviceloginOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (devicelogin.mEmployeeId != deviceloginOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, devicelogin);
				audit.mField = "employee_id";
				audit.mOldValue = deviceloginOld.mEmployeeId.ToString();
				audit.mNewValue = devicelogin.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (devicelogin.mDeviceSerial != deviceloginOld.mDeviceSerial)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, devicelogin);
				audit.mField = "device_serial";
				audit.mOldValue = deviceloginOld.mDeviceSerial;
				audit.mNewValue = devicelogin.mDeviceSerial;
				audit_collection.Add(audit);
			}

			if (devicelogin.mLoginDate != deviceloginOld.mLoginDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, devicelogin);
				audit.mField = "login_date";
				audit.mOldValue = deviceloginOld.mLoginDate.ToString();
				audit.mNewValue = devicelogin.mLoginDate.ToString();
				audit_collection.Add(audit);
			}

			if (devicelogin.mStatus != deviceloginOld.mStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, devicelogin);
				audit.mField = "status";
				audit.mOldValue = deviceloginOld.mStatus;
				audit.mNewValue = devicelogin.mStatus;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, DeviceLogin devicelogin)
		{
			audit.mUserFullName = devicelogin.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_DeviceLogin);
			audit.mRowId = devicelogin.mId;
			audit.mAction = 2;
		}
	}
}