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
	public static class LmmEntryManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static LmmEntryCollection GetList()
		{
			LmmEntryCriteria lmmentry = new LmmEntryCriteria();
			return GetList(lmmentry, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmEntryCollection GetList(string sortExpression)
		{
			LmmEntryCriteria lmmentry = new LmmEntryCriteria();
			return GetList(lmmentry, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmEntryCollection GetList(int startRowIndex, int maximumRows)
		{
			LmmEntryCriteria lmmentry = new LmmEntryCriteria();
			return GetList(lmmentry, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmEntryCollection GetList(LmmEntryCriteria lmmentryCriteria)
		{
			return GetList(lmmentryCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmEntryCollection GetList(LmmEntryCriteria lmmentryCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			LmmEntryCollection myCollection = LmmEntryDB.GetList(lmmentryCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new LmmEntryComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new LmmEntryCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(LmmEntryCriteria lmmentryCriteria)
		{
			return LmmEntryDB.SelectCountForGetList(lmmentryCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmEntry GetItem(int id)
		{
			LmmEntry lmmentry = LmmEntryDB.GetItem(id);
			return lmmentry;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(LmmEntry myLmmEntry)
		{
			if (!myLmmEntry.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid lmmentry. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myLmmEntry.mId != 0)
					AuditUpdate(myLmmEntry);

				int id = LmmEntryDB.Save(myLmmEntry);
				if(myLmmEntry.mId == 0)
					AuditInsert(myLmmEntry, id);

				myLmmEntry.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(LmmEntry myLmmEntry)
		{
			if (LmmEntryDB.Delete(myLmmEntry.mId))
			{
				AuditDelete(myLmmEntry);
				return myLmmEntry.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(LmmEntry myLmmEntry, int id)
		{
			AuditManager.AuditInsert(false, myLmmEntry.mUserFullName,(int)(Tables.ptApi_LmmEntry),id,"Insert");
		}
		private static void AuditDelete( LmmEntry myLmmEntry)
		{
			AuditManager.AuditDelete(false, myLmmEntry.mUserFullName,(int)(Tables.ptApi_LmmEntry),myLmmEntry.mId,"Delete");
		}
		private static void AuditUpdate( LmmEntry myLmmEntry)
		{
			LmmEntry old_lmmentry = GetItem(myLmmEntry.mId);
			AuditCollection audit_collection = LmmEntryAudit.Audit(myLmmEntry, old_lmmentry);
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
		private class LmmEntryComparer : IComparer < LmmEntry >
		{
			private string _sortColumn;
			private bool _reverse;
			public LmmEntryComparer(string sortExpression)
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

			public int Compare(LmmEntry x, LmmEntry y)
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