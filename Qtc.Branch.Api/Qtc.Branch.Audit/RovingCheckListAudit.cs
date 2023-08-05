using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingCheckListAudit
	{
		public static AuditCollection Audit(RovingCheckList rovingchecklist,RovingCheckList rovingchecklistOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingchecklist.mRecordId != rovingchecklistOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklist);
				audit.mField = "record_id";
				audit.mOldValue = rovingchecklistOld.mRecordId.ToString();
				audit.mNewValue = rovingchecklist.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingchecklist.mChecklistCategoryId != rovingchecklistOld.mChecklistCategoryId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklist);
				audit.mField = "checklist_category_id";
				audit.mOldValue = rovingchecklistOld.mChecklistCategoryId.ToString();
				audit.mNewValue = rovingchecklist.mChecklistCategoryId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingchecklist.mName != rovingchecklistOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklist);
				audit.mField = "name";
				audit.mOldValue = rovingchecklistOld.mName;
				audit.mNewValue = rovingchecklist.mName;
				audit_collection.Add(audit);
			}

			if (rovingchecklist.mRemarks != rovingchecklistOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingchecklist);
				audit.mField = "remarks";
				audit.mOldValue = rovingchecklistOld.mRemarks;
				audit.mNewValue = rovingchecklist.mRemarks;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingCheckList rovingchecklist)
		{
			audit.mUserFullName = rovingchecklist.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingCheckList);
			audit.mRowId = rovingchecklist.mId;
			audit.mAction = 2;
		}
	}
}