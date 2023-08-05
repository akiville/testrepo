using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class NotifyLmmForTransferedSalesEmployeeAudit
	{
		public static AuditCollection Audit(NotifyLmmForTransferedSalesEmployee notifylmmfortransferedsalesemployee,NotifyLmmForTransferedSalesEmployee notifylmmfortransferedsalesemployeeOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (notifylmmfortransferedsalesemployee.mLmmFromId != notifylmmfortransferedsalesemployeeOld.mLmmFromId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, notifylmmfortransferedsalesemployee);
				audit.mField = "lmm_from_id";
				audit.mOldValue = notifylmmfortransferedsalesemployeeOld.mLmmFromId.ToString();
				audit.mNewValue = notifylmmfortransferedsalesemployee.mLmmFromId.ToString();
				audit_collection.Add(audit);
			}

			if (notifylmmfortransferedsalesemployee.mLmmToId != notifylmmfortransferedsalesemployeeOld.mLmmToId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, notifylmmfortransferedsalesemployee);
				audit.mField = "lmm_to_id";
				audit.mOldValue = notifylmmfortransferedsalesemployeeOld.mLmmToId.ToString();
				audit.mNewValue = notifylmmfortransferedsalesemployee.mLmmToId.ToString();
				audit_collection.Add(audit);
			}

			if (notifylmmfortransferedsalesemployee.mEmployeeId != notifylmmfortransferedsalesemployeeOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, notifylmmfortransferedsalesemployee);
				audit.mField = "employee_id";
				audit.mOldValue = notifylmmfortransferedsalesemployeeOld.mEmployeeId.ToString();
				audit.mNewValue = notifylmmfortransferedsalesemployee.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (notifylmmfortransferedsalesemployee.mDate != notifylmmfortransferedsalesemployeeOld.mDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, notifylmmfortransferedsalesemployee);
				audit.mField = "date";
				audit.mOldValue = notifylmmfortransferedsalesemployeeOld.mDate.ToString();
				audit.mNewValue = notifylmmfortransferedsalesemployee.mDate.ToString();
				audit_collection.Add(audit);
			}

			if (notifylmmfortransferedsalesemployee.mBranchId != notifylmmfortransferedsalesemployeeOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, notifylmmfortransferedsalesemployee);
				audit.mField = "branch_id";
				audit.mOldValue = notifylmmfortransferedsalesemployeeOld.mBranchId.ToString();
				audit.mNewValue = notifylmmfortransferedsalesemployee.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (notifylmmfortransferedsalesemployee.mBranchIdTo != notifylmmfortransferedsalesemployeeOld.mBranchIdTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, notifylmmfortransferedsalesemployee);
				audit.mField = "branch_id_to";
				audit.mOldValue = notifylmmfortransferedsalesemployeeOld.mBranchIdTo.ToString();
				audit.mNewValue = notifylmmfortransferedsalesemployee.mBranchIdTo.ToString();
				audit_collection.Add(audit);
			}

			if (notifylmmfortransferedsalesemployee.mRecordId != notifylmmfortransferedsalesemployeeOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, notifylmmfortransferedsalesemployee);
				audit.mField = "record_id";
				audit.mOldValue = notifylmmfortransferedsalesemployeeOld.mRecordId.ToString();
				audit.mNewValue = notifylmmfortransferedsalesemployee.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, NotifyLmmForTransferedSalesEmployee notifylmmfortransferedsalesemployee)
		{
			audit.mUserFullName = notifylmmfortransferedsalesemployee.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_NotifyLmmForTransferedSalesEmployee);
			audit.mRowId = notifylmmfortransferedsalesemployee.mId;
			audit.mAction = 2;
		}
	}
}