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
	public static class HrLetterHistoryMovementManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static HrLetterHistoryMovementCollection GetList()
		{
			HrLetterHistoryMovementCriteria hrletterhistorymovement = new HrLetterHistoryMovementCriteria();
			return GetList(hrletterhistorymovement, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterHistoryMovementCollection GetList(string sortExpression)
		{
			HrLetterHistoryMovementCriteria hrletterhistorymovement = new HrLetterHistoryMovementCriteria();
			return GetList(hrletterhistorymovement, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterHistoryMovementCollection GetList(int startRowIndex, int maximumRows)
		{
			HrLetterHistoryMovementCriteria hrletterhistorymovement = new HrLetterHistoryMovementCriteria();
			return GetList(hrletterhistorymovement, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterHistoryMovementCollection GetList(HrLetterHistoryMovementCriteria hrletterhistorymovementCriteria)
		{
			return GetList(hrletterhistorymovementCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterHistoryMovementCollection GetList(HrLetterHistoryMovementCriteria hrletterhistorymovementCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			HrLetterHistoryMovementCollection myCollection = HrLetterHistoryMovementDB.GetList(hrletterhistorymovementCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new HrLetterHistoryMovementComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new HrLetterHistoryMovementCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(HrLetterHistoryMovementCriteria hrletterhistorymovementCriteria)
		{
			return HrLetterHistoryMovementDB.SelectCountForGetList(hrletterhistorymovementCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterHistoryMovement GetItem(int id)
		{
			HrLetterHistoryMovement hrletterhistorymovement = HrLetterHistoryMovementDB.GetItem(id);
			return hrletterhistorymovement;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(HrLetterHistoryMovement myHrLetterHistoryMovement)
		{
			if (!myHrLetterHistoryMovement.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid hrletterhistorymovement. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myHrLetterHistoryMovement.mId != 0)
					AuditUpdate(myHrLetterHistoryMovement);

				int id = HrLetterHistoryMovementDB.Save(myHrLetterHistoryMovement);
				if(myHrLetterHistoryMovement.mId == 0)
					AuditInsert(myHrLetterHistoryMovement, id);

				myHrLetterHistoryMovement.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(HrLetterHistoryMovement myHrLetterHistoryMovement)
		{
			if (HrLetterHistoryMovementDB.Delete(myHrLetterHistoryMovement.mId))
			{
				AuditDelete(myHrLetterHistoryMovement);
				return myHrLetterHistoryMovement.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(HrLetterHistoryMovement myHrLetterHistoryMovement, int id)
		{
			AuditManager.AuditInsert(false, myHrLetterHistoryMovement.mUserFullName,(int)(Tables.ptApi_HrLetterHistoryMovement),id,"Insert");
		}
		private static void AuditDelete( HrLetterHistoryMovement myHrLetterHistoryMovement)
		{
			AuditManager.AuditDelete(false, myHrLetterHistoryMovement.mUserFullName,(int)(Tables.ptApi_HrLetterHistoryMovement),myHrLetterHistoryMovement.mId,"Delete");
		}
		private static void AuditUpdate( HrLetterHistoryMovement myHrLetterHistoryMovement)
		{
			HrLetterHistoryMovement old_hrletterhistorymovement = GetItem(myHrLetterHistoryMovement.mId);
			AuditCollection audit_collection = HrLetterHistoryMovementAudit.Audit(myHrLetterHistoryMovement, old_hrletterhistorymovement);
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
		private class HrLetterHistoryMovementComparer : IComparer < HrLetterHistoryMovement >
		{
			private string _sortColumn;
			private bool _reverse;
			public HrLetterHistoryMovementComparer(string sortExpression)
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

			public int Compare(HrLetterHistoryMovement x, HrLetterHistoryMovement y)
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