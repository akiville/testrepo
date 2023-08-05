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
	public static class RovingCheckListViolationManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingCheckListViolationCollection GetList()
		{
			RovingCheckListViolationCriteria rovingchecklistviolation = new RovingCheckListViolationCriteria();
			return GetList(rovingchecklistviolation, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationCollection GetList(string sortExpression)
		{
			RovingCheckListViolationCriteria rovingchecklistviolation = new RovingCheckListViolationCriteria();
			return GetList(rovingchecklistviolation, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingCheckListViolationCriteria rovingchecklistviolation = new RovingCheckListViolationCriteria();
			return GetList(rovingchecklistviolation, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationCollection GetList(RovingCheckListViolationCriteria rovingchecklistviolationCriteria)
		{
			return GetList(rovingchecklistviolationCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationCollection GetList(RovingCheckListViolationCriteria rovingchecklistviolationCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingCheckListViolationCollection myCollection = RovingCheckListViolationDB.GetList(rovingchecklistviolationCriteria);

           
            if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingCheckListViolationComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingCheckListViolationCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingCheckListViolationCriteria rovingchecklistviolationCriteria)
		{
			return RovingCheckListViolationDB.SelectCountForGetList(rovingchecklistviolationCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolation GetItem(int id)
		{
			RovingCheckListViolation rovingchecklistviolation = RovingCheckListViolationDB.GetItem(id);
			return rovingchecklistviolation;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingCheckListViolation myRovingCheckListViolation)
		{
			if (!myRovingCheckListViolation.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingchecklistviolation. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingCheckListViolation.mId != 0)
					AuditUpdate(myRovingCheckListViolation);

				int id = RovingCheckListViolationDB.Save(myRovingCheckListViolation);
				if(myRovingCheckListViolation.mId == 0)
					AuditInsert(myRovingCheckListViolation, id);

				myRovingCheckListViolation.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingCheckListViolation myRovingCheckListViolation)
		{
			if (RovingCheckListViolationDB.Delete(myRovingCheckListViolation.mId))
			{
				AuditDelete(myRovingCheckListViolation);
				return myRovingCheckListViolation.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingCheckListViolation myRovingCheckListViolation, int id)
		{
			AuditManager.AuditInsert(false, myRovingCheckListViolation.mUserFullName,(int)(Tables.ptApi_RovingCheckListViolation),id,"Insert");
		}
		private static void AuditDelete( RovingCheckListViolation myRovingCheckListViolation)
		{
			AuditManager.AuditDelete(false, myRovingCheckListViolation.mUserFullName,(int)(Tables.ptApi_RovingCheckListViolation),myRovingCheckListViolation.mId,"Delete");
		}
		private static void AuditUpdate( RovingCheckListViolation myRovingCheckListViolation)
		{
			RovingCheckListViolation old_rovingchecklistviolation = GetItem(myRovingCheckListViolation.mId);
			AuditCollection audit_collection = RovingCheckListViolationAudit.Audit(myRovingCheckListViolation, old_rovingchecklistviolation);
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
		private class RovingCheckListViolationComparer : IComparer < RovingCheckListViolation >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingCheckListViolationComparer(string sortExpression)
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

			public int Compare(RovingCheckListViolation x, RovingCheckListViolation y)
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