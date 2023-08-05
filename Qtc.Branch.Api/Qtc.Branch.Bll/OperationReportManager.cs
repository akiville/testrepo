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
	public static class OperationReportManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static OperationReportCollection GetList()
		{
			OperationReportCriteria operationreport = new OperationReportCriteria();
			return GetList(operationreport, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static OperationReportCollection GetList(string sortExpression)
		{
			OperationReportCriteria operationreport = new OperationReportCriteria();
			return GetList(operationreport, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static OperationReportCollection GetList(int startRowIndex, int maximumRows)
		{
			OperationReportCriteria operationreport = new OperationReportCriteria();
			return GetList(operationreport, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static OperationReportCollection GetList(OperationReportCriteria operationreportCriteria)
		{
			return GetList(operationreportCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static OperationReportCollection GetList(OperationReportCriteria operationreportCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			OperationReportCollection myCollection = OperationReportDB.GetList(operationreportCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new OperationReportComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new OperationReportCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(OperationReportCriteria operationreportCriteria)
		{
			return OperationReportDB.SelectCountForGetList(operationreportCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static OperationReport GetItem(int id)
		{
			OperationReport operationreport = OperationReportDB.GetItem(id);
			return operationreport;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(OperationReport myOperationReport)
		{
			if (!myOperationReport.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid operationreport. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myOperationReport.mId != 0)
					AuditUpdate(myOperationReport);

				int id = OperationReportDB.Save(myOperationReport);
				if(myOperationReport.mId == 0)
					AuditInsert(myOperationReport, id);

				myOperationReport.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(OperationReport myOperationReport)
		{
			if (OperationReportDB.Delete(myOperationReport.mId))
			{
				AuditDelete(myOperationReport);
				return myOperationReport.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(OperationReport myOperationReport, int id)
		{
			AuditManager.AuditInsert(false, myOperationReport.mUserFullName,(int)(Tables.ptApi_OperationReport),id,"Insert");
		}
		private static void AuditDelete( OperationReport myOperationReport)
		{
			AuditManager.AuditDelete(false, myOperationReport.mUserFullName,(int)(Tables.ptApi_OperationReport),myOperationReport.mId,"Delete");
		}
		private static void AuditUpdate( OperationReport myOperationReport)
		{
			OperationReport old_operationreport = GetItem(myOperationReport.mId);
			AuditCollection audit_collection = OperationReportAudit.Audit(myOperationReport, old_operationreport);
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
		private class OperationReportComparer : IComparer < OperationReport >
		{
			private string _sortColumn;
			private bool _reverse;
			public OperationReportComparer(string sortExpression)
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

			public int Compare(OperationReport x, OperationReport y)
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