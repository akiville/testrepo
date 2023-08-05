using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RepairImageAudit
	{
		public static AuditCollection Audit(RepairImage repairimage,RepairImage repairimageOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (repairimage.mRequestRepairId != repairimageOld.mRequestRepairId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairimage);
				audit.mField = "request_repair_id";
				audit.mOldValue = repairimageOld.mRequestRepairId.ToString();
				audit.mNewValue = repairimage.mRequestRepairId.ToString();
				audit_collection.Add(audit);
			}

			if (repairimage.mName != repairimageOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairimage);
				audit.mField = "name";
				audit.mOldValue = repairimageOld.mName;
				audit.mNewValue = repairimage.mName;
				audit_collection.Add(audit);
			}

			if (repairimage.mImageFileName != repairimageOld.mImageFileName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairimage);
				audit.mField = "image_file_name";
				audit.mOldValue = repairimageOld.mImageFileName;
				audit.mNewValue = repairimage.mImageFileName;
				audit_collection.Add(audit);
			}

			if (repairimage.mDescription != repairimageOld.mDescription)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairimage);
				audit.mField = "description";
				audit.mOldValue = repairimageOld.mDescription;
				audit.mNewValue = repairimage.mDescription;
				audit_collection.Add(audit);
			}

			if (repairimage.mRecordId != repairimageOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairimage);
				audit.mField = "record_id";
				audit.mOldValue = repairimageOld.mRecordId.ToString();
				audit.mNewValue = repairimage.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RepairImage repairimage)
		{
			audit.mUserFullName = repairimage.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RepairImage);
			audit.mRowId = repairimage.mId;
			audit.mAction = 2;
		}
	}
}