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
	public static class CategoryManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static CategoryCollection GetList()
		{
			CategoryCriteria category = new CategoryCriteria();
			return GetList(category, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static CategoryCollection GetList(string sortExpression)
		{
			CategoryCriteria category = new CategoryCriteria();
			return GetList(category, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static CategoryCollection GetList(int startRowIndex, int maximumRows)
		{
			CategoryCriteria category = new CategoryCriteria();
			return GetList(category, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static CategoryCollection GetList(CategoryCriteria categoryCriteria)
		{
			return GetList(categoryCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static CategoryCollection GetList(CategoryCriteria categoryCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			CategoryCollection myCollection = CategoryDB.GetList(categoryCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new CategoryComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new CategoryCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(CategoryCriteria categoryCriteria)
		{
			return CategoryDB.SelectCountForGetList(categoryCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Category GetItem(int id)
		{
			Category category = CategoryDB.GetItem(id);
			return category;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Category myCategory)
		{
			if (!myCategory.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid category. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myCategory.mId != 0)
					AuditUpdate(myCategory);

				int id = CategoryDB.Save(myCategory);
				if(myCategory.mId == 0)
					AuditInsert(myCategory, id);

				myCategory.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Category myCategory)
		{
			if (CategoryDB.Delete(myCategory.mId))
			{
				AuditDelete(myCategory);
				return myCategory.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Category myCategory, int id)
		{
			AuditManager.AuditInsert(false, myCategory.mUserFullName,(int)(Tables.ptApi_Category),id,"Insert");
		}
		private static void AuditDelete( Category myCategory)
		{
			AuditManager.AuditDelete(false, myCategory.mUserFullName,(int)(Tables.ptApi_Category),myCategory.mId,"Delete");
		}
		private static void AuditUpdate( Category myCategory)
		{
			Category old_category = GetItem(myCategory.mId);
			AuditCollection audit_collection = CategoryAudit.Audit(myCategory, old_category);
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
		private class CategoryComparer : IComparer < Category >
		{
			private string _sortColumn;
			private bool _reverse;
			public CategoryComparer(string sortExpression)
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

			public int Compare(Category x, Category y)
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