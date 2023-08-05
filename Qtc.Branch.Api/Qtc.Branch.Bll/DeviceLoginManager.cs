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
	public static class DeviceLoginManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DeviceLoginCollection GetList()
		{
			DeviceLoginCriteria devicelogin = new DeviceLoginCriteria();
			return GetList(devicelogin, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceLoginCollection GetList(string sortExpression)
		{
			DeviceLoginCriteria devicelogin = new DeviceLoginCriteria();
			return GetList(devicelogin, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceLoginCollection GetList(int startRowIndex, int maximumRows)
		{
			DeviceLoginCriteria devicelogin = new DeviceLoginCriteria();
			return GetList(devicelogin, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceLoginCollection GetList(DeviceLoginCriteria deviceloginCriteria)
		{
			return GetList(deviceloginCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceLoginCollection GetList(DeviceLoginCriteria deviceloginCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DeviceLoginCollection myCollection = DeviceLoginDB.GetList(deviceloginCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DeviceLoginComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DeviceLoginCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DeviceLoginCriteria deviceloginCriteria)
		{
			return DeviceLoginDB.SelectCountForGetList(deviceloginCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DeviceLogin GetItem(int id)
		{
			DeviceLogin devicelogin = DeviceLoginDB.GetItem(id);
			return devicelogin;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(DeviceLogin myDeviceLogin)
		{
			if (!myDeviceLogin.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid devicelogin. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDeviceLogin.mId != 0)
					AuditUpdate(myDeviceLogin);

				int id = DeviceLoginDB.Save(myDeviceLogin);
				if(myDeviceLogin.mId == 0)
					AuditInsert(myDeviceLogin, id);

				myDeviceLogin.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(DeviceLogin myDeviceLogin)
		{
			if (DeviceLoginDB.Delete(myDeviceLogin.mId))
			{
				AuditDelete(myDeviceLogin);
				return myDeviceLogin.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(DeviceLogin myDeviceLogin, int id)
		{
			AuditManager.AuditInsert(false, myDeviceLogin.mUserFullName,(int)(Tables.ptApi_DeviceLogin),id,"Insert");
		}
		private static void AuditDelete( DeviceLogin myDeviceLogin)
		{
			AuditManager.AuditDelete(false, myDeviceLogin.mUserFullName,(int)(Tables.ptApi_DeviceLogin),myDeviceLogin.mId,"Delete");
		}
		private static void AuditUpdate( DeviceLogin myDeviceLogin)
		{
			DeviceLogin old_devicelogin = GetItem(myDeviceLogin.mId);
			AuditCollection audit_collection = DeviceLoginAudit.Audit(myDeviceLogin, old_devicelogin);
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
		private class DeviceLoginComparer : IComparer < DeviceLogin >
		{
			private string _sortColumn;
			private bool _reverse;
			public DeviceLoginComparer(string sortExpression)
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

			public int Compare(DeviceLogin x, DeviceLogin y)
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