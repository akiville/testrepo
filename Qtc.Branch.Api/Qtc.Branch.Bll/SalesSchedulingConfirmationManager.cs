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
	public static class SalesSchedulingConfirmationManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static SalesSchedulingConfirmationCollection GetList()
		{
			SalesSchedulingConfirmationCriteria salesschedulingconfirmation = new SalesSchedulingConfirmationCriteria();
			return GetList(salesschedulingconfirmation, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingConfirmationCollection GetList(string sortExpression)
		{
			SalesSchedulingConfirmationCriteria salesschedulingconfirmation = new SalesSchedulingConfirmationCriteria();
			return GetList(salesschedulingconfirmation, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingConfirmationCollection GetList(int startRowIndex, int maximumRows)
		{
			SalesSchedulingConfirmationCriteria salesschedulingconfirmation = new SalesSchedulingConfirmationCriteria();
			return GetList(salesschedulingconfirmation, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingConfirmationCollection GetList(SalesSchedulingConfirmationCriteria salesschedulingconfirmationCriteria)
		{
			return GetList(salesschedulingconfirmationCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingConfirmationCollection GetList(SalesSchedulingConfirmationCriteria salesschedulingconfirmationCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			SalesSchedulingConfirmationCollection myCollection = SalesSchedulingConfirmationDB.GetList(salesschedulingconfirmationCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new SalesSchedulingConfirmationComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new SalesSchedulingConfirmationCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(SalesSchedulingConfirmationCriteria salesschedulingconfirmationCriteria)
		{
			return SalesSchedulingConfirmationDB.SelectCountForGetList(salesschedulingconfirmationCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingConfirmation GetItem(int id)
		{
			SalesSchedulingConfirmation salesschedulingconfirmation = SalesSchedulingConfirmationDB.GetItem(id);
			return salesschedulingconfirmation;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(SalesSchedulingConfirmation mySalesSchedulingConfirmation)
		{
			if (!mySalesSchedulingConfirmation.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid salesschedulingconfirmation. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(mySalesSchedulingConfirmation.mId != 0)
					AuditUpdate(mySalesSchedulingConfirmation);

				int id = SalesSchedulingConfirmationDB.Save(mySalesSchedulingConfirmation);
				if(mySalesSchedulingConfirmation.mId == 0)
					AuditInsert(mySalesSchedulingConfirmation, id);

				mySalesSchedulingConfirmation.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(SalesSchedulingConfirmation mySalesSchedulingConfirmation)
		{
			if (SalesSchedulingConfirmationDB.Delete(mySalesSchedulingConfirmation.mId))
			{
				AuditDelete(mySalesSchedulingConfirmation);
				return mySalesSchedulingConfirmation.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(SalesSchedulingConfirmation mySalesSchedulingConfirmation, int id)
		{
			AuditManager.AuditInsert(false, mySalesSchedulingConfirmation.mUserFullName,(int)(Tables.ptApi_SalesSchedulingConfirmation),id,"Insert");
		}
		private static void AuditDelete( SalesSchedulingConfirmation mySalesSchedulingConfirmation)
		{
			AuditManager.AuditDelete(false, mySalesSchedulingConfirmation.mUserFullName,(int)(Tables.ptApi_SalesSchedulingConfirmation),mySalesSchedulingConfirmation.mId,"Delete");
		}
		private static void AuditUpdate( SalesSchedulingConfirmation mySalesSchedulingConfirmation)
		{
			SalesSchedulingConfirmation old_salesschedulingconfirmation = GetItem(mySalesSchedulingConfirmation.mId);
			AuditCollection audit_collection = SalesSchedulingConfirmationAudit.Audit(mySalesSchedulingConfirmation, old_salesschedulingconfirmation);
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
		private class SalesSchedulingConfirmationComparer : IComparer < SalesSchedulingConfirmation >
		{
			private string _sortColumn;
			private bool _reverse;
			public SalesSchedulingConfirmationComparer(string sortExpression)
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

			public int Compare(SalesSchedulingConfirmation x, SalesSchedulingConfirmation y)
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