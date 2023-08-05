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
	public static class LogsManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static LogsCollection GetList()
		{
			LogsCriteria logs = new LogsCriteria();
			return GetList(logs, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LogsCollection GetList(string sortExpression)
		{
			LogsCriteria logs = new LogsCriteria();
			return GetList(logs, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LogsCollection GetList(int startRowIndex, int maximumRows)
		{
			LogsCriteria logs = new LogsCriteria();
			return GetList(logs, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LogsCollection GetList(LogsCriteria logsCriteria)
		{
			return GetList(logsCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LogsCollection GetList(LogsCriteria logsCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			LogsCollection myCollection = LogsDB.GetList(logsCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new LogsComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new LogsCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(LogsCriteria logsCriteria)
		{
			return LogsDB.SelectCountForGetList(logsCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Logs GetItem(int id)
		{
			Logs logs = LogsDB.GetItem(id);
			return logs;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Logs myLogs)
		{
			if (!myLogs.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid logs. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myLogs.mId != 0)
					AuditUpdate(myLogs);

				int id = LogsDB.Save(myLogs);
				if(myLogs.mId == 0)
					AuditInsert(myLogs, id);

				myLogs.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Logs myLogs)
		{
			if (LogsDB.Delete(myLogs.mId))
			{
				AuditDelete(myLogs);
				return myLogs.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Logs myLogs, int id)
		{
			AuditManager.AuditInsert(false, myLogs.mUserFullName,(int)(Tables.ptApi_Logs),id,"Insert");
		}
		private static void AuditDelete( Logs myLogs)
		{
			AuditManager.AuditDelete(false, myLogs.mUserFullName,(int)(Tables.ptApi_Logs),myLogs.mId,"Delete");
		}
		private static void AuditUpdate( Logs myLogs)
		{
			Logs old_logs = GetItem(myLogs.mId);
			AuditCollection audit_collection = LogsAudit.Audit(myLogs, old_logs);
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
		private class LogsComparer : IComparer < Logs >
		{
			private string _sortColumn;
			private bool _reverse;
			public LogsComparer(string sortExpression)
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

			public int Compare(Logs x, Logs y)
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