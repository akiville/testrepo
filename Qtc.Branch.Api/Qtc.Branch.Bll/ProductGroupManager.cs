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
	public static class ProductGroupManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ProductGroupCollection GetList()
		{
			ProductGroupCriteria productgroup = new ProductGroupCriteria();
			return GetList(productgroup, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductGroupCollection GetList(string sortExpression)
		{
			ProductGroupCriteria productgroup = new ProductGroupCriteria();
			return GetList(productgroup, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductGroupCollection GetList(int startRowIndex, int maximumRows)
		{
			ProductGroupCriteria productgroup = new ProductGroupCriteria();
			return GetList(productgroup, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductGroupCollection GetList(ProductGroupCriteria productgroupCriteria)
		{
			return GetList(productgroupCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductGroupCollection GetList(ProductGroupCriteria productgroupCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			ProductGroupCollection myCollection = ProductGroupDB.GetList(productgroupCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new ProductGroupComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new ProductGroupCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(ProductGroupCriteria productgroupCriteria)
		{
			return ProductGroupDB.SelectCountForGetList(productgroupCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductGroup GetItem(int id)
		{
			ProductGroup productgroup = ProductGroupDB.GetItem(id);
			return productgroup;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(ProductGroup myProductGroup)
		{
			if (!myProductGroup.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid productgroup. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myProductGroup.mId != 0)
					AuditUpdate(myProductGroup);

				int id = ProductGroupDB.Save(myProductGroup);
				if(myProductGroup.mId == 0)
					AuditInsert(myProductGroup, id);

				myProductGroup.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(ProductGroup myProductGroup)
		{
			if (ProductGroupDB.Delete(myProductGroup.mId))
			{
				AuditDelete(myProductGroup);
				return myProductGroup.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(ProductGroup myProductGroup, int id)
		{
			AuditManager.AuditInsert(false, myProductGroup.mUserFullName,(int)(Tables.ptApi_ProductGroup),id,"Insert");
		}
		private static void AuditDelete( ProductGroup myProductGroup)
		{
			AuditManager.AuditDelete(false, myProductGroup.mUserFullName,(int)(Tables.ptApi_ProductGroup),myProductGroup.mId,"Delete");
		}
		private static void AuditUpdate( ProductGroup myProductGroup)
		{
			ProductGroup old_productgroup = GetItem(myProductGroup.mId);
			AuditCollection audit_collection = ProductGroupAudit.Audit(myProductGroup, old_productgroup);
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
		private class ProductGroupComparer : IComparer < ProductGroup >
		{
			private string _sortColumn;
			private bool _reverse;
			public ProductGroupComparer(string sortExpression)
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

			public int Compare(ProductGroup x, ProductGroup y)
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