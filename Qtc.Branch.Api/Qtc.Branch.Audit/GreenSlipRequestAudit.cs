using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class GreenSlipRequestAudit
	{
		public static AuditCollection Audit(GreenSlipRequest greensliprequest,GreenSlipRequest greensliprequestOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (greensliprequest.mDate != greensliprequestOld.mDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "date";
				audit.mOldValue = greensliprequestOld.mDate.ToString();
				audit.mNewValue = greensliprequest.mDate.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequest.mDateRequested != greensliprequestOld.mDateRequested)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "date_requested";
				audit.mOldValue = greensliprequestOld.mDateRequested.ToString();
				audit.mNewValue = greensliprequest.mDateRequested.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequest.mBranchId != greensliprequestOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "branch_id";
				audit.mOldValue = greensliprequestOld.mBranchId.ToString();
				audit.mNewValue = greensliprequest.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequest.mRequestedById != greensliprequestOld.mRequestedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "requested_by_id";
				audit.mOldValue = greensliprequestOld.mRequestedById.ToString();
				audit.mNewValue = greensliprequest.mRequestedById.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequest.mType != greensliprequestOld.mType)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "type";
				audit.mOldValue = greensliprequestOld.mType;
				audit.mNewValue = greensliprequest.mType;
				audit_collection.Add(audit);
			}

			if (greensliprequest.mType2 != greensliprequestOld.mType2)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "type2";
				audit.mOldValue = greensliprequestOld.mType2;
				audit.mNewValue = greensliprequest.mType2;
				audit_collection.Add(audit);
			}

			if (greensliprequest.mGreenslipRequestId != greensliprequestOld.mGreenslipRequestId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "greenslip_request_id";
				audit.mOldValue = greensliprequestOld.mGreenslipRequestId.ToString();
				audit.mNewValue = greensliprequest.mGreenslipRequestId.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequest.mExplanation != greensliprequestOld.mExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "explanation";
				audit.mOldValue = greensliprequestOld.mExplanation;
				audit.mNewValue = greensliprequest.mExplanation;
				audit_collection.Add(audit);
			}

			if (greensliprequest.mRemarks != greensliprequestOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "remarks";
				audit.mOldValue = greensliprequestOld.mRemarks;
				audit.mNewValue = greensliprequest.mRemarks;
				audit_collection.Add(audit);
			}

			if (greensliprequest.mApprovedBy != greensliprequestOld.mApprovedBy)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "approved_by";
				audit.mOldValue = greensliprequestOld.mApprovedBy;
				audit.mNewValue = greensliprequest.mApprovedBy;
				audit_collection.Add(audit);
			}

			if (greensliprequest.mNumber != greensliprequestOld.mNumber)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "number";
				audit.mOldValue = greensliprequestOld.mNumber.ToString();
				audit.mNewValue = greensliprequest.mNumber.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequest.mDenied != greensliprequestOld.mDenied)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "denied";
				audit.mOldValue = greensliprequestOld.mDenied.ToString();
				audit.mNewValue = greensliprequest.mDenied.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequest.mCancelled != greensliprequestOld.mCancelled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "cancelled";
				audit.mOldValue = greensliprequestOld.mCancelled.ToString();
				audit.mNewValue = greensliprequest.mCancelled.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequest.mTakenAction != greensliprequestOld.mTakenAction)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "taken_action";
				audit.mOldValue = greensliprequestOld.mTakenAction.ToString();
				audit.mNewValue = greensliprequest.mTakenAction.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequest.mDeliveryDate != greensliprequestOld.mDeliveryDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "delivery_date";
				audit.mOldValue = greensliprequestOld.mDeliveryDate.ToString();
				audit.mNewValue = greensliprequest.mDeliveryDate.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequest.mIsDownloaded != greensliprequestOld.mIsDownloaded)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "is_downloaded";
				audit.mOldValue = greensliprequestOld.mIsDownloaded.ToString();
				audit.mNewValue = greensliprequest.mIsDownloaded.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequest.mAppDownloadDate != greensliprequestOld.mAppDownloadDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequest);
				audit.mField = "app_download_date";
				audit.mOldValue = greensliprequestOld.mAppDownloadDate.ToString();
				audit.mNewValue = greensliprequest.mAppDownloadDate.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, GreenSlipRequest greensliprequest)
		{
			audit.mUserFullName = greensliprequest.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_GreenSlipRequest);
			audit.mRowId = greensliprequest.mId;
			audit.mAction = 2;
		}
	}
}