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
	public static class ProductSerialManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ProductSerialCollection GetList()
		{
			ProductSerialCriteria productserial = new ProductSerialCriteria();
			return GetList(productserial, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductSerialCollection GetList(string sortExpression)
		{
			ProductSerialCriteria productserial = new ProductSerialCriteria();
			return GetList(productserial, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductSerialCollection GetList(int startRowIndex, int maximumRows)
		{
			ProductSerialCriteria productserial = new ProductSerialCriteria();
			return GetList(productserial, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductSerialCollection GetList(ProductSerialCriteria productserialCriteria)
		{
			return GetList(productserialCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductSerialCollection GetList(ProductSerialCriteria productserialCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			ProductSerialCollection myCollection = ProductSerialDB.GetList(productserialCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new ProductSerialComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new ProductSerialCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(ProductSerialCriteria productserialCriteria)
		{
			return ProductSerialDB.SelectCountForGetList(productserialCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ProductSerial GetItem(int id)
		{
			ProductSerial productserial = ProductSerialDB.GetItem(id);
			return productserial;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(ProductSerial myProductSerial)
		{
			if (!myProductSerial.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid productserial. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				//if(myProductSerial.mId != 0)
				//	AuditUpdate(myProductSerial);

				int id = ProductSerialDB.Save(myProductSerial);
				//if(myProductSerial.mId == 0)
				//	AuditInsert(myProductSerial, id);

				myProductSerial.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(ProductSerial myProductSerial)
		{
			if (ProductSerialDB.Delete(myProductSerial.mId))
			{
				AuditDelete(myProductSerial);
				return myProductSerial.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(ProductSerial myProductSerial, int id)
		{
			AuditManager.AuditInsert(false, myProductSerial.mUserFullName,(int)(Tables.ptApi_ProductSerial),id,"Insert");
		}
		private static void AuditDelete( ProductSerial myProductSerial)
		{
			AuditManager.AuditDelete(false, myProductSerial.mUserFullName,(int)(Tables.ptApi_ProductSerial),myProductSerial.mId,"Delete");
		}
		private static void AuditUpdate( ProductSerial myProductSerial)
		{
			ProductSerial old_productserial = GetItem(myProductSerial.mId);
			AuditCollection audit_collection = ProductSerialAudit.Audit(myProductSerial, old_productserial);
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
		private class ProductSerialComparer : IComparer < ProductSerial >
		{
			private string _sortColumn;
			private bool _reverse;
			public ProductSerialComparer(string sortExpression)
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

			public int Compare(ProductSerial x, ProductSerial y)
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