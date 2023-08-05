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
	public static class CashDenominationManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static CashDenominationCollection GetList()
		{
			CashDenominationCriteria cashdenomination = new CashDenominationCriteria();
			return GetList(cashdenomination, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static CashDenominationCollection GetList(string sortExpression)
		{
			CashDenominationCriteria cashdenomination = new CashDenominationCriteria();
			return GetList(cashdenomination, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static CashDenominationCollection GetList(int startRowIndex, int maximumRows)
		{
			CashDenominationCriteria cashdenomination = new CashDenominationCriteria();
			return GetList(cashdenomination, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static CashDenominationCollection GetList(CashDenominationCriteria cashdenominationCriteria)
		{
			return GetList(cashdenominationCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static CashDenominationCollection GetList(CashDenominationCriteria cashdenominationCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			CashDenominationCollection myCollection = CashDenominationDB.GetList(cashdenominationCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new CashDenominationComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new CashDenominationCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(CashDenominationCriteria cashdenominationCriteria)
		{
			return CashDenominationDB.SelectCountForGetList(cashdenominationCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static CashDenomination GetItem(int id)
		{
			CashDenomination cashdenomination = CashDenominationDB.GetItem(id);
			return cashdenomination;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(CashDenomination myCashDenomination)
		{
			if (!myCashDenomination.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid cashdenomination. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myCashDenomination.mId != 0)
					AuditUpdate(myCashDenomination);

				int id = CashDenominationDB.Save(myCashDenomination);
				if(myCashDenomination.mId == 0)
					AuditInsert(myCashDenomination, id);

				myCashDenomination.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(CashDenomination myCashDenomination)
		{
			if (CashDenominationDB.Delete(myCashDenomination.mId))
			{
				AuditDelete(myCashDenomination);
				return myCashDenomination.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(CashDenomination myCashDenomination, int id)
		{
			AuditManager.AuditInsert(false, myCashDenomination.mUserFullName,(int)(Tables.ptApi_CashDenomination),id,"Insert");
		}
		private static void AuditDelete( CashDenomination myCashDenomination)
		{
			AuditManager.AuditDelete(false, myCashDenomination.mUserFullName,(int)(Tables.ptApi_CashDenomination),myCashDenomination.mId,"Delete");
		}
		private static void AuditUpdate( CashDenomination myCashDenomination)
		{
			CashDenomination old_cashdenomination = GetItem(myCashDenomination.mId);
			AuditCollection audit_collection = CashDenominationAudit.Audit(myCashDenomination, old_cashdenomination);
			if (audit_collection != null)
			{
				foreach (BusinessEntities.Audit audit in audit_collection)
				{
					AuditManager.Save( audit);
				}
			}
		}
		#endregion

		#region IComparable
		private class CashDenominationComparer : IComparer < CashDenomination >
		{
			private string _sortColumn;
			private bool _reverse;
			public CashDenominationComparer(string sortExpression)
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

			public int Compare(CashDenomination x, CashDenomination y)
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