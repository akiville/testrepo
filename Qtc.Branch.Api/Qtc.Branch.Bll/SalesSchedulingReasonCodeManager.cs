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
	public static class SalesSchedulingReasonCodeManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static SalesSchedulingReasonCodeCollection GetList()
		{
			SalesSchedulingReasonCodeCriteria salesschedulingreasoncode = new SalesSchedulingReasonCodeCriteria();
			return GetList(salesschedulingreasoncode, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingReasonCodeCollection GetList(string sortExpression)
		{
			SalesSchedulingReasonCodeCriteria salesschedulingreasoncode = new SalesSchedulingReasonCodeCriteria();
			return GetList(salesschedulingreasoncode, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingReasonCodeCollection GetList(int startRowIndex, int maximumRows)
		{
			SalesSchedulingReasonCodeCriteria salesschedulingreasoncode = new SalesSchedulingReasonCodeCriteria();
			return GetList(salesschedulingreasoncode, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingReasonCodeCollection GetList(SalesSchedulingReasonCodeCriteria salesschedulingreasoncodeCriteria)
		{
			return GetList(salesschedulingreasoncodeCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingReasonCodeCollection GetList(SalesSchedulingReasonCodeCriteria salesschedulingreasoncodeCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			SalesSchedulingReasonCodeCollection myCollection = SalesSchedulingReasonCodeDB.GetList(salesschedulingreasoncodeCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new SalesSchedulingReasonCodeComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new SalesSchedulingReasonCodeCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(SalesSchedulingReasonCodeCriteria salesschedulingreasoncodeCriteria)
		{
			return SalesSchedulingReasonCodeDB.SelectCountForGetList(salesschedulingreasoncodeCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingReasonCode GetItem(int id)
		{
			SalesSchedulingReasonCode salesschedulingreasoncode = SalesSchedulingReasonCodeDB.GetItem(id);
			return salesschedulingreasoncode;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(SalesSchedulingReasonCode mySalesSchedulingReasonCode)
		{
			if (!mySalesSchedulingReasonCode.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid salesschedulingreasoncode. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(mySalesSchedulingReasonCode.mId != 0)
					AuditUpdate(mySalesSchedulingReasonCode);

				int id = SalesSchedulingReasonCodeDB.Save(mySalesSchedulingReasonCode);
				if(mySalesSchedulingReasonCode.mId == 0)
					AuditInsert(mySalesSchedulingReasonCode, id);

				mySalesSchedulingReasonCode.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(SalesSchedulingReasonCode mySalesSchedulingReasonCode)
		{
			if (SalesSchedulingReasonCodeDB.Delete(mySalesSchedulingReasonCode.mId))
			{
				AuditDelete(mySalesSchedulingReasonCode);
				return mySalesSchedulingReasonCode.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(SalesSchedulingReasonCode mySalesSchedulingReasonCode, int id)
		{
			AuditManager.AuditInsert(false, mySalesSchedulingReasonCode.mUserFullName,(int)(Tables.ptApi_SalesSchedulingReasonCode),id,"Insert");
		}
		private static void AuditDelete( SalesSchedulingReasonCode mySalesSchedulingReasonCode)
		{
			AuditManager.AuditDelete(false, mySalesSchedulingReasonCode.mUserFullName,(int)(Tables.ptApi_SalesSchedulingReasonCode),mySalesSchedulingReasonCode.mId,"Delete");
		}
		private static void AuditUpdate( SalesSchedulingReasonCode mySalesSchedulingReasonCode)
		{
			SalesSchedulingReasonCode old_salesschedulingreasoncode = GetItem(mySalesSchedulingReasonCode.mId);
			AuditCollection audit_collection = SalesSchedulingReasonCodeAudit.Audit(mySalesSchedulingReasonCode, old_salesschedulingreasoncode);
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
		private class SalesSchedulingReasonCodeComparer : IComparer < SalesSchedulingReasonCode >
		{
			private string _sortColumn;
			private bool _reverse;
			public SalesSchedulingReasonCodeComparer(string sortExpression)
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

			public int Compare(SalesSchedulingReasonCode x, SalesSchedulingReasonCode y)
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