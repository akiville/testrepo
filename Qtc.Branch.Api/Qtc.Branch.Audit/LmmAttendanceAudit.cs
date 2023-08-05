using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class LmmAttendanceAudit
	{
		public static AuditCollection Audit(LmmAttendance lmmattendance,LmmAttendance lmmattendanceOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (lmmattendance.mLmmId != lmmattendanceOld.mLmmId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendance);
				audit.mField = "lmm_id";
				audit.mOldValue = lmmattendanceOld.mLmmId.ToString();
				audit.mNewValue = lmmattendance.mLmmId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendance.mEmployeeId != lmmattendanceOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendance);
				audit.mField = "employee_id";
				audit.mOldValue = lmmattendanceOld.mEmployeeId.ToString();
				audit.mNewValue = lmmattendance.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendance.mCutoffId != lmmattendanceOld.mCutoffId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendance);
				audit.mField = "cutoff_id";
				audit.mOldValue = lmmattendanceOld.mCutoffId.ToString();
				audit.mNewValue = lmmattendance.mCutoffId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendance.mAttendanceTypeId != lmmattendanceOld.mAttendanceTypeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendance);
				audit.mField = "attendance_type_id";
				audit.mOldValue = lmmattendanceOld.mAttendanceTypeId.ToString();
				audit.mNewValue = lmmattendance.mAttendanceTypeId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendance.mBranchId != lmmattendanceOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendance);
				audit.mField = "branch_id";
				audit.mOldValue = lmmattendanceOld.mBranchId.ToString();
				audit.mNewValue = lmmattendance.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendance.mAttendanceDate != lmmattendanceOld.mAttendanceDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendance);
				audit.mField = "attendance_date";
				audit.mOldValue = lmmattendanceOld.mAttendanceDate.ToString();
				audit.mNewValue = lmmattendance.mAttendanceDate.ToString();
				audit_collection.Add(audit);
			}

			if (lmmattendance.mRemarks != lmmattendanceOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendance);
				audit.mField = "remarks";
				audit.mOldValue = lmmattendanceOld.mRemarks;
				audit.mNewValue = lmmattendance.mRemarks;
				audit_collection.Add(audit);
			}

			if (lmmattendance.mDatestamp != lmmattendanceOld.mDatestamp)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, lmmattendance);
				audit.mField = "datestamp";
				audit.mOldValue = lmmattendanceOld.mDatestamp.ToString();
				audit.mNewValue = lmmattendance.mDatestamp.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, LmmAttendance lmmattendance)
		{
			audit.mUserFullName = lmmattendance.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_LmmAttendance);
			audit.mRowId = lmmattendance.mId;
			audit.mAction = 2;
		}
	}
}