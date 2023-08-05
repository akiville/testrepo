using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class AddBackAudit
	{
		public static AuditCollection Audit(AddBack addback,AddBack addbackOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (addback.mProductAvailId != addbackOld.mProductAvailId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, addback);
				audit.mField = "product_avail_id";
				audit.mOldValue = addbackOld.mProductAvailId.ToString();
				audit.mNewValue = addback.mProductAvailId.ToString();
				audit_collection.Add(audit);
			}

			if (addback.mBranchId != addbackOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, addback);
				audit.mField = "branch_id";
				audit.mOldValue = addbackOld.mBranchId.ToString();
				audit.mNewValue = addback.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (addback.mPersonnelId != addbackOld.mPersonnelId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, addback);
				audit.mField = "personnel_id";
				audit.mOldValue = addbackOld.mPersonnelId.ToString();
				audit.mNewValue = addback.mPersonnelId.ToString();
				audit_collection.Add(audit);
			}

			if (addback.mProductId != addbackOld.mProductId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, addback);
				audit.mField = "product_id";
				audit.mOldValue = addbackOld.mProductId.ToString();
				audit.mNewValue = addback.mProductId.ToString();
				audit_collection.Add(audit);
			}

			if (addback.mSalesDate != addbackOld.mSalesDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, addback);
				audit.mField = "sales_date";
				audit.mOldValue = addbackOld.mSalesDate.ToString();
				audit.mNewValue = addback.mSalesDate.ToString();
				audit_collection.Add(audit);
			}

			if (addback.mAddBackQty != addbackOld.mAddBackQty)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, addback);
				audit.mField = "add_back_qty";
				audit.mOldValue = addbackOld.mAddBackQty.ToString();
				audit.mNewValue = addback.mAddBackQty.ToString();
				audit_collection.Add(audit);
			}

			if (addback.mAddBackReason != addbackOld.mAddBackReason)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, addback);
				audit.mField = "add_back_reason";
				audit.mOldValue = addbackOld.mAddBackReason;
				audit.mNewValue = addback.mAddBackReason;
				audit_collection.Add(audit);
			}

			if (addback.mAddBackStatus != addbackOld.mAddBackStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, addback);
				audit.mField = "add_back_status";
				audit.mOldValue = addbackOld.mAddBackStatus;
				audit.mNewValue = addback.mAddBackStatus;
				audit_collection.Add(audit);
			}

			if (addback.mPriorQty != addbackOld.mPriorQty)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, addback);
				audit.mField = "prior_qty";
				audit.mOldValue = addbackOld.mPriorQty.ToString();
				audit.mNewValue = addback.mPriorQty.ToString();
				audit_collection.Add(audit);
			}

			if (addback.mAvailQty != addbackOld.mAvailQty)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, addback);
				audit.mField = "avail_qty";
				audit.mOldValue = addbackOld.mAvailQty.ToString();
				audit.mNewValue = addback.mAvailQty.ToString();
				audit_collection.Add(audit);
			}

			if (addback.mDatestamp != addbackOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, addback);
				audit.mField = "datestamp";
				audit.mOldValue = addbackOld.mDatestamp.ToString();
				audit.mNewValue = addback.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, AddBack addback)
		{
			audit.mUserFullName = addback.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_AddBack);
			audit.mRowId = addback.mId;
			audit.mAction = 2;
		}
	}
}