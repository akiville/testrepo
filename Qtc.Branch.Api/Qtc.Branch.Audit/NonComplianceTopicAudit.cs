using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class NonComplianceTopicAudit
	{
		public static AuditCollection Audit(NonComplianceTopic noncompliancetopic,NonComplianceTopic noncompliancetopicOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (noncompliancetopic.mName != noncompliancetopicOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, noncompliancetopic);
				audit.mField = "name";
				audit.mOldValue = noncompliancetopicOld.mName;
				audit.mNewValue = noncompliancetopic.mName;
				audit_collection.Add(audit);
			}

			if (noncompliancetopic.mRemarks != noncompliancetopicOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, noncompliancetopic);
				audit.mField = "remarks";
				audit.mOldValue = noncompliancetopicOld.mRemarks;
				audit.mNewValue = noncompliancetopic.mRemarks;
				audit_collection.Add(audit);
			}

			if (noncompliancetopic.mCode != noncompliancetopicOld.mCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, noncompliancetopic);
				audit.mField = "code";
				audit.mOldValue = noncompliancetopicOld.mCode;
				audit.mNewValue = noncompliancetopic.mCode;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, NonComplianceTopic noncompliancetopic)
		{
			audit.mUserFullName = noncompliancetopic.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_NonComplianceTopic);
			audit.mRowId = noncompliancetopic.mId;
			audit.mAction = 2;
		}
	}
}