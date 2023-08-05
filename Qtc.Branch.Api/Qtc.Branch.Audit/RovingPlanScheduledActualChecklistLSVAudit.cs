using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingPlanScheduledActualChecklistLSVAudit
	{
		public static AuditCollection Audit(RovingPlanScheduledActualChecklistLSV rovingplanscheduledactualchecklistlsv,RovingPlanScheduledActualChecklistLSV rovingplanscheduledactualchecklistlsvOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingplanscheduledactualchecklistlsv.mRecordId != rovingplanscheduledactualchecklistlsvOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistlsv);
				audit.mField = "record_id";
				audit.mOldValue = rovingplanscheduledactualchecklistlsvOld.mRecordId.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklistlsv.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklistlsv.mRpsId != rovingplanscheduledactualchecklistlsvOld.mRpsId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistlsv);
				audit.mField = "rps_id";
				audit.mOldValue = rovingplanscheduledactualchecklistlsvOld.mRpsId.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklistlsv.mRpsId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklistlsv.mRpsChklistId != rovingplanscheduledactualchecklistlsvOld.mRpsChklistId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistlsv);
				audit.mField = "rps_chklist_id";
				audit.mOldValue = rovingplanscheduledactualchecklistlsvOld.mRpsChklistId.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklistlsv.mRpsChklistId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklistlsv.mViolationId != rovingplanscheduledactualchecklistlsvOld.mViolationId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistlsv);
				audit.mField = "violation_id";
				audit.mOldValue = rovingplanscheduledactualchecklistlsvOld.mViolationId.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklistlsv.mViolationId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklistlsv.mWithViolation != rovingplanscheduledactualchecklistlsvOld.mWithViolation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistlsv);
				audit.mField = "with_violation";
				audit.mOldValue = rovingplanscheduledactualchecklistlsvOld.mWithViolation.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklistlsv.mWithViolation.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklistlsv.mLostSales != rovingplanscheduledactualchecklistlsvOld.mLostSales)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistlsv);
				audit.mField = "lost_sales";
				audit.mOldValue = rovingplanscheduledactualchecklistlsvOld.mLostSales.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklistlsv.mLostSales.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklistlsv.mRemarks != rovingplanscheduledactualchecklistlsvOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistlsv);
				audit.mField = "remarks";
				audit.mOldValue = rovingplanscheduledactualchecklistlsvOld.mRemarks;
				audit.mNewValue = rovingplanscheduledactualchecklistlsv.mRemarks;
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklistlsv.mExplanation != rovingplanscheduledactualchecklistlsvOld.mExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistlsv);
				audit.mField = "explanation";
				audit.mOldValue = rovingplanscheduledactualchecklistlsvOld.mExplanation;
				audit.mNewValue = rovingplanscheduledactualchecklistlsv.mExplanation;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingPlanScheduledActualChecklistLSV rovingplanscheduledactualchecklistlsv)
		{
			audit.mUserFullName = rovingplanscheduledactualchecklistlsv.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingPlanScheduledActualChecklistLSV);
			audit.mRowId = rovingplanscheduledactualchecklistlsv.mId;
			audit.mAction = 2;
		}
	}
}