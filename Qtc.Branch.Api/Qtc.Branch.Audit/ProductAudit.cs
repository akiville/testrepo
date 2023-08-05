using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class ProductAudit
	{
		public static AuditCollection Audit(Product product,Product productOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (product.mProductGroupId != productOld.mProductGroupId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "product_group_id";
				audit.mOldValue = productOld.mProductGroupId.ToString();
				audit.mNewValue = product.mProductGroupId.ToString();
				audit_collection.Add(audit);
			}

			if (product.mCategoryId != productOld.mCategoryId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "category_id";
				audit.mOldValue = productOld.mCategoryId.ToString();
				audit.mNewValue = product.mCategoryId.ToString();
				audit_collection.Add(audit);
			}

			if (product.mProductTypeId != productOld.mProductTypeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "product_type_id";
				audit.mOldValue = productOld.mProductTypeId.ToString();
				audit.mNewValue = product.mProductTypeId.ToString();
				audit_collection.Add(audit);
			}

			if (product.mCategory2Id != productOld.mCategory2Id)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "category_2_id";
				audit.mOldValue = productOld.mCategory2Id.ToString();
				audit.mNewValue = product.mCategory2Id.ToString();
				audit_collection.Add(audit);
			}

			if (product.mName != productOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "name";
				audit.mOldValue = productOld.mName;
				audit.mNewValue = product.mName;
				audit_collection.Add(audit);
			}

			if (product.mCode != productOld.mCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "code";
				audit.mOldValue = productOld.mCode;
				audit.mNewValue = product.mCode;
				audit_collection.Add(audit);
			}

			if (product.mDescription != productOld.mDescription)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "description";
				audit.mOldValue = productOld.mDescription;
				audit.mNewValue = product.mDescription;
				audit_collection.Add(audit);
			}

			if (product.mSize != productOld.mSize)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "size";
				audit.mOldValue = productOld.mSize;
				audit.mNewValue = product.mSize;
				audit_collection.Add(audit);
			}

			if (product.mBrandId != productOld.mBrandId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "brand_id";
				audit.mOldValue = productOld.mBrandId.ToString();
				audit.mNewValue = product.mBrandId.ToString();
				audit_collection.Add(audit);
			}

			if (product.mBrand != productOld.mBrand)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "brand";
				audit.mOldValue = productOld.mBrand;
				audit.mNewValue = product.mBrand;
				audit_collection.Add(audit);
			}

			if (product.mColorId != productOld.mColorId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "color_id";
				audit.mOldValue = productOld.mColorId.ToString();
				audit.mNewValue = product.mColorId.ToString();
				audit_collection.Add(audit);
			}

			if (product.mColor != productOld.mColor)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "color";
				audit.mOldValue = productOld.mColor;
				audit.mNewValue = product.mColor;
				audit_collection.Add(audit);
			}

			if (product.mDimension != productOld.mDimension)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "dimension";
				audit.mOldValue = productOld.mDimension;
				audit.mNewValue = product.mDimension;
				audit_collection.Add(audit);
			}

			if (product.mSerial != productOld.mSerial)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "serial";
				audit.mOldValue = productOld.mSerial;
				audit.mNewValue = product.mSerial;
				audit_collection.Add(audit);
			}

			if (product.mModel != productOld.mModel)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "model";
				audit.mOldValue = productOld.mModel;
				audit.mNewValue = product.mModel;
				audit_collection.Add(audit);
			}

			if (product.mControlNo != productOld.mControlNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "control_no";
				audit.mOldValue = productOld.mControlNo;
				audit.mNewValue = product.mControlNo;
				audit_collection.Add(audit);
			}

			if (product.mUnit != productOld.mUnit)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "unit";
				audit.mOldValue = productOld.mUnit;
				audit.mNewValue = product.mUnit;
				audit_collection.Add(audit);
			}

			if (product.mMatchingItems != productOld.mMatchingItems)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "matching_items";
				audit.mOldValue = productOld.mMatchingItems;
				audit.mNewValue = product.mMatchingItems;
				audit_collection.Add(audit);
			}

			if (product.mOrdering != productOld.mOrdering)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "ordering";
				audit.mOldValue = productOld.mOrdering.ToString();
				audit.mNewValue = product.mOrdering.ToString();
				audit_collection.Add(audit);
			}

			if (product.mPerishable != productOld.mPerishable)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "perishable";
				audit.mOldValue = productOld.mPerishable.ToString();
				audit.mNewValue = product.mPerishable.ToString();
				audit_collection.Add(audit);
			}

			if (product.mRequiredDate != productOld.mRequiredDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "required_date";
				audit.mOldValue = productOld.mRequiredDate.ToString();
				audit.mNewValue = product.mRequiredDate.ToString();
				audit_collection.Add(audit);
			}

			if (product.mChargeable != productOld.mChargeable)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "chargeable";
				audit.mOldValue = productOld.mChargeable.ToString();
				audit.mNewValue = product.mChargeable.ToString();
				audit_collection.Add(audit);
			}

			if (product.mBudget != productOld.mBudget)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "budget";
				audit.mOldValue = productOld.mBudget.ToString();
				audit.mNewValue = product.mBudget.ToString();
				audit_collection.Add(audit);
			}

			if (product.mEquipment != productOld.mEquipment)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "equipment";
				audit.mOldValue = productOld.mEquipment.ToString();
				audit.mNewValue = product.mEquipment.ToString();
				audit_collection.Add(audit);
			}

			if (product.mRepairParts != productOld.mRepairParts)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, product);
				audit.mField = "repair_parts";
				audit.mOldValue = productOld.mRepairParts.ToString();
				audit.mNewValue = product.mRepairParts.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Product product)
		{
			audit.mUserFullName = product.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Product);
			audit.mRowId = product.mId;
			audit.mAction = 2;
		}
	}
}