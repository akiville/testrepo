using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class OperationReportAudit
	{
		public static AuditCollection Audit(OperationReport operationreport,OperationReport operationreportOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (operationreport.mConcernDate != operationreportOld.mConcernDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, operationreport);
				audit.mField = "concern_date";
				audit.mOldValue = operationreportOld.mConcernDate.ToString();
				audit.mNewValue = operationreport.mConcernDate.ToString();
				audit_collection.Add(audit);
			}

			if (operationreport.mBranchId != operationreportOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, operationreport);
				audit.mField = "branch_id";
				audit.mOldValue = operationreportOld.mBranchId.ToString();
				audit.mNewValue = operationreport.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (operationreport.mLmId != operationreportOld.mLmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, operationreport);
				audit.mField = "lm_id";
				audit.mOldValue = operationreportOld.mLmId.ToString();
				audit.mNewValue = operationreport.mLmId.ToString();
				audit_collection.Add(audit);
			}

			if (operationreport.mEmployeeId != operationreportOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, operationreport);
				audit.mField = "employee_id";
				audit.mOldValue = operationreportOld.mEmployeeId.ToString();
				audit.mNewValue = operationreport.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (operationreport.mConcern != operationreportOld.mConcern)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, operationreport);
				audit.mField = "concern";
				audit.mOldValue = operationreportOld.mConcern;
				audit.mNewValue = operationreport.mConcern;
				audit_collection.Add(audit);
			}

			if (operationreport.mDateFiled != operationreportOld.mDateFiled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, operationreport);
				audit.mField = "date_filed";
				audit.mOldValue = operationreportOld.mDateFiled.ToString();
				audit.mNewValue = operationreport.mDateFiled.ToString();
				audit_collection.Add(audit);
			}

			if (operationreport.mImagePath != operationreportOld.mImagePath)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, operationreport);
				audit.mField = "image_path";
				audit.mOldValue = operationreportOld.mImagePath;
				audit.mNewValue = operationreport.mImagePath;
				audit_collection.Add(audit);
			}

			if (operationreport.mImageName != operationreportOld.mImageName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, operationreport);
				audit.mField = "image_name";
				audit.mOldValue = operationreportOld.mImageName;
				audit.mNewValue = operationreport.mImageName;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, OperationReport operationreport)
		{
			audit.mUserFullName = operationreport.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_OperationReport);
			audit.mRowId = operationreport.mId;
			audit.mAction = 2;
		}
	}
}