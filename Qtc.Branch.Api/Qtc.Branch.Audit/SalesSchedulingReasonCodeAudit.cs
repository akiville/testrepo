using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class SalesSchedulingReasonCodeAudit
	{
		public static AuditCollection Audit(SalesSchedulingReasonCode salesschedulingreasoncode,SalesSchedulingReasonCode salesschedulingreasoncodeOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (salesschedulingreasoncode.mName != salesschedulingreasoncodeOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingreasoncode);
				audit.mField = "name";
				audit.mOldValue = salesschedulingreasoncodeOld.mName;
				audit.mNewValue = salesschedulingreasoncode.mName;
				audit_collection.Add(audit);
			}

			if (salesschedulingreasoncode.mRemarks != salesschedulingreasoncodeOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingreasoncode);
				audit.mField = "remarks";
				audit.mOldValue = salesschedulingreasoncodeOld.mRemarks;
				audit.mNewValue = salesschedulingreasoncode.mRemarks;
				audit_collection.Add(audit);
			}

			if (salesschedulingreasoncode.mModuleId != salesschedulingreasoncodeOld.mModuleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, salesschedulingreasoncode);
				audit.mField = "module_id";
				audit.mOldValue = salesschedulingreasoncodeOld.mModuleId.ToString();
				audit.mNewValue = salesschedulingreasoncode.mModuleId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, SalesSchedulingReasonCode salesschedulingreasoncode)
		{
			audit.mUserFullName = salesschedulingreasoncode.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_SalesSchedulingReasonCode);
			audit.mRowId = salesschedulingreasoncode.mId;
			audit.mAction = 2;
		}
	}
}