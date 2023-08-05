using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class LmmEntryAudit
	{
		public static AuditCollection Audit(LmmEntry lmmentry,LmmEntry lmmentryOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (lmmentry.mType != lmmentryOld.mType)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmentry);
				audit.mField = "type";
				audit.mOldValue = lmmentryOld.mType;
				audit.mNewValue = lmmentry.mType;
				audit_collection.Add(audit);
			}

			if (lmmentry.mLmmId != lmmentryOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmentry);
				audit.mField = "lmm_id";
				audit.mOldValue = lmmentryOld.mLmmId.ToString();
				audit.mNewValue = lmmentry.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmentry.mWithEntry != lmmentryOld.mWithEntry)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmentry);
				audit.mField = "with_entry";
				audit.mOldValue = lmmentryOld.mWithEntry.ToString();
				audit.mNewValue = lmmentry.mWithEntry.ToString();
				audit_collection.Add(audit);
			}

			if (lmmentry.mTotalItem != lmmentryOld.mTotalItem)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmentry);
				audit.mField = "total_item";
				audit.mOldValue = lmmentryOld.mTotalItem.ToString();
				audit.mNewValue = lmmentry.mTotalItem.ToString();
				audit_collection.Add(audit);
			}

			if (lmmentry.mLmmName != lmmentryOld.mLmmName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmentry);
				audit.mField = "lmm_name";
				audit.mOldValue = lmmentryOld.mLmmName;
				audit.mNewValue = lmmentry.mLmmName;
				audit_collection.Add(audit);
			}

			if (lmmentry.mSalesDate != lmmentryOld.mSalesDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmentry);
				audit.mField = "sales_date";
				audit.mOldValue = lmmentryOld.mSalesDate.ToString();
				audit.mNewValue = lmmentry.mSalesDate.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, LmmEntry lmmentry)
		{
			audit.mUserFullName = lmmentry.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_LmmEntry);
			audit.mRowId = lmmentry.mId;
			audit.mAction = 2;
		}
	}
}