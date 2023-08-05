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
	public static class DtrLogsOvertimeReasonManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DtrLogsOvertimeReasonCollection GetList()
		{
			DtrLogsOvertimeReasonCriteria dtrlogsovertimereason = new DtrLogsOvertimeReasonCriteria();
			return GetList(dtrlogsovertimereason, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DtrLogsOvertimeReasonCollection GetList(string sortExpression)
		{
			DtrLogsOvertimeReasonCriteria dtrlogsovertimereason = new DtrLogsOvertimeReasonCriteria();
			return GetList(dtrlogsovertimereason, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DtrLogsOvertimeReasonCollection GetList(int startRowIndex, int maximumRows)
		{
			DtrLogsOvertimeReasonCriteria dtrlogsovertimereason = new DtrLogsOvertimeReasonCriteria();
			return GetList(dtrlogsovertimereason, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DtrLogsOvertimeReasonCollection GetList(DtrLogsOvertimeReasonCriteria dtrlogsovertimereasonCriteria)
		{
			return GetList(dtrlogsovertimereasonCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DtrLogsOvertimeReasonCollection GetList(DtrLogsOvertimeReasonCriteria dtrlogsovertimereasonCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DtrLogsOvertimeReasonCollection myCollection = DtrLogsOvertimeReasonDB.GetList(dtrlogsovertimereasonCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DtrLogsOvertimeReasonComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DtrLogsOvertimeReasonCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DtrLogsOvertimeReasonCriteria dtrlogsovertimereasonCriteria)
		{
			return DtrLogsOvertimeReasonDB.SelectCountForGetList(dtrlogsovertimereasonCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DtrLogsOvertimeReason GetItem(int id)
		{
			DtrLogsOvertimeReason dtrlogsovertimereason = DtrLogsOvertimeReasonDB.GetItem(id);
			return dtrlogsovertimereason;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(DtrLogsOvertimeReason myDtrLogsOvertimeReason)
		{
			if (!myDtrLogsOvertimeReason.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid dtrlogsovertimereason. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDtrLogsOvertimeReason.mId != 0)
					AuditUpdate(myDtrLogsOvertimeReason);

				int id = DtrLogsOvertimeReasonDB.Save(myDtrLogsOvertimeReason);
				if(myDtrLogsOvertimeReason.mId == 0)
					AuditInsert(myDtrLogsOvertimeReason, id);

				myDtrLogsOvertimeReason.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(DtrLogsOvertimeReason myDtrLogsOvertimeReason)
		{
			if (DtrLogsOvertimeReasonDB.Delete(myDtrLogsOvertimeReason.mId))
			{
				AuditDelete(myDtrLogsOvertimeReason);
				return myDtrLogsOvertimeReason.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(DtrLogsOvertimeReason myDtrLogsOvertimeReason, int id)
		{
			AuditManager.AuditInsert(false, myDtrLogsOvertimeReason.mUserFullName,(int)(Tables.ptApi_DtrLogsOvertimeReason),id,"Insert");
		}
		private static void AuditDelete( DtrLogsOvertimeReason myDtrLogsOvertimeReason)
		{
			AuditManager.AuditDelete(false, myDtrLogsOvertimeReason.mUserFullName,(int)(Tables.ptApi_DtrLogsOvertimeReason),myDtrLogsOvertimeReason.mId,"Delete");
		}
		private static void AuditUpdate( DtrLogsOvertimeReason myDtrLogsOvertimeReason)
		{
			DtrLogsOvertimeReason old_dtrlogsovertimereason = GetItem(myDtrLogsOvertimeReason.mId);
			AuditCollection audit_collection = DtrLogsOvertimeReasonAudit.Audit(myDtrLogsOvertimeReason, old_dtrlogsovertimereason);
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
		private class DtrLogsOvertimeReasonComparer : IComparer < DtrLogsOvertimeReason >
		{
			private string _sortColumn;
			private bool _reverse;
			public DtrLogsOvertimeReasonComparer(string sortExpression)
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

			public int Compare(DtrLogsOvertimeReason x, DtrLogsOvertimeReason y)
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