using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingPlanScheduledActualChecklistAudit
	{
		public static AuditCollection Audit(RovingPlanScheduledActualChecklist rovingplanscheduledactualchecklist,RovingPlanScheduledActualChecklist rovingplanscheduledactualchecklistOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingplanscheduledactualchecklist.mRecordId != rovingplanscheduledactualchecklistOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklist);
				audit.mField = "record_id";
				audit.mOldValue = rovingplanscheduledactualchecklistOld.mRecordId.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklist.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklist.mDateCreated != rovingplanscheduledactualchecklistOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklist);
				audit.mField = "date_created";
				audit.mOldValue = rovingplanscheduledactualchecklistOld.mDateCreated.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklist.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklist.mRpsId != rovingplanscheduledactualchecklistOld.mRpsId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklist);
				audit.mField = "rps_id";
				audit.mOldValue = rovingplanscheduledactualchecklistOld.mRpsId.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklist.mRpsId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklist.mRcId != rovingplanscheduledactualchecklistOld.mRcId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklist);
				audit.mField = "rc_id";
				audit.mOldValue = rovingplanscheduledactualchecklistOld.mRcId.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklist.mRcId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklist.mIsCheck != rovingplanscheduledactualchecklistOld.mIsCheck)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklist);
				audit.mField = "is_check";
				audit.mOldValue = rovingplanscheduledactualchecklistOld.mIsCheck.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklist.mIsCheck.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklist.mRemarks != rovingplanscheduledactualchecklistOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklist);
				audit.mField = "remarks";
				audit.mOldValue = rovingplanscheduledactualchecklistOld.mRemarks;
				audit.mNewValue = rovingplanscheduledactualchecklist.mRemarks;
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklist.mLostSales != rovingplanscheduledactualchecklistOld.mLostSales)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklist);
				audit.mField = "lost_sales";
				audit.mOldValue = rovingplanscheduledactualchecklistOld.mLostSales.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklist.mLostSales.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklist.mWithViolation != rovingplanscheduledactualchecklistOld.mWithViolation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklist);
				audit.mField = "with_violation";
				audit.mOldValue = rovingplanscheduledactualchecklistOld.mWithViolation.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklist.mWithViolation.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklist.mActionTaken != rovingplanscheduledactualchecklistOld.mActionTaken)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklist);
				audit.mField = "action_taken";
				audit.mOldValue = rovingplanscheduledactualchecklistOld.mActionTaken;
				audit.mNewValue = rovingplanscheduledactualchecklist.mActionTaken;
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklist.mRpsaId != rovingplanscheduledactualchecklistOld.mRpsaId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklist);
				audit.mField = "rpsa_id";
				audit.mOldValue = rovingplanscheduledactualchecklistOld.mRpsaId.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklist.mRpsaId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingPlanScheduledActualChecklist rovingplanscheduledactualchecklist)
		{
			audit.mUserFullName = rovingplanscheduledactualchecklist.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingPlanScheduledActualChecklist);
			audit.mRowId = rovingplanscheduledactualchecklist.mId;
			audit.mAction = 2;
		}
	}
}