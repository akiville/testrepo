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
	public static class LmmCashCountManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static LmmCashCountCollection GetList()
		{
			LmmCashCountCriteria lmmcashcount = new LmmCashCountCriteria();
			return GetList(lmmcashcount, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmCashCountCollection GetList(string sortExpression)
		{
			LmmCashCountCriteria lmmcashcount = new LmmCashCountCriteria();
			return GetList(lmmcashcount, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmCashCountCollection GetList(int startRowIndex, int maximumRows)
		{
			LmmCashCountCriteria lmmcashcount = new LmmCashCountCriteria();
			return GetList(lmmcashcount, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmCashCountCollection GetList(LmmCashCountCriteria lmmcashcountCriteria)
		{
			return GetList(lmmcashcountCriteria, string.Empty, -1, -1);
		}
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static LmmCashCountCollection GetListCount(LmmCashCountCriteria lmmcashcountCriteria)
        {
            return GetListCount(lmmcashcountCriteria, string.Empty, -1, -1);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static LmmCashCountCollection GetListCount(LmmCashCountCriteria lmmcashcountCriteria, string sortExpression, int startRowIndex, int maximumRows)
        {
            LmmCashCountCollection myCollection = LmmCashCountDB.GetListCount(lmmcashcountCriteria);

            if (!string.IsNullOrEmpty(sortExpression))
            {
                myCollection.Sort(new LmmCashCountComparer(sortExpression));
            }
            if (startRowIndex >= 0 && maximumRows > 0)
            {
                return new LmmCashCountCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
            }
            return myCollection;
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmCashCountCollection GetList(LmmCashCountCriteria lmmcashcountCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			LmmCashCountCollection myCollection = LmmCashCountDB.GetList(lmmcashcountCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new LmmCashCountComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new LmmCashCountCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(LmmCashCountCriteria lmmcashcountCriteria)
		{
			return LmmCashCountDB.SelectCountForGetList(lmmcashcountCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmCashCount GetItem(int id)
		{
			LmmCashCount lmmcashcount = LmmCashCountDB.GetItem(id);
			return lmmcashcount;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(LmmCashCount myLmmCashCount)
		{
			if (!myLmmCashCount.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid lmmcashcount. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myLmmCashCount.mId != 0)
					AuditUpdate(myLmmCashCount);

				int id = LmmCashCountDB.Save(myLmmCashCount);
				if(myLmmCashCount.mId == 0)
					AuditInsert(myLmmCashCount, id);

				myLmmCashCount.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(LmmCashCount myLmmCashCount)
		{
			if (LmmCashCountDB.Delete(myLmmCashCount.mId))
			{
				AuditDelete(myLmmCashCount);
				return myLmmCashCount.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(LmmCashCount myLmmCashCount, int id)
		{
			AuditManager.AuditInsert(false, myLmmCashCount.mUserFullName,(int)(Tables.ptApi_LmmCashCount),id,"Insert");
		}
		private static void AuditDelete( LmmCashCount myLmmCashCount)
		{
			AuditManager.AuditDelete(false, myLmmCashCount.mUserFullName,(int)(Tables.ptApi_LmmCashCount),myLmmCashCount.mId,"Delete");
		}
		private static void AuditUpdate( LmmCashCount myLmmCashCount)
		{
			LmmCashCount old_lmmcashcount = GetItem(myLmmCashCount.mId);
			AuditCollection audit_collection = LmmCashCountAudit.Audit(myLmmCashCount, old_lmmcashcount);
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
		private class LmmCashCountComparer : IComparer < LmmCashCount >
		{
			private string _sortColumn;
			private bool _reverse;
			public LmmCashCountComparer(string sortExpression)
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

			public int Compare(LmmCashCount x, LmmCashCount y)
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