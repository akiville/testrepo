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
	public static class RequestForIssuanceManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RequestForIssuanceCollection GetList()
		{
			RequestForIssuanceCriteria requestforissuance = new RequestForIssuanceCriteria();
			return GetList(requestforissuance, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForIssuanceCollection GetList(string sortExpression)
		{
			RequestForIssuanceCriteria requestforissuance = new RequestForIssuanceCriteria();
			return GetList(requestforissuance, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForIssuanceCollection GetList(int startRowIndex, int maximumRows)
		{
			RequestForIssuanceCriteria requestforissuance = new RequestForIssuanceCriteria();
			return GetList(requestforissuance, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForIssuanceCollection GetList(RequestForIssuanceCriteria requestforissuanceCriteria)
		{
			return GetList(requestforissuanceCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForIssuanceCollection GetList(RequestForIssuanceCriteria requestforissuanceCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RequestForIssuanceCollection myCollection = RequestForIssuanceDB.GetList(requestforissuanceCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RequestForIssuanceComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RequestForIssuanceCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RequestForIssuanceCriteria requestforissuanceCriteria)
		{
			return RequestForIssuanceDB.SelectCountForGetList(requestforissuanceCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForIssuance GetItem(int id)
		{
			RequestForIssuance requestforissuance = RequestForIssuanceDB.GetItem(id);
			return requestforissuance;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RequestForIssuance myRequestForIssuance)
		{
			if (!myRequestForIssuance.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid requestforissuance. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRequestForIssuance.mId != 0)
					AuditUpdate(myRequestForIssuance);

				int id = RequestForIssuanceDB.Save(myRequestForIssuance);
				if(myRequestForIssuance.mId == 0)
					AuditInsert(myRequestForIssuance, id);

				myRequestForIssuance.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RequestForIssuance myRequestForIssuance)
		{
			if (RequestForIssuanceDB.Delete(myRequestForIssuance.mId))
			{
				AuditDelete(myRequestForIssuance);
				return myRequestForIssuance.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RequestForIssuance myRequestForIssuance, int id)
		{
			AuditManager.AuditInsert(false, myRequestForIssuance.mUserFullName,(int)(Tables.ptApi_RequestForIssuance),id,"Insert");
		}
		private static void AuditDelete( RequestForIssuance myRequestForIssuance)
		{
			AuditManager.AuditDelete(false, myRequestForIssuance.mUserFullName,(int)(Tables.ptApi_RequestForIssuance),myRequestForIssuance.mId,"Delete");
		}
		private static void AuditUpdate( RequestForIssuance myRequestForIssuance)
		{
			RequestForIssuance old_requestforissuance = GetItem(myRequestForIssuance.mId);
			AuditCollection audit_collection = RequestForIssuanceAudit.Audit(myRequestForIssuance, old_requestforissuance);
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
		private class RequestForIssuanceComparer : IComparer < RequestForIssuance >
		{
			private string _sortColumn;
			private bool _reverse;
			public RequestForIssuanceComparer(string sortExpression)
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

			public int Compare(RequestForIssuance x, RequestForIssuance y)
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