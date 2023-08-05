using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingPlanScheduledActualAudit
	{
		public static AuditCollection Audit(RovingPlanScheduledActual rovingplanscheduledactual,RovingPlanScheduledActual rovingplanscheduledactualOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingplanscheduledactual.mDateCreated != rovingplanscheduledactualOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "date_created";
				audit.mOldValue = rovingplanscheduledactualOld.mDateCreated.ToString();
				audit.mNewValue = rovingplanscheduledactual.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mRpsId != rovingplanscheduledactualOld.mRpsId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "rps_id";
				audit.mOldValue = rovingplanscheduledactualOld.mRpsId.ToString();
				audit.mNewValue = rovingplanscheduledactual.mRpsId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mUserId != rovingplanscheduledactualOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "user_id";
				audit.mOldValue = rovingplanscheduledactualOld.mUserId.ToString();
				audit.mNewValue = rovingplanscheduledactual.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mRovingTaskId != rovingplanscheduledactualOld.mRovingTaskId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "roving_task_id";
				audit.mOldValue = rovingplanscheduledactualOld.mRovingTaskId.ToString();
				audit.mNewValue = rovingplanscheduledactual.mRovingTaskId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mMcOnDutyId != rovingplanscheduledactualOld.mMcOnDutyId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "mc_on_duty_id";
				audit.mOldValue = rovingplanscheduledactualOld.mMcOnDutyId.ToString();
				audit.mNewValue = rovingplanscheduledactual.mMcOnDutyId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mTimeIn != rovingplanscheduledactualOld.mTimeIn)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "time_in";
				audit.mOldValue = rovingplanscheduledactualOld.mTimeIn.ToString();
				audit.mNewValue = rovingplanscheduledactual.mTimeIn.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mTimeOut != rovingplanscheduledactualOld.mTimeOut)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "time_out";
				audit.mOldValue = rovingplanscheduledactualOld.mTimeOut.ToString();
				audit.mNewValue = rovingplanscheduledactual.mTimeOut.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mLateExplain != rovingplanscheduledactualOld.mLateExplain)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "late_explain";
				audit.mOldValue = rovingplanscheduledactualOld.mLateExplain;
				audit.mNewValue = rovingplanscheduledactual.mLateExplain;
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mRemarks != rovingplanscheduledactualOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "remarks";
				audit.mOldValue = rovingplanscheduledactualOld.mRemarks;
				audit.mNewValue = rovingplanscheduledactual.mRemarks;
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mNovId != rovingplanscheduledactualOld.mNovId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "nov_id";
				audit.mOldValue = rovingplanscheduledactualOld.mNovId.ToString();
				audit.mNewValue = rovingplanscheduledactual.mNovId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mNovNo != rovingplanscheduledactualOld.mNovNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "nov_no";
				audit.mOldValue = rovingplanscheduledactualOld.mNovNo;
				audit.mNewValue = rovingplanscheduledactual.mNovNo;
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mRfdId != rovingplanscheduledactualOld.mRfdId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "rfd_id";
				audit.mOldValue = rovingplanscheduledactualOld.mRfdId.ToString();
				audit.mNewValue = rovingplanscheduledactual.mRfdId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mRfdNo != rovingplanscheduledactualOld.mRfdNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "rfd_no";
				audit.mOldValue = rovingplanscheduledactualOld.mRfdNo;
				audit.mNewValue = rovingplanscheduledactual.mRfdNo;
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mRpId != rovingplanscheduledactualOld.mRpId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "rp_id";
				audit.mOldValue = rovingplanscheduledactualOld.mRpId.ToString();
				audit.mNewValue = rovingplanscheduledactual.mRpId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mRpNo != rovingplanscheduledactualOld.mRpNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "rp_no";
				audit.mOldValue = rovingplanscheduledactualOld.mRpNo;
				audit.mNewValue = rovingplanscheduledactual.mRpNo;
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mRmId != rovingplanscheduledactualOld.mRmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "rm_id";
				audit.mOldValue = rovingplanscheduledactualOld.mRmId.ToString();
				audit.mNewValue = rovingplanscheduledactual.mRmId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mRmNo != rovingplanscheduledactualOld.mRmNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "rm_no";
				audit.mOldValue = rovingplanscheduledactualOld.mRmNo;
				audit.mNewValue = rovingplanscheduledactual.mRmNo;
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mNoDeduction != rovingplanscheduledactualOld.mNoDeduction)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "no_deduction";
				audit.mOldValue = rovingplanscheduledactualOld.mNoDeduction.ToString();
				audit.mNewValue = rovingplanscheduledactual.mNoDeduction.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactual.mRecordId != rovingplanscheduledactualOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactual);
				audit.mField = "record_id";
				audit.mOldValue = rovingplanscheduledactualOld.mRecordId.ToString();
				audit.mNewValue = rovingplanscheduledactual.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingPlanScheduledActual rovingplanscheduledactual)
		{
			audit.mUserFullName = rovingplanscheduledactual.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingPlanScheduledActual);
			audit.mRowId = rovingplanscheduledactual.mId;
			audit.mAction = 2;
		}
	}
}