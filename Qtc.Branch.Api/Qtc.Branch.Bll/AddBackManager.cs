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
	public static class AddBackManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static AddBackCollection GetList()
		{
			AddBackCriteria addback = new AddBackCriteria();
			return GetList(addback, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AddBackCollection GetList(string sortExpression)
		{
			AddBackCriteria addback = new AddBackCriteria();
			return GetList(addback, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AddBackCollection GetList(int startRowIndex, int maximumRows)
		{
			AddBackCriteria addback = new AddBackCriteria();
			return GetList(addback, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AddBackCollection GetList(AddBackCriteria addbackCriteria)
		{
			return GetList(addbackCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AddBackCollection GetList(AddBackCriteria addbackCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			AddBackCollection myCollection = AddBackDB.GetList(addbackCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new AddBackComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new AddBackCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(AddBackCriteria addbackCriteria)
		{
			return AddBackDB.SelectCountForGetList(addbackCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AddBack GetItem(int id)
		{
			AddBack addback = AddBackDB.GetItem(id);
			return addback;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(AddBack myAddBack)
		{
			if (!myAddBack.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid addback. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myAddBack.mId != 0)
					AuditUpdate(myAddBack);

				int id = AddBackDB.Save(myAddBack);
				if(myAddBack.mId == 0)
					AuditInsert(myAddBack, id);

				myAddBack.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(AddBack myAddBack)
		{
			if (AddBackDB.Delete(myAddBack.mId))
			{
				AuditDelete(myAddBack);
				return myAddBack.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(AddBack myAddBack, int id)
		{
			AuditManager.AuditInsert(false, myAddBack.mUserFullName,(int)(Tables.ptApi_AddBack),id,"Insert");
		}
		private static void AuditDelete( AddBack myAddBack)
		{
			AuditManager.AuditDelete(false, myAddBack.mUserFullName,(int)(Tables.ptApi_AddBack),myAddBack.mId,"Delete");
		}
		private static void AuditUpdate( AddBack myAddBack)
		{
			AddBack old_addback = GetItem(myAddBack.mId);
			AuditCollection audit_collection = AddBackAudit.Audit(myAddBack, old_addback);
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
		private class AddBackComparer : IComparer < AddBack >
		{
			private string _sortColumn;
			private bool _reverse;
			public AddBackComparer(string sortExpression)
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

			public int Compare(AddBack x, AddBack y)
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