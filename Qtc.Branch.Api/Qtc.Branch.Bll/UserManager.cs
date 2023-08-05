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
	public static class UserManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static UserCollection GetList()
		{
			UserCriteria user = new UserCriteria();
			return GetList(user, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UserCollection GetList(string sortExpression)
		{
			UserCriteria user = new UserCriteria();
			return GetList(user, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UserCollection GetList(int startRowIndex, int maximumRows)
		{
			UserCriteria user = new UserCriteria();
			return GetList(user, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UserCollection GetList(UserCriteria userCriteria)
		{
			return GetList(userCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UserCollection GetList(UserCriteria userCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			UserCollection myCollection = UserDB.GetList(userCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new UserComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new UserCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(UserCriteria userCriteria)
		{
			return UserDB.SelectCountForGetList(userCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static User GetItem(int id)
		{
			User user = UserDB.GetItem(id);
			return user;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(User myUser)
		{
			if (!myUser.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid user. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myUser.mId != 0)
					AuditUpdate(myUser);

				int id = UserDB.Save(myUser);
				if(myUser.mId == 0)
					AuditInsert(myUser, id);

				myUser.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(User myUser)
		{
			if (UserDB.Delete(myUser.mId))
			{
				AuditDelete(myUser);
				return myUser.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(User myUser, int id)
		{
			AuditManager.AuditInsert(false, myUser.mUserFullName,(int)(Tables.ptApi_User),id,"Insert");
		}
		private static void AuditDelete( User myUser)
		{
			AuditManager.AuditDelete(false, myUser.mUserFullName,(int)(Tables.ptApi_User),myUser.mId,"Delete");
		}
		private static void AuditUpdate( User myUser)
		{
			User old_user = GetItem(myUser.mId);
			AuditCollection audit_collection = UserAudit.Audit(myUser, old_user);
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
		private class UserComparer : IComparer < User >
		{
			private string _sortColumn;
			private bool _reverse;
			public UserComparer(string sortExpression)
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

			public int Compare(User x, User y)
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