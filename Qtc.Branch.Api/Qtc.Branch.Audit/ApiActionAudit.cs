using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class ApiActionAudit
	{
		public static AuditCollection Audit(ApiAction apiaction,ApiAction apiactionOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (apiaction.mModuleName != apiactionOld.mModuleName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiaction);
				audit.mField = "module_name";
				audit.mOldValue = apiactionOld.mModuleName;
				audit.mNewValue = apiaction.mModuleName;
				audit_collection.Add(audit);
			}

			if (apiaction.mRecordId != apiactionOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiaction);
				audit.mField = "record_id";
				audit.mOldValue = apiactionOld.mRecordId.ToString();
				audit.mNewValue = apiaction.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (apiaction.mBranchId != apiactionOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiaction);
				audit.mField = "branch_id";
				audit.mOldValue = apiactionOld.mBranchId.ToString();
				audit.mNewValue = apiaction.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (apiaction.mActionTaken != apiactionOld.mActionTaken)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiaction);
				audit.mField = "action_taken";
				audit.mOldValue = apiactionOld.mActionTaken.ToString();
				audit.mNewValue = apiaction.mActionTaken.ToString();
				audit_collection.Add(audit);
			}

			if (apiaction.mActionDate != apiactionOld.mActionDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiaction);
				audit.mField = "action_date";
				audit.mOldValue = apiactionOld.mActionDate.ToString();
				audit.mNewValue = apiaction.mActionDate.ToString();
				audit_collection.Add(audit);
			}

			if (apiaction.mDatestamp != apiactionOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiaction);
				audit.mField = "datestamp";
				audit.mOldValue = apiactionOld.mDatestamp.ToString();
				audit.mNewValue = apiaction.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			if (apiaction.mUserId != apiactionOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, apiaction);
				audit.mField = "user_id";
				audit.mOldValue = apiactionOld.mUserId.ToString();
				audit.mNewValue = apiaction.mUserId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, ApiAction apiaction)
		{
			audit.mUserFullName = apiaction.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_ApiAction);
			audit.mRowId = apiaction.mId;
			audit.mAction = 2;
		}
	}
}