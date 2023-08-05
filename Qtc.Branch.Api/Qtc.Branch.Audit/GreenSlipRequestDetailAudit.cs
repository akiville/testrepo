using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class GreenSlipRequestDetailAudit
	{
		public static AuditCollection Audit(GreenSlipRequestDetail greensliprequestdetail,GreenSlipRequestDetail greensliprequestdetailOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (greensliprequestdetail.mGreenSlipId != greensliprequestdetailOld.mGreenSlipId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequestdetail);
				audit.mField = "green_slip_id";
				audit.mOldValue = greensliprequestdetailOld.mGreenSlipId.ToString();
				audit.mNewValue = greensliprequestdetail.mGreenSlipId.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequestdetail.mProductId != greensliprequestdetailOld.mProductId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequestdetail);
				audit.mField = "product_id";
				audit.mOldValue = greensliprequestdetailOld.mProductId.ToString();
				audit.mNewValue = greensliprequestdetail.mProductId.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequestdetail.mOtherQuantity != greensliprequestdetailOld.mOtherQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequestdetail);
				audit.mField = "other_quantity";
				audit.mOldValue = greensliprequestdetailOld.mOtherQuantity.ToString();
				audit.mNewValue = greensliprequestdetail.mOtherQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequestdetail.mQuantity != greensliprequestdetailOld.mQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequestdetail);
				audit.mField = "quantity";
				audit.mOldValue = greensliprequestdetailOld.mQuantity.ToString();
				audit.mNewValue = greensliprequestdetail.mQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequestdetail.mApprovedQuantity != greensliprequestdetailOld.mApprovedQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequestdetail);
				audit.mField = "approved_quantity";
				audit.mOldValue = greensliprequestdetailOld.mApprovedQuantity.ToString();
				audit.mNewValue = greensliprequestdetail.mApprovedQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequestdetail.mOtherCancel != greensliprequestdetailOld.mOtherCancel)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequestdetail);
				audit.mField = "other_cancel";
				audit.mOldValue = greensliprequestdetailOld.mOtherCancel.ToString();
				audit.mNewValue = greensliprequestdetail.mOtherCancel.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequestdetail.mCancel != greensliprequestdetailOld.mCancel)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequestdetail);
				audit.mField = "cancel";
				audit.mOldValue = greensliprequestdetailOld.mCancel.ToString();
				audit.mNewValue = greensliprequestdetail.mCancel.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequestdetail.mApprovedCancel != greensliprequestdetailOld.mApprovedCancel)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequestdetail);
				audit.mField = "approved_cancel";
				audit.mOldValue = greensliprequestdetailOld.mApprovedCancel.ToString();
				audit.mNewValue = greensliprequestdetail.mApprovedCancel.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequestdetail.mDispatch != greensliprequestdetailOld.mDispatch)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequestdetail);
				audit.mField = "dispatch";
				audit.mOldValue = greensliprequestdetailOld.mDispatch.ToString();
				audit.mNewValue = greensliprequestdetail.mDispatch.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequestdetail.mReceivedQuantity != greensliprequestdetailOld.mReceivedQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequestdetail);
				audit.mField = "received_quantity";
				audit.mOldValue = greensliprequestdetailOld.mReceivedQuantity.ToString();
				audit.mNewValue = greensliprequestdetail.mReceivedQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (greensliprequestdetail.mRemarks != greensliprequestdetailOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greensliprequestdetail);
				audit.mField = "remarks";
				audit.mOldValue = greensliprequestdetailOld.mRemarks;
				audit.mNewValue = greensliprequestdetail.mRemarks;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, GreenSlipRequestDetail greensliprequestdetail)
		{
			audit.mUserFullName = greensliprequestdetail.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_GreenSlipRequestDetail);
			audit.mRowId = greensliprequestdetail.mId;
			audit.mAction = 2;
		}
	}
}