using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class LoeDetailAudit
	{
		public static AuditCollection Audit(LoeDetail loedetail,LoeDetail loedetailOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (loedetail.mLoeId != loedetailOld.mLoeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loedetail);
				audit.mField = "loe_id";
				audit.mOldValue = loedetailOld.mLoeId.ToString();
				audit.mNewValue = loedetail.mLoeId.ToString();
				audit_collection.Add(audit);
			}

			if (loedetail.mQuantity != loedetailOld.mQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loedetail);
				audit.mField = "quantity";
				audit.mOldValue = loedetailOld.mQuantity.ToString();
				audit.mNewValue = loedetail.mQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (loedetail.mApprovedQuantity != loedetailOld.mApprovedQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loedetail);
				audit.mField = "approved_quantity";
				audit.mOldValue = loedetailOld.mApprovedQuantity.ToString();
				audit.mNewValue = loedetail.mApprovedQuantity.ToString();
				audit_collection.Add(audit);
			}

			if (loedetail.mProductId != loedetailOld.mProductId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loedetail);
				audit.mField = "product_id";
				audit.mOldValue = loedetailOld.mProductId.ToString();
				audit.mNewValue = loedetail.mProductId.ToString();
				audit_collection.Add(audit);
			}

			if (loedetail.mPrice != loedetailOld.mPrice)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loedetail);
				audit.mField = "price";
				audit.mOldValue = loedetailOld.mPrice.ToString();
				audit.mNewValue = loedetail.mPrice.ToString();
				audit_collection.Add(audit);
			}

			if (loedetail.mDiscount != loedetailOld.mDiscount)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loedetail);
				audit.mField = "discount";
				audit.mOldValue = loedetailOld.mDiscount.ToString();
				audit.mNewValue = loedetail.mDiscount.ToString();
				audit_collection.Add(audit);
			}

			if (loedetail.mEmployeeId != loedetailOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loedetail);
				audit.mField = "employee_id";
				audit.mOldValue = loedetailOld.mEmployeeId.ToString();
				audit.mNewValue = loedetail.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (loedetail.mUnit != loedetailOld.mUnit)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loedetail);
				audit.mField = "unit";
				audit.mOldValue = loedetailOld.mUnit;
				audit.mNewValue = loedetail.mUnit;
				audit_collection.Add(audit);
			}

			if (loedetail.mProduct != loedetailOld.mProduct)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loedetail);
				audit.mField = "product";
				audit.mOldValue = loedetailOld.mProduct;
				audit.mNewValue = loedetail.mProduct;
				audit_collection.Add(audit);
			}

			if (loedetail.mEditedQuantity != loedetailOld.mEditedQuantity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loedetail);
				audit.mField = "edited_quantity";
				audit.mOldValue = loedetailOld.mEditedQuantity;
				audit.mNewValue = loedetail.mEditedQuantity;
				audit_collection.Add(audit);
			}

			if (loedetail.mEditedPrice != loedetailOld.mEditedPrice)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loedetail);
				audit.mField = "edited_price";
				audit.mOldValue = loedetailOld.mEditedPrice;
				audit.mNewValue = loedetail.mEditedPrice;
				audit_collection.Add(audit);
			}

			if (loedetail.mEditedDiscount != loedetailOld.mEditedDiscount)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, loedetail);
				audit.mField = "edited_discount";
				audit.mOldValue = loedetailOld.mEditedDiscount;
				audit.mNewValue = loedetail.mEditedDiscount;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, LoeDetail loedetail)
		{
			audit.mUserFullName = loedetail.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_LoeDetail);
			audit.mRowId = loedetail.mId;
			audit.mAction = 2;
		}
	}
}