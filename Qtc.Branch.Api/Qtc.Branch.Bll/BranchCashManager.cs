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
	public static class BranchCashManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static BranchCashCollection GetList()
		{
			BranchCashCriteria branchcash = new BranchCashCriteria();
			return GetList(branchcash, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchCashCollection GetList(string sortExpression)
		{
			BranchCashCriteria branchcash = new BranchCashCriteria();
			return GetList(branchcash, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchCashCollection GetList(int startRowIndex, int maximumRows)
		{
			BranchCashCriteria branchcash = new BranchCashCriteria();
			return GetList(branchcash, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchCashCollection GetList(BranchCashCriteria branchcashCriteria)
		{
			return GetList(branchcashCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchCashCollection GetList(BranchCashCriteria branchcashCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			BranchCashCollection myCollection = BranchCashDB.GetList(branchcashCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new BranchCashComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new BranchCashCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(BranchCashCriteria branchcashCriteria)
		{
			return BranchCashDB.SelectCountForGetList(branchcashCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchCash GetItem(int id)
		{
			BranchCash branchcash = BranchCashDB.GetItem(id);
			return branchcash;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(BranchCash myBranchCash)
		{
			if (!myBranchCash.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid branchcash. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myBranchCash.mId != 0)
					AuditUpdate(myBranchCash);

				int id = BranchCashDB.Save(myBranchCash);
				if(myBranchCash.mId == 0)
					AuditInsert(myBranchCash, id);

				myBranchCash.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(BranchCash myBranchCash)
		{
			if (BranchCashDB.Delete(myBranchCash.mId))
			{
				AuditDelete(myBranchCash);
				return myBranchCash.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(BranchCash myBranchCash, int id)
		{
			AuditManager.AuditInsert(false, myBranchCash.mUserFullName,(int)(Tables.ptApi_BranchCash),id,"Insert");
		}
		private static void AuditDelete( BranchCash myBranchCash)
		{
			AuditManager.AuditDelete(false, myBranchCash.mUserFullName,(int)(Tables.ptApi_BranchCash),myBranchCash.mId,"Delete");
		}
		private static void AuditUpdate( BranchCash myBranchCash)
		{
			BranchCash old_branchcash = GetItem(myBranchCash.mId);
			AuditCollection audit_collection = BranchCashAudit.Audit(myBranchCash, old_branchcash);
			if (audit_collection != null)
			{
				foreach (BusinessEntities.Audit audit in audit_collection)
				{
					AuditManager.Save( audit);
				}
			}
		}
		#endregion

		#region IComparable
		private class BranchCashComparer : IComparer < BranchCash >
		{
			private string _sortColumn;
			private bool _reverse;
			public BranchCashComparer(string sortExpression)
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

			public int Compare(BranchCash x, BranchCash y)
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