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
	public static class RovingAgentLoginManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingAgentLoginCollection GetList()
		{
			RovingAgentLoginCriteria rovingagentlogin = new RovingAgentLoginCriteria();
			return GetList(rovingagentlogin, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingAgentLoginCollection GetList(string sortExpression)
		{
			RovingAgentLoginCriteria rovingagentlogin = new RovingAgentLoginCriteria();
			return GetList(rovingagentlogin, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingAgentLoginCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingAgentLoginCriteria rovingagentlogin = new RovingAgentLoginCriteria();
			return GetList(rovingagentlogin, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingAgentLoginCollection GetList(RovingAgentLoginCriteria rovingagentloginCriteria)
		{
			return GetList(rovingagentloginCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingAgentLoginCollection GetList(RovingAgentLoginCriteria rovingagentloginCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingAgentLoginCollection myCollection = RovingAgentLoginDB.GetList(rovingagentloginCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingAgentLoginComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingAgentLoginCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingAgentLoginCriteria rovingagentloginCriteria)
		{
			return RovingAgentLoginDB.SelectCountForGetList(rovingagentloginCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingAgentLogin GetItem(int id)
		{
			RovingAgentLogin rovingagentlogin = RovingAgentLoginDB.GetItem(id);
			return rovingagentlogin;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingAgentLogin myRovingAgentLogin)
		{
			if (!myRovingAgentLogin.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingagentlogin. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingAgentLogin.mId != 0)
					AuditUpdate(myRovingAgentLogin);

				int id = RovingAgentLoginDB.Save(myRovingAgentLogin);
				if(myRovingAgentLogin.mId == 0)
					AuditInsert(myRovingAgentLogin, id);

				myRovingAgentLogin.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingAgentLogin myRovingAgentLogin)
		{
			if (RovingAgentLoginDB.Delete(myRovingAgentLogin.mId))
			{
				AuditDelete(myRovingAgentLogin);
				return myRovingAgentLogin.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingAgentLogin myRovingAgentLogin, int id)
		{
			AuditManager.AuditInsert(false, myRovingAgentLogin.mUserFullName,(int)(Tables.ptApi_RovingAgentLogin),id,"Insert");
		}
		private static void AuditDelete( RovingAgentLogin myRovingAgentLogin)
		{
			AuditManager.AuditDelete(false, myRovingAgentLogin.mUserFullName,(int)(Tables.ptApi_RovingAgentLogin),myRovingAgentLogin.mId,"Delete");
		}
		private static void AuditUpdate( RovingAgentLogin myRovingAgentLogin)
		{
			RovingAgentLogin old_rovingagentlogin = GetItem(myRovingAgentLogin.mId);
			AuditCollection audit_collection = RovingAgentLoginAudit.Audit(myRovingAgentLogin, old_rovingagentlogin);
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
		private class RovingAgentLoginComparer : IComparer < RovingAgentLogin >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingAgentLoginComparer(string sortExpression)
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

			public int Compare(RovingAgentLogin x, RovingAgentLogin y)
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