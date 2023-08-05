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
	public static class NotifyLmmForTransferedSalesEmployeeManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static NotifyLmmForTransferedSalesEmployeeCollection GetList()
		{
			NotifyLmmForTransferedSalesEmployeeCriteria notifylmmfortransferedsalesemployee = new NotifyLmmForTransferedSalesEmployeeCriteria();
			return GetList(notifylmmfortransferedsalesemployee, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NotifyLmmForTransferedSalesEmployeeCollection GetList(string sortExpression)
		{
			NotifyLmmForTransferedSalesEmployeeCriteria notifylmmfortransferedsalesemployee = new NotifyLmmForTransferedSalesEmployeeCriteria();
			return GetList(notifylmmfortransferedsalesemployee, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NotifyLmmForTransferedSalesEmployeeCollection GetList(int startRowIndex, int maximumRows)
		{
			NotifyLmmForTransferedSalesEmployeeCriteria notifylmmfortransferedsalesemployee = new NotifyLmmForTransferedSalesEmployeeCriteria();
			return GetList(notifylmmfortransferedsalesemployee, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NotifyLmmForTransferedSalesEmployeeCollection GetList(NotifyLmmForTransferedSalesEmployeeCriteria notifylmmfortransferedsalesemployeeCriteria)
		{
			return GetList(notifylmmfortransferedsalesemployeeCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NotifyLmmForTransferedSalesEmployeeCollection GetList(NotifyLmmForTransferedSalesEmployeeCriteria notifylmmfortransferedsalesemployeeCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			NotifyLmmForTransferedSalesEmployeeCollection myCollection = NotifyLmmForTransferedSalesEmployeeDB.GetList(notifylmmfortransferedsalesemployeeCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new NotifyLmmForTransferedSalesEmployeeComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new NotifyLmmForTransferedSalesEmployeeCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(NotifyLmmForTransferedSalesEmployeeCriteria notifylmmfortransferedsalesemployeeCriteria)
		{
			return NotifyLmmForTransferedSalesEmployeeDB.SelectCountForGetList(notifylmmfortransferedsalesemployeeCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NotifyLmmForTransferedSalesEmployee GetItem(int id)
		{
			NotifyLmmForTransferedSalesEmployee notifylmmfortransferedsalesemployee = NotifyLmmForTransferedSalesEmployeeDB.GetItem(id);
			return notifylmmfortransferedsalesemployee;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(NotifyLmmForTransferedSalesEmployee myNotifyLmmForTransferedSalesEmployee)
		{
			if (!myNotifyLmmForTransferedSalesEmployee.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid notifylmmfortransferedsalesemployee. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myNotifyLmmForTransferedSalesEmployee.mId != 0)
					AuditUpdate(myNotifyLmmForTransferedSalesEmployee);

				int id = NotifyLmmForTransferedSalesEmployeeDB.Save(myNotifyLmmForTransferedSalesEmployee);
				if(myNotifyLmmForTransferedSalesEmployee.mId == 0)
					AuditInsert(myNotifyLmmForTransferedSalesEmployee, id);

				myNotifyLmmForTransferedSalesEmployee.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(NotifyLmmForTransferedSalesEmployee myNotifyLmmForTransferedSalesEmployee)
		{
			if (NotifyLmmForTransferedSalesEmployeeDB.Delete(myNotifyLmmForTransferedSalesEmployee.mId))
			{
				AuditDelete(myNotifyLmmForTransferedSalesEmployee);
				return myNotifyLmmForTransferedSalesEmployee.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(NotifyLmmForTransferedSalesEmployee myNotifyLmmForTransferedSalesEmployee, int id)
		{
			AuditManager.AuditInsert(false, myNotifyLmmForTransferedSalesEmployee.mUserFullName,(int)(Tables.ptApi_NotifyLmmForTransferedSalesEmployee),id,"Insert");
		}
		private static void AuditDelete( NotifyLmmForTransferedSalesEmployee myNotifyLmmForTransferedSalesEmployee)
		{
			AuditManager.AuditDelete(false, myNotifyLmmForTransferedSalesEmployee.mUserFullName,(int)(Tables.ptApi_NotifyLmmForTransferedSalesEmployee),myNotifyLmmForTransferedSalesEmployee.mId,"Delete");
		}
		private static void AuditUpdate( NotifyLmmForTransferedSalesEmployee myNotifyLmmForTransferedSalesEmployee)
		{
			NotifyLmmForTransferedSalesEmployee old_notifylmmfortransferedsalesemployee = GetItem(myNotifyLmmForTransferedSalesEmployee.mId);
			AuditCollection audit_collection = NotifyLmmForTransferedSalesEmployeeAudit.Audit(myNotifyLmmForTransferedSalesEmployee, old_notifylmmfortransferedsalesemployee);
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
		private class NotifyLmmForTransferedSalesEmployeeComparer : IComparer < NotifyLmmForTransferedSalesEmployee >
		{
			private string _sortColumn;
			private bool _reverse;
			public NotifyLmmForTransferedSalesEmployeeComparer(string sortExpression)
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

			public int Compare(NotifyLmmForTransferedSalesEmployee x, NotifyLmmForTransferedSalesEmployee y)
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