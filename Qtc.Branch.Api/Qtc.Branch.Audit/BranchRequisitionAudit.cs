using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class BranchRequisitionAudit
	{
		public static AuditCollection Audit(BranchRequisition branchrequisition,BranchRequisition branchrequisitionOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (branchrequisition.mSalesDate != branchrequisitionOld.mSalesDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisition);
				audit.mField = "sales_date";
				audit.mOldValue = branchrequisitionOld.mSalesDate.ToString();
				audit.mNewValue = branchrequisition.mSalesDate.ToString();
				audit_collection.Add(audit);
			}

			if (branchrequisition.mBranchId != branchrequisitionOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisition);
				audit.mField = "branch_id";
				audit.mOldValue = branchrequisitionOld.mBranchId.ToString();
				audit.mNewValue = branchrequisition.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (branchrequisition.mLmmId != branchrequisitionOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisition);
				audit.mField = "lmm_id";
				audit.mOldValue = branchrequisitionOld.mLmmId.ToString();
				audit.mNewValue = branchrequisition.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (branchrequisition.mEmployeeId != branchrequisitionOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisition);
				audit.mField = "employee_id";
				audit.mOldValue = branchrequisitionOld.mEmployeeId.ToString();
				audit.mNewValue = branchrequisition.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (branchrequisition.mDateCreated != branchrequisitionOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisition);
				audit.mField = "date_created";
				audit.mOldValue = branchrequisitionOld.mDateCreated.ToString();
				audit.mNewValue = branchrequisition.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (branchrequisition.mDateUpdated != branchrequisitionOld.mDateUpdated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisition);
				audit.mField = "date_updated";
				audit.mOldValue = branchrequisitionOld.mDateUpdated.ToString();
				audit.mNewValue = branchrequisition.mDateUpdated.ToString();
				audit_collection.Add(audit);
			}

			if (branchrequisition.mLmmRemarks != branchrequisitionOld.mLmmRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisition);
				audit.mField = "lmm_remarks";
				audit.mOldValue = branchrequisitionOld.mLmmRemarks;
				audit.mNewValue = branchrequisition.mLmmRemarks;
				audit_collection.Add(audit);
			}

			if (branchrequisition.mEmployeeRemarks != branchrequisitionOld.mEmployeeRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisition);
				audit.mField = "employee_remarks";
				audit.mOldValue = branchrequisitionOld.mEmployeeRemarks;
				audit.mNewValue = branchrequisition.mEmployeeRemarks;
				audit_collection.Add(audit);
			}

			if (branchrequisition.mDatestamp != branchrequisitionOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisition);
				audit.mField = "datestamp";
				audit.mOldValue = branchrequisitionOld.mDatestamp.ToString();
				audit.mNewValue = branchrequisition.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			if (branchrequisition.mUserId != branchrequisitionOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisition);
				audit.mField = "user_id";
				audit.mOldValue = branchrequisitionOld.mUserId.ToString();
				audit.mNewValue = branchrequisition.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (branchrequisition.mCode != branchrequisitionOld.mCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, branchrequisition);
				audit.mField = "code";
				audit.mOldValue = branchrequisitionOld.mCode;
				audit.mNewValue = branchrequisition.mCode;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, BranchRequisition branchrequisition)
		{
			audit.mUserFullName = branchrequisition.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_BranchRequisition);
			audit.mRowId = branchrequisition.mId;
			audit.mAction = 2;
		}
	}
}