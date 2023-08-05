using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class ApiVersionAudit
	{
		public static AuditCollection Audit(ApiVersion apiversion,ApiVersion apiversionOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (apiversion.mVersionCode != apiversionOld.mVersionCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiversion);
				audit.mField = "version_code";
				audit.mOldValue = apiversionOld.mVersionCode.ToString();
				audit.mNewValue = apiversion.mVersionCode.ToString();
				audit_collection.Add(audit);
			}

			if (apiversion.mUrl != apiversionOld.mUrl)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiversion);
				audit.mField = "url";
				audit.mOldValue = apiversionOld.mUrl;
				audit.mNewValue = apiversion.mUrl;
				audit_collection.Add(audit);
			}

			if (apiversion.mUpdateMessage != apiversionOld.mUpdateMessage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiversion);
				audit.mField = "update_message";
				audit.mOldValue = apiversionOld.mUpdateMessage;
				audit.mNewValue = apiversion.mUpdateMessage;
				audit_collection.Add(audit);
			}

			if (apiversion.mUserId != apiversionOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiversion);
				audit.mField = "user_id";
				audit.mOldValue = apiversionOld.mUserId.ToString();
				audit.mNewValue = apiversion.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (apiversion.mDatestamp != apiversionOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiversion);
				audit.mField = "datestamp";
				audit.mOldValue = apiversionOld.mDatestamp.ToString();
				audit.mNewValue = apiversion.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, ApiVersion apiversion)
		{
			audit.mUserFullName = apiversion.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_ApiVersion);
			audit.mRowId = apiversion.mId;
			audit.mAction = 2;
		}
	}
}