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
	public static class RequestForIssuanceDetailManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RequestForIssuanceDetailCollection GetList()
		{
			RequestForIssuanceDetailCriteria requestforissuancedetail = new RequestForIssuanceDetailCriteria();
			return GetList(requestforissuancedetail, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForIssuanceDetailCollection GetList(string sortExpression)
		{
			RequestForIssuanceDetailCriteria requestforissuancedetail = new RequestForIssuanceDetailCriteria();
			return GetList(requestforissuancedetail, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForIssuanceDetailCollection GetList(int startRowIndex, int maximumRows)
		{
			RequestForIssuanceDetailCriteria requestforissuancedetail = new RequestForIssuanceDetailCriteria();
			return GetList(requestforissuancedetail, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForIssuanceDetailCollection GetList(RequestForIssuanceDetailCriteria requestforissuancedetailCriteria)
		{
			return GetList(requestforissuancedetailCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForIssuanceDetailCollection GetList(RequestForIssuanceDetailCriteria requestforissuancedetailCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RequestForIssuanceDetailCollection myCollection = RequestForIssuanceDetailDB.GetList(requestforissuancedetailCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RequestForIssuanceDetailComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RequestForIssuanceDetailCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RequestForIssuanceDetailCriteria requestforissuancedetailCriteria)
		{
			return RequestForIssuanceDetailDB.SelectCountForGetList(requestforissuancedetailCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForIssuanceDetail GetItem(int id)
		{
			RequestForIssuanceDetail requestforissuancedetail = RequestForIssuanceDetailDB.GetItem(id);
			return requestforissuancedetail;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RequestForIssuanceDetail myRequestForIssuanceDetail)
		{
			if (!myRequestForIssuanceDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid requestforissuancedetail. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRequestForIssuanceDetail.mId != 0)
					AuditUpdate(myRequestForIssuanceDetail);

				int id = RequestForIssuanceDetailDB.Save(myRequestForIssuanceDetail);
				if(myRequestForIssuanceDetail.mId == 0)
					AuditInsert(myRequestForIssuanceDetail, id);

				myRequestForIssuanceDetail.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RequestForIssuanceDetail myRequestForIssuanceDetail)
		{
			if (RequestForIssuanceDetailDB.Delete(myRequestForIssuanceDetail.mId))
			{
				AuditDelete(myRequestForIssuanceDetail);
				return myRequestForIssuanceDetail.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RequestForIssuanceDetail myRequestForIssuanceDetail, int id)
		{
			AuditManager.AuditInsert(false, myRequestForIssuanceDetail.mUserFullName,(int)(Tables.ptApi_RequestForIssuanceDetail),id,"Insert");
		}
		private static void AuditDelete( RequestForIssuanceDetail myRequestForIssuanceDetail)
		{
			AuditManager.AuditDelete(false, myRequestForIssuanceDetail.mUserFullName,(int)(Tables.ptApi_RequestForIssuanceDetail),myRequestForIssuanceDetail.mId,"Delete");
		}
		private static void AuditUpdate( RequestForIssuanceDetail myRequestForIssuanceDetail)
		{
			RequestForIssuanceDetail old_requestforissuancedetail = GetItem(myRequestForIssuanceDetail.mId);
			AuditCollection audit_collection = RequestForIssuanceDetailAudit.Audit(myRequestForIssuanceDetail, old_requestforissuancedetail);
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
		private class RequestForIssuanceDetailComparer : IComparer < RequestForIssuanceDetail >
		{
			private string _sortColumn;
			private bool _reverse;
			public RequestForIssuanceDetailComparer(string sortExpression)
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

			public int Compare(RequestForIssuanceDetail x, RequestForIssuanceDetail y)
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