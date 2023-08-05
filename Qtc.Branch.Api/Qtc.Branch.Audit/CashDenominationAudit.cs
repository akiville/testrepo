using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class CashDenominationAudit
	{
		public static AuditCollection Audit(CashDenomination cashdenomination,CashDenomination cashdenominationOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (cashdenomination.mName != cashdenominationOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, cashdenomination);
				audit.mField = "name";
				audit.mOldValue = cashdenominationOld.mName;
				audit.mNewValue = cashdenomination.mName;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, CashDenomination cashdenomination)
		{
			audit.mUserFullName = cashdenomination.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_CashDenomination);
			audit.mRowId = cashdenomination.mId;
			audit.mAction = 2;
		}
	}
}