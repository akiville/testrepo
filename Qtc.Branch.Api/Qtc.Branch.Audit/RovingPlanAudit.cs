using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingPlanAudit
	{
		public static AuditCollection Audit(RovingPlan rovingplan,RovingPlan rovingplanOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingplan.mRecordId != rovingplanOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplan);
				audit.mField = "record_id";
				audit.mOldValue = rovingplanOld.mRecordId.ToString();
				audit.mNewValue = rovingplan.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplan.mStartDate != rovingplanOld.mStartDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplan);
				audit.mField = "start_date";
				audit.mOldValue = rovingplanOld.mStartDate.ToString();
				audit.mNewValue = rovingplan.mStartDate.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplan.mEndDate != rovingplanOld.mEndDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplan);
				audit.mField = "end_date";
				audit.mOldValue = rovingplanOld.mEndDate.ToString();
				audit.mNewValue = rovingplan.mEndDate.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplan.mPlannedBy != rovingplanOld.mPlannedBy)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplan);
				audit.mField = "planned_by";
				audit.mOldValue = rovingplanOld.mPlannedBy.ToString();
				audit.mNewValue = rovingplan.mPlannedBy.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplan.mPost != rovingplanOld.mPost)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplan);
				audit.mField = "post";
				audit.mOldValue = rovingplanOld.mPost.ToString();
				audit.mNewValue = rovingplan.mPost.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingPlan rovingplan)
		{
			audit.mUserFullName = rovingplan.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingPlan);
			audit.mRowId = rovingplan.mId;
			audit.mAction = 2;
		}
	}
}