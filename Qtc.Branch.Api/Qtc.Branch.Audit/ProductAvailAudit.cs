using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class ProductAvailAudit
	{
		public static AuditCollection Audit(ProductAvail productavail,ProductAvail productavailOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (productavail.mBranchId != productavailOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "branch_id";
				audit.mOldValue = productavailOld.mBranchId.ToString();
				audit.mNewValue = productavail.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mInventoryDate != productavailOld.mInventoryDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "inventory_date";
				audit.mOldValue = productavailOld.mInventoryDate.ToString();
				audit.mNewValue = productavail.mInventoryDate.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mProductId != productavailOld.mProductId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "product_id";
				audit.mOldValue = productavailOld.mProductId.ToString();
				audit.mNewValue = productavail.mProductId.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mCategoryId != productavailOld.mCategoryId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "category_id";
				audit.mOldValue = productavailOld.mCategoryId.ToString();
				audit.mNewValue = productavail.mCategoryId.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mPrior != productavailOld.mPrior)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "prior";
				audit.mOldValue = productavailOld.mPrior.ToString();
				audit.mNewValue = productavail.mPrior.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mAvail != productavailOld.mAvail)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "avail";
				audit.mOldValue = productavailOld.mAvail.ToString();
				audit.mNewValue = productavail.mAvail.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mAddBack != productavailOld.mAddBack)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "add_back";
				audit.mOldValue = productavailOld.mAddBack.ToString();
				audit.mNewValue = productavail.mAddBack.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mAddBackReason != productavailOld.mAddBackReason)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "add_back_reason";
				audit.mOldValue = productavailOld.mAddBackReason;
				audit.mNewValue = productavail.mAddBackReason;
				audit_collection.Add(audit);
			}

			if (productavail.mDispatch != productavailOld.mDispatch)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "dispatch";
				audit.mOldValue = productavailOld.mDispatch.ToString();
				audit.mNewValue = productavail.mDispatch.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mGreenslipAdd != productavailOld.mGreenslipAdd)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "greenslip_add";
				audit.mOldValue = productavailOld.mGreenslipAdd.ToString();
				audit.mNewValue = productavail.mGreenslipAdd.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mGreenslipCancel != productavailOld.mGreenslipCancel)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "greenslip_cancel";
				audit.mOldValue = productavailOld.mGreenslipCancel.ToString();
				audit.mNewValue = productavail.mGreenslipCancel.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mDdirOut != productavailOld.mDdirOut)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "ddir_out";
				audit.mOldValue = productavailOld.mDdirOut.ToString();
				audit.mNewValue = productavail.mDdirOut.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mDdirIn != productavailOld.mDdirIn)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "ddir_in";
				audit.mOldValue = productavailOld.mDdirIn.ToString();
				audit.mNewValue = productavail.mDdirIn.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mLoe != productavailOld.mLoe)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "loe";
				audit.mOldValue = productavailOld.mLoe.ToString();
				audit.mNewValue = productavail.mLoe.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mIbwOut != productavailOld.mIbwOut)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "ibw_out";
				audit.mOldValue = productavailOld.mIbwOut.ToString();
				audit.mNewValue = productavail.mIbwOut.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mIbwIn != productavailOld.mIbwIn)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "ibw_in";
				audit.mOldValue = productavailOld.mIbwIn.ToString();
				audit.mNewValue = productavail.mIbwIn.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mRma != productavailOld.mRma)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "rma";
				audit.mOldValue = productavailOld.mRma.ToString();
				audit.mNewValue = productavail.mRma.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mEmployeeId != productavailOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "employee_id";
				audit.mOldValue = productavailOld.mEmployeeId.ToString();
				audit.mNewValue = productavail.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (productavail.mUploadDate != productavailOld.mUploadDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, productavail);
				audit.mField = "upload_date";
				audit.mOldValue = productavailOld.mUploadDate.ToString();
				audit.mNewValue = productavail.mUploadDate.ToString();
				audit_collection.Add(audit);
			}

			//if (productavail.mSignature != productavailOld.mSignature)
			//{
			//	audit = new BusinessEntities.Audit();
			//	LoadCommonData(ref audit, productavail);
			//	audit.mField = "signature";
			//	audit.mOldValue = productavailOld.mSignature.ToString();
			//	audit.mNewValue = productavail.mSignature.ToString();
			//	audit_collection.Add(audit);
			//}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, ProductAvail productavail)
		{
			audit.mUserFullName = productavail.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_ProductAvail);
			audit.mRowId = productavail.mId;
			audit.mAction = 2;
		}
	}
}