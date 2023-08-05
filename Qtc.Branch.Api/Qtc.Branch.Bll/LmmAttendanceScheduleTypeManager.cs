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
	public static class LmmAttendanceScheduleTypeManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static LmmAttendanceScheduleTypeCollection GetList()
		{
			LmmAttendanceScheduleTypeCriteria lmmattendancescheduletype = new LmmAttendanceScheduleTypeCriteria();
			return GetList(lmmattendancescheduletype, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceScheduleTypeCollection GetList(string sortExpression)
		{
			LmmAttendanceScheduleTypeCriteria lmmattendancescheduletype = new LmmAttendanceScheduleTypeCriteria();
			return GetList(lmmattendancescheduletype, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceScheduleTypeCollection GetList(int startRowIndex, int maximumRows)
		{
			LmmAttendanceScheduleTypeCriteria lmmattendancescheduletype = new LmmAttendanceScheduleTypeCriteria();
			return GetList(lmmattendancescheduletype, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceScheduleTypeCollection GetList(LmmAttendanceScheduleTypeCriteria lmmattendancescheduletypeCriteria)
		{
			return GetList(lmmattendancescheduletypeCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceScheduleTypeCollection GetList(LmmAttendanceScheduleTypeCriteria lmmattendancescheduletypeCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			LmmAttendanceScheduleTypeCollection myCollection = LmmAttendanceScheduleTypeDB.GetList(lmmattendancescheduletypeCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new LmmAttendanceScheduleTypeComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new LmmAttendanceScheduleTypeCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(LmmAttendanceScheduleTypeCriteria lmmattendancescheduletypeCriteria)
		{
			return LmmAttendanceScheduleTypeDB.SelectCountForGetList(lmmattendancescheduletypeCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceScheduleType GetItem(int id)
		{
			LmmAttendanceScheduleType lmmattendancescheduletype = LmmAttendanceScheduleTypeDB.GetItem(id);
			return lmmattendancescheduletype;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(LmmAttendanceScheduleType myLmmAttendanceScheduleType)
		{
			if (!myLmmAttendanceScheduleType.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid lmmattendancescheduletype. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myLmmAttendanceScheduleType.mId != 0)
					AuditUpdate(myLmmAttendanceScheduleType);

				int id = LmmAttendanceScheduleTypeDB.Save(myLmmAttendanceScheduleType);
				if(myLmmAttendanceScheduleType.mId == 0)
					AuditInsert(myLmmAttendanceScheduleType, id);

				myLmmAttendanceScheduleType.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(LmmAttendanceScheduleType myLmmAttendanceScheduleType)
		{
			if (LmmAttendanceScheduleTypeDB.Delete(myLmmAttendanceScheduleType.mId))
			{
				AuditDelete(myLmmAttendanceScheduleType);
				return myLmmAttendanceScheduleType.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(LmmAttendanceScheduleType myLmmAttendanceScheduleType, int id)
		{
			AuditManager.AuditInsert(false, myLmmAttendanceScheduleType.mUserFullName,(int)(Tables.ptApi_LmmAttendanceScheduleType),id,"Insert");
		}
		private static void AuditDelete( LmmAttendanceScheduleType myLmmAttendanceScheduleType)
		{
			AuditManager.AuditDelete(false, myLmmAttendanceScheduleType.mUserFullName,(int)(Tables.ptApi_LmmAttendanceScheduleType),myLmmAttendanceScheduleType.mId,"Delete");
		}
		private static void AuditUpdate( LmmAttendanceScheduleType myLmmAttendanceScheduleType)
		{
			LmmAttendanceScheduleType old_lmmattendancescheduletype = GetItem(myLmmAttendanceScheduleType.mId);
			AuditCollection audit_collection = LmmAttendanceScheduleTypeAudit.Audit(myLmmAttendanceScheduleType, old_lmmattendancescheduletype);
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
		private class LmmAttendanceScheduleTypeComparer : IComparer < LmmAttendanceScheduleType >
		{
			private string _sortColumn;
			private bool _reverse;
			public LmmAttendanceScheduleTypeComparer(string sortExpression)
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

			public int Compare(LmmAttendanceScheduleType x, LmmAttendanceScheduleType y)
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