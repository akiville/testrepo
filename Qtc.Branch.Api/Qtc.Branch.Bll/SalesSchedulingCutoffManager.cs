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
	public static class SalesSchedulingCutoffManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static SalesSchedulingCutoffCollection GetList()
		{
			SalesSchedulingCutoffCriteria salesschedulingcutoff = new SalesSchedulingCutoffCriteria();
			return GetList(salesschedulingcutoff, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingCutoffCollection GetList(string sortExpression)
		{
			SalesSchedulingCutoffCriteria salesschedulingcutoff = new SalesSchedulingCutoffCriteria();
			return GetList(salesschedulingcutoff, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingCutoffCollection GetList(int startRowIndex, int maximumRows)
		{
			SalesSchedulingCutoffCriteria salesschedulingcutoff = new SalesSchedulingCutoffCriteria();
			return GetList(salesschedulingcutoff, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingCutoffCollection GetList(SalesSchedulingCutoffCriteria salesschedulingcutoffCriteria)
		{
			return GetList(salesschedulingcutoffCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingCutoffCollection GetList(SalesSchedulingCutoffCriteria salesschedulingcutoffCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			SalesSchedulingCutoffCollection myCollection = SalesSchedulingCutoffDB.GetList(salesschedulingcutoffCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new SalesSchedulingCutoffComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new SalesSchedulingCutoffCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(SalesSchedulingCutoffCriteria salesschedulingcutoffCriteria)
		{
			return SalesSchedulingCutoffDB.SelectCountForGetList(salesschedulingcutoffCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingCutoff GetItem(int id)
		{
			SalesSchedulingCutoff salesschedulingcutoff = SalesSchedulingCutoffDB.GetItem(id);
			return salesschedulingcutoff;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(SalesSchedulingCutoff mySalesSchedulingCutoff)
		{
			if (!mySalesSchedulingCutoff.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid salesschedulingcutoff. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(mySalesSchedulingCutoff.mId != 0)
					AuditUpdate(mySalesSchedulingCutoff);

				int id = SalesSchedulingCutoffDB.Save(mySalesSchedulingCutoff);
				if(mySalesSchedulingCutoff.mId == 0)
					AuditInsert(mySalesSchedulingCutoff, id);

				mySalesSchedulingCutoff.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(SalesSchedulingCutoff mySalesSchedulingCutoff)
		{
			if (SalesSchedulingCutoffDB.Delete(mySalesSchedulingCutoff.mId))
			{
				AuditDelete(mySalesSchedulingCutoff);
				return mySalesSchedulingCutoff.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(SalesSchedulingCutoff mySalesSchedulingCutoff, int id)
		{
			AuditManager.AuditInsert(false, mySalesSchedulingCutoff.mUserFullName,(int)(Tables.ptApi_SalesSchedulingCutoff),id,"Insert");
		}
		private static void AuditDelete( SalesSchedulingCutoff mySalesSchedulingCutoff)
		{
			AuditManager.AuditDelete(false, mySalesSchedulingCutoff.mUserFullName,(int)(Tables.ptApi_SalesSchedulingCutoff),mySalesSchedulingCutoff.mId,"Delete");
		}
		private static void AuditUpdate( SalesSchedulingCutoff mySalesSchedulingCutoff)
		{
			SalesSchedulingCutoff old_salesschedulingcutoff = GetItem(mySalesSchedulingCutoff.mId);
			AuditCollection audit_collection = SalesSchedulingCutoffAudit.Audit(mySalesSchedulingCutoff, old_salesschedulingcutoff);
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
		private class SalesSchedulingCutoffComparer : IComparer < SalesSchedulingCutoff >
		{
			private string _sortColumn;
			private bool _reverse;
			public SalesSchedulingCutoffComparer(string sortExpression)
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

			public int Compare(SalesSchedulingCutoff x, SalesSchedulingCutoff y)
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