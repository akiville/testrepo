using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingTasksAudit
	{
		public static AuditCollection Audit(RovingTasks rovingtasks,RovingTasks rovingtasksOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingtasks.mRecordId != rovingtasksOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingtasks);
				audit.mField = "record_id";
				audit.mOldValue = rovingtasksOld.mRecordId.ToString();
				audit.mNewValue = rovingtasks.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingtasks.mName != rovingtasksOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingtasks);
				audit.mField = "name";
				audit.mOldValue = rovingtasksOld.mName;
				audit.mNewValue = rovingtasks.mName;
				audit_collection.Add(audit);
			}

			if (rovingtasks.mRemarks != rovingtasksOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingtasks);
				audit.mField = "remarks";
				audit.mOldValue = rovingtasksOld.mRemarks;
				audit.mNewValue = rovingtasks.mRemarks;
				audit_collection.Add(audit);
			}

			if (rovingtasks.mCode != rovingtasksOld.mCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingtasks);
				audit.mField = "code";
				audit.mOldValue = rovingtasksOld.mCode;
				audit.mNewValue = rovingtasks.mCode;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingTasks rovingtasks)
		{
			audit.mUserFullName = rovingtasks.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingTasks);
			audit.mRowId = rovingtasks.mId;
			audit.mAction = 2;
		}
	}
}