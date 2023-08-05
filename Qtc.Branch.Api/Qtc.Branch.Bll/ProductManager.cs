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
	public static class ProductManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ProductCollection GetList()
		{
			ProductCriteria product = new ProductCriteria();
			return GetList(product, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductCollection GetList(string sortExpression)
		{
			ProductCriteria product = new ProductCriteria();
			return GetList(product, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductCollection GetList(int startRowIndex, int maximumRows)
		{
			ProductCriteria product = new ProductCriteria();
			return GetList(product, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductCollection GetList(ProductCriteria productCriteria)
		{
			return GetList(productCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductCollection GetList(ProductCriteria productCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			ProductCollection myCollection = ProductDB.GetList(productCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new ProductComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new ProductCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(ProductCriteria productCriteria)
		{
			return ProductDB.SelectCountForGetList(productCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Product GetItem(int id)
		{
			Product product = ProductDB.GetItem(id);
			return product;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Product myProduct)
		{
			if (!myProduct.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid product. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				//if(myProduct.mId != 0)
				//	AuditUpdate(myProduct);

				int id = ProductDB.Save(myProduct);
				//if(myProduct.mId == 0)
				//	AuditInsert(myProduct, id);

				myProduct.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Product myProduct)
		{
			if (ProductDB.Delete(myProduct.mId))
			{
				AuditDelete(myProduct);
				return myProduct.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Product myProduct, int id)
		{
			AuditManager.AuditInsert(false, myProduct.mUserFullName,(int)(Tables.ptApi_Product),id,"Insert");
		}
		private static void AuditDelete( Product myProduct)
		{
			AuditManager.AuditDelete(false, myProduct.mUserFullName,(int)(Tables.ptApi_Product),myProduct.mId,"Delete");
		}
		private static void AuditUpdate( Product myProduct)
		{
			Product old_product = GetItem(myProduct.mId);
			AuditCollection audit_collection = ProductAudit.Audit(myProduct, old_product);
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
		private class ProductComparer : IComparer < Product >
		{
			private string _sortColumn;
			private bool _reverse;
			public ProductComparer(string sortExpression)
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

			public int Compare(Product x, Product y)
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