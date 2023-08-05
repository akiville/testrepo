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
	public static class RovingCheckListViolationPersonnelManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingCheckListViolationPersonnelCollection GetList()
		{
			RovingCheckListViolationPersonnelCriteria rovingchecklistviolationpersonnel = new RovingCheckListViolationPersonnelCriteria();
			return GetList(rovingchecklistviolationpersonnel, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationPersonnelCollection GetList(string sortExpression)
		{
			RovingCheckListViolationPersonnelCriteria rovingchecklistviolationpersonnel = new RovingCheckListViolationPersonnelCriteria();
			return GetList(rovingchecklistviolationpersonnel, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationPersonnelCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingCheckListViolationPersonnelCriteria rovingchecklistviolationpersonnel = new RovingCheckListViolationPersonnelCriteria();
			return GetList(rovingchecklistviolationpersonnel, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationPersonnelCollection GetList(RovingCheckListViolationPersonnelCriteria rovingchecklistviolationpersonnelCriteria)
		{
			return GetList(rovingchecklistviolationpersonnelCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationPersonnelCollection GetList(RovingCheckListViolationPersonnelCriteria rovingchecklistviolationpersonnelCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingCheckListViolationPersonnelCollection myCollection = RovingCheckListViolationPersonnelDB.GetList(rovingchecklistviolationpersonnelCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingCheckListViolationPersonnelComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingCheckListViolationPersonnelCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingCheckListViolationPersonnelCriteria rovingchecklistviolationpersonnelCriteria)
		{
			return RovingCheckListViolationPersonnelDB.SelectCountForGetList(rovingchecklistviolationpersonnelCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationPersonnel GetItem(int id)
		{
			RovingCheckListViolationPersonnel rovingchecklistviolationpersonnel = RovingCheckListViolationPersonnelDB.GetItem(id);
			return rovingchecklistviolationpersonnel;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingCheckListViolationPersonnel myRovingCheckListViolationPersonnel)
		{
			if (!myRovingCheckListViolationPersonnel.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingchecklistviolationpersonnel. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingCheckListViolationPersonnel.mId != 0)
					AuditUpdate(myRovingCheckListViolationPersonnel);

				int id = RovingCheckListViolationPersonnelDB.Save(myRovingCheckListViolationPersonnel);
				if(myRovingCheckListViolationPersonnel.mId == 0)
					AuditInsert(myRovingCheckListViolationPersonnel, id);

				myRovingCheckListViolationPersonnel.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingCheckListViolationPersonnel myRovingCheckListViolationPersonnel)
		{
			if (RovingCheckListViolationPersonnelDB.Delete(myRovingCheckListViolationPersonnel.mId))
			{
				AuditDelete(myRovingCheckListViolationPersonnel);
				return myRovingCheckListViolationPersonnel.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingCheckListViolationPersonnel myRovingCheckListViolationPersonnel, int id)
		{
			AuditManager.AuditInsert(false, myRovingCheckListViolationPersonnel.mUserFullName,(int)(Tables.ptApi_RovingCheckListViolationPersonnel),id,"Insert");
		}
		private static void AuditDelete( RovingCheckListViolationPersonnel myRovingCheckListViolationPersonnel)
		{
			AuditManager.AuditDelete(false, myRovingCheckListViolationPersonnel.mUserFullName,(int)(Tables.ptApi_RovingCheckListViolationPersonnel),myRovingCheckListViolationPersonnel.mId,"Delete");
		}
		private static void AuditUpdate( RovingCheckListViolationPersonnel myRovingCheckListViolationPersonnel)
		{
			RovingCheckListViolationPersonnel old_rovingchecklistviolationpersonnel = GetItem(myRovingCheckListViolationPersonnel.mId);
			AuditCollection audit_collection = RovingCheckListViolationPersonnelAudit.Audit(myRovingCheckListViolationPersonnel, old_rovingchecklistviolationpersonnel);
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
		private class RovingCheckListViolationPersonnelComparer : IComparer < RovingCheckListViolationPersonnel >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingCheckListViolationPersonnelComparer(string sortExpression)
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

			public int Compare(RovingCheckListViolationPersonnel x, RovingCheckListViolationPersonnel y)
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