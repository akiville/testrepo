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
	public static class DeviceConfigurationManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DeviceConfigurationCollection GetList()
		{
			DeviceConfigurationCriteria deviceconfiguration = new DeviceConfigurationCriteria();
			return GetList(deviceconfiguration, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceConfigurationCollection GetList(string sortExpression)
		{
			DeviceConfigurationCriteria deviceconfiguration = new DeviceConfigurationCriteria();
			return GetList(deviceconfiguration, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceConfigurationCollection GetList(int startRowIndex, int maximumRows)
		{
			DeviceConfigurationCriteria deviceconfiguration = new DeviceConfigurationCriteria();
			return GetList(deviceconfiguration, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceConfigurationCollection GetList(DeviceConfigurationCriteria deviceconfigurationCriteria)
		{
			return GetList(deviceconfigurationCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceConfigurationCollection GetList(DeviceConfigurationCriteria deviceconfigurationCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DeviceConfigurationCollection myCollection = DeviceConfigurationDB.GetList(deviceconfigurationCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DeviceConfigurationComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DeviceConfigurationCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DeviceConfigurationCriteria deviceconfigurationCriteria)
		{
			return DeviceConfigurationDB.SelectCountForGetList(deviceconfigurationCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceConfiguration GetItem(int id)
		{
			DeviceConfiguration deviceconfiguration = DeviceConfigurationDB.GetItem(id);
			return deviceconfiguration;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(DeviceConfiguration myDeviceConfiguration)
		{
			if (!myDeviceConfiguration.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid deviceconfiguration. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDeviceConfiguration.mId != 0)
					AuditUpdate(myDeviceConfiguration);

				int id = DeviceConfigurationDB.Save(myDeviceConfiguration);
				if(myDeviceConfiguration.mId == 0)
					AuditInsert(myDeviceConfiguration, id);

				myDeviceConfiguration.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(DeviceConfiguration myDeviceConfiguration)
		{
			if (DeviceConfigurationDB.Delete(myDeviceConfiguration.mId))
			{
				AuditDelete(myDeviceConfiguration);
				return myDeviceConfiguration.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(DeviceConfiguration myDeviceConfiguration, int id)
		{
			AuditManager.AuditInsert(false, myDeviceConfiguration.mUserFullName,(int)(Tables.ptApi_DeviceConfiguration),id,"Insert");
		}
		private static void AuditDelete( DeviceConfiguration myDeviceConfiguration)
		{
			AuditManager.AuditDelete(false, myDeviceConfiguration.mUserFullName,(int)(Tables.ptApi_DeviceConfiguration),myDeviceConfiguration.mId,"Delete");
		}
		private static void AuditUpdate( DeviceConfiguration myDeviceConfiguration)
		{
			DeviceConfiguration old_deviceconfiguration = GetItem(myDeviceConfiguration.mId);
			AuditCollection audit_collection = DeviceConfigurationAudit.Audit(myDeviceConfiguration, old_deviceconfiguration);
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
		private class DeviceConfigurationComparer : IComparer < DeviceConfiguration >
		{
			private string _sortColumn;
			private bool _reverse;
			public DeviceConfigurationComparer(string sortExpression)
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

			public int Compare(DeviceConfiguration x, DeviceConfiguration y)
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