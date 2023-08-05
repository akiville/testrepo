using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class EmployeeAttendanceAudit
	{
		public static AuditCollection Audit(EmployeeAttendance employeeattendance,EmployeeAttendance employeeattendanceOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (employeeattendance.mEmployeeId != employeeattendanceOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeeattendance);
				audit.mField = "employee_id";
				audit.mOldValue = employeeattendanceOld.mEmployeeId.ToString();
				audit.mNewValue = employeeattendance.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (employeeattendance.mAttendanceId != employeeattendanceOld.mAttendanceId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeeattendance);
				audit.mField = "attendance_id";
				audit.mOldValue = employeeattendanceOld.mAttendanceId.ToString();
				audit.mNewValue = employeeattendance.mAttendanceId.ToString();
				audit_collection.Add(audit);
			}

			if (employeeattendance.mAttendanceTrackingNo != employeeattendanceOld.mAttendanceTrackingNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeeattendance);
				audit.mField = "attendance_tracking_no";
				audit.mOldValue = employeeattendanceOld.mAttendanceTrackingNo.ToString();
				audit.mNewValue = employeeattendance.mAttendanceTrackingNo.ToString();
				audit_collection.Add(audit);
			}

			if (employeeattendance.mBranchCode != employeeattendanceOld.mBranchCode)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeeattendance);
				audit.mField = "branch_code";
				audit.mOldValue = employeeattendanceOld.mBranchCode.ToString();
				audit.mNewValue = employeeattendance.mBranchCode.ToString();
				audit_collection.Add(audit);
			}

			if (employeeattendance.mAttendanceDate != employeeattendanceOld.mAttendanceDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeeattendance);
				audit.mField = "attendance_date";
				audit.mOldValue = employeeattendanceOld.mAttendanceDate.ToString();
				audit.mNewValue = employeeattendance.mAttendanceDate.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, EmployeeAttendance employeeattendance)
		{
			audit.mUserFullName = employeeattendance.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_EmployeeAttendance);
			audit.mRowId = employeeattendance.mId;
			audit.mAction = 2;
		}
	}
}