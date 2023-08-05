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
	public static class DeliveryDetailsManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DeliveryDetailsCollection GetList()
		{
			DeliveryDetailsCriteria deliverydetails = new DeliveryDetailsCriteria();
			return GetList(deliverydetails, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryDetailsCollection GetList(string sortExpression)
		{
			DeliveryDetailsCriteria deliverydetails = new DeliveryDetailsCriteria();
			return GetList(deliverydetails, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryDetailsCollection GetList(int startRowIndex, int maximumRows)
		{
			DeliveryDetailsCriteria deliverydetails = new DeliveryDetailsCriteria();
			return GetList(deliverydetails, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryDetailsCollection GetList(DeliveryDetailsCriteria deliverydetailsCriteria)
		{
			return GetList(deliverydetailsCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryDetailsCollection GetList(DeliveryDetailsCriteria deliverydetailsCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DeliveryDetailsCollection myCollection = DeliveryDetailsDB.GetList(deliverydetailsCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DeliveryDetailsComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DeliveryDetailsCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DeliveryDetailsCriteria deliverydetailsCriteria)
		{
			return DeliveryDetailsDB.SelectCountForGetList(deliverydetailsCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryDetails GetItem(int id)
		{
			DeliveryDetails deliverydetails = DeliveryDetailsDB.GetItem(id);
			return deliverydetails;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(DeliveryDetails myDeliveryDetails)
		{
			if (!myDeliveryDetails.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid deliverydetails. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDeliveryDetails.mId != 0)
					AuditUpdate(myDeliveryDetails);

				int id = DeliveryDetailsDB.Save(myDeliveryDetails);
				if(myDeliveryDetails.mId == 0)
					AuditInsert(myDeliveryDetails, id);

				myDeliveryDetails.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(DeliveryDetails myDeliveryDetails)
		{
			if (DeliveryDetailsDB.Delete(myDeliveryDetails.mId))
			{
				AuditDelete(myDeliveryDetails);
				return myDeliveryDetails.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(DeliveryDetails myDeliveryDetails, int id)
		{
			AuditManager.AuditInsert(false, myDeliveryDetails.mUserFullName,(int)(Tables.ptApi_DeliveryDetails),id,"Insert");
		}
		private static void AuditDelete( DeliveryDetails myDeliveryDetails)
		{
			AuditManager.AuditDelete(false, myDeliveryDetails.mUserFullName,(int)(Tables.ptApi_DeliveryDetails),myDeliveryDetails.mId,"Delete");
		}
		private static void AuditUpdate( DeliveryDetails myDeliveryDetails)
		{
			DeliveryDetails old_deliverydetails = GetItem(myDeliveryDetails.mId);
			AuditCollection audit_collection = DeliveryDetailsAudit.Audit(myDeliveryDetails, old_deliverydetails);
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
		private class DeliveryDetailsComparer : IComparer < DeliveryDetails >
		{
			private string _sortColumn;
			private bool _reverse;
			public DeliveryDetailsComparer(string sortExpression)
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

			public int Compare(DeliveryDetails x, DeliveryDetails y)
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