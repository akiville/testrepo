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
	public static class EmployeeHrLetterManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static EmployeeHrLetterCollection GetList()
		{
			EmployeeHrLetterCriteria employeehrletter = new EmployeeHrLetterCriteria();
			return GetList(employeehrletter, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeHrLetterCollection GetList(string sortExpression)
		{
			EmployeeHrLetterCriteria employeehrletter = new EmployeeHrLetterCriteria();
			return GetList(employeehrletter, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeHrLetterCollection GetList(int startRowIndex, int maximumRows)
		{
			EmployeeHrLetterCriteria employeehrletter = new EmployeeHrLetterCriteria();
			return GetList(employeehrletter, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeHrLetterCollection GetList(EmployeeHrLetterCriteria employeehrletterCriteria)
		{
			return GetList(employeehrletterCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeHrLetterCollection GetList(EmployeeHrLetterCriteria employeehrletterCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			EmployeeHrLetterCollection myCollection = EmployeeHrLetterDB.GetList(employeehrletterCriteria);

            //foreach (EmployeeHrLetter item in myCollection)
            //{
            //    HrLetterHistoryMovementCollection hr_letter_history_movement_list = new HrLetterHistoryMovementCollection();
            //    HrLetterHistoryMovementCriteria criteria = new HrLetterHistoryMovementCriteria();
            //    criteria.mHrLetterId = item.mId;

            //    item.mHrLetterHistoryMovementCollection = HrLetterHistoryMovementManager.GetList(criteria);
            //}

			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new EmployeeHrLetterComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new EmployeeHrLetterCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(EmployeeHrLetterCriteria employeehrletterCriteria)
		{
			return EmployeeHrLetterDB.SelectCountForGetList(employeehrletterCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static EmployeeHrLetter GetItem(int id)
		{
			EmployeeHrLetter employeehrletter = EmployeeHrLetterDB.GetItem(id);
			return employeehrletter;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(EmployeeHrLetter myEmployeeHrLetter)
		{
			if (!myEmployeeHrLetter.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid employeehrletter. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				//if(myEmployeeHrLetter.mId != 0)
				//	AuditUpdate(myEmployeeHrLetter);

				int id = EmployeeHrLetterDB.Save(myEmployeeHrLetter);
				if(myEmployeeHrLetter.mId == 0)
					AuditInsert(myEmployeeHrLetter, id);

				myEmployeeHrLetter.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(EmployeeHrLetter myEmployeeHrLetter)
		{
			if (EmployeeHrLetterDB.Delete(myEmployeeHrLetter.mId))
			{
				AuditDelete(myEmployeeHrLetter);
				return myEmployeeHrLetter.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(EmployeeHrLetter myEmployeeHrLetter, int id)
		{
			AuditManager.AuditInsert(false, myEmployeeHrLetter.mUserFullName,(int)(Tables.ptApi_EmployeeHrLetter),id,"Insert");
		}
		private static void AuditDelete( EmployeeHrLetter myEmployeeHrLetter)
		{
			AuditManager.AuditDelete(false, myEmployeeHrLetter.mUserFullName,(int)(Tables.ptApi_EmployeeHrLetter),myEmployeeHrLetter.mId,"Delete");
		}
		private static void AuditUpdate( EmployeeHrLetter myEmployeeHrLetter)
		{
			EmployeeHrLetter old_employeehrletter = GetItem(myEmployeeHrLetter.mId);
			AuditCollection audit_collection = EmployeeHrLetterAudit.Audit(myEmployeeHrLetter, old_employeehrletter);
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
		private class EmployeeHrLetterComparer : IComparer < EmployeeHrLetter >
		{
			private string _sortColumn;
			private bool _reverse;
			public EmployeeHrLetterComparer(string sortExpression)
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

			public int Compare(EmployeeHrLetter x, EmployeeHrLetter y)
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