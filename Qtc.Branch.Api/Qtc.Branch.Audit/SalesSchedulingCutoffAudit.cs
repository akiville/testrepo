using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class SalesSchedulingCutoffAudit
	{
		public static AuditCollection Audit(SalesSchedulingCutoff salesschedulingcutoff,SalesSchedulingCutoff salesschedulingcutoffOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (salesschedulingcutoff.mStartDate != salesschedulingcutoffOld.mStartDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcutoff);
				audit.mField = "start_date";
				audit.mOldValue = salesschedulingcutoffOld.mStartDate.ToString();
				audit.mNewValue = salesschedulingcutoff.mStartDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingcutoff.mEndDate != salesschedulingcutoffOld.mEndDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcutoff);
				audit.mField = "end_date";
				audit.mOldValue = salesschedulingcutoffOld.mEndDate.ToString();
				audit.mNewValue = salesschedulingcutoff.mEndDate.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingcutoff.mIsFinal != salesschedulingcutoffOld.mIsFinal)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcutoff);
				audit.mField = "is_final";
				audit.mOldValue = salesschedulingcutoffOld.mIsFinal.ToString();
				audit.mNewValue = salesschedulingcutoff.mIsFinal.ToString();
				audit_collection.Add(audit);
			}

			if (salesschedulingcutoff.mMirroredCutoffId != salesschedulingcutoffOld.mMirroredCutoffId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingcutoff);
				audit.mField = "mirrored_cutoff_id";
				audit.mOldValue = salesschedulingcutoffOld.mMirroredCutoffId.ToString();
				audit.mNewValue = salesschedulingcutoff.mMirroredCutoffId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, SalesSchedulingCutoff salesschedulingcutoff)
		{
			audit.mUserFullName = salesschedulingcutoff.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_SalesSchedulingCutoff);
			audit.mRowId = salesschedulingcutoff.mId;
			audit.mAction = 2;
		}
	}
}