using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class NovExplanationAudit
	{
		public static AuditCollection Audit(NovExplanation novexplanation,NovExplanation novexplanationOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (novexplanation.mRecordId != novexplanationOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, novexplanation);
				audit.mField = "record_id";
				audit.mOldValue = novexplanationOld.mRecordId.ToString();
				audit.mNewValue = novexplanation.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (novexplanation.mModuleName != novexplanationOld.mModuleName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, novexplanation);
				audit.mField = "module_name";
				audit.mOldValue = novexplanationOld.mModuleName;
				audit.mNewValue = novexplanation.mModuleName;
				audit_collection.Add(audit);
			}

			if (novexplanation.mExplanation != novexplanationOld.mExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, novexplanation);
				audit.mField = "explanation";
				audit.mOldValue = novexplanationOld.mExplanation;
				audit.mNewValue = novexplanation.mExplanation;
				audit_collection.Add(audit);
			}

			if (novexplanation.mEmployeeId != novexplanationOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, novexplanation);
				audit.mField = "employee_id";
				audit.mOldValue = novexplanationOld.mEmployeeId.ToString();
				audit.mNewValue = novexplanation.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (novexplanation.mDatestamp != novexplanationOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, novexplanation);
				audit.mField = "datestamp";
				audit.mOldValue = novexplanationOld.mDatestamp.ToString();
				audit.mNewValue = novexplanation.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, NovExplanation novexplanation)
		{
			audit.mUserFullName = novexplanation.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_NovExplanation);
			audit.mRowId = novexplanation.mId;
			audit.mAction = 2;
		}
	}
}