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
	public static class DeliveryScheduleManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DeliveryScheduleCollection GetList()
		{
			DeliveryScheduleCriteria deliveryschedule = new DeliveryScheduleCriteria();
			return GetList(deliveryschedule, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleCollection GetList(string sortExpression)
		{
			DeliveryScheduleCriteria deliveryschedule = new DeliveryScheduleCriteria();
			return GetList(deliveryschedule, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleCollection GetList(int startRowIndex, int maximumRows)
		{
			DeliveryScheduleCriteria deliveryschedule = new DeliveryScheduleCriteria();
			return GetList(deliveryschedule, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleCollection GetList(DeliveryScheduleCriteria deliveryscheduleCriteria)
		{
			return GetList(deliveryscheduleCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliveryScheduleCollection GetList(DeliveryScheduleCriteria deliveryscheduleCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DeliveryScheduleCollection myCollection = DeliveryScheduleDB.GetList(deliveryscheduleCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DeliveryScheduleComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DeliveryScheduleCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DeliveryScheduleCriteria deliveryscheduleCriteria)
		{
			return DeliveryScheduleDB.SelectCountForGetList(deliveryscheduleCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeliverySchedule GetItem(int id)
		{
			DeliverySchedule deliveryschedule = DeliveryScheduleDB.GetItem(id);
			return deliveryschedule;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(DeliverySchedule myDeliverySchedule)
		{
			if (!myDeliverySchedule.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid deliveryschedule. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDeliverySchedule.mId != 0)
					AuditUpdate(myDeliverySchedule);

				int id = DeliveryScheduleDB.Save(myDeliverySchedule);
				if(myDeliverySchedule.mId == 0)
					AuditInsert(myDeliverySchedule, id);

				myDeliverySchedule.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(DeliverySchedule myDeliverySchedule)
		{
			if (DeliveryScheduleDB.Delete(myDeliverySchedule.mId))
			{
				AuditDelete(myDeliverySchedule);
				return myDeliverySchedule.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(DeliverySchedule myDeliverySchedule, int id)
		{
			AuditManager.AuditInsert(false, myDeliverySchedule.mUserFullName,(int)(Tables.ptApi_DeliverySchedule),id,"Insert");
		}
		private static void AuditDelete( DeliverySchedule myDeliverySchedule)
		{
			AuditManager.AuditDelete(false, myDeliverySchedule.mUserFullName,(int)(Tables.ptApi_DeliverySchedule),myDeliverySchedule.mId,"Delete");
		}
		private static void AuditUpdate( DeliverySchedule myDeliverySchedule)
		{
			DeliverySchedule old_deliveryschedule = GetItem(myDeliverySchedule.mId);
			AuditCollection audit_collection = DeliveryScheduleAudit.Audit(myDeliverySchedule, old_deliveryschedule);
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
		private class DeliveryScheduleComparer : IComparer < DeliverySchedule >
		{
			private string _sortColumn;
			private bool _reverse;
			public DeliveryScheduleComparer(string sortExpression)
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

			public int Compare(DeliverySchedule x, DeliverySchedule y)
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