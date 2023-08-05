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
	public static class RequestForScheduleChangeManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RequestForScheduleChangeCollection GetList()
		{
			RequestForScheduleChangeCriteria requestforschedulechange = new RequestForScheduleChangeCriteria();
			return GetList(requestforschedulechange, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForScheduleChangeCollection GetList(string sortExpression)
		{
			RequestForScheduleChangeCriteria requestforschedulechange = new RequestForScheduleChangeCriteria();
			return GetList(requestforschedulechange, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForScheduleChangeCollection GetList(int startRowIndex, int maximumRows)
		{
			RequestForScheduleChangeCriteria requestforschedulechange = new RequestForScheduleChangeCriteria();
			return GetList(requestforschedulechange, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForScheduleChangeCollection GetList(RequestForScheduleChangeCriteria requestforschedulechangeCriteria)
		{
			return GetList(requestforschedulechangeCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForScheduleChangeCollection GetList(RequestForScheduleChangeCriteria requestforschedulechangeCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RequestForScheduleChangeCollection myCollection = RequestForScheduleChangeDB.GetList(requestforschedulechangeCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RequestForScheduleChangeComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RequestForScheduleChangeCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RequestForScheduleChangeCriteria requestforschedulechangeCriteria)
		{
			return RequestForScheduleChangeDB.SelectCountForGetList(requestforschedulechangeCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RequestForScheduleChange GetItem(int id)
		{
			RequestForScheduleChange requestforschedulechange = RequestForScheduleChangeDB.GetItem(id);
			return requestforschedulechange;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RequestForScheduleChange myRequestForScheduleChange)
		{
			if (!myRequestForScheduleChange.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid requestforschedulechange. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRequestForScheduleChange.mId != 0)
					AuditUpdate(myRequestForScheduleChange);

				int id = RequestForScheduleChangeDB.Save(myRequestForScheduleChange);
				if(myRequestForScheduleChange.mId == 0)
					AuditInsert(myRequestForScheduleChange, id);

				myRequestForScheduleChange.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RequestForScheduleChange myRequestForScheduleChange)
		{
			if (RequestForScheduleChangeDB.Delete(myRequestForScheduleChange.mId))
			{
				AuditDelete(myRequestForScheduleChange);
				return myRequestForScheduleChange.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RequestForScheduleChange myRequestForScheduleChange, int id)
		{
			AuditManager.AuditInsert(false, myRequestForScheduleChange.mUserFullName,(int)(Tables.ptApi_RequestForScheduleChange),id,"Insert");
		}
		private static void AuditDelete( RequestForScheduleChange myRequestForScheduleChange)
		{
			AuditManager.AuditDelete(false, myRequestForScheduleChange.mUserFullName,(int)(Tables.ptApi_RequestForScheduleChange),myRequestForScheduleChange.mId,"Delete");
		}
		private static void AuditUpdate( RequestForScheduleChange myRequestForScheduleChange)
		{
			RequestForScheduleChange old_requestforschedulechange = GetItem(myRequestForScheduleChange.mId);
			AuditCollection audit_collection = RequestForScheduleChangeAudit.Audit(myRequestForScheduleChange, old_requestforschedulechange);
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
		private class RequestForScheduleChangeComparer : IComparer < RequestForScheduleChange >
		{
			private string _sortColumn;
			private bool _reverse;
			public RequestForScheduleChangeComparer(string sortExpression)
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

			public int Compare(RequestForScheduleChange x, RequestForScheduleChange y)
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