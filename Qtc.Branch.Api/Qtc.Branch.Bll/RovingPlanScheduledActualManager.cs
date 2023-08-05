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
	public static class RovingPlanScheduledActualManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingPlanScheduledActualCollection GetList()
		{
			RovingPlanScheduledActualCriteria rovingplanscheduledactual = new RovingPlanScheduledActualCriteria();
			return GetList(rovingplanscheduledactual, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualCollection GetList(string sortExpression)
		{
			RovingPlanScheduledActualCriteria rovingplanscheduledactual = new RovingPlanScheduledActualCriteria();
			return GetList(rovingplanscheduledactual, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingPlanScheduledActualCriteria rovingplanscheduledactual = new RovingPlanScheduledActualCriteria();
			return GetList(rovingplanscheduledactual, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualCollection GetList(RovingPlanScheduledActualCriteria rovingplanscheduledactualCriteria)
		{
			return GetList(rovingplanscheduledactualCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualCollection GetList(RovingPlanScheduledActualCriteria rovingplanscheduledactualCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingPlanScheduledActualCollection myCollection = RovingPlanScheduledActualDB.GetList(rovingplanscheduledactualCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingPlanScheduledActualComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingPlanScheduledActualCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingPlanScheduledActualCriteria rovingplanscheduledactualCriteria)
		{
			return RovingPlanScheduledActualDB.SelectCountForGetList(rovingplanscheduledactualCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActual GetItem(int id)
		{
			RovingPlanScheduledActual rovingplanscheduledactual = RovingPlanScheduledActualDB.GetItem(id);
			return rovingplanscheduledactual;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingPlanScheduledActual myRovingPlanScheduledActual)
		{
			if (!myRovingPlanScheduledActual.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingplanscheduledactual. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingPlanScheduledActual.mId != 0)
					AuditUpdate(myRovingPlanScheduledActual);

				int id = RovingPlanScheduledActualDB.Save(myRovingPlanScheduledActual);
				if(myRovingPlanScheduledActual.mId == 0)
					AuditInsert(myRovingPlanScheduledActual, id);

				myRovingPlanScheduledActual.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingPlanScheduledActual myRovingPlanScheduledActual)
		{
			if (RovingPlanScheduledActualDB.Delete(myRovingPlanScheduledActual.mId))
			{
				AuditDelete(myRovingPlanScheduledActual);
				return myRovingPlanScheduledActual.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingPlanScheduledActual myRovingPlanScheduledActual, int id)
		{
			AuditManager.AuditInsert(false, myRovingPlanScheduledActual.mUserFullName,(int)(Tables.ptApi_RovingPlanScheduledActual),id,"Insert");
		}
		private static void AuditDelete( RovingPlanScheduledActual myRovingPlanScheduledActual)
		{
			AuditManager.AuditDelete(false, myRovingPlanScheduledActual.mUserFullName,(int)(Tables.ptApi_RovingPlanScheduledActual),myRovingPlanScheduledActual.mId,"Delete");
		}
		private static void AuditUpdate( RovingPlanScheduledActual myRovingPlanScheduledActual)
		{
			RovingPlanScheduledActual old_rovingplanscheduledactual = GetItem(myRovingPlanScheduledActual.mId);
			AuditCollection audit_collection = RovingPlanScheduledActualAudit.Audit(myRovingPlanScheduledActual, old_rovingplanscheduledactual);
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
		private class RovingPlanScheduledActualComparer : IComparer < RovingPlanScheduledActual >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingPlanScheduledActualComparer(string sortExpression)
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

			public int Compare(RovingPlanScheduledActual x, RovingPlanScheduledActual y)
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