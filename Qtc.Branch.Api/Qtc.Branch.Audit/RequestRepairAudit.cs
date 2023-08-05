using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RequestRepairAudit
	{
		public static AuditCollection Audit(RequestRepair requestrepair,RequestRepair requestrepairOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (requestrepair.mRequestRepairId != requestrepairOld.mRequestRepairId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "request_repair_id";
				audit.mOldValue = requestrepairOld.mRequestRepairId.ToString();
				audit.mNewValue = requestrepair.mRequestRepairId.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mNumber != requestrepairOld.mNumber)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "number";
				audit.mOldValue = requestrepairOld.mNumber.ToString();
				audit.mNewValue = requestrepair.mNumber.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mDate != requestrepairOld.mDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "date";
				audit.mOldValue = requestrepairOld.mDate.ToString();
				audit.mNewValue = requestrepair.mDate.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mDateIncident != requestrepairOld.mDateIncident)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "date_incident";
				audit.mOldValue = requestrepairOld.mDateIncident.ToString();
				audit.mNewValue = requestrepair.mDateIncident.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mBranchId != requestrepairOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "branch_id";
				audit.mOldValue = requestrepairOld.mBranchId.ToString();
				audit.mNewValue = requestrepair.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mPlanned != requestrepairOld.mPlanned)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "planned";
				audit.mOldValue = requestrepairOld.mPlanned.ToString();
				audit.mNewValue = requestrepair.mPlanned.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mContractor != requestrepairOld.mContractor)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "contractor";
				audit.mOldValue = requestrepairOld.mContractor.ToString();
				audit.mNewValue = requestrepair.mContractor.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mRequestedById != requestrepairOld.mRequestedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "requested_by_id";
				audit.mOldValue = requestrepairOld.mRequestedById.ToString();
				audit.mNewValue = requestrepair.mRequestedById.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mMcId != requestrepairOld.mMcId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "mc_id";
				audit.mOldValue = requestrepairOld.mMcId.ToString();
				audit.mNewValue = requestrepair.mMcId.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mApprovedById != requestrepairOld.mApprovedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "approved_by_id";
				audit.mOldValue = requestrepairOld.mApprovedById.ToString();
				audit.mNewValue = requestrepair.mApprovedById.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mCodeId != requestrepairOld.mCodeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "code_id";
				audit.mOldValue = requestrepairOld.mCodeId.ToString();
				audit.mNewValue = requestrepair.mCodeId.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mProductId != requestrepairOld.mProductId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "product_id";
				audit.mOldValue = requestrepairOld.mProductId.ToString();
				audit.mNewValue = requestrepair.mProductId.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mProductSerialId != requestrepairOld.mProductSerialId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "product_serial_id";
				audit.mOldValue = requestrepairOld.mProductSerialId.ToString();
				audit.mNewValue = requestrepair.mProductSerialId.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mReasonId != requestrepairOld.mReasonId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "reason_id";
				audit.mOldValue = requestrepairOld.mReasonId.ToString();
				audit.mNewValue = requestrepair.mReasonId.ToString();
				audit_collection.Add(audit);
			}

			if (requestrepair.mComplaint != requestrepairOld.mComplaint)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, requestrepair);
				audit.mField = "complaint";
				audit.mOldValue = requestrepairOld.mComplaint;
				audit.mNewValue = requestrepair.mComplaint;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RequestRepair requestrepair)
		{
			audit.mUserFullName = requestrepair.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RequestRepair);
			audit.mRowId = requestrepair.mId;
			audit.mAction = 2;
		}
	}
}