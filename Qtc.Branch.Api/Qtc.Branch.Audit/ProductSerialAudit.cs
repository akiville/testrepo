using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class ProductSerialAudit
	{
		public static AuditCollection Audit(ProductSerial productserial,ProductSerial productserialOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (productserial.mProductId != productserialOld.mProductId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "product_id";
				audit.mOldValue = productserialOld.mProductId.ToString();
				audit.mNewValue = productserial.mProductId.ToString();
				audit_collection.Add(audit);
			}

			if (productserial.mBrand != productserialOld.mBrand)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "brand";
				audit.mOldValue = productserialOld.mBrand;
				audit.mNewValue = productserial.mBrand;
				audit_collection.Add(audit);
			}

			if (productserial.mDescription != productserialOld.mDescription)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "description";
				audit.mOldValue = productserialOld.mDescription;
				audit.mNewValue = productserial.mDescription;
				audit_collection.Add(audit);
			}

			if (productserial.mStickerNo != productserialOld.mStickerNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "sticker_no";
				audit.mOldValue = productserialOld.mStickerNo;
				audit.mNewValue = productserial.mStickerNo;
				audit_collection.Add(audit);
			}

			if (productserial.mControlNo != productserialOld.mControlNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "control_no";
				audit.mOldValue = productserialOld.mControlNo;
				audit.mNewValue = productserial.mControlNo;
				audit_collection.Add(audit);
			}

			if (productserial.mSerialNo != productserialOld.mSerialNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "serial_no";
				audit.mOldValue = productserialOld.mSerialNo;
				audit.mNewValue = productserial.mSerialNo;
				audit_collection.Add(audit);
			}

			if (productserial.mModelNo != productserialOld.mModelNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "model_no";
				audit.mOldValue = productserialOld.mModelNo;
				audit.mNewValue = productserial.mModelNo;
				audit_collection.Add(audit);
			}

			if (productserial.mDimension != productserialOld.mDimension)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "dimension";
				audit.mOldValue = productserialOld.mDimension;
				audit.mNewValue = productserial.mDimension;
				audit_collection.Add(audit);
			}

			if (productserial.mService != productserialOld.mService)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "service";
				audit.mOldValue = productserialOld.mService.ToString();
				audit.mNewValue = productserial.mService.ToString();
				audit_collection.Add(audit);
			}

			if (productserial.mBranchIdLocation != productserialOld.mBranchIdLocation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "branch_id_location";
				audit.mOldValue = productserialOld.mBranchIdLocation.ToString();
				audit.mNewValue = productserial.mBranchIdLocation.ToString();
				audit_collection.Add(audit);
			}

			if (productserial.mBranchId != productserialOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "branch_id";
				audit.mOldValue = productserialOld.mBranchId.ToString();
				audit.mNewValue = productserial.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (productserial.mCost != productserialOld.mCost)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "cost";
				audit.mOldValue = productserialOld.mCost.ToString();
				audit.mNewValue = productserial.mCost.ToString();
				audit_collection.Add(audit);
			}

			if (productserial.mSupplier != productserialOld.mSupplier)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "supplier";
				audit.mOldValue = productserialOld.mSupplier;
				audit.mNewValue = productserial.mSupplier;
				audit_collection.Add(audit);
			}

			if (productserial.mRm2Id != productserialOld.mRm2Id)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "rm2_id";
				audit.mOldValue = productserialOld.mRm2Id.ToString();
				audit.mNewValue = productserial.mRm2Id.ToString();
				audit_collection.Add(audit);
			}

			if (productserial.mRpId != productserialOld.mRpId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "rp_id";
				audit.mOldValue = productserialOld.mRpId.ToString();
				audit.mNewValue = productserial.mRpId.ToString();
				audit_collection.Add(audit);
			}

			if (productserial.mProductModelId != productserialOld.mProductModelId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "product_model_id";
				audit.mOldValue = productserialOld.mProductModelId.ToString();
				audit.mNewValue = productserial.mProductModelId.ToString();
				audit_collection.Add(audit);
			}

			if (productserial.mForInventory != productserialOld.mForInventory)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productserial);
				audit.mField = "for_inventory";
				audit.mOldValue = productserialOld.mForInventory.ToString();
				audit.mNewValue = productserial.mForInventory.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, ProductSerial productserial)
		{
			audit.mUserFullName = productserial.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_ProductSerial);
			audit.mRowId = productserial.mId;
			audit.mAction = 2;
		}
	}
}