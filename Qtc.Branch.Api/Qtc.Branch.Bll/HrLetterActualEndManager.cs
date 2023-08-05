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
	public static class HrLetterActualEndManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static HrLetterActualEndCollection GetList()
		{
			HrLetterActualEndCriteria hrletteractualend = new HrLetterActualEndCriteria();
			return GetList(hrletteractualend, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterActualEndCollection GetList(string sortExpression)
		{
			HrLetterActualEndCriteria hrletteractualend = new HrLetterActualEndCriteria();
			return GetList(hrletteractualend, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterActualEndCollection GetList(int startRowIndex, int maximumRows)
		{
			HrLetterActualEndCriteria hrletteractualend = new HrLetterActualEndCriteria();
			return GetList(hrletteractualend, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterActualEndCollection GetList(HrLetterActualEndCriteria hrletteractualendCriteria)
		{
			return GetList(hrletteractualendCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterActualEndCollection GetList(HrLetterActualEndCriteria hrletteractualendCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			HrLetterActualEndCollection myCollection = HrLetterActualEndDB.GetList(hrletteractualendCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new HrLetterActualEndComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new HrLetterActualEndCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(HrLetterActualEndCriteria hrletteractualendCriteria)
		{
			return HrLetterActualEndDB.SelectCountForGetList(hrletteractualendCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterActualEnd GetItem(int id)
		{
			HrLetterActualEnd hrletteractualend = HrLetterActualEndDB.GetItem(id);
			return hrletteractualend;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(HrLetterActualEnd myHrLetterActualEnd)
		{
			if (!myHrLetterActualEnd.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid hrletteractualend. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myHrLetterActualEnd.mId != 0)
					AuditUpdate(myHrLetterActualEnd);

				int id = HrLetterActualEndDB.Save(myHrLetterActualEnd);
				if(myHrLetterActualEnd.mId == 0)
					AuditInsert(myHrLetterActualEnd, id);

				myHrLetterActualEnd.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(HrLetterActualEnd myHrLetterActualEnd)
		{
			if (HrLetterActualEndDB.Delete(myHrLetterActualEnd.mId))
			{
				AuditDelete(myHrLetterActualEnd);
				return myHrLetterActualEnd.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(HrLetterActualEnd myHrLetterActualEnd, int id)
		{
			AuditManager.AuditInsert(false, myHrLetterActualEnd.mUserFullName,(int)(Tables.ptApi_HrLetterActualEnd),id,"Insert");
		}
		private static void AuditDelete( HrLetterActualEnd myHrLetterActualEnd)
		{
			AuditManager.AuditDelete(false, myHrLetterActualEnd.mUserFullName,(int)(Tables.ptApi_HrLetterActualEnd),myHrLetterActualEnd.mId,"Delete");
		}
		private static void AuditUpdate( HrLetterActualEnd myHrLetterActualEnd)
		{
			HrLetterActualEnd old_hrletteractualend = GetItem(myHrLetterActualEnd.mId);
			AuditCollection audit_collection = HrLetterActualEndAudit.Audit(myHrLetterActualEnd, old_hrletteractualend);
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
		private class HrLetterActualEndComparer : IComparer < HrLetterActualEnd >
		{
			private string _sortColumn;
			private bool _reverse;
			public HrLetterActualEndComparer(string sortExpression)
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

			public int Compare(HrLetterActualEnd x, HrLetterActualEnd y)
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