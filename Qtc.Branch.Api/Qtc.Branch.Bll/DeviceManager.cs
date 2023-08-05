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
	public static class DeviceManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DeviceCollection GetList()
		{
			DeviceCriteria device = new DeviceCriteria();
			return GetList(device, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceCollection GetList(string sortExpression)
		{
			DeviceCriteria device = new DeviceCriteria();
			return GetList(device, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceCollection GetList(int startRowIndex, int maximumRows)
		{
			DeviceCriteria device = new DeviceCriteria();
			return GetList(device, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceCollection GetList(DeviceCriteria deviceCriteria)
		{
			return GetList(deviceCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceCollection GetList(DeviceCriteria deviceCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DeviceCollection myCollection = DeviceDB.GetList(deviceCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DeviceComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DeviceCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DeviceCriteria deviceCriteria)
		{
			return DeviceDB.SelectCountForGetList(deviceCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Device GetItem(int id)
		{
			Device device = DeviceDB.GetItem(id);
			return device;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Device myDevice)
		{
			if (!myDevice.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid device. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDevice.mId != 0)
					AuditUpdate(myDevice);

				int id = DeviceDB.Save(myDevice);
				if(myDevice.mId == 0)
					AuditInsert(myDevice, id);

				myDevice.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Device myDevice)
		{
			if (DeviceDB.Delete(myDevice.mId))
			{
				AuditDelete(myDevice);
				return myDevice.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Device myDevice, int id)
		{
			AuditManager.AuditInsert(false, myDevice.mUserFullName,(int)(Tables.ptApi_Device),id,"Insert");
		}
		private static void AuditDelete( Device myDevice)
		{
			AuditManager.AuditDelete(false, myDevice.mUserFullName,(int)(Tables.ptApi_Device),myDevice.mId,"Delete");
		}
		private static void AuditUpdate( Device myDevice)
		{
			Device old_device = GetItem(myDevice.mId);
			AuditCollection audit_collection = DeviceAudit.Audit(myDevice, old_device);
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
		private class DeviceComparer : IComparer < Device >
		{
			private string _sortColumn;
			private bool _reverse;
			public DeviceComparer(string sortExpression)
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

			public int Compare(Device x, Device y)
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