using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RovingPlanScheduledActualChecklistImageAudit
	{
		public static AuditCollection Audit(RovingPlanScheduledActualChecklistImage rovingplanscheduledactualchecklistimage,RovingPlanScheduledActualChecklistImage rovingplanscheduledactualchecklistimageOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rovingplanscheduledactualchecklistimage.mRovingChecklistActualId != rovingplanscheduledactualchecklistimageOld.mRovingChecklistActualId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistimage);
				audit.mField = "roving_checklist_actual_id";
				audit.mOldValue = rovingplanscheduledactualchecklistimageOld.mRovingChecklistActualId.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklistimage.mRovingChecklistActualId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklistimage.mImageUrl != rovingplanscheduledactualchecklistimageOld.mImageUrl)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistimage);
				audit.mField = "image_url";
				audit.mOldValue = rovingplanscheduledactualchecklistimageOld.mImageUrl;
				audit.mNewValue = rovingplanscheduledactualchecklistimage.mImageUrl;
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklistimage.mRemarks != rovingplanscheduledactualchecklistimageOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistimage);
				audit.mField = "remarks";
				audit.mOldValue = rovingplanscheduledactualchecklistimageOld.mRemarks;
				audit.mNewValue = rovingplanscheduledactualchecklistimage.mRemarks;
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklistimage.mDatestamp != rovingplanscheduledactualchecklistimageOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistimage);
				audit.mField = "datestamp";
				audit.mOldValue = rovingplanscheduledactualchecklistimageOld.mDatestamp.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklistimage.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklistimage.mRecordId != rovingplanscheduledactualchecklistimageOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistimage);
				audit.mField = "record_id";
				audit.mOldValue = rovingplanscheduledactualchecklistimageOld.mRecordId.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklistimage.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklistimage.mRpsId != rovingplanscheduledactualchecklistimageOld.mRpsId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistimage);
				audit.mField = "rps_id";
				audit.mOldValue = rovingplanscheduledactualchecklistimageOld.mRpsId.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklistimage.mRpsId.ToString();
				audit_collection.Add(audit);
			}

			if (rovingplanscheduledactualchecklistimage.mRcId != rovingplanscheduledactualchecklistimageOld.mRcId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rovingplanscheduledactualchecklistimage);
				audit.mField = "rc_id";
				audit.mOldValue = rovingplanscheduledactualchecklistimageOld.mRcId.ToString();
				audit.mNewValue = rovingplanscheduledactualchecklistimage.mRcId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RovingPlanScheduledActualChecklistImage rovingplanscheduledactualchecklistimage)
		{
			audit.mUserFullName = rovingplanscheduledactualchecklistimage.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RovingPlanScheduledActualChecklistImage);
			audit.mRowId = rovingplanscheduledactualchecklistimage.mId;
			audit.mAction = 2;
		}
	}
}