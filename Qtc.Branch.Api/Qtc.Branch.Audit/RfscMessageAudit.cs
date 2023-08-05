using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RfscMessageAudit
	{
		public static AuditCollection Audit(RfscMessage rfscmessage,RfscMessage rfscmessageOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rfscmessage.mRfscId != rfscmessageOld.mRfscId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfscmessage);
				audit.mField = "rfsc_id";
				audit.mOldValue = rfscmessageOld.mRfscId.ToString();
				audit.mNewValue = rfscmessage.mRfscId.ToString();
				audit_collection.Add(audit);
			}

			if (rfscmessage.mLmmId != rfscmessageOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfscmessage);
				audit.mField = "lmm_id";
				audit.mOldValue = rfscmessageOld.mLmmId.ToString();
				audit.mNewValue = rfscmessage.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (rfscmessage.mBranchId != rfscmessageOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfscmessage);
				audit.mField = "branch_id";
				audit.mOldValue = rfscmessageOld.mBranchId.ToString();
				audit.mNewValue = rfscmessage.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (rfscmessage.mStartDate != rfscmessageOld.mStartDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfscmessage);
				audit.mField = "start_date";
				audit.mOldValue = rfscmessageOld.mStartDate.ToString();
				audit.mNewValue = rfscmessage.mStartDate.ToString();
				audit_collection.Add(audit);
			}

			if (rfscmessage.mEndDate != rfscmessageOld.mEndDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfscmessage);
				audit.mField = "end_date";
				audit.mOldValue = rfscmessageOld.mEndDate.ToString();
				audit.mNewValue = rfscmessage.mEndDate.ToString();
				audit_collection.Add(audit);
			}

			if (rfscmessage.mToLmmId != rfscmessageOld.mToLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfscmessage);
				audit.mField = "to_lmm_id";
				audit.mOldValue = rfscmessageOld.mToLmmId.ToString();
				audit.mNewValue = rfscmessage.mToLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (rfscmessage.mStatus != rfscmessageOld.mStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfscmessage);
				audit.mField = "status";
				audit.mOldValue = rfscmessageOld.mStatus;
				audit.mNewValue = rfscmessage.mStatus;
				audit_collection.Add(audit);
			}

			if (rfscmessage.mPersonnelId != rfscmessageOld.mPersonnelId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfscmessage);
				audit.mField = "personnel_id";
				audit.mOldValue = rfscmessageOld.mPersonnelId.ToString();
				audit.mNewValue = rfscmessage.mPersonnelId.ToString();
				audit_collection.Add(audit);
			}

			if (rfscmessage.mPersonnelName != rfscmessageOld.mPersonnelName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfscmessage);
				audit.mField = "personnel_name";
				audit.mOldValue = rfscmessageOld.mPersonnelName;
				audit.mNewValue = rfscmessage.mPersonnelName;
				audit_collection.Add(audit);
			}

			if (rfscmessage.mDatetime != rfscmessageOld.mDatetime)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfscmessage);
				audit.mField = "datetime";
				audit.mOldValue = rfscmessageOld.mDatetime.ToString();
				audit.mNewValue = rfscmessage.mDatetime.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RfscMessage rfscmessage)
		{
			audit.mUserFullName = rfscmessage.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RfscMessage);
			audit.mRowId = rfscmessage.mId;
			audit.mAction = 2;
		}
	}
}