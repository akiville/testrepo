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
	public static class BranchCashDetailsManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static BranchCashDetailsCollection GetList()
		{
			BranchCashDetailsCriteria branchcashdetails = new BranchCashDetailsCriteria();
			return GetList(branchcashdetails, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchCashDetailsCollection GetList(string sortExpression)
		{
			BranchCashDetailsCriteria branchcashdetails = new BranchCashDetailsCriteria();
			return GetList(branchcashdetails, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchCashDetailsCollection GetList(int startRowIndex, int maximumRows)
		{
			BranchCashDetailsCriteria branchcashdetails = new BranchCashDetailsCriteria();
			return GetList(branchcashdetails, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchCashDetailsCollection GetList(BranchCashDetailsCriteria branchcashdetailsCriteria)
		{
			return GetList(branchcashdetailsCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchCashDetailsCollection GetList(BranchCashDetailsCriteria branchcashdetailsCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			BranchCashDetailsCollection myCollection = BranchCashDetailsDB.GetList(branchcashdetailsCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new BranchCashDetailsComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new BranchCashDetailsCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(BranchCashDetailsCriteria branchcashdetailsCriteria)
		{
			return BranchCashDetailsDB.SelectCountForGetList(branchcashdetailsCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchCashDetails GetItem(int id)
		{
			BranchCashDetails branchcashdetails = BranchCashDetailsDB.GetItem(id);
			return branchcashdetails;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(BranchCashDetails myBranchCashDetails)
		{
			if (!myBranchCashDetails.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid branchcashdetails. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myBranchCashDetails.mId != 0)
					AuditUpdate(myBranchCashDetails);

				int id = BranchCashDetailsDB.Save(myBranchCashDetails);
				if(myBranchCashDetails.mId == 0)
					AuditInsert(myBranchCashDetails, id);

				myBranchCashDetails.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(BranchCashDetails myBranchCashDetails)
		{
			if (BranchCashDetailsDB.Delete(myBranchCashDetails.mId))
			{
				AuditDelete(myBranchCashDetails);
				return myBranchCashDetails.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(BranchCashDetails myBranchCashDetails, int id)
		{
			AuditManager.AuditInsert(false, myBranchCashDetails.mUserFullName,(int)(Tables.ptApi_BranchCashDetails),id,"Insert");
		}
		private static void AuditDelete( BranchCashDetails myBranchCashDetails)
		{
			AuditManager.AuditDelete(false, myBranchCashDetails.mUserFullName,(int)(Tables.ptApi_BranchCashDetails),myBranchCashDetails.mId,"Delete");
		}
		private static void AuditUpdate( BranchCashDetails myBranchCashDetails)
		{
			BranchCashDetails old_branchcashdetails = GetItem(myBranchCashDetails.mId);
			AuditCollection audit_collection = BranchCashDetailsAudit.Audit(myBranchCashDetails, old_branchcashdetails);
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
		private class BranchCashDetailsComparer : IComparer < BranchCashDetails >
		{
			private string _sortColumn;
			private bool _reverse;
			public BranchCashDetailsComparer(string sortExpression)
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

			public int Compare(BranchCashDetails x, BranchCashDetails y)
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