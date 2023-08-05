using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class ReasonAudit
	{
		public static AuditCollection Audit(Reason reason,Reason reasonOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (reason.mName != reasonOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "name";
				audit.mOldValue = reasonOld.mName;
				audit.mNewValue = reason.mName;
				audit_collection.Add(audit);
			}

			if (reason.mFormId != reasonOld.mFormId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "form_id";
				audit.mOldValue = reasonOld.mFormId.ToString();
				audit.mNewValue = reason.mFormId.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mRfd != reasonOld.mRfd)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "rfd";
				audit.mOldValue = reasonOld.mRfd.ToString();
				audit.mNewValue = reason.mRfd.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mNcs != reasonOld.mNcs)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "ncs";
				audit.mOldValue = reasonOld.mNcs.ToString();
				audit.mNewValue = reason.mNcs.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mInventory != reasonOld.mInventory)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "inventory";
				audit.mOldValue = reasonOld.mInventory.ToString();
				audit.mNewValue = reason.mInventory.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mLogistics != reasonOld.mLogistics)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "logistics";
				audit.mOldValue = reasonOld.mLogistics.ToString();
				audit.mNewValue = reason.mLogistics.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mRepair != reasonOld.mRepair)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "repair";
				audit.mOldValue = reasonOld.mRepair.ToString();
				audit.mNewValue = reason.mRepair.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mActivity != reasonOld.mActivity)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "activity";
				audit.mOldValue = reasonOld.mActivity.ToString();
				audit.mNewValue = reason.mActivity.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mDdir != reasonOld.mDdir)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "ddir";
				audit.mOldValue = reasonOld.mDdir.ToString();
				audit.mNewValue = reason.mDdir.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mOrdering != reasonOld.mOrdering)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "ordering";
				audit.mOldValue = reasonOld.mOrdering.ToString();
				audit.mNewValue = reason.mOrdering.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mDdirNo != reasonOld.mDdirNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "ddir_no";
				audit.mOldValue = reasonOld.mDdirNo.ToString();
				audit.mNewValue = reason.mDdirNo.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mRpNo != reasonOld.mRpNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "rp_no";
				audit.mOldValue = reasonOld.mRpNo.ToString();
				audit.mNewValue = reason.mRpNo.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mMcBag != reasonOld.mMcBag)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "mc_bag";
				audit.mOldValue = reasonOld.mMcBag.ToString();
				audit.mNewValue = reason.mMcBag.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mLoe != reasonOld.mLoe)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "loe";
				audit.mOldValue = reasonOld.mLoe.ToString();
				audit.mNewValue = reason.mLoe.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mRma != reasonOld.mRma)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "rma";
				audit.mOldValue = reasonOld.mRma.ToString();
				audit.mNewValue = reason.mRma.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mOds != reasonOld.mOds)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "ods";
				audit.mOldValue = reasonOld.mOds.ToString();
				audit.mNewValue = reason.mOds.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mDdd != reasonOld.mDdd)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "ddd";
				audit.mOldValue = reasonOld.mDdd.ToString();
				audit.mNewValue = reason.mDdd.ToString();
				audit_collection.Add(audit);
			}

			if (reason.mDddRequiredIncident != reasonOld.mDddRequiredIncident)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, reason);
				audit.mField = "ddd_required_incident";
				audit.mOldValue = reasonOld.mDddRequiredIncident.ToString();
				audit.mNewValue = reason.mDddRequiredIncident.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Reason reason)
		{
			audit.mUserFullName = reason.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Reason);
			audit.mRowId = reason.mId;
			audit.mAction = 2;
		}
	}
}