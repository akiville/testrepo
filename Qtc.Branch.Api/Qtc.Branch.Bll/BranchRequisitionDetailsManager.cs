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
	public static class BranchRequisitionDetailsManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static BranchRequisitionDetailsCollection GetList()
		{
			BranchRequisitionDetailsCriteria branchrequisitiondetails = new BranchRequisitionDetailsCriteria();
			return GetList(branchrequisitiondetails, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchRequisitionDetailsCollection GetList(string sortExpression)
		{
			BranchRequisitionDetailsCriteria branchrequisitiondetails = new BranchRequisitionDetailsCriteria();
			return GetList(branchrequisitiondetails, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchRequisitionDetailsCollection GetList(int startRowIndex, int maximumRows)
		{
			BranchRequisitionDetailsCriteria branchrequisitiondetails = new BranchRequisitionDetailsCriteria();
			return GetList(branchrequisitiondetails, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchRequisitionDetailsCollection GetList(BranchRequisitionDetailsCriteria branchrequisitiondetailsCriteria)
		{
			return GetList(branchrequisitiondetailsCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchRequisitionDetailsCollection GetList(BranchRequisitionDetailsCriteria branchrequisitiondetailsCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			BranchRequisitionDetailsCollection myCollection = BranchRequisitionDetailsDB.GetList(branchrequisitiondetailsCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new BranchRequisitionDetailsComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new BranchRequisitionDetailsCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(BranchRequisitionDetailsCriteria branchrequisitiondetailsCriteria)
		{
			return BranchRequisitionDetailsDB.SelectCountForGetList(branchrequisitiondetailsCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchRequisitionDetails GetItem(int id)
		{
			BranchRequisitionDetails branchrequisitiondetails = BranchRequisitionDetailsDB.GetItem(id);
			return branchrequisitiondetails;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(BranchRequisitionDetails myBranchRequisitionDetails)
		{
			if (!myBranchRequisitionDetails.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid branchrequisitiondetails. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myBranchRequisitionDetails.mId != 0)
					AuditUpdate(myBranchRequisitionDetails);

				int id = BranchRequisitionDetailsDB.Save(myBranchRequisitionDetails);
				if(myBranchRequisitionDetails.mId == 0)
					AuditInsert(myBranchRequisitionDetails, id);

				myBranchRequisitionDetails.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(BranchRequisitionDetails myBranchRequisitionDetails)
		{
			if (BranchRequisitionDetailsDB.Delete(myBranchRequisitionDetails.mId))
			{
				AuditDelete(myBranchRequisitionDetails);
				return myBranchRequisitionDetails.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(BranchRequisitionDetails myBranchRequisitionDetails, int id)
		{
			AuditManager.AuditInsert(false, myBranchRequisitionDetails.mUserFullName,(int)(Tables.ptApi_BranchRequisitionDetails),id,"Insert");
		}
		private static void AuditDelete( BranchRequisitionDetails myBranchRequisitionDetails)
		{
			AuditManager.AuditDelete(false, myBranchRequisitionDetails.mUserFullName,(int)(Tables.ptApi_BranchRequisitionDetails),myBranchRequisitionDetails.mId,"Delete");
		}
		private static void AuditUpdate( BranchRequisitionDetails myBranchRequisitionDetails)
		{
			BranchRequisitionDetails old_branchrequisitiondetails = GetItem(myBranchRequisitionDetails.mId);
			AuditCollection audit_collection = BranchRequisitionDetailsAudit.Audit(myBranchRequisitionDetails, old_branchrequisitiondetails);
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
		private class BranchRequisitionDetailsComparer : IComparer < BranchRequisitionDetails >
		{
			private string _sortColumn;
			private bool _reverse;
			public BranchRequisitionDetailsComparer(string sortExpression)
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

			public int Compare(BranchRequisitionDetails x, BranchRequisitionDetails y)
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