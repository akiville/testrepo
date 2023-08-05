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
	public static class ProductAvailManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ProductAvailCollection GetList()
		{
			ProductAvailCriteria productavail = new ProductAvailCriteria();
			return GetList(productavail, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductAvailCollection GetList(string sortExpression)
		{
			ProductAvailCriteria productavail = new ProductAvailCriteria();
			return GetList(productavail, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductAvailCollection GetList(int startRowIndex, int maximumRows)
		{
			ProductAvailCriteria productavail = new ProductAvailCriteria();
			return GetList(productavail, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductAvailCollection GetList(ProductAvailCriteria productavailCriteria)
		{
			return GetList(productavailCriteria, string.Empty, -1, -1);
		}

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ProductAvailCollection GetListForDownload(ProductAvailCriteria productavailCriteria)
        {
            return GetListForDownload(productavailCriteria, string.Empty, -1, -1);
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductAvailCollection GetList(ProductAvailCriteria productavailCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			ProductAvailCollection myCollection = ProductAvailDB.GetList(productavailCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new ProductAvailComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new ProductAvailCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static ProductAvailCollection GetListForDownload(ProductAvailCriteria productavailCriteria, string sortExpression, int startRowIndex, int maximumRows)
        {
            ProductAvailCollection myCollection = ProductAvailDB.GetListForDownload(productavailCriteria);
            if (!string.IsNullOrEmpty(sortExpression))
            {
                myCollection.Sort(new ProductAvailComparer(sortExpression));
            }
            if (startRowIndex >= 0 && maximumRows > 0)
            {
                return new ProductAvailCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
            }
            return myCollection;
        }

        public static int SelectCountForGetList(ProductAvailCriteria productavailCriteria)
		{
			return ProductAvailDB.SelectCountForGetList(productavailCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductAvail GetItem(int id)
		{
			ProductAvail productavail = ProductAvailDB.GetItem(id);
			return productavail;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(ProductAvail myProductAvail)
		{
			if (!myProductAvail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid productavail. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myProductAvail.mId != 0)
					AuditUpdate(myProductAvail);

				int id = ProductAvailDB.Save(myProductAvail);
				if(myProductAvail.mId == 0)
					AuditInsert(myProductAvail, id);

				myProductAvail.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(ProductAvail myProductAvail)
		{
			if (ProductAvailDB.Delete(myProductAvail.mId))
			{
				AuditDelete(myProductAvail);
				return myProductAvail.mId;
			}
			else
				return 0;
		}
        public static void UpdateInventory(int branch_id, DateTime inventory_date)
        {
            ProductAvailDB.Update(branch_id, inventory_date);
            
        }
		#endregion

		#region Audit
		private static void AuditInsert(ProductAvail myProductAvail, int id)
		{
			AuditManager.AuditInsert(false, myProductAvail.mUserFullName,(int)(Tables.ptApi_ProductAvail),id,"Insert");
		}
		private static void AuditDelete( ProductAvail myProductAvail)
		{
			AuditManager.AuditDelete(false, myProductAvail.mUserFullName,(int)(Tables.ptApi_ProductAvail),myProductAvail.mId,"Delete");
		}
		private static void AuditUpdate( ProductAvail myProductAvail)
		{
			ProductAvail old_productavail = GetItem(myProductAvail.mId);
			AuditCollection audit_collection = ProductAvailAudit.Audit(myProductAvail, old_productavail);
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
		private class ProductAvailComparer : IComparer < ProductAvail >
		{
			private string _sortColumn;
			private bool _reverse;
			public ProductAvailComparer(string sortExpression)
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

			public int Compare(ProductAvail x, ProductAvail y)
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