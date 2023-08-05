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
	public static class ApiActionManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ApiActionCollection GetList()
		{
			ApiActionCriteria apiaction = new ApiActionCriteria();
			return GetList(apiaction, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiActionCollection GetList(string sortExpression)
		{
			ApiActionCriteria apiaction = new ApiActionCriteria();
			return GetList(apiaction, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiActionCollection GetList(int startRowIndex, int maximumRows)
		{
			ApiActionCriteria apiaction = new ApiActionCriteria();
			return GetList(apiaction, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiActionCollection GetList(ApiActionCriteria apiactionCriteria)
		{
			return GetList(apiactionCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiActionCollection GetList(ApiActionCriteria apiactionCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			ApiActionCollection myCollection = ApiActionDB.GetList(apiactionCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new ApiActionComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new ApiActionCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(ApiActionCriteria apiactionCriteria)
		{
			return ApiActionDB.SelectCountForGetList(apiactionCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiAction GetItem(int id)
		{
			ApiAction apiaction = ApiActionDB.GetItem(id);
			return apiaction;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(ApiAction myApiAction)
		{
			if (!myApiAction.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid apiaction. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myApiAction.mId != 0)
					AuditUpdate(myApiAction);

				int id = ApiActionDB.Save(myApiAction);
				if(myApiAction.mId == 0)
					AuditInsert(myApiAction, id);

				myApiAction.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(ApiAction myApiAction)
		{
			if (ApiActionDB.Delete(myApiAction.mId))
			{
				AuditDelete(myApiAction);
				return myApiAction.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(ApiAction myApiAction, int id)
		{
			AuditManager.AuditInsert(false, myApiAction.mUserFullName,(int)(Tables.ptApi_ApiAction),id,"Insert");
		}
		private static void AuditDelete( ApiAction myApiAction)
		{
			AuditManager.AuditDelete(false, myApiAction.mUserFullName,(int)(Tables.ptApi_ApiAction),myApiAction.mId,"Delete");
		}
		private static void AuditUpdate( ApiAction myApiAction)
		{
			ApiAction old_apiaction = GetItem(myApiAction.mId);
			AuditCollection audit_collection = ApiActionAudit.Audit(myApiAction, old_apiaction);
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
		private class ApiActionComparer : IComparer < ApiAction >
		{
			private string _sortColumn;
			private bool _reverse;
			public ApiActionComparer(string sortExpression)
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

			public int Compare(ApiAction x, ApiAction y)
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