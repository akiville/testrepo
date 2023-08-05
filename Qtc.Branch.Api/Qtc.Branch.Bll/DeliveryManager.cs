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
	public static class DeliveryManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DeliveryCollection GetList()
		{
			DeliveryCriteria delivery = new DeliveryCriteria();
			return GetList(delivery, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryCollection GetList(string sortExpression)
		{
			DeliveryCriteria delivery = new DeliveryCriteria();
			return GetList(delivery, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryCollection GetList(int startRowIndex, int maximumRows)
		{
			DeliveryCriteria delivery = new DeliveryCriteria();
			return GetList(delivery, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryCollection GetList(DeliveryCriteria deliveryCriteria)
		{
			return GetList(deliveryCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryCollection GetList(DeliveryCriteria deliveryCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DeliveryCollection myCollection = DeliveryDB.GetList(deliveryCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DeliveryComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DeliveryCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DeliveryCriteria deliveryCriteria)
		{
			return DeliveryDB.SelectCountForGetList(deliveryCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Delivery GetItem(int id)
		{
			Delivery delivery = DeliveryDB.GetItem(id);
			return delivery;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Delivery myDelivery)
		{
			if (!myDelivery.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid delivery. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDelivery.mId != 0)
					AuditUpdate(myDelivery);

				int id = DeliveryDB.Save(myDelivery);
				if(myDelivery.mId == 0)
					AuditInsert(myDelivery, id);

				myDelivery.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Delivery myDelivery)
		{
			if (DeliveryDB.Delete(myDelivery.mId))
			{
				AuditDelete(myDelivery);
				return myDelivery.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Delivery myDelivery, int id)
		{
			AuditManager.AuditInsert(false, myDelivery.mUserFullName,(int)(Tables.ptApi_Delivery),id,"Insert");
		}
		private static void AuditDelete( Delivery myDelivery)
		{
			AuditManager.AuditDelete(false, myDelivery.mUserFullName,(int)(Tables.ptApi_Delivery),myDelivery.mId,"Delete");
		}
		private static void AuditUpdate( Delivery myDelivery)
		{
			Delivery old_delivery = GetItem(myDelivery.mId);
			AuditCollection audit_collection = DeliveryAudit.Audit(myDelivery, old_delivery);
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
		private class DeliveryComparer : IComparer < Delivery >
		{
			private string _sortColumn;
			private bool _reverse;
			public DeliveryComparer(string sortExpression)
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

			public int Compare(Delivery x, Delivery y)
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