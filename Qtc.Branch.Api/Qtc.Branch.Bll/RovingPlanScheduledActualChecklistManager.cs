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
	public static class RovingPlanScheduledActualChecklistManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingPlanScheduledActualChecklistCollection GetList()
		{
			RovingPlanScheduledActualChecklistCriteria rovingplanscheduledactualchecklist = new RovingPlanScheduledActualChecklistCriteria();
			return GetList(rovingplanscheduledactualchecklist, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistCollection GetList(string sortExpression)
		{
			RovingPlanScheduledActualChecklistCriteria rovingplanscheduledactualchecklist = new RovingPlanScheduledActualChecklistCriteria();
			return GetList(rovingplanscheduledactualchecklist, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingPlanScheduledActualChecklistCriteria rovingplanscheduledactualchecklist = new RovingPlanScheduledActualChecklistCriteria();
			return GetList(rovingplanscheduledactualchecklist, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistCollection GetList(RovingPlanScheduledActualChecklistCriteria rovingplanscheduledactualchecklistCriteria)
		{
			return GetList(rovingplanscheduledactualchecklistCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistCollection GetList(RovingPlanScheduledActualChecklistCriteria rovingplanscheduledactualchecklistCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingPlanScheduledActualChecklistCollection myCollection = RovingPlanScheduledActualChecklistDB.GetList(rovingplanscheduledactualchecklistCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingPlanScheduledActualChecklistComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingPlanScheduledActualChecklistCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingPlanScheduledActualChecklistCriteria rovingplanscheduledactualchecklistCriteria)
		{
			return RovingPlanScheduledActualChecklistDB.SelectCountForGetList(rovingplanscheduledactualchecklistCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklist GetItem(int id)
		{
			RovingPlanScheduledActualChecklist rovingplanscheduledactualchecklist = RovingPlanScheduledActualChecklistDB.GetItem(id);
			return rovingplanscheduledactualchecklist;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingPlanScheduledActualChecklist myRovingPlanScheduledActualChecklist)
		{
			if (!myRovingPlanScheduledActualChecklist.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingplanscheduledactualchecklist. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingPlanScheduledActualChecklist.mId != 0)
					AuditUpdate(myRovingPlanScheduledActualChecklist);

				int id = RovingPlanScheduledActualChecklistDB.Save(myRovingPlanScheduledActualChecklist);
				if(myRovingPlanScheduledActualChecklist.mId == 0)
					AuditInsert(myRovingPlanScheduledActualChecklist, id);

				myRovingPlanScheduledActualChecklist.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingPlanScheduledActualChecklist myRovingPlanScheduledActualChecklist)
		{
			if (RovingPlanScheduledActualChecklistDB.Delete(myRovingPlanScheduledActualChecklist.mId))
			{
				AuditDelete(myRovingPlanScheduledActualChecklist);
				return myRovingPlanScheduledActualChecklist.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingPlanScheduledActualChecklist myRovingPlanScheduledActualChecklist, int id)
		{
			AuditManager.AuditInsert(false, myRovingPlanScheduledActualChecklist.mUserFullName,(int)(Tables.ptApi_RovingPlanScheduledActualChecklist),id,"Insert");
		}
		private static void AuditDelete( RovingPlanScheduledActualChecklist myRovingPlanScheduledActualChecklist)
		{
			AuditManager.AuditDelete(false, myRovingPlanScheduledActualChecklist.mUserFullName,(int)(Tables.ptApi_RovingPlanScheduledActualChecklist),myRovingPlanScheduledActualChecklist.mId,"Delete");
		}
		private static void AuditUpdate( RovingPlanScheduledActualChecklist myRovingPlanScheduledActualChecklist)
		{
			RovingPlanScheduledActualChecklist old_rovingplanscheduledactualchecklist = GetItem(myRovingPlanScheduledActualChecklist.mId);
			AuditCollection audit_collection = RovingPlanScheduledActualChecklistAudit.Audit(myRovingPlanScheduledActualChecklist, old_rovingplanscheduledactualchecklist);
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
		private class RovingPlanScheduledActualChecklistComparer : IComparer < RovingPlanScheduledActualChecklist >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingPlanScheduledActualChecklistComparer(string sortExpression)
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

			public int Compare(RovingPlanScheduledActualChecklist x, RovingPlanScheduledActualChecklist y)
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