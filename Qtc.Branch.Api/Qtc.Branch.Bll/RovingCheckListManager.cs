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
	public static class RovingCheckListManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingCheckListCollection GetList()
		{
			RovingCheckListCriteria rovingchecklist = new RovingCheckListCriteria();
			return GetList(rovingchecklist, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListCollection GetList(string sortExpression)
		{
			RovingCheckListCriteria rovingchecklist = new RovingCheckListCriteria();
			return GetList(rovingchecklist, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingCheckListCriteria rovingchecklist = new RovingCheckListCriteria();
			return GetList(rovingchecklist, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListCollection GetList(RovingCheckListCriteria rovingchecklistCriteria)
		{
			return GetList(rovingchecklistCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListCollection GetList(RovingCheckListCriteria rovingchecklistCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingCheckListCollection myCollection = RovingCheckListDB.GetList(rovingchecklistCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingCheckListComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingCheckListCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingCheckListCriteria rovingchecklistCriteria)
		{
			return RovingCheckListDB.SelectCountForGetList(rovingchecklistCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckList GetItem(int id)
		{
			RovingCheckList rovingchecklist = RovingCheckListDB.GetItem(id);
			return rovingchecklist;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingCheckList myRovingCheckList)
		{
			if (!myRovingCheckList.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingchecklist. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingCheckList.mId != 0)
					AuditUpdate(myRovingCheckList);

				int id = RovingCheckListDB.Save(myRovingCheckList);
				if(myRovingCheckList.mId == 0)
					AuditInsert(myRovingCheckList, id);

				myRovingCheckList.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingCheckList myRovingCheckList)
		{
			if (RovingCheckListDB.Delete(myRovingCheckList.mId))
			{
				AuditDelete(myRovingCheckList);
				return myRovingCheckList.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingCheckList myRovingCheckList, int id)
		{
			AuditManager.AuditInsert(false, myRovingCheckList.mUserFullName,(int)(Tables.ptApi_RovingCheckList),id,"Insert");
		}
		private static void AuditDelete( RovingCheckList myRovingCheckList)
		{
			AuditManager.AuditDelete(false, myRovingCheckList.mUserFullName,(int)(Tables.ptApi_RovingCheckList),myRovingCheckList.mId,"Delete");
		}
		private static void AuditUpdate( RovingCheckList myRovingCheckList)
		{
			RovingCheckList old_rovingchecklist = GetItem(myRovingCheckList.mId);
			AuditCollection audit_collection = RovingCheckListAudit.Audit(myRovingCheckList, old_rovingchecklist);
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
		private class RovingCheckListComparer : IComparer < RovingCheckList >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingCheckListComparer(string sortExpression)
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

			public int Compare(RovingCheckList x, RovingCheckList y)
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