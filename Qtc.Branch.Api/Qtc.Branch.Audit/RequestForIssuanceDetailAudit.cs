using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RequestForIssuanceDetailAudit
	{
		public static AuditCollection Audit(RequestForIssuanceDetail requestforissuancedetail,RequestForIssuanceDetail requestforissuancedetailOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (requestforissuancedetail.mRequestForIssuanceId != requestforissuancedetailOld.mRequestForIssuanceId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "request_for_issuance_id";
				audit.mOldValue = requestforissuancedetailOld.mRequestForIssuanceId.ToString();
				audit.mNewValue = requestforissuancedetail.mRequestForIssuanceId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuancedetail.mProductId != requestforissuancedetailOld.mProductId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "product_id";
				audit.mOldValue = requestforissuancedetailOld.mProductId.ToString();
				audit.mNewValue = requestforissuancedetail.mProductId.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuancedetail.mRequestedQty != requestforissuancedetailOld.mRequestedQty)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "requested_qty";
				audit.mOldValue = requestforissuancedetailOld.mRequestedQty.ToString();
				audit.mNewValue = requestforissuancedetail.mRequestedQty.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuancedetail.mApprovedQty != requestforissuancedetailOld.mApprovedQty)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "approved_qty";
				audit.mOldValue = requestforissuancedetailOld.mApprovedQty.ToString();
				audit.mNewValue = requestforissuancedetail.mApprovedQty.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuancedetail.mForPurchaseQuantity != requestforissuancedetailOld.mForPurchaseQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "for_purchase_quantity";
				audit.mOldValue = requestforissuancedetailOld.mForPurchaseQuantity.ToString();
				audit.mNewValue = requestforissuancedetail.mForPurchaseQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuancedetail.mApprovedQuantityForPurchase != requestforissuancedetailOld.mApprovedQuantityForPurchase)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "approved_quantity_for_purchase";
				audit.mOldValue = requestforissuancedetailOld.mApprovedQuantityForPurchase.ToString();
				audit.mNewValue = requestforissuancedetail.mApprovedQuantityForPurchase.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuancedetail.mReleasedQuantity != requestforissuancedetailOld.mReleasedQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "released_quantity";
				audit.mOldValue = requestforissuancedetailOld.mReleasedQuantity.ToString();
				audit.mNewValue = requestforissuancedetail.mReleasedQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuancedetail.mUsedQuantity != requestforissuancedetailOld.mUsedQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "used_quantity";
				audit.mOldValue = requestforissuancedetailOld.mUsedQuantity.ToString();
				audit.mNewValue = requestforissuancedetail.mUsedQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuancedetail.mCost != requestforissuancedetailOld.mCost)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "cost";
				audit.mOldValue = requestforissuancedetailOld.mCost.ToString();
				audit.mNewValue = requestforissuancedetail.mCost.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuancedetail.mRemarks != requestforissuancedetailOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "remarks";
				audit.mOldValue = requestforissuancedetailOld.mRemarks;
				audit.mNewValue = requestforissuancedetail.mRemarks;
				audit_collection.Add(audit);
			}

			if (requestforissuancedetail.mCancelled != requestforissuancedetailOld.mCancelled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "cancelled";
				audit.mOldValue = requestforissuancedetailOld.mCancelled.ToString();
				audit.mNewValue = requestforissuancedetail.mCancelled.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuancedetail.mProductSerialIdGatepass != requestforissuancedetailOld.mProductSerialIdGatepass)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "product_serial_id_gatepass";
				audit.mOldValue = requestforissuancedetailOld.mProductSerialIdGatepass.ToString();
				audit.mNewValue = requestforissuancedetail.mProductSerialIdGatepass.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuancedetail.mProductSerialIdIngress != requestforissuancedetailOld.mProductSerialIdIngress)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "product_serial_id_ingress";
				audit.mOldValue = requestforissuancedetailOld.mProductSerialIdIngress.ToString();
				audit.mNewValue = requestforissuancedetail.mProductSerialIdIngress.ToString();
				audit_collection.Add(audit);
			}

			if (requestforissuancedetail.mDatestamp != requestforissuancedetailOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestforissuancedetail);
				audit.mField = "datestamp";
				audit.mOldValue = requestforissuancedetailOld.mDatestamp.ToString();
				audit.mNewValue = requestforissuancedetail.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RequestForIssuanceDetail requestforissuancedetail)
		{
			audit.mUserFullName = requestforissuancedetail.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RequestForIssuanceDetail);
			audit.mRowId = requestforissuancedetail.mId;
			audit.mAction = 2;
		}
	}
}