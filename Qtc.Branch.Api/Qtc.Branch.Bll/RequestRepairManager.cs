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
	public static class RequestRepairManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RequestRepairCollection GetList()
		{
			RequestRepairCriteria requestrepair = new RequestRepairCriteria();
			return GetList(requestrepair, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestRepairCollection GetList(string sortExpression)
		{
			RequestRepairCriteria requestrepair = new RequestRepairCriteria();
			return GetList(requestrepair, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestRepairCollection GetList(int startRowIndex, int maximumRows)
		{
			RequestRepairCriteria requestrepair = new RequestRepairCriteria();
			return GetList(requestrepair, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestRepairCollection GetList(RequestRepairCriteria requestrepairCriteria)
		{
			return GetList(requestrepairCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestRepairCollection GetList(RequestRepairCriteria requestrepairCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RequestRepairCollection myCollection = RequestRepairDB.GetList(requestrepairCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RequestRepairComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RequestRepairCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RequestRepairCriteria requestrepairCriteria)
		{
			return RequestRepairDB.SelectCountForGetList(requestrepairCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestRepair GetItem(int id)
		{
			RequestRepair requestrepair = RequestRepairDB.GetItem(id);
			return requestrepair;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RequestRepair myRequestRepair)
		{
			if (!myRequestRepair.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid requestrepair. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRequestRepair.mId != 0)
					AuditUpdate(myRequestRepair);

				int id = RequestRepairDB.Save(myRequestRepair);
				if(myRequestRepair.mId == 0)
					AuditInsert(myRequestRepair, id);

				myRequestRepair.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RequestRepair myRequestRepair)
		{
			if (RequestRepairDB.Delete(myRequestRepair.mId))
			{
				AuditDelete(myRequestRepair);
				return myRequestRepair.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RequestRepair myRequestRepair, int id)
		{
			AuditManager.AuditInsert(false, myRequestRepair.mUserFullName,(int)(Tables.ptApi_RequestRepair),id,"Insert");
		}
		private static void AuditDelete( RequestRepair myRequestRepair)
		{
			AuditManager.AuditDelete(false, myRequestRepair.mUserFullName,(int)(Tables.ptApi_RequestRepair),myRequestRepair.mId,"Delete");
		}
		private static void AuditUpdate( RequestRepair myRequestRepair)
		{
			RequestRepair old_requestrepair = GetItem(myRequestRepair.mId);
			AuditCollection audit_collection = RequestRepairAudit.Audit(myRequestRepair, old_requestrepair);
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
		private class RequestRepairComparer : IComparer < RequestRepair >
		{
			private string _sortColumn;
			private bool _reverse;
			public RequestRepairComparer(string sortExpression)
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

			public int Compare(RequestRepair x, RequestRepair y)
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