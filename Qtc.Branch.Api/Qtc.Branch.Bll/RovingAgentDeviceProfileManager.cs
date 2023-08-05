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
	public static class RovingAgentDeviceProfileManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingAgentDeviceProfileCollection GetList()
		{
			RovingAgentDeviceProfileCriteria rovingagentdeviceprofile = new RovingAgentDeviceProfileCriteria();
			return GetList(rovingagentdeviceprofile, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingAgentDeviceProfileCollection GetList(string sortExpression)
		{
			RovingAgentDeviceProfileCriteria rovingagentdeviceprofile = new RovingAgentDeviceProfileCriteria();
			return GetList(rovingagentdeviceprofile, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingAgentDeviceProfileCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingAgentDeviceProfileCriteria rovingagentdeviceprofile = new RovingAgentDeviceProfileCriteria();
			return GetList(rovingagentdeviceprofile, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingAgentDeviceProfileCollection GetList(RovingAgentDeviceProfileCriteria rovingagentdeviceprofileCriteria)
		{
			return GetList(rovingagentdeviceprofileCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingAgentDeviceProfileCollection GetList(RovingAgentDeviceProfileCriteria rovingagentdeviceprofileCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingAgentDeviceProfileCollection myCollection = RovingAgentDeviceProfileDB.GetList(rovingagentdeviceprofileCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingAgentDeviceProfileComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingAgentDeviceProfileCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingAgentDeviceProfileCriteria rovingagentdeviceprofileCriteria)
		{
			return RovingAgentDeviceProfileDB.SelectCountForGetList(rovingagentdeviceprofileCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingAgentDeviceProfile GetItem(int id)
		{
			RovingAgentDeviceProfile rovingagentdeviceprofile = RovingAgentDeviceProfileDB.GetItem(id);
			return rovingagentdeviceprofile;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingAgentDeviceProfile myRovingAgentDeviceProfile)
		{
			if (!myRovingAgentDeviceProfile.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingagentdeviceprofile. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingAgentDeviceProfile.mId != 0)
					AuditUpdate(myRovingAgentDeviceProfile);

				int id = RovingAgentDeviceProfileDB.Save(myRovingAgentDeviceProfile);
				if(myRovingAgentDeviceProfile.mId == 0)
					AuditInsert(myRovingAgentDeviceProfile, id);

				myRovingAgentDeviceProfile.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingAgentDeviceProfile myRovingAgentDeviceProfile)
		{
			if (RovingAgentDeviceProfileDB.Delete(myRovingAgentDeviceProfile.mId))
			{
				AuditDelete(myRovingAgentDeviceProfile);
				return myRovingAgentDeviceProfile.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingAgentDeviceProfile myRovingAgentDeviceProfile, int id)
		{
			AuditManager.AuditInsert(false, myRovingAgentDeviceProfile.mUserFullName,(int)(Tables.ptApi_RovingAgentDeviceProfile),id,"Insert");
		}
		private static void AuditDelete( RovingAgentDeviceProfile myRovingAgentDeviceProfile)
		{
			AuditManager.AuditDelete(false, myRovingAgentDeviceProfile.mUserFullName,(int)(Tables.ptApi_RovingAgentDeviceProfile),myRovingAgentDeviceProfile.mId,"Delete");
		}
		private static void AuditUpdate( RovingAgentDeviceProfile myRovingAgentDeviceProfile)
		{
			RovingAgentDeviceProfile old_rovingagentdeviceprofile = GetItem(myRovingAgentDeviceProfile.mId);
			AuditCollection audit_collection = RovingAgentDeviceProfileAudit.Audit(myRovingAgentDeviceProfile, old_rovingagentdeviceprofile);
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
		private class RovingAgentDeviceProfileComparer : IComparer < RovingAgentDeviceProfile >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingAgentDeviceProfileComparer(string sortExpression)
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

			public int Compare(RovingAgentDeviceProfile x, RovingAgentDeviceProfile y)
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