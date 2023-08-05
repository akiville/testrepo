using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Transactions;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Dal;
using Qtc.Branch.Validation;
using Qtc.Branch.Audit;

namespace Qtc.Branch.Bll
{
	[DataObjectAttribute()]
	public static class EmployeeManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static EmployeeCollection GetList()
		{
			EmployeeCriteria employee = new EmployeeCriteria();
			return GetList(employee, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeCollection GetList(string sortExpression)
		{
			EmployeeCriteria employee = new EmployeeCriteria();
			return GetList(employee, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeCollection GetList(int startRowIndex, int maximumRows)
		{
			EmployeeCriteria employee = new EmployeeCriteria();
			return GetList(employee, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeCollection GetList(EmployeeCriteria employeeCriteria)
		{
			return GetList(employeeCriteria, string.Empty, -1, -1);
		}

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EmployeeCollection GetListWithDate(EmployeeCriteria employeeCriteria)
        {
            return GetListWithDate(employeeCriteria, string.Empty, -1, -1);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeCollection GetList(EmployeeCriteria employeeCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			EmployeeCollection myCollection = EmployeeDB.GetList(employeeCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new EmployeeComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new EmployeeCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static EmployeeCollection GetListWithDate(EmployeeCriteria employeeCriteria, string sortExpression, int startRowIndex, int maximumRows)
        {
            EmployeeCollection myCollection = EmployeeDB.GetListWithDate(employeeCriteria);
            if (!string.IsNullOrEmpty(sortExpression))
            {
                myCollection.Sort(new EmployeeComparer(sortExpression));
            }
            if (startRowIndex >= 0 && maximumRows > 0)
            {
                return new EmployeeCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
            }
            return myCollection;
        }

        public static int SelectCountForGetList(EmployeeCriteria employeeCriteria)
		{
			return EmployeeDB.SelectCountForGetList(employeeCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Employee GetItem(int id)
		{
			Employee employee = EmployeeDB.GetItem(id);
			return employee;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Employee myEmployee)
		{
			if (!myEmployee.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid employee. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myEmployee.mId != 0)
					AuditUpdate(myEmployee);

				int id = EmployeeDB.Save(myEmployee);
				if(myEmployee.mId == 0)
					AuditInsert(myEmployee, id);

				myEmployee.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Employee myEmployee)
		{
			if (EmployeeDB.Delete(myEmployee.mId))
			{
				AuditDelete(myEmployee);
				return myEmployee.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Employee myEmployee, int id)
		{
			AuditManager.AuditInsert(false, myEmployee.mUserFullName,(int)(Tables.ptApi_Employee),id,"Insert");
		}
		private static void AuditDelete( Employee myEmployee)
		{
			AuditManager.AuditDelete(false, myEmployee.mUserFullName,(int)(Tables.ptApi_Employee),myEmployee.mId,"Delete");
		}
		private static void AuditUpdate( Employee myEmployee)
		{
			Employee old_employee = GetItem(myEmployee.mId);
			AuditCollection audit_collection = EmployeeAudit.Audit(myEmployee, old_employee);
			if (audit_collection != null)
			{
				foreach (BusinessEntities.Audit audit in audit_collection)
				{
					AuditManager.Save(audit);
				}
			}
		}
		#endregion

		#region IComparable
		private class EmployeeComparer : IComparer < Employee >
		{
			private string _sortColumn;
			private bool _reverse;
			public EmployeeComparer(string sortExpression)
			{
				if (string.IsNullOrEmpty(sortExpression))
				{
					sortExpression = "field_name desc";
				}
				_reverse = sortExpression.ToUpperInvariant().EndsWith(" desc", StringComparison.OrdinalIgnoreCase);
				if (_reverse)
				{
					_sortColumn = sortExpression.Substring(0, sortExpression.Length - 5);
				}
				else
				{
					_sortColumn = sortExpression;
				}
			}

			public int Compare(Employee x, Employee y)
			{
				int retVal = 0;
				switch (_sortColumn.ToUpperInvariant())
				{
					case "FIELD":
						retVal = string.Compare(x.mRemarks, y.mRemarks, StringComparison.OrdinalIgnoreCase);
						break;
				}
				int _reverseInt = 1;
				if ((_reverse))
				{
					_reverseInt = -1;
				}
				return (retVal * _reverseInt);
			}
		}
		#endregion
	}
}