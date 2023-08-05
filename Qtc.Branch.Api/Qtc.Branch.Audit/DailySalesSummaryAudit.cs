using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DailySalesSummaryAudit
	{
		public static AuditCollection Audit(DailySalesSummary dailysalessummary,DailySalesSummary dailysalessummaryOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (dailysalessummary.mInventoryDate != dailysalessummaryOld.mInventoryDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dailysalessummary);
				audit.mField = "inventory_date";
				audit.mOldValue = dailysalessummaryOld.mInventoryDate.ToString();
				audit.mNewValue = dailysalessummary.mInventoryDate.ToString();
				audit_collection.Add(audit);
			}

			if (dailysalessummary.mBranchId != dailysalessummaryOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dailysalessummary);
				audit.mField = "branch_id";
				audit.mOldValue = dailysalessummaryOld.mBranchId.ToString();
				audit.mNewValue = dailysalessummary.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (dailysalessummary.mUserId != dailysalessummaryOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dailysalessummary);
				audit.mField = "user_id";
				audit.mOldValue = dailysalessummaryOld.mUserId.ToString();
				audit.mNewValue = dailysalessummary.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (dailysalessummary.mCashExplanation != dailysalessummaryOld.mCashExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dailysalessummary);
				audit.mField = "cash_explanation";
				audit.mOldValue = dailysalessummaryOld.mCashExplanation;
				audit.mNewValue = dailysalessummary.mCashExplanation;
				audit_collection.Add(audit);
			}

			if (dailysalessummary.mInventoryExplanation != dailysalessummaryOld.mInventoryExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dailysalessummary);
				audit.mField = "inventory_explanation";
				audit.mOldValue = dailysalessummaryOld.mInventoryExplanation;
				audit.mNewValue = dailysalessummary.mInventoryExplanation;
				audit_collection.Add(audit);
			}

			//if (dailysalessummary.mSignature != dailysalessummaryOld.mSignature)
			//{
			//	audit = new BusinessEntities.Audit();
			//	LoadCommonData(ref audit, dailysalessummary);
			//	audit.mField = "signature";
			//	audit.mOldValue = dailysalessummaryOld.mSignature.ToString();
			//	audit.mNewValue = dailysalessummary.mSignature.ToString();
			//	audit_collection.Add(audit);
			//}

			if (dailysalessummary.mDatestamp != dailysalessummaryOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dailysalessummary);
				audit.mField = "datestamp";
				audit.mOldValue = dailysalessummaryOld.mDatestamp.ToString();
				audit.mNewValue = dailysalessummary.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, DailySalesSummary dailysalessummary)
		{
			audit.mUserFullName = dailysalessummary.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_DailySalesSummary);
			audit.mRowId = dailysalessummary.mId;
			audit.mAction = 2;
		}
	}
}