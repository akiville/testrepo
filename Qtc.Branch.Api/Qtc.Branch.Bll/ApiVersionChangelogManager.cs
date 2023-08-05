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
	public static class ApiVersionChangelogManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ApiVersionChangelogCollection GetList()
		{
			ApiVersionChangelogCriteria apiversionchangelog = new ApiVersionChangelogCriteria();
			return GetList(apiversionchangelog, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiVersionChangelogCollection GetList(string sortExpression)
		{
			ApiVersionChangelogCriteria apiversionchangelog = new ApiVersionChangelogCriteria();
			return GetList(apiversionchangelog, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiVersionChangelogCollection GetList(int startRowIndex, int maximumRows)
		{
			ApiVersionChangelogCriteria apiversionchangelog = new ApiVersionChangelogCriteria();
			return GetList(apiversionchangelog, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiVersionChangelogCollection GetList(ApiVersionChangelogCriteria apiversionchangelogCriteria)
		{
			return GetList(apiversionchangelogCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiVersionChangelogCollection GetList(ApiVersionChangelogCriteria apiversionchangelogCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			ApiVersionChangelogCollection myCollection = ApiVersionChangelogDB.GetList(apiversionchangelogCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new ApiVersionChangelogComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new ApiVersionChangelogCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(ApiVersionChangelogCriteria apiversionchangelogCriteria)
		{
			return ApiVersionChangelogDB.SelectCountForGetList(apiversionchangelogCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ApiVersionChangelog GetItem(int id)
		{
			ApiVersionChangelog apiversionchangelog = ApiVersionChangelogDB.GetItem(id);
			return apiversionchangelog;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(ApiVersionChangelog myApiVersionChangelog)
		{
			if (!myApiVersionChangelog.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid apiversionchangelog. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myApiVersionChangelog.mId != 0)
					AuditUpdate(myApiVersionChangelog);

				int id = ApiVersionChangelogDB.Save(myApiVersionChangelog);
				if(myApiVersionChangelog.mId == 0)
					AuditInsert(myApiVersionChangelog, id);

				myApiVersionChangelog.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(ApiVersionChangelog myApiVersionChangelog)
		{
			if (ApiVersionChangelogDB.Delete(myApiVersionChangelog.mId))
			{
				AuditDelete(myApiVersionChangelog);
				return myApiVersionChangelog.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(ApiVersionChangelog myApiVersionChangelog, int id)
		{
			AuditManager.AuditInsert(false, myApiVersionChangelog.mUserFullName,(int)(Tables.ptApi_ApiVersionChangelog),id,"Insert");
		}
		private static void AuditDelete( ApiVersionChangelog myApiVersionChangelog)
		{
			AuditManager.AuditDelete(false, myApiVersionChangelog.mUserFullName,(int)(Tables.ptApi_ApiVersionChangelog),myApiVersionChangelog.mId,"Delete");
		}
		private static void AuditUpdate( ApiVersionChangelog myApiVersionChangelog)
		{
			ApiVersionChangelog old_apiversionchangelog = GetItem(myApiVersionChangelog.mId);
			AuditCollection audit_collection = ApiVersionChangelogAudit.Audit(myApiVersionChangelog, old_apiversionchangelog);
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
		private class ApiVersionChangelogComparer : IComparer < ApiVersionChangelog >
		{
			private string _sortColumn;
			private bool _reverse;
			public ApiVersionChangelogComparer(string sortExpression)
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

			public int Compare(ApiVersionChangelog x, ApiVersionChangelog y)
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