using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DeviceConfigurationAudit
	{
		public static AuditCollection Audit(DeviceConfiguration deviceconfiguration,DeviceConfiguration deviceconfigurationOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (deviceconfiguration.mVersion != deviceconfigurationOld.mVersion)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deviceconfiguration);
				audit.mField = "version";
				audit.mOldValue = deviceconfigurationOld.mVersion.ToString();
				audit.mNewValue = deviceconfiguration.mVersion.ToString();
				audit_collection.Add(audit);
			}

			if (deviceconfiguration.mWelcomeMessage != deviceconfigurationOld.mWelcomeMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deviceconfiguration);
				audit.mField = "welcome_message";
				audit.mOldValue = deviceconfigurationOld.mWelcomeMessage;
				audit.mNewValue = deviceconfiguration.mWelcomeMessage;
				audit_collection.Add(audit);
			}

			if (deviceconfiguration.mDatestamp != deviceconfigurationOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deviceconfiguration);
				audit.mField = "datestamp";
				audit.mOldValue = deviceconfigurationOld.mDatestamp.ToString();
				audit.mNewValue = deviceconfiguration.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			if (deviceconfiguration.mWeCareMessage != deviceconfigurationOld.mWeCareMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deviceconfiguration);
				audit.mField = "we_care_message";
				audit.mOldValue = deviceconfigurationOld.mWeCareMessage;
				audit.mNewValue = deviceconfiguration.mWeCareMessage;
				audit_collection.Add(audit);
			}

			if (deviceconfiguration.mWeAchieveMessage != deviceconfigurationOld.mWeAchieveMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deviceconfiguration);
				audit.mField = "we_achieve_message";
				audit.mOldValue = deviceconfigurationOld.mWeAchieveMessage;
				audit.mNewValue = deviceconfiguration.mWeAchieveMessage;
				audit_collection.Add(audit);
			}

			if (deviceconfiguration.mWeEnsureMessage != deviceconfigurationOld.mWeEnsureMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deviceconfiguration);
				audit.mField = "we_ensure_message";
				audit.mOldValue = deviceconfigurationOld.mWeEnsureMessage;
				audit.mNewValue = deviceconfiguration.mWeEnsureMessage;
				audit_collection.Add(audit);
			}

			if (deviceconfiguration.mWeImproveMessage != deviceconfigurationOld.mWeImproveMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deviceconfiguration);
				audit.mField = "we_improve_message";
				audit.mOldValue = deviceconfigurationOld.mWeImproveMessage;
				audit.mNewValue = deviceconfiguration.mWeImproveMessage;
				audit_collection.Add(audit);
			}

			if (deviceconfiguration.mWeProvideMessage != deviceconfigurationOld.mWeProvideMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deviceconfiguration);
				audit.mField = "we_provide_message";
				audit.mOldValue = deviceconfigurationOld.mWeProvideMessage;
				audit.mNewValue = deviceconfiguration.mWeProvideMessage;
				audit_collection.Add(audit);
			}

			if (deviceconfiguration.mWeAlignMessage != deviceconfigurationOld.mWeAlignMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, deviceconfiguration);
				audit.mField = "we_align_message";
				audit.mOldValue = deviceconfigurationOld.mWeAlignMessage;
				audit.mNewValue = deviceconfiguration.mWeAlignMessage;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, DeviceConfiguration deviceconfiguration)
		{
			audit.mUserFullName = deviceconfiguration.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_DeviceConfiguration);
			audit.mRowId = deviceconfiguration.mId;
			audit.mAction = 2;
		}
	}
}