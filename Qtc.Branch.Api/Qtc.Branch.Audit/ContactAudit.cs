using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class ContactAudit
	{
		public static AuditCollection Audit(Contact contact,Contact contactOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (contact.mContactNo != contactOld.mContactNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, contact);
				audit.mField = "contact_no";
				audit.mOldValue = contactOld.mContactNo;
				audit.mNewValue = contact.mContactNo;
				audit_collection.Add(audit);
			}

			if (contact.mTitle != contactOld.mTitle)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, contact);
				audit.mField = "title";
				audit.mOldValue = contactOld.mTitle;
				audit.mNewValue = contact.mTitle;
				audit_collection.Add(audit);
			}

			if (contact.mGroupName != contactOld.mGroupName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, contact);
				audit.mField = "group_name";
				audit.mOldValue = contactOld.mGroupName;
				audit.mNewValue = contact.mGroupName;
				audit_collection.Add(audit);
			}

			if (contact.mDatestamp != contactOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, contact);
				audit.mField = "datestamp";
				audit.mOldValue = contactOld.mDatestamp.ToString();
				audit.mNewValue = contact.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Contact contact)
		{
			audit.mUserFullName = contact.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Contact);
			audit.mRowId = contact.mId;
			audit.mAction = 2;
		}
	}
}