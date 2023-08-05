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
	public static class EmployeeAttendanceManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static EmployeeAttendanceCollection GetList()
		{
			EmployeeAttendanceCriteria employeeattendance = new EmployeeAttendanceCriteria();
			return GetList(employeeattendance, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeAttendanceCollection GetList(string sortExpression)
		{
			EmployeeAttendanceCriteria employeeattendance = new EmployeeAttendanceCriteria();
			return GetList(employeeattendance, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeAttendanceCollection GetList(int startRowIndex, int maximumRows)
		{
			EmployeeAttendanceCriteria employeeattendance = new EmployeeAttendanceCriteria();
			return GetList(employeeattendance, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeAttendanceCollection GetList(EmployeeAttendanceCriteria employeeattendanceCriteria)
		{
			return GetList(employeeattendanceCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeAttendanceCollection GetList(EmployeeAttendanceCriteria employeeattendanceCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			EmployeeAttendanceCollection myCollection = EmployeeAttendanceDB.GetList(employeeattendanceCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new EmployeeAttendanceComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new EmployeeAttendanceCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(EmployeeAttendanceCriteria employeeattendanceCriteria)
		{
			return EmployeeAttendanceDB.SelectCountForGetList(employeeattendanceCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeAttendance GetItem(int id)
		{
			EmployeeAttendance employeeattendance = EmployeeAttendanceDB.GetItem(id);
			return employeeattendance;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(EmployeeAttendance myEmployeeAttendance)
		{
			if (!myEmployeeAttendance.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid employeeattendance. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myEmployeeAttendance.mId != 0)
					AuditUpdate(myEmployeeAttendance);

				int id = EmployeeAttendanceDB.Save(myEmployeeAttendance);
				if(myEmployeeAttendance.mId == 0)
					AuditInsert(myEmployeeAttendance, id);

				myEmployeeAttendance.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(EmployeeAttendance myEmployeeAttendance)
		{
			if (EmployeeAttendanceDB.Delete(myEmployeeAttendance.mId))
			{
				AuditDelete(myEmployeeAttendance);
				return myEmployeeAttendance.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(EmployeeAttendance myEmployeeAttendance, int id)
		{
			AuditManager.AuditInsert(false, myEmployeeAttendance.mUserFullName,(int)(Tables.ptApi_EmployeeAttendance),id,"Insert");
		}
		private static void AuditDelete( EmployeeAttendance myEmployeeAttendance)
		{
			AuditManager.AuditDelete(false, myEmployeeAttendance.mUserFullName,(int)(Tables.ptApi_EmployeeAttendance),myEmployeeAttendance.mId,"Delete");
		}
		private static void AuditUpdate( EmployeeAttendance myEmployeeAttendance)
		{
			EmployeeAttendance old_employeeattendance = GetItem(myEmployeeAttendance.mId);
			AuditCollection audit_collection = EmployeeAttendanceAudit.Audit(myEmployeeAttendance, old_employeeattendance);
			if (audit_collection != null)
			{
				foreach (BusinessEntities.Audit audit in audit_collection)
				{
					AuditManager.Save( audit);
				}
			}
		}
		#endregion

		#region IComparable
		private class EmployeeAttendanceComparer : IComparer < EmployeeAttendance >
		{
			private string _sortColumn;
			private bool _reverse;
			public EmployeeAttendanceComparer(string sortExpression)
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

			public int Compare(EmployeeAttendance x, EmployeeAttendance y)
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