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
	public static class DtrLogsOvertimeManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DtrLogsOvertimeCollection GetList()
		{
			DtrLogsOvertimeCriteria dtrlogsovertime = new DtrLogsOvertimeCriteria();
			return GetList(dtrlogsovertime, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DtrLogsOvertimeCollection GetList(string sortExpression)
		{
			DtrLogsOvertimeCriteria dtrlogsovertime = new DtrLogsOvertimeCriteria();
			return GetList(dtrlogsovertime, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DtrLogsOvertimeCollection GetList(int startRowIndex, int maximumRows)
		{
			DtrLogsOvertimeCriteria dtrlogsovertime = new DtrLogsOvertimeCriteria();
			return GetList(dtrlogsovertime, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DtrLogsOvertimeCollection GetList(DtrLogsOvertimeCriteria dtrlogsovertimeCriteria)
		{
			return GetList(dtrlogsovertimeCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DtrLogsOvertimeCollection GetList(DtrLogsOvertimeCriteria dtrlogsovertimeCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DtrLogsOvertimeCollection myCollection = DtrLogsOvertimeDB.GetList(dtrlogsovertimeCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DtrLogsOvertimeComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DtrLogsOvertimeCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DtrLogsOvertimeCriteria dtrlogsovertimeCriteria)
		{
			return DtrLogsOvertimeDB.SelectCountForGetList(dtrlogsovertimeCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DtrLogsOvertime GetItem(int id)
		{
			DtrLogsOvertime dtrlogsovertime = DtrLogsOvertimeDB.GetItem(id);
			return dtrlogsovertime;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(DtrLogsOvertime myDtrLogsOvertime)
		{
			if (!myDtrLogsOvertime.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid dtrlogsovertime. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDtrLogsOvertime.mId != 0)
					AuditUpdate(myDtrLogsOvertime);

				int id = DtrLogsOvertimeDB.Save(myDtrLogsOvertime);
				if(myDtrLogsOvertime.mId == 0)
					AuditInsert(myDtrLogsOvertime, id);

				myDtrLogsOvertime.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(DtrLogsOvertime myDtrLogsOvertime)
		{
			if (DtrLogsOvertimeDB.Delete(myDtrLogsOvertime.mId))
			{
				AuditDelete(myDtrLogsOvertime);
				return myDtrLogsOvertime.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(DtrLogsOvertime myDtrLogsOvertime, int id)
		{
			AuditManager.AuditInsert(false, myDtrLogsOvertime.mUserFullName,(int)(Tables.ptApi_DtrLogsOvertime),id,"Insert");
		}
		private static void AuditDelete( DtrLogsOvertime myDtrLogsOvertime)
		{
			AuditManager.AuditDelete(false, myDtrLogsOvertime.mUserFullName,(int)(Tables.ptApi_DtrLogsOvertime),myDtrLogsOvertime.mId,"Delete");
		}
		private static void AuditUpdate( DtrLogsOvertime myDtrLogsOvertime)
		{
			DtrLogsOvertime old_dtrlogsovertime = GetItem(myDtrLogsOvertime.mId);
			AuditCollection audit_collection = DtrLogsOvertimeAudit.Audit(myDtrLogsOvertime, old_dtrlogsovertime);
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
		private class DtrLogsOvertimeComparer : IComparer < DtrLogsOvertime >
		{
			private string _sortColumn;
			private bool _reverse;
			public DtrLogsOvertimeComparer(string sortExpression)
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

			public int Compare(DtrLogsOvertime x, DtrLogsOvertime y)
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