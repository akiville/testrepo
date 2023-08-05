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
	public static class BranchProductManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static BranchProductCollection GetList()
		{
			BranchProductCriteria branchproduct = new BranchProductCriteria();
			return GetList(branchproduct, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchProductCollection GetList(string sortExpression)
		{
			BranchProductCriteria branchproduct = new BranchProductCriteria();
			return GetList(branchproduct, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchProductCollection GetList(int startRowIndex, int maximumRows)
		{
			BranchProductCriteria branchproduct = new BranchProductCriteria();
			return GetList(branchproduct, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchProductCollection GetList(BranchProductCriteria branchproductCriteria)
		{
			return GetList(branchproductCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchProductCollection GetList(BranchProductCriteria branchproductCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			BranchProductCollection myCollection = BranchProductDB.GetList(branchproductCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new BranchProductComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new BranchProductCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(BranchProductCriteria branchproductCriteria)
		{
			return BranchProductDB.SelectCountForGetList(branchproductCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchProduct GetItem(int id)
		{
			BranchProduct branchproduct = BranchProductDB.GetItem(id);
			return branchproduct;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(BranchProduct myBranchProduct)
		{
			if (!myBranchProduct.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid branchproduct. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myBranchProduct.mId != 0)
					AuditUpdate(myBranchProduct);

				int id = BranchProductDB.Save(myBranchProduct);
				if(myBranchProduct.mId == 0)
					AuditInsert(myBranchProduct, id);

				myBranchProduct.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(BranchProduct myBranchProduct)
		{
			if (BranchProductDB.Delete(myBranchProduct.mId))
			{
				AuditDelete(myBranchProduct);
				return myBranchProduct.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(BranchProduct myBranchProduct, int id)
		{
			AuditManager.AuditInsert(false, myBranchProduct.mUserFullName,(int)(Tables.ptApi_BranchProduct),id,"Insert");
		}
		private static void AuditDelete( BranchProduct myBranchProduct)
		{
			AuditManager.AuditDelete(false, myBranchProduct.mUserFullName,(int)(Tables.ptApi_BranchProduct),myBranchProduct.mId,"Delete");
		}
		private static void AuditUpdate( BranchProduct myBranchProduct)
		{
			BranchProduct old_branchproduct = GetItem(myBranchProduct.mId);
			AuditCollection audit_collection = BranchProductAudit.Audit(myBranchProduct, old_branchproduct);
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
		private class BranchProductComparer : IComparer < BranchProduct >
		{
			private string _sortColumn;
			private bool _reverse;
			public BranchProductComparer(string sortExpression)
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

			public int Compare(BranchProduct x, BranchProduct y)
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