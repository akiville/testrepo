using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class PurposeAudit
	{
		public static AuditCollection Audit(Purpose purpose,Purpose purposeOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (purpose.mName != purposeOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, purpose);
				audit.mField = "name";
				audit.mOldValue = purposeOld.mName;
				audit.mNewValue = purpose.mName;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Purpose purpose)
		{
			audit.mUserFullName = purpose.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Purpose);
			audit.mRowId = purpose.mId;
			audit.mAction = 2;
		}
	}
}