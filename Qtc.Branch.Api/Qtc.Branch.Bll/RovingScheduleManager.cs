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
	public static class RovingScheduleManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingScheduleCollection GetList()
		{
			RovingScheduleCriteria rovingschedule = new RovingScheduleCriteria();
			return GetList(rovingschedule, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingScheduleCollection GetList(string sortExpression)
		{
			RovingScheduleCriteria rovingschedule = new RovingScheduleCriteria();
			return GetList(rovingschedule, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingScheduleCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingScheduleCriteria rovingschedule = new RovingScheduleCriteria();
			return GetList(rovingschedule, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingScheduleCollection GetList(RovingScheduleCriteria rovingscheduleCriteria)
		{
			return GetList(rovingscheduleCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingScheduleCollection GetList(RovingScheduleCriteria rovingscheduleCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingScheduleCollection myCollection = RovingScheduleDB.GetList(rovingscheduleCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingScheduleComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingScheduleCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingScheduleCriteria rovingscheduleCriteria)
		{
			return RovingScheduleDB.SelectCountForGetList(rovingscheduleCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingSchedule GetItem(int id)
		{
			RovingSchedule rovingschedule = RovingScheduleDB.GetItem(id);
			return rovingschedule;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingSchedule myRovingSchedule)
		{
			if (!myRovingSchedule.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingschedule. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingSchedule.mId != 0)
					AuditUpdate(myRovingSchedule);

				int id = RovingScheduleDB.Save(myRovingSchedule);
				if(myRovingSchedule.mId == 0)
					AuditInsert(myRovingSchedule, id);

				myRovingSchedule.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingSchedule myRovingSchedule)
		{
			if (RovingScheduleDB.Delete(myRovingSchedule.mId))
			{
				AuditDelete(myRovingSchedule);
				return myRovingSchedule.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingSchedule myRovingSchedule, int id)
		{
			AuditManager.AuditInsert(false, myRovingSchedule.mUserFullName,(int)(Tables.ptApi_RovingSchedule),id,"Insert");
		}
		private static void AuditDelete( RovingSchedule myRovingSchedule)
		{
			AuditManager.AuditDelete(false, myRovingSchedule.mUserFullName,(int)(Tables.ptApi_RovingSchedule),myRovingSchedule.mId,"Delete");
		}
		private static void AuditUpdate( RovingSchedule myRovingSchedule)
		{
			RovingSchedule old_rovingschedule = GetItem(myRovingSchedule.mId);
			AuditCollection audit_collection = RovingScheduleAudit.Audit(myRovingSchedule, old_rovingschedule);
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
		private class RovingScheduleComparer : IComparer < RovingSchedule >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingScheduleComparer(string sortExpression)
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

			public int Compare(RovingSchedule x, RovingSchedule y)
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