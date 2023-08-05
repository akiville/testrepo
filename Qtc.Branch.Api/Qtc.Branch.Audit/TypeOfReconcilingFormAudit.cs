using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class TypeOfReconcilingFormAudit
	{
		public static AuditCollection Audit(TypeOfReconcilingForm typeofreconcilingform,TypeOfReconcilingForm typeofreconcilingformOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (typeofreconcilingform.mName != typeofreconcilingformOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofreconcilingform);
				audit.mField = "name";
				audit.mOldValue = typeofreconcilingformOld.mName;
				audit.mNewValue = typeofreconcilingform.mName;
				audit_collection.Add(audit);
			}

			if (typeofreconcilingform.mRemarks != typeofreconcilingformOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofreconcilingform);
				audit.mField = "remarks";
				audit.mOldValue = typeofreconcilingformOld.mRemarks;
				audit.mNewValue = typeofreconcilingform.mRemarks;
				audit_collection.Add(audit);
			}

			if (typeofreconcilingform.mRequiredCheckPrevious != typeofreconcilingformOld.mRequiredCheckPrevious)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, typeofreconcilingform);
				audit.mField = "required_check_previous";
				audit.mOldValue = typeofreconcilingformOld.mRequiredCheckPrevious.ToString();
				audit.mNewValue = typeofreconcilingform.mRequiredCheckPrevious.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, TypeOfReconcilingForm typeofreconcilingform)
		{
			audit.mUserFullName = typeofreconcilingform.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_TypeOfReconcilingForm);
			audit.mRowId = typeofreconcilingform.mId;
			audit.mAction = 2;
		}
	}
}