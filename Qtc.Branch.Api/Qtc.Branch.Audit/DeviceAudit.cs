using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DeviceAudit
	{
		public static AuditCollection Audit(Device device,Device deviceOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (device.mDeviceSerialNo != deviceOld.mDeviceSerialNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, device);
				audit.mField = "device_serial_no";
				audit.mOldValue = deviceOld.mDeviceSerialNo;
				audit.mNewValue = device.mDeviceSerialNo;
				audit_collection.Add(audit);
			}

			if (device.mStatus != deviceOld.mStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, device);
				audit.mField = "status";
				audit.mOldValue = deviceOld.mStatus;
				audit.mNewValue = device.mStatus;
				audit_collection.Add(audit);
			}

			if (device.mLastCoordinates != deviceOld.mLastCoordinates)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, device);
				audit.mField = "last_coordinates";
				audit.mOldValue = deviceOld.mLastCoordinates;
				audit.mNewValue = device.mLastCoordinates;
				audit_collection.Add(audit);
			}

			if (device.mDatestamp != deviceOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, device);
				audit.mField = "datestamp";
				audit.mOldValue = deviceOld.mDatestamp.ToString();
				audit.mNewValue = device.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Device device)
		{
			audit.mUserFullName = device.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Device);
			audit.mRowId = device.mId;
			audit.mAction = 2;
		}
	}
}