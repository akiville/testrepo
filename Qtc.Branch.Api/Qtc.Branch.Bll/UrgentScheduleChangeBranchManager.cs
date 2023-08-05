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
	public static class UrgentScheduleChangeBranchManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static UrgentScheduleChangeBranchCollection GetList()
		{
			UrgentScheduleChangeBranchCriteria urgentschedulechangebranch = new UrgentScheduleChangeBranchCriteria();
			return GetList(urgentschedulechangebranch, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeBranchCollection GetList(string sortExpression)
		{
			UrgentScheduleChangeBranchCriteria urgentschedulechangebranch = new UrgentScheduleChangeBranchCriteria();
			return GetList(urgentschedulechangebranch, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeBranchCollection GetList(int startRowIndex, int maximumRows)
		{
			UrgentScheduleChangeBranchCriteria urgentschedulechangebranch = new UrgentScheduleChangeBranchCriteria();
			return GetList(urgentschedulechangebranch, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeBranchCollection GetList(UrgentScheduleChangeBranchCriteria urgentschedulechangebranchCriteria)
		{
			return GetList(urgentschedulechangebranchCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeBranchCollection GetList(UrgentScheduleChangeBranchCriteria urgentschedulechangebranchCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			UrgentScheduleChangeBranchCollection myCollection = UrgentScheduleChangeBranchDB.GetList(urgentschedulechangebranchCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new UrgentScheduleChangeBranchComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new UrgentScheduleChangeBranchCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(UrgentScheduleChangeBranchCriteria urgentschedulechangebranchCriteria)
		{
			return UrgentScheduleChangeBranchDB.SelectCountForGetList(urgentschedulechangebranchCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static UrgentScheduleChangeBranch GetItem(int id)
		{
			UrgentScheduleChangeBranch urgentschedulechangebranch = UrgentScheduleChangeBranchDB.GetItem(id);
			return urgentschedulechangebranch;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(UrgentScheduleChangeBranch myUrgentScheduleChangeBranch)
		{
			if (!myUrgentScheduleChangeBranch.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid urgentschedulechangebranch. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myUrgentScheduleChangeBranch.mId != 0)
					AuditUpdate(myUrgentScheduleChangeBranch);

				int id = UrgentScheduleChangeBranchDB.Save(myUrgentScheduleChangeBranch);
				if(myUrgentScheduleChangeBranch.mId == 0)
					AuditInsert(myUrgentScheduleChangeBranch, id);

				myUrgentScheduleChangeBranch.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(UrgentScheduleChangeBranch myUrgentScheduleChangeBranch)
		{
			if (UrgentScheduleChangeBranchDB.Delete(myUrgentScheduleChangeBranch.mId))
			{
				AuditDelete(myUrgentScheduleChangeBranch);
				return myUrgentScheduleChangeBranch.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(UrgentScheduleChangeBranch myUrgentScheduleChangeBranch, int id)
		{
			AuditManager.AuditInsert(false, myUrgentScheduleChangeBranch.mUserFullName,(int)(Tables.ptApi_UrgentScheduleChangeBranch),id,"Insert");
		}
		private static void AuditDelete( UrgentScheduleChangeBranch myUrgentScheduleChangeBranch)
		{
			AuditManager.AuditDelete(false, myUrgentScheduleChangeBranch.mUserFullName,(int)(Tables.ptApi_UrgentScheduleChangeBranch),myUrgentScheduleChangeBranch.mId,"Delete");
		}
		private static void AuditUpdate( UrgentScheduleChangeBranch myUrgentScheduleChangeBranch)
		{
			UrgentScheduleChangeBranch old_urgentschedulechangebranch = GetItem(myUrgentScheduleChangeBranch.mId);
			AuditCollection audit_collection = UrgentScheduleChangeBranchAudit.Audit(myUrgentScheduleChangeBranch, old_urgentschedulechangebranch);
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
		private class UrgentScheduleChangeBranchComparer : IComparer < UrgentScheduleChangeBranch >
		{
			private string _sortColumn;
			private bool _reverse;
			public UrgentScheduleChangeBranchComparer(string sortExpression)
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

			public int Compare(UrgentScheduleChangeBranch x, UrgentScheduleChangeBranch y)
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