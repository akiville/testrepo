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
	public static class DeliveryScheduleConcernDetailManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DeliveryScheduleConcernDetailCollection GetList()
		{
			DeliveryScheduleConcernDetailCriteria deliveryscheduleconcerndetail = new DeliveryScheduleConcernDetailCriteria();
			return GetList(deliveryscheduleconcerndetail, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleConcernDetailCollection GetList(string sortExpression)
		{
			DeliveryScheduleConcernDetailCriteria deliveryscheduleconcerndetail = new DeliveryScheduleConcernDetailCriteria();
			return GetList(deliveryscheduleconcerndetail, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleConcernDetailCollection GetList(int startRowIndex, int maximumRows)
		{
			DeliveryScheduleConcernDetailCriteria deliveryscheduleconcerndetail = new DeliveryScheduleConcernDetailCriteria();
			return GetList(deliveryscheduleconcerndetail, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleConcernDetailCollection GetList(DeliveryScheduleConcernDetailCriteria deliveryscheduleconcerndetailCriteria)
		{
			return GetList(deliveryscheduleconcerndetailCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleConcernDetailCollection GetList(DeliveryScheduleConcernDetailCriteria deliveryscheduleconcerndetailCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DeliveryScheduleConcernDetailCollection myCollection = DeliveryScheduleConcernDetailDB.GetList(deliveryscheduleconcerndetailCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DeliveryScheduleConcernDetailComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DeliveryScheduleConcernDetailCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DeliveryScheduleConcernDetailCriteria deliveryscheduleconcerndetailCriteria)
		{
			return DeliveryScheduleConcernDetailDB.SelectCountForGetList(deliveryscheduleconcerndetailCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleConcernDetail GetItem(int id)
		{
			DeliveryScheduleConcernDetail deliveryscheduleconcerndetail = DeliveryScheduleConcernDetailDB.GetItem(id);
			return deliveryscheduleconcerndetail;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(DeliveryScheduleConcernDetail myDeliveryScheduleConcernDetail)
		{
			if (!myDeliveryScheduleConcernDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid deliveryscheduleconcerndetail. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDeliveryScheduleConcernDetail.mId != 0)
					AuditUpdate(myDeliveryScheduleConcernDetail);

				int id = DeliveryScheduleConcernDetailDB.Save(myDeliveryScheduleConcernDetail);
				if(myDeliveryScheduleConcernDetail.mId == 0)
					AuditInsert(myDeliveryScheduleConcernDetail, id);

				myDeliveryScheduleConcernDetail.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(DeliveryScheduleConcernDetail myDeliveryScheduleConcernDetail)
		{
			if (DeliveryScheduleConcernDetailDB.Delete(myDeliveryScheduleConcernDetail.mId))
			{
				AuditDelete(myDeliveryScheduleConcernDetail);
				return myDeliveryScheduleConcernDetail.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(DeliveryScheduleConcernDetail myDeliveryScheduleConcernDetail, int id)
		{
			AuditManager.AuditInsert(false, myDeliveryScheduleConcernDetail.mUserFullName,(int)(Tables.ptApi_DeliveryScheduleConcernDetail),id,"Insert");
		}
		private static void AuditDelete( DeliveryScheduleConcernDetail myDeliveryScheduleConcernDetail)
		{
			AuditManager.AuditDelete(false, myDeliveryScheduleConcernDetail.mUserFullName,(int)(Tables.ptApi_DeliveryScheduleConcernDetail),myDeliveryScheduleConcernDetail.mId,"Delete");
		}
		private static void AuditUpdate( DeliveryScheduleConcernDetail myDeliveryScheduleConcernDetail)
		{
			DeliveryScheduleConcernDetail old_deliveryscheduleconcerndetail = GetItem(myDeliveryScheduleConcernDetail.mId);
			AuditCollection audit_collection = DeliveryScheduleConcernDetailAudit.Audit(myDeliveryScheduleConcernDetail, old_deliveryscheduleconcerndetail);
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
		private class DeliveryScheduleConcernDetailComparer : IComparer < DeliveryScheduleConcernDetail >
		{
			private string _sortColumn;
			private bool _reverse;
			public DeliveryScheduleConcernDetailComparer(string sortExpression)
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

			public int Compare(DeliveryScheduleConcernDetail x, DeliveryScheduleConcernDetail y)
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