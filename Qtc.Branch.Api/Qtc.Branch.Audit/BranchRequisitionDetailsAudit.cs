using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class BranchRequisitionDetailsAudit
	{
		public static AuditCollection Audit(BranchRequisitionDetails branchrequisitiondetails,BranchRequisitionDetails branchrequisitiondetailsOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (branchrequisitiondetails.mBranchRequisitionId != branchrequisitiondetailsOld.mBranchRequisitionId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisitiondetails);
				audit.mField = "branch_requisition_id";
				audit.mOldValue = branchrequisitiondetailsOld.mBranchRequisitionId.ToString();
				audit.mNewValue = branchrequisitiondetails.mBranchRequisitionId.ToString();
				audit_collection.Add(audit);
			}

			if (branchrequisitiondetails.mProductId != branchrequisitiondetailsOld.mProductId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisitiondetails);
				audit.mField = "product_id";
				audit.mOldValue = branchrequisitiondetailsOld.mProductId.ToString();
				audit.mNewValue = branchrequisitiondetails.mProductId.ToString();
				audit_collection.Add(audit);
			}

			if (branchrequisitiondetails.mAvailQty != branchrequisitiondetailsOld.mAvailQty)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisitiondetails);
				audit.mField = "avail_qty";
				audit.mOldValue = branchrequisitiondetailsOld.mAvailQty.ToString();
				audit.mNewValue = branchrequisitiondetails.mAvailQty.ToString();
				audit_collection.Add(audit);
			}

			if (branchrequisitiondetails.mUserId != branchrequisitiondetailsOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisitiondetails);
				audit.mField = "user_id";
				audit.mOldValue = branchrequisitiondetailsOld.mUserId.ToString();
				audit.mNewValue = branchrequisitiondetails.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (branchrequisitiondetails.mRemarks != branchrequisitiondetailsOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisitiondetails);
				audit.mField = "remarks";
				audit.mOldValue = branchrequisitiondetailsOld.mRemarks;
				audit.mNewValue = branchrequisitiondetails.mRemarks;
				audit_collection.Add(audit);
			}

			if (branchrequisitiondetails.mDatestamp != branchrequisitiondetailsOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisitiondetails);
				audit.mField = "datestamp";
				audit.mOldValue = branchrequisitiondetailsOld.mDatestamp.ToString();
				audit.mNewValue = branchrequisitiondetails.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, BranchRequisitionDetails branchrequisitiondetails)
		{
			audit.mUserFullName = branchrequisitiondetails.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_BranchRequisitionDetails);
			audit.mRowId = branchrequisitiondetails.mId;
			audit.mAction = 2;
		}
	}
}