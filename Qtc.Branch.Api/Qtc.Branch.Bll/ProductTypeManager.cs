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
	public static class ProductTypeManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ProductTypeCollection GetList()
		{
			ProductTypeCriteria producttype = new ProductTypeCriteria();
			return GetList(producttype, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductTypeCollection GetList(string sortExpression)
		{
			ProductTypeCriteria producttype = new ProductTypeCriteria();
			return GetList(producttype, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductTypeCollection GetList(int startRowIndex, int maximumRows)
		{
			ProductTypeCriteria producttype = new ProductTypeCriteria();
			return GetList(producttype, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductTypeCollection GetList(ProductTypeCriteria producttypeCriteria)
		{
			return GetList(producttypeCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductTypeCollection GetList(ProductTypeCriteria producttypeCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			ProductTypeCollection myCollection = ProductTypeDB.GetList(producttypeCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new ProductTypeComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new ProductTypeCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(ProductTypeCriteria producttypeCriteria)
		{
			return ProductTypeDB.SelectCountForGetList(producttypeCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductType GetItem(int id)
		{
			ProductType producttype = ProductTypeDB.GetItem(id);
			return producttype;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(ProductType myProductType)
		{
			if (!myProductType.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid producttype. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myProductType.mId != 0)
					AuditUpdate(myProductType);

				int id = ProductTypeDB.Save(myProductType);
				if(myProductType.mId == 0)
					AuditInsert(myProductType, id);

				myProductType.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(ProductType myProductType)
		{
			if (ProductTypeDB.Delete(myProductType.mId))
			{
				AuditDelete(myProductType);
				return myProductType.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(ProductType myProductType, int id)
		{
			AuditManager.AuditInsert(false, myProductType.mUserFullName,(int)(Tables.ptApi_ProductType),id,"Insert");
		}
		private static void AuditDelete( ProductType myProductType)
		{
			AuditManager.AuditDelete(false, myProductType.mUserFullName,(int)(Tables.ptApi_ProductType),myProductType.mId,"Delete");
		}
		private static void AuditUpdate( ProductType myProductType)
		{
			ProductType old_producttype = GetItem(myProductType.mId);
			AuditCollection audit_collection = ProductTypeAudit.Audit(myProductType, old_producttype);
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
		private class ProductTypeComparer : IComparer < ProductType >
		{
			private string _sortColumn;
			private bool _reverse;
			public ProductTypeComparer(string sortExpression)
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

			public int Compare(ProductType x, ProductType y)
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