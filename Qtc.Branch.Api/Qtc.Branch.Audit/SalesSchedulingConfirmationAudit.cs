using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class SalesSchedulingConfirmationAudit
	{
		public static AuditCollection Audit(SalesSchedulingConfirmation salesschedulingconfirmation,SalesSchedulingConfirmation salesschedulingconfirmationOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (salesschedulingconfirmation.mStartDate != salesschedulingconfirmationOld.mStartDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconfirmation);
				audit.mField = "start_date";
				audit.mOldValue = salesschedulingconfirmationOld.mStartDate.ToString();
				audit.mNewValue = salesschedulingconfirmation.mStartDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingconfirmation.mEndDate != salesschedulingconfirmationOld.mEndDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconfirmation);
				audit.mField = "end_date";
				audit.mOldValue = salesschedulingconfirmationOld.mEndDate.ToString();
				audit.mNewValue = salesschedulingconfirmation.mEndDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingconfirmation.mLmmId != salesschedulingconfirmationOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconfirmation);
				audit.mField = "lmm_id";
				audit.mOldValue = salesschedulingconfirmationOld.mLmmId.ToString();
				audit.mNewValue = salesschedulingconfirmation.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingconfirmation.mRemarks != salesschedulingconfirmationOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconfirmation);
				audit.mField = "remarks";
				audit.mOldValue = salesschedulingconfirmationOld.mRemarks;
				audit.mNewValue = salesschedulingconfirmation.mRemarks;
				audit_collection.Add(audit);
			}

			if (salesschedulingconfirmation.mDatestamp != salesschedulingconfirmationOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingconfirmation);
				audit.mField = "datestamp";
				audit.mOldValue = salesschedulingconfirmationOld.mDatestamp.ToString();
				audit.mNewValue = salesschedulingconfirmation.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, SalesSchedulingConfirmation salesschedulingconfirmation)
		{
			audit.mUserFullName = salesschedulingconfirmation.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_SalesSchedulingConfirmation);
			audit.mRowId = salesschedulingconfirmation.mId;
			audit.mAction = 2;
		}
	}
}