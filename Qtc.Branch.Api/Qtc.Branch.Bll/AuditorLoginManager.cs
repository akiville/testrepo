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
	public static class AuditorLoginManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static AuditorLoginCollection GetList()
		{
			AuditorLoginCriteria auditorlogin = new AuditorLoginCriteria();
			return GetList(auditorlogin, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditorLoginCollection GetList(string sortExpression)
		{
			AuditorLoginCriteria auditorlogin = new AuditorLoginCriteria();
			return GetList(auditorlogin, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditorLoginCollection GetList(int startRowIndex, int maximumRows)
		{
			AuditorLoginCriteria auditorlogin = new AuditorLoginCriteria();
			return GetList(auditorlogin, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditorLoginCollection GetList(AuditorLoginCriteria auditorloginCriteria)
		{
			return GetList(auditorloginCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditorLoginCollection GetList(AuditorLoginCriteria auditorloginCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			AuditorLoginCollection myCollection = AuditorLoginDB.GetList(auditorloginCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new AuditorLoginComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new AuditorLoginCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(AuditorLoginCriteria auditorloginCriteria)
		{
			return AuditorLoginDB.SelectCountForGetList(auditorloginCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditorLogin GetItem(int id)
		{
			AuditorLogin auditorlogin = AuditorLoginDB.GetItem(id);
			return auditorlogin;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(AuditorLogin myAuditorLogin)
		{
			if (!myAuditorLogin.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid auditorlogin. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myAuditorLogin.mId != 0)
					AuditUpdate(myAuditorLogin);

				int id = AuditorLoginDB.Save(myAuditorLogin);
				if(myAuditorLogin.mId == 0)
					AuditInsert(myAuditorLogin, id);

				myAuditorLogin.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(AuditorLogin myAuditorLogin)
		{
			if (AuditorLoginDB.Delete(myAuditorLogin.mId))
			{
				AuditDelete(myAuditorLogin);
				return myAuditorLogin.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(AuditorLogin myAuditorLogin, int id)
		{
			AuditManager.AuditInsert(false, myAuditorLogin.mUserFullName,(int)(Tables.ptApi_AuditorLogin),id,"Insert");
		}
		private static void AuditDelete( AuditorLogin myAuditorLogin)
		{
			AuditManager.AuditDelete(false, myAuditorLogin.mUserFullName,(int)(Tables.ptApi_AuditorLogin),myAuditorLogin.mId,"Delete");
		}
		private static void AuditUpdate( AuditorLogin myAuditorLogin)
		{
			AuditorLogin old_auditorlogin = GetItem(myAuditorLogin.mId);
			AuditCollection audit_collection = AuditorLoginAudit.Audit(myAuditorLogin, old_auditorlogin);
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
		private class AuditorLoginComparer : IComparer < AuditorLogin >
		{
			private string _sortColumn;
			private bool _reverse;
			public AuditorLoginComparer(string sortExpression)
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

			public int Compare(AuditorLogin x, AuditorLogin y)
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