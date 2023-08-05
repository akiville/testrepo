using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class GreenslipAudit
	{
		public static AuditCollection Audit(Greenslip greenslip,Greenslip greenslipOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (greenslip.mDate != greenslipOld.mDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "date";
				audit.mOldValue = greenslipOld.mDate.ToString();
				audit.mNewValue = greenslip.mDate.ToString();
				audit_collection.Add(audit);
			}

			if (greenslip.mDateRequested != greenslipOld.mDateRequested)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "date_requested";
				audit.mOldValue = greenslipOld.mDateRequested.ToString();
				audit.mNewValue = greenslip.mDateRequested.ToString();
				audit_collection.Add(audit);
			}

			if (greenslip.mBranchId != greenslipOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "branch_id";
				audit.mOldValue = greenslipOld.mBranchId.ToString();
				audit.mNewValue = greenslip.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (greenslip.mRequestedById != greenslipOld.mRequestedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "requested_by_id";
				audit.mOldValue = greenslipOld.mRequestedById.ToString();
				audit.mNewValue = greenslip.mRequestedById.ToString();
				audit_collection.Add(audit);
			}

			if (greenslip.mType != greenslipOld.mType)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "type";
				audit.mOldValue = greenslipOld.mType;
				audit.mNewValue = greenslip.mType;
				audit_collection.Add(audit);
			}

			if (greenslip.mType2 != greenslipOld.mType2)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "type2";
				audit.mOldValue = greenslipOld.mType2;
				audit.mNewValue = greenslip.mType2;
				audit_collection.Add(audit);
			}

			if (greenslip.mGreenslipRequestId != greenslipOld.mGreenslipRequestId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "greenslip_request_id";
				audit.mOldValue = greenslipOld.mGreenslipRequestId.ToString();
				audit.mNewValue = greenslip.mGreenslipRequestId.ToString();
				audit_collection.Add(audit);
			}

			if (greenslip.mExplanation != greenslipOld.mExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "explanation";
				audit.mOldValue = greenslipOld.mExplanation;
				audit.mNewValue = greenslip.mExplanation;
				audit_collection.Add(audit);
			}

			if (greenslip.mRemarks != greenslipOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "remarks";
				audit.mOldValue = greenslipOld.mRemarks;
				audit.mNewValue = greenslip.mRemarks;
				audit_collection.Add(audit);
			}

			if (greenslip.mApprovedBy != greenslipOld.mApprovedBy)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "approved_by";
				audit.mOldValue = greenslipOld.mApprovedBy;
				audit.mNewValue = greenslip.mApprovedBy;
				audit_collection.Add(audit);
			}

			if (greenslip.mNumber != greenslipOld.mNumber)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "number";
				audit.mOldValue = greenslipOld.mNumber.ToString();
				audit.mNewValue = greenslip.mNumber.ToString();
				audit_collection.Add(audit);
			}

			if (greenslip.mDenied != greenslipOld.mDenied)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "denied";
				audit.mOldValue = greenslipOld.mDenied.ToString();
				audit.mNewValue = greenslip.mDenied.ToString();
				audit_collection.Add(audit);
			}

			if (greenslip.mCancelled != greenslipOld.mCancelled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "cancelled";
				audit.mOldValue = greenslipOld.mCancelled.ToString();
				audit.mNewValue = greenslip.mCancelled.ToString();
				audit_collection.Add(audit);
			}

			if (greenslip.mDeliveryDate != greenslipOld.mDeliveryDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "delivery_date";
				audit.mOldValue = greenslipOld.mDeliveryDate.ToString();
				audit.mNewValue = greenslip.mDeliveryDate.ToString();
				audit_collection.Add(audit);
			}

			if (greenslip.mIsDownloaded != greenslipOld.mIsDownloaded)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "is_downloaded";
				audit.mOldValue = greenslipOld.mIsDownloaded.ToString();
				audit.mNewValue = greenslip.mIsDownloaded.ToString();
				audit_collection.Add(audit);
			}

			if (greenslip.mAppDownloadDate != greenslipOld.mAppDownloadDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslip);
				audit.mField = "app_download_date";
				audit.mOldValue = greenslipOld.mAppDownloadDate.ToString();
				audit.mNewValue = greenslip.mAppDownloadDate.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Greenslip greenslip)
		{
			audit.mUserFullName = greenslip.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Greenslip);
			audit.mRowId = greenslip.mId;
			audit.mAction = 2;
		}
	}
}