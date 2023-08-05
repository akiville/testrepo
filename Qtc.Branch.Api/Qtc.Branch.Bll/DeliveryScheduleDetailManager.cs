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
	public static class DeliveryScheduleDetailManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DeliveryScheduleDetailCollection GetList()
		{
			DeliveryScheduleDetailCriteria deliveryscheduledetail = new DeliveryScheduleDetailCriteria();
			return GetList(deliveryscheduledetail, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleDetailCollection GetList(string sortExpression)
		{
			DeliveryScheduleDetailCriteria deliveryscheduledetail = new DeliveryScheduleDetailCriteria();
			return GetList(deliveryscheduledetail, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleDetailCollection GetList(int startRowIndex, int maximumRows)
		{
			DeliveryScheduleDetailCriteria deliveryscheduledetail = new DeliveryScheduleDetailCriteria();
			return GetList(deliveryscheduledetail, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleDetailCollection GetList(DeliveryScheduleDetailCriteria deliveryscheduledetailCriteria)
		{
			return GetList(deliveryscheduledetailCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleDetailCollection GetList(DeliveryScheduleDetailCriteria deliveryscheduledetailCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DeliveryScheduleDetailCollection myCollection = DeliveryScheduleDetailDB.GetList(deliveryscheduledetailCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DeliveryScheduleDetailComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DeliveryScheduleDetailCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DeliveryScheduleDetailCriteria deliveryscheduledetailCriteria)
		{
			return DeliveryScheduleDetailDB.SelectCountForGetList(deliveryscheduledetailCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleDetail GetItem(int id)
		{
			DeliveryScheduleDetail deliveryscheduledetail = DeliveryScheduleDetailDB.GetItem(id);
			return deliveryscheduledetail;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(DeliveryScheduleDetail myDeliveryScheduleDetail)
		{
			if (!myDeliveryScheduleDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid deliveryscheduledetail. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDeliveryScheduleDetail.mId != 0)
					AuditUpdate(myDeliveryScheduleDetail);

				int id = DeliveryScheduleDetailDB.Save(myDeliveryScheduleDetail);
				if(myDeliveryScheduleDetail.mId == 0)
					AuditInsert(myDeliveryScheduleDetail, id);

				myDeliveryScheduleDetail.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(DeliveryScheduleDetail myDeliveryScheduleDetail)
		{
			if (DeliveryScheduleDetailDB.Delete(myDeliveryScheduleDetail.mId))
			{
				AuditDelete(myDeliveryScheduleDetail);
				return myDeliveryScheduleDetail.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(DeliveryScheduleDetail myDeliveryScheduleDetail, int id)
		{
			AuditManager.AuditInsert(false, myDeliveryScheduleDetail.mUserFullName,(int)(Tables.ptApi_DeliveryScheduleDetail),id,"Insert");
		}
		private static void AuditDelete( DeliveryScheduleDetail myDeliveryScheduleDetail)
		{
			AuditManager.AuditDelete(false, myDeliveryScheduleDetail.mUserFullName,(int)(Tables.ptApi_DeliveryScheduleDetail),myDeliveryScheduleDetail.mId,"Delete");
		}
		private static void AuditUpdate( DeliveryScheduleDetail myDeliveryScheduleDetail)
		{
			DeliveryScheduleDetail old_deliveryscheduledetail = GetItem(myDeliveryScheduleDetail.mId);
			AuditCollection audit_collection = DeliveryScheduleDetailAudit.Audit(myDeliveryScheduleDetail, old_deliveryscheduledetail);
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
		private class DeliveryScheduleDetailComparer : IComparer < DeliveryScheduleDetail >
		{
			private string _sortColumn;
			private bool _reverse;
			public DeliveryScheduleDetailComparer(string sortExpression)
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

			public int Compare(DeliveryScheduleDetail x, DeliveryScheduleDetail y)
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