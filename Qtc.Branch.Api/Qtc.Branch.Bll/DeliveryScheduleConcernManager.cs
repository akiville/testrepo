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
	public static class DeliveryScheduleConcernManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DeliveryScheduleConcernCollection GetList()
		{
			DeliveryScheduleConcernCriteria deliveryscheduleconcern = new DeliveryScheduleConcernCriteria();
			return GetList(deliveryscheduleconcern, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleConcernCollection GetList(string sortExpression)
		{
			DeliveryScheduleConcernCriteria deliveryscheduleconcern = new DeliveryScheduleConcernCriteria();
			return GetList(deliveryscheduleconcern, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleConcernCollection GetList(int startRowIndex, int maximumRows)
		{
			DeliveryScheduleConcernCriteria deliveryscheduleconcern = new DeliveryScheduleConcernCriteria();
			return GetList(deliveryscheduleconcern, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleConcernCollection GetList(DeliveryScheduleConcernCriteria deliveryscheduleconcernCriteria)
		{
			return GetList(deliveryscheduleconcernCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleConcernCollection GetList(DeliveryScheduleConcernCriteria deliveryscheduleconcernCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DeliveryScheduleConcernCollection myCollection = DeliveryScheduleConcernDB.GetList(deliveryscheduleconcernCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DeliveryScheduleConcernComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DeliveryScheduleConcernCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DeliveryScheduleConcernCriteria deliveryscheduleconcernCriteria)
		{
			return DeliveryScheduleConcernDB.SelectCountForGetList(deliveryscheduleconcernCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleConcern GetItem(int id)
		{
			DeliveryScheduleConcern deliveryscheduleconcern = DeliveryScheduleConcernDB.GetItem(id);
			return deliveryscheduleconcern;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(DeliveryScheduleConcern myDeliveryScheduleConcern)
		{
			if (!myDeliveryScheduleConcern.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid deliveryscheduleconcern. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDeliveryScheduleConcern.mId != 0)
					AuditUpdate(myDeliveryScheduleConcern);

				int id = DeliveryScheduleConcernDB.Save(myDeliveryScheduleConcern);
				if(myDeliveryScheduleConcern.mId == 0)
					AuditInsert(myDeliveryScheduleConcern, id);

				myDeliveryScheduleConcern.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(DeliveryScheduleConcern myDeliveryScheduleConcern)
		{
			if (DeliveryScheduleConcernDB.Delete(myDeliveryScheduleConcern.mId))
			{
				AuditDelete(myDeliveryScheduleConcern);
				return myDeliveryScheduleConcern.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(DeliveryScheduleConcern myDeliveryScheduleConcern, int id)
		{
			AuditManager.AuditInsert(false, myDeliveryScheduleConcern.mUserFullName,(int)(Tables.ptApi_DeliveryScheduleConcern),id,"Insert");
		}
		private static void AuditDelete( DeliveryScheduleConcern myDeliveryScheduleConcern)
		{
			AuditManager.AuditDelete(false, myDeliveryScheduleConcern.mUserFullName,(int)(Tables.ptApi_DeliveryScheduleConcern),myDeliveryScheduleConcern.mId,"Delete");
		}
		private static void AuditUpdate( DeliveryScheduleConcern myDeliveryScheduleConcern)
		{
			DeliveryScheduleConcern old_deliveryscheduleconcern = GetItem(myDeliveryScheduleConcern.mId);
			AuditCollection audit_collection = DeliveryScheduleConcernAudit.Audit(myDeliveryScheduleConcern, old_deliveryscheduleconcern);
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
		private class DeliveryScheduleConcernComparer : IComparer < DeliveryScheduleConcern >
		{
			private string _sortColumn;
			private bool _reverse;
			public DeliveryScheduleConcernComparer(string sortExpression)
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

			public int Compare(DeliveryScheduleConcern x, DeliveryScheduleConcern y)
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