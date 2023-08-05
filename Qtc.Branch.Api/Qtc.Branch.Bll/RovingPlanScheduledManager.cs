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
	public static class RovingPlanScheduledManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingPlanScheduledCollection GetList()
		{
			RovingPlanScheduledCriteria rovingplanscheduled = new RovingPlanScheduledCriteria();
			return GetList(rovingplanscheduled, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledCollection GetList(string sortExpression)
		{
			RovingPlanScheduledCriteria rovingplanscheduled = new RovingPlanScheduledCriteria();
			return GetList(rovingplanscheduled, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingPlanScheduledCriteria rovingplanscheduled = new RovingPlanScheduledCriteria();
			return GetList(rovingplanscheduled, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledCollection GetList(RovingPlanScheduledCriteria rovingplanscheduledCriteria)
		{
			return GetList(rovingplanscheduledCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledCollection GetList(RovingPlanScheduledCriteria rovingplanscheduledCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingPlanScheduledCollection myCollection = RovingPlanScheduledDB.GetList(rovingplanscheduledCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingPlanScheduledComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingPlanScheduledCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingPlanScheduledCriteria rovingplanscheduledCriteria)
		{
			return RovingPlanScheduledDB.SelectCountForGetList(rovingplanscheduledCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduled GetItem(int id)
		{
			RovingPlanScheduled rovingplanscheduled = RovingPlanScheduledDB.GetItem(id);
			return rovingplanscheduled;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingPlanScheduled myRovingPlanScheduled)
		{
			if (!myRovingPlanScheduled.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingplanscheduled. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingPlanScheduled.mId != 0)
					AuditUpdate(myRovingPlanScheduled);

				int id = RovingPlanScheduledDB.Save(myRovingPlanScheduled);
				if(myRovingPlanScheduled.mId == 0)
					AuditInsert(myRovingPlanScheduled, id);

				myRovingPlanScheduled.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingPlanScheduled myRovingPlanScheduled)
		{
			if (RovingPlanScheduledDB.Delete(myRovingPlanScheduled.mId))
			{
				AuditDelete(myRovingPlanScheduled);
				return myRovingPlanScheduled.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingPlanScheduled myRovingPlanScheduled, int id)
		{
			AuditManager.AuditInsert(false, myRovingPlanScheduled.mUserFullName,(int)(Tables.ptApi_RovingPlanScheduled),id,"Insert");
		}
		private static void AuditDelete( RovingPlanScheduled myRovingPlanScheduled)
		{
			AuditManager.AuditDelete(false, myRovingPlanScheduled.mUserFullName,(int)(Tables.ptApi_RovingPlanScheduled),myRovingPlanScheduled.mId,"Delete");
		}
		private static void AuditUpdate( RovingPlanScheduled myRovingPlanScheduled)
		{
			RovingPlanScheduled old_rovingplanscheduled = GetItem(myRovingPlanScheduled.mId);
			AuditCollection audit_collection = RovingPlanScheduledAudit.Audit(myRovingPlanScheduled, old_rovingplanscheduled);
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
		private class RovingPlanScheduledComparer : IComparer < RovingPlanScheduled >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingPlanScheduledComparer(string sortExpression)
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

			public int Compare(RovingPlanScheduled x, RovingPlanScheduled y)
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