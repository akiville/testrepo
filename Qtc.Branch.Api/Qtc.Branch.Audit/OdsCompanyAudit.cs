using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class OdsCompanyAudit
	{
		public static AuditCollection Audit(OdsCompany odscompany,OdsCompany odscompanyOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (odscompany.mRecordId != odscompanyOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, odscompany);
				audit.mField = "record_id";
				audit.mOldValue = odscompanyOld.mRecordId.ToString();
				audit.mNewValue = odscompany.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (odscompany.mCode != odscompanyOld.mCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, odscompany);
				audit.mField = "code";
				audit.mOldValue = odscompanyOld.mCode;
				audit.mNewValue = odscompany.mCode;
				audit_collection.Add(audit);
			}

			if (odscompany.mName != odscompanyOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, odscompany);
				audit.mField = "name";
				audit.mOldValue = odscompanyOld.mName;
				audit.mNewValue = odscompany.mName;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, OdsCompany odscompany)
		{
			audit.mUserFullName = odscompany.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_OdsCompany);
			audit.mRowId = odscompany.mId;
			audit.mAction = 2;
		}
	}
}