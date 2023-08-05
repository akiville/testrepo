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
	public static class ApiVersionManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ApiVersionCollection GetList()
		{
			ApiVersionCriteria apiversion = new ApiVersionCriteria();
			return GetList(apiversion, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiVersionCollection GetList(string sortExpression)
		{
			ApiVersionCriteria apiversion = new ApiVersionCriteria();
			return GetList(apiversion, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiVersionCollection GetList(int startRowIndex, int maximumRows)
		{
			ApiVersionCriteria apiversion = new ApiVersionCriteria();
			return GetList(apiversion, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiVersionCollection GetList(ApiVersionCriteria apiversionCriteria)
		{
			return GetList(apiversionCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiVersionCollection GetList(ApiVersionCriteria apiversionCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			ApiVersionCollection myCollection = ApiVersionDB.GetList(apiversionCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new ApiVersionComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new ApiVersionCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(ApiVersionCriteria apiversionCriteria)
		{
			return ApiVersionDB.SelectCountForGetList(apiversionCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiVersion GetItem(int id)
		{
			ApiVersion apiversion = ApiVersionDB.GetItem(id);
			return apiversion;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(ApiVersion myApiVersion)
		{
			if (!myApiVersion.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid apiversion. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myApiVersion.mId != 0)
					AuditUpdate(myApiVersion);

				int id = ApiVersionDB.Save(myApiVersion);
				if(myApiVersion.mId == 0)
					AuditInsert(myApiVersion, id);

				myApiVersion.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(ApiVersion myApiVersion)
		{
			if (ApiVersionDB.Delete(myApiVersion.mId))
			{
				AuditDelete(myApiVersion);
				return myApiVersion.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(ApiVersion myApiVersion, int id)
		{
			AuditManager.AuditInsert(false, myApiVersion.mUserFullName,(int)(Tables.ptApi_ApiVersion),id,"Insert");
		}
		private static void AuditDelete( ApiVersion myApiVersion)
		{
			AuditManager.AuditDelete(false, myApiVersion.mUserFullName,(int)(Tables.ptApi_ApiVersion),myApiVersion.mId,"Delete");
		}
		private static void AuditUpdate( ApiVersion myApiVersion)
		{
			ApiVersion old_apiversion = GetItem(myApiVersion.mId);
			AuditCollection audit_collection = ApiVersionAudit.Audit(myApiVersion, old_apiversion);
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
		private class ApiVersionComparer : IComparer < ApiVersion >
		{
			private string _sortColumn;
			private bool _reverse;
			public ApiVersionComparer(string sortExpression)
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

			public int Compare(ApiVersion x, ApiVersion y)
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