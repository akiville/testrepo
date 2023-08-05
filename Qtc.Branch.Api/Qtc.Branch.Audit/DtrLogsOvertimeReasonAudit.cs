using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DtrLogsOvertimeReasonAudit
	{
		public static AuditCollection Audit(DtrLogsOvertimeReason dtrlogsovertimereason,DtrLogsOvertimeReason dtrlogsovertimereasonOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (dtrlogsovertimereason.mName != dtrlogsovertimereasonOld.mName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertimereason);
				audit.mField = "name";
				audit.mOldValue = dtrlogsovertimereasonOld.mName;
				audit.mNewValue = dtrlogsovertimereason.mName;
				audit_collection.Add(audit);
			}

			if (dtrlogsovertimereason.mRemarks != dtrlogsovertimereasonOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertimereason);
				audit.mField = "remarks";
				audit.mOldValue = dtrlogsovertimereasonOld.mRemarks;
				audit.mNewValue = dtrlogsovertimereason.mRemarks;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, DtrLogsOvertimeReason dtrlogsovertimereason)
		{
			audit.mUserFullName = dtrlogsovertimereason.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_DtrLogsOvertimeReason);
			audit.mRowId = dtrlogsovertimereason.mId;
			audit.mAction = 2;
		}
	}
}