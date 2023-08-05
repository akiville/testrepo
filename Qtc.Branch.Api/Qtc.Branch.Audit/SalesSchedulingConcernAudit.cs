using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class SalesSchedulingConcernAudit
	{
		public static AuditCollection Audit(SalesSchedulingConcern salesschedulingconcern,SalesSchedulingConcern salesschedulingconcernOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (salesschedulingconcern.mSalesSchedulingId != salesschedulingconcernOld.mSalesSchedulingId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconcern);
				audit.mField = "sales_scheduling_id";
				audit.mOldValue = salesschedulingconcernOld.mSalesSchedulingId.ToString();
				audit.mNewValue = salesschedulingconcern.mSalesSchedulingId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingconcern.mBranchId != salesschedulingconcernOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconcern);
				audit.mField = "branch_id";
				audit.mOldValue = salesschedulingconcernOld.mBranchId.ToString();
				audit.mNewValue = salesschedulingconcern.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingconcern.mLmmId != salesschedulingconcernOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconcern);
				audit.mField = "lmm_id";
				audit.mOldValue = salesschedulingconcernOld.mLmmId.ToString();
				audit.mNewValue = salesschedulingconcern.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingconcern.mConcern != salesschedulingconcernOld.mConcern)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconcern);
				audit.mField = "concern";
				audit.mOldValue = salesschedulingconcernOld.mConcern;
				audit.mNewValue = salesschedulingconcern.mConcern;
				audit_collection.Add(audit);
			}

			if (salesschedulingconcern.mStatus != salesschedulingconcernOld.mStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconcern);
				audit.mField = "status";
				audit.mOldValue = salesschedulingconcernOld.mStatus;
				audit.mNewValue = salesschedulingconcern.mStatus;
				audit_collection.Add(audit);
			}

			if (salesschedulingconcern.mDateSubmitted != salesschedulingconcernOld.mDateSubmitted)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconcern);
				audit.mField = "date_submitted";
				audit.mOldValue = salesschedulingconcernOld.mDateSubmitted.ToString();
				audit.mNewValue = salesschedulingconcern.mDateSubmitted.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingconcern.mCheckedBy != salesschedulingconcernOld.mCheckedBy)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconcern);
				audit.mField = "checked_by";
				audit.mOldValue = salesschedulingconcernOld.mCheckedBy.ToString();
				audit.mNewValue = salesschedulingconcern.mCheckedBy.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingconcern.mCheckedDate != salesschedulingconcernOld.mCheckedDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconcern);
				audit.mField = "checked_date";
				audit.mOldValue = salesschedulingconcernOld.mCheckedDate.ToString();
				audit.mNewValue = salesschedulingconcern.mCheckedDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingconcern.mDatestamp != salesschedulingconcernOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconcern);
				audit.mField = "datestamp";
				audit.mOldValue = salesschedulingconcernOld.mDatestamp.ToString();
				audit.mNewValue = salesschedulingconcern.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, SalesSchedulingConcern salesschedulingconcern)
		{
			audit.mUserFullName = salesschedulingconcern.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_SalesSchedulingConcern);
			audit.mRowId = salesschedulingconcern.mId;
			audit.mAction = 2;
		}
	}
}