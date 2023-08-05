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
	public static class EmployeeStatusEmploymentManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static EmployeeStatusEmploymentCollection GetList()
		{
			EmployeeStatusEmploymentCriteria employeestatusemployment = new EmployeeStatusEmploymentCriteria();
			return GetList(employeestatusemployment, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeStatusEmploymentCollection GetList(string sortExpression)
		{
			EmployeeStatusEmploymentCriteria employeestatusemployment = new EmployeeStatusEmploymentCriteria();
			return GetList(employeestatusemployment, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeStatusEmploymentCollection GetList(int startRowIndex, int maximumRows)
		{
			EmployeeStatusEmploymentCriteria employeestatusemployment = new EmployeeStatusEmploymentCriteria();
			return GetList(employeestatusemployment, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeStatusEmploymentCollection GetList(EmployeeStatusEmploymentCriteria employeestatusemploymentCriteria)
		{
			return GetList(employeestatusemploymentCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeStatusEmploymentCollection GetList(EmployeeStatusEmploymentCriteria employeestatusemploymentCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			EmployeeStatusEmploymentCollection myCollection = EmployeeStatusEmploymentDB.GetList(employeestatusemploymentCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new EmployeeStatusEmploymentComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new EmployeeStatusEmploymentCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(EmployeeStatusEmploymentCriteria employeestatusemploymentCriteria)
		{
			return EmployeeStatusEmploymentDB.SelectCountForGetList(employeestatusemploymentCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeStatusEmployment GetItem(int id)
		{
			EmployeeStatusEmployment employeestatusemployment = EmployeeStatusEmploymentDB.GetItem(id);
			return employeestatusemployment;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(EmployeeStatusEmployment myEmployeeStatusEmployment)
		{
			if (!myEmployeeStatusEmployment.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid employeestatusemployment. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myEmployeeStatusEmployment.mId != 0)
					AuditUpdate(myEmployeeStatusEmployment);

				int id = EmployeeStatusEmploymentDB.Save(myEmployeeStatusEmployment);
				if(myEmployeeStatusEmployment.mId == 0)
					AuditInsert(myEmployeeStatusEmployment, id);

				myEmployeeStatusEmployment.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(EmployeeStatusEmployment myEmployeeStatusEmployment)
		{
			if (EmployeeStatusEmploymentDB.Delete(myEmployeeStatusEmployment.mId))
			{
				AuditDelete(myEmployeeStatusEmployment);
				return myEmployeeStatusEmployment.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(EmployeeStatusEmployment myEmployeeStatusEmployment, int id)
		{
			AuditManager.AuditInsert(false, myEmployeeStatusEmployment.mUserFullName,(int)(Tables.ptApi_EmployeeStatusEmployment),id,"Insert");
		}
		private static void AuditDelete( EmployeeStatusEmployment myEmployeeStatusEmployment)
		{
			AuditManager.AuditDelete(false, myEmployeeStatusEmployment.mUserFullName,(int)(Tables.ptApi_EmployeeStatusEmployment),myEmployeeStatusEmployment.mId,"Delete");
		}
		private static void AuditUpdate( EmployeeStatusEmployment myEmployeeStatusEmployment)
		{
			EmployeeStatusEmployment old_employeestatusemployment = GetItem(myEmployeeStatusEmployment.mId);
			AuditCollection audit_collection = EmployeeStatusEmploymentAudit.Audit(myEmployeeStatusEmployment, old_employeestatusemployment);
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
		private class EmployeeStatusEmploymentComparer : IComparer < EmployeeStatusEmployment >
		{
			private string _sortColumn;
			private bool _reverse;
			public EmployeeStatusEmploymentComparer(string sortExpression)
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

			public int Compare(EmployeeStatusEmployment x, EmployeeStatusEmployment y)
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