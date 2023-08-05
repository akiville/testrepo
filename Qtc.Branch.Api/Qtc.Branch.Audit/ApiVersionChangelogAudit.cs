using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class ApiVersionChangelogAudit
	{
		public static AuditCollection Audit(ApiVersionChangelog apiversionchangelog,ApiVersionChangelog apiversionchangelogOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (apiversionchangelog.mVersionId != apiversionchangelogOld.mVersionId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiversionchangelog);
				audit.mField = "version_id";
				audit.mOldValue = apiversionchangelogOld.mVersionId.ToString();
				audit.mNewValue = apiversionchangelog.mVersionId.ToString();
				audit_collection.Add(audit);
			}

			if (apiversionchangelog.mChangelog != apiversionchangelogOld.mChangelog)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiversionchangelog);
				audit.mField = "changelog";
				audit.mOldValue = apiversionchangelogOld.mChangelog;
				audit.mNewValue = apiversionchangelog.mChangelog;
				audit_collection.Add(audit);
			}

			if (apiversionchangelog.mDateCreated != apiversionchangelogOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiversionchangelog);
				audit.mField = "date_created";
				audit.mOldValue = apiversionchangelogOld.mDateCreated.ToString();
				audit.mNewValue = apiversionchangelog.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (apiversionchangelog.mStatus != apiversionchangelogOld.mStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiversionchangelog);
				audit.mField = "status";
				audit.mOldValue = apiversionchangelogOld.mStatus;
				audit.mNewValue = apiversionchangelog.mStatus;
				audit_collection.Add(audit);
			}

			if (apiversionchangelog.mDatestamp != apiversionchangelogOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiversionchangelog);
				audit.mField = "datestamp";
				audit.mOldValue = apiversionchangelogOld.mDatestamp.ToString();
				audit.mNewValue = apiversionchangelog.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, ApiVersionChangelog apiversionchangelog)
		{
			audit.mUserFullName = apiversionchangelog.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_ApiVersionChangelog);
			audit.mRowId = apiversionchangelog.mId;
			audit.mAction = 2;
		}
	}
}