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
	public static class RovingPlanManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingPlanCollection GetList()
		{
			RovingPlanCriteria rovingplan = new RovingPlanCriteria();
			return GetList(rovingplan, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanCollection GetList(string sortExpression)
		{
			RovingPlanCriteria rovingplan = new RovingPlanCriteria();
			return GetList(rovingplan, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingPlanCriteria rovingplan = new RovingPlanCriteria();
			return GetList(rovingplan, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanCollection GetList(RovingPlanCriteria rovingplanCriteria)
		{
			return GetList(rovingplanCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanCollection GetList(RovingPlanCriteria rovingplanCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingPlanCollection myCollection = RovingPlanDB.GetList(rovingplanCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingPlanComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingPlanCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingPlanCriteria rovingplanCriteria)
		{
			return RovingPlanDB.SelectCountForGetList(rovingplanCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlan GetItem(int id)
		{
			RovingPlan rovingplan = RovingPlanDB.GetItem(id);
			return rovingplan;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingPlan myRovingPlan)
		{
			if (!myRovingPlan.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingplan. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingPlan.mId != 0)
					AuditUpdate(myRovingPlan);

				int id = RovingPlanDB.Save(myRovingPlan);
				if(myRovingPlan.mId == 0)
					AuditInsert(myRovingPlan, id);

				myRovingPlan.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingPlan myRovingPlan)
		{
			if (RovingPlanDB.Delete(myRovingPlan.mId))
			{
				AuditDelete(myRovingPlan);
				return myRovingPlan.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingPlan myRovingPlan, int id)
		{
			AuditManager.AuditInsert(false, myRovingPlan.mUserFullName,(int)(Tables.ptApi_RovingPlan),id,"Insert");
		}
		private static void AuditDelete( RovingPlan myRovingPlan)
		{
			AuditManager.AuditDelete(false, myRovingPlan.mUserFullName,(int)(Tables.ptApi_RovingPlan),myRovingPlan.mId,"Delete");
		}
		private static void AuditUpdate( RovingPlan myRovingPlan)
		{
			RovingPlan old_rovingplan = GetItem(myRovingPlan.mId);
			AuditCollection audit_collection = RovingPlanAudit.Audit(myRovingPlan, old_rovingplan);
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
		private class RovingPlanComparer : IComparer < RovingPlan >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingPlanComparer(string sortExpression)
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

			public int Compare(RovingPlan x, RovingPlan y)
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