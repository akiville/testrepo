using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class EmployeeAudit
	{
		public static AuditCollection Audit(Employee employee,Employee employeeOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (employee.mEmployeeId != employeeOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employee);
				audit.mField = "employee_id";
				audit.mOldValue = employeeOld.mEmployeeId.ToString();
				audit.mNewValue = employee.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (employee.mLastname != employeeOld.mLastname)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employee);
				audit.mField = "lastname";
				audit.mOldValue = employeeOld.mLastname;
				audit.mNewValue = employee.mLastname;
				audit_collection.Add(audit);
			}

			if (employee.mFirstname != employeeOld.mFirstname)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employee);
				audit.mField = "firstname";
				audit.mOldValue = employeeOld.mFirstname;
				audit.mNewValue = employee.mFirstname;
				audit_collection.Add(audit);
			}

			if (employee.mMiddlename != employeeOld.mMiddlename)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employee);
				audit.mField = "middlename";
				audit.mOldValue = employeeOld.mMiddlename;
				audit.mNewValue = employee.mMiddlename;
				audit_collection.Add(audit);
			}

			if (employee.mPositionName != employeeOld.mPositionName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employee);
				audit.mField = "position_name";
				audit.mOldValue = employeeOld.mPositionName;
				audit.mNewValue = employee.mPositionName;
				audit_collection.Add(audit);
			}

			if (employee.mCellphoneNo != employeeOld.mCellphoneNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employee);
				audit.mField = "cellphone_no";
				audit.mOldValue = employeeOld.mCellphoneNo;
				audit.mNewValue = employee.mCellphoneNo;
				audit_collection.Add(audit);
			}

			if (employee.mResidentialAddress != employeeOld.mResidentialAddress)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employee);
				audit.mField = "residential_address";
				audit.mOldValue = employeeOld.mResidentialAddress;
				audit.mNewValue = employee.mResidentialAddress;
				audit_collection.Add(audit);
			}

			if (employee.mPassword != employeeOld.mPassword)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employee);
				audit.mField = "password";
				audit.mOldValue = employeeOld.mPassword;
				audit.mNewValue = employee.mPassword;
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Employee employee)
		{
			audit.mUserFullName = employee.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Employee);
			audit.mRowId = employee.mId;
			audit.mAction = 2;
		}
	}
}