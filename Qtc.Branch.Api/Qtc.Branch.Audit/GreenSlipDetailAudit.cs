using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class GreenSlipDetailAudit
	{
		public static AuditCollection Audit(GreenSlipDetail greenslipdetail,GreenSlipDetail greenslipdetailOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (greenslipdetail.mGreenSlipId != greenslipdetailOld.mGreenSlipId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslipdetail);
				audit.mField = "green_slip_id";
				audit.mOldValue = greenslipdetailOld.mGreenSlipId.ToString();
				audit.mNewValue = greenslipdetail.mGreenSlipId.ToString();
				audit_collection.Add(audit);
			}

			if (greenslipdetail.mProductId != greenslipdetailOld.mProductId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslipdetail);
				audit.mField = "product_id";
				audit.mOldValue = greenslipdetailOld.mProductId.ToString();
				audit.mNewValue = greenslipdetail.mProductId.ToString();
				audit_collection.Add(audit);
			}

			if (greenslipdetail.mOtherQuantity != greenslipdetailOld.mOtherQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslipdetail);
				audit.mField = "other_quantity";
				audit.mOldValue = greenslipdetailOld.mOtherQuantity.ToString();
				audit.mNewValue = greenslipdetail.mOtherQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (greenslipdetail.mQuantity != greenslipdetailOld.mQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslipdetail);
				audit.mField = "quantity";
				audit.mOldValue = greenslipdetailOld.mQuantity.ToString();
				audit.mNewValue = greenslipdetail.mQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (greenslipdetail.mApprovedQuantity != greenslipdetailOld.mApprovedQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslipdetail);
				audit.mField = "approved_quantity";
				audit.mOldValue = greenslipdetailOld.mApprovedQuantity.ToString();
				audit.mNewValue = greenslipdetail.mApprovedQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (greenslipdetail.mOtherCancel != greenslipdetailOld.mOtherCancel)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslipdetail);
				audit.mField = "other_cancel";
				audit.mOldValue = greenslipdetailOld.mOtherCancel.ToString();
				audit.mNewValue = greenslipdetail.mOtherCancel.ToString();
				audit_collection.Add(audit);
			}

			if (greenslipdetail.mCancel != greenslipdetailOld.mCancel)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslipdetail);
				audit.mField = "cancel";
				audit.mOldValue = greenslipdetailOld.mCancel.ToString();
				audit.mNewValue = greenslipdetail.mCancel.ToString();
				audit_collection.Add(audit);
			}

			if (greenslipdetail.mApprovedCancel != greenslipdetailOld.mApprovedCancel)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslipdetail);
				audit.mField = "approved_cancel";
				audit.mOldValue = greenslipdetailOld.mApprovedCancel.ToString();
				audit.mNewValue = greenslipdetail.mApprovedCancel.ToString();
				audit_collection.Add(audit);
			}

			if (greenslipdetail.mDispatch != greenslipdetailOld.mDispatch)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslipdetail);
				audit.mField = "dispatch";
				audit.mOldValue = greenslipdetailOld.mDispatch.ToString();
				audit.mNewValue = greenslipdetail.mDispatch.ToString();
				audit_collection.Add(audit);
			}

			if (greenslipdetail.mReceivedQuantity != greenslipdetailOld.mReceivedQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslipdetail);
				audit.mField = "received_quantity";
				audit.mOldValue = greenslipdetailOld.mReceivedQuantity.ToString();
				audit.mNewValue = greenslipdetail.mReceivedQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (greenslipdetail.mRemarks != greenslipdetailOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, greenslipdetail);
				audit.mField = "remarks";
				audit.mOldValue = greenslipdetailOld.mRemarks;
				audit.mNewValue = greenslipdetail.mRemarks;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, GreenSlipDetail greenslipdetail)
		{
			audit.mUserFullName = greenslipdetail.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_GreenSlipDetail);
			audit.mRowId = greenslipdetail.mId;
			audit.mAction = 2;
		}
	}
}