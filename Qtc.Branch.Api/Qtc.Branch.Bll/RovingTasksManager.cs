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
	public static class RovingTasksManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingTasksCollection GetList()
		{
			RovingTasksCriteria rovingtasks = new RovingTasksCriteria();
			return GetList(rovingtasks, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingTasksCollection GetList(string sortExpression)
		{
			RovingTasksCriteria rovingtasks = new RovingTasksCriteria();
			return GetList(rovingtasks, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingTasksCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingTasksCriteria rovingtasks = new RovingTasksCriteria();
			return GetList(rovingtasks, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingTasksCollection GetList(RovingTasksCriteria rovingtasksCriteria)
		{
			return GetList(rovingtasksCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingTasksCollection GetList(RovingTasksCriteria rovingtasksCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingTasksCollection myCollection = RovingTasksDB.GetList(rovingtasksCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingTasksComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingTasksCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingTasksCriteria rovingtasksCriteria)
		{
			return RovingTasksDB.SelectCountForGetList(rovingtasksCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingTasks GetItem(int id)
		{
			RovingTasks rovingtasks = RovingTasksDB.GetItem(id);
			return rovingtasks;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingTasks myRovingTasks)
		{
			if (!myRovingTasks.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingtasks. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingTasks.mId != 0)
					AuditUpdate(myRovingTasks);

				int id = RovingTasksDB.Save(myRovingTasks);
				if(myRovingTasks.mId == 0)
					AuditInsert(myRovingTasks, id);

				myRovingTasks.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingTasks myRovingTasks)
		{
			if (RovingTasksDB.Delete(myRovingTasks.mId))
			{
				AuditDelete(myRovingTasks);
				return myRovingTasks.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingTasks myRovingTasks, int id)
		{
			AuditManager.AuditInsert(false, myRovingTasks.mUserFullName,(int)(Tables.ptApi_RovingTasks),id,"Insert");
		}
		private static void AuditDelete( RovingTasks myRovingTasks)
		{
			AuditManager.AuditDelete(false, myRovingTasks.mUserFullName,(int)(Tables.ptApi_RovingTasks),myRovingTasks.mId,"Delete");
		}
		private static void AuditUpdate( RovingTasks myRovingTasks)
		{
			RovingTasks old_rovingtasks = GetItem(myRovingTasks.mId);
			AuditCollection audit_collection = RovingTasksAudit.Audit(myRovingTasks, old_rovingtasks);
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
		private class RovingTasksComparer : IComparer < RovingTasks >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingTasksComparer(string sortExpression)
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

			public int Compare(RovingTasks x, RovingTasks y)
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