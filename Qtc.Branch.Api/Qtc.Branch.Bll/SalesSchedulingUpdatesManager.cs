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
	public static class SalesSchedulingUpdatesManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static SalesSchedulingUpdatesCollection GetList()
		{
			SalesSchedulingUpdatesCriteria salesschedulingupdates = new SalesSchedulingUpdatesCriteria();
			return GetList(salesschedulingupdates, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingUpdatesCollection GetList(string sortExpression)
		{
			SalesSchedulingUpdatesCriteria salesschedulingupdates = new SalesSchedulingUpdatesCriteria();
			return GetList(salesschedulingupdates, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingUpdatesCollection GetList(int startRowIndex, int maximumRows)
		{
			SalesSchedulingUpdatesCriteria salesschedulingupdates = new SalesSchedulingUpdatesCriteria();
			return GetList(salesschedulingupdates, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingUpdatesCollection GetList(SalesSchedulingUpdatesCriteria salesschedulingupdatesCriteria)
		{
			return GetList(salesschedulingupdatesCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingUpdatesCollection GetList(SalesSchedulingUpdatesCriteria salesschedulingupdatesCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			SalesSchedulingUpdatesCollection myCollection = SalesSchedulingUpdatesDB.GetList(salesschedulingupdatesCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new SalesSchedulingUpdatesComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new SalesSchedulingUpdatesCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(SalesSchedulingUpdatesCriteria salesschedulingupdatesCriteria)
		{
			return SalesSchedulingUpdatesDB.SelectCountForGetList(salesschedulingupdatesCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static SalesSchedulingUpdates GetItem(int id)
		{
			SalesSchedulingUpdates salesschedulingupdates = SalesSchedulingUpdatesDB.GetItem(id);
			return salesschedulingupdates;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(SalesSchedulingUpdates mySalesSchedulingUpdates)
		{
			if (!mySalesSchedulingUpdates.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid salesschedulingupdates. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(mySalesSchedulingUpdates.mId != 0)
					AuditUpdate(mySalesSchedulingUpdates);

				int id = SalesSchedulingUpdatesDB.Save(mySalesSchedulingUpdates);
				if(mySalesSchedulingUpdates.mId == 0)
					AuditInsert(mySalesSchedulingUpdates, id);

				mySalesSchedulingUpdates.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(int id)
		{
			if (SalesSchedulingUpdatesDB.Delete(id))
			{
				AuditDelete(id, "");
				return id;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(SalesSchedulingUpdates mySalesSchedulingUpdates, int id)
		{
			AuditManager.AuditInsert(false, mySalesSchedulingUpdates.mUserFullName,(int)(Tables.ptApi_SalesSchedulingUpdates),id,"Insert");
		}
		private static void AuditDelete( int id, String user_fullname)
		{
			AuditManager.AuditDelete(false, user_fullname, (int)(Tables.ptApi_SalesSchedulingUpdates),id,"Delete");
		}
		private static void AuditUpdate( SalesSchedulingUpdates mySalesSchedulingUpdates)
		{
			SalesSchedulingUpdates old_salesschedulingupdates = GetItem(mySalesSchedulingUpdates.mId);
			AuditCollection audit_collection = SalesSchedulingUpdatesAudit.Audit(mySalesSchedulingUpdates, old_salesschedulingupdates);
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
		private class SalesSchedulingUpdatesComparer : IComparer < SalesSchedulingUpdates >
		{
			private string _sortColumn;
			private bool _reverse;
			public SalesSchedulingUpdatesComparer(string sortExpression)
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

			public int Compare(SalesSchedulingUpdates x, SalesSchedulingUpdates y)
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