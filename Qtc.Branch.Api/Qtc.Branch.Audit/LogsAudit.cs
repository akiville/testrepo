using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class LogsAudit
	{
		public static AuditCollection Audit(Logs logs,Logs logsOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (logs.mModuleName != logsOld.mModuleName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, logs);
				audit.mField = "module_name";
				audit.mOldValue = logsOld.mModuleName;
				audit.mNewValue = logs.mModuleName;
				audit_collection.Add(audit);
			}

			if (logs.mEmployeeId != logsOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, logs);
				audit.mField = "employee_id";
				audit.mOldValue = logsOld.mEmployeeId.ToString();
				audit.mNewValue = logs.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (logs.mDeviceSerial != logsOld.mDeviceSerial)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, logs);
				audit.mField = "device_serial";
				audit.mOldValue = logsOld.mDeviceSerial;
				audit.mNewValue = logs.mDeviceSerial;
				audit_collection.Add(audit);
			}

			if (logs.mAction != logsOld.mAction)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, logs);
				audit.mField = "action";
				audit.mOldValue = logsOld.mAction;
				audit.mNewValue = logs.mAction;
				audit_collection.Add(audit);
			}

			if (logs.mDesription != logsOld.mDesription)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, logs);
				audit.mField = "desription";
				audit.mOldValue = logsOld.mDesription;
				audit.mNewValue = logs.mDesription;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Logs logs)
		{
			audit.mUserFullName = logs.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Logs);
			audit.mRowId = logs.mId;
			audit.mAction = 2;
		}
	}
}