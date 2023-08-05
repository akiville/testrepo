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
	public static class LmmAttendanceManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static LmmAttendanceCollection GetList()
		{
			LmmAttendanceCriteria lmmattendance = new LmmAttendanceCriteria();
			return GetList(lmmattendance, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceCollection GetList(string sortExpression)
		{
			LmmAttendanceCriteria lmmattendance = new LmmAttendanceCriteria();
			return GetList(lmmattendance, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceCollection GetList(int startRowIndex, int maximumRows)
		{
			LmmAttendanceCriteria lmmattendance = new LmmAttendanceCriteria();
			return GetList(lmmattendance, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceCollection GetList(LmmAttendanceCriteria lmmattendanceCriteria)
		{
			return GetList(lmmattendanceCriteria, string.Empty, -1, -1);
		}

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static LmmAttendanceCollection GetListCount(LmmAttendanceCriteria lmmattendanceCriteria)
        {
            return GetListCount(lmmattendanceCriteria, string.Empty, -1, -1);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public static LmmAttendanceCollection GetListCount(LmmAttendanceCriteria lmmattendanceCriteria, string sortExpression, int startRowIndex, int maximumRows)
        {
            LmmAttendanceCollection myCollection = LmmAttendanceDB.GetListCount(lmmattendanceCriteria);

            if (!string.IsNullOrEmpty(sortExpression))
            {
                myCollection.Sort(new LmmAttendanceComparer(sortExpression));
            }
            if (startRowIndex >= 0 && maximumRows > 0)
            {
                return new LmmAttendanceCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
            }
            return myCollection;
        }
		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceCollection GetList(LmmAttendanceCriteria lmmattendanceCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			LmmAttendanceCollection myCollection = LmmAttendanceDB.GetList(lmmattendanceCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new LmmAttendanceComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new LmmAttendanceCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(LmmAttendanceCriteria lmmattendanceCriteria)
		{
			return LmmAttendanceDB.SelectCountForGetList(lmmattendanceCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendance GetItem(int id)
		{
			LmmAttendance lmmattendance = LmmAttendanceDB.GetItem(id);
			return lmmattendance;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(LmmAttendance myLmmAttendance)
		{
			if (!myLmmAttendance.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid lmmattendance. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myLmmAttendance.mId != 0)
					AuditUpdate(myLmmAttendance);

				int id = LmmAttendanceDB.Save(myLmmAttendance);
				if(myLmmAttendance.mId == 0)
					AuditInsert(myLmmAttendance, id);

				myLmmAttendance.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(LmmAttendance myLmmAttendance)
		{
			if (LmmAttendanceDB.Delete(myLmmAttendance.mId))
			{
				AuditDelete(myLmmAttendance);
				return myLmmAttendance.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(LmmAttendance myLmmAttendance, int id)
		{
			AuditManager.AuditInsert(false, myLmmAttendance.mUserFullName,(int)(Tables.ptApi_LmmAttendance),id,"Insert");
		}
		private static void AuditDelete( LmmAttendance myLmmAttendance)
		{
			AuditManager.AuditDelete(false, myLmmAttendance.mUserFullName,(int)(Tables.ptApi_LmmAttendance),myLmmAttendance.mId,"Delete");
		}
		private static void AuditUpdate( LmmAttendance myLmmAttendance)
		{
			LmmAttendance old_lmmattendance = GetItem(myLmmAttendance.mId);
			AuditCollection audit_collection = LmmAttendanceAudit.Audit(myLmmAttendance, old_lmmattendance);
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
		private class LmmAttendanceComparer : IComparer < LmmAttendance >
		{
			private string _sortColumn;
			private bool _reverse;
			public LmmAttendanceComparer(string sortExpression)
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

			public int Compare(LmmAttendance x, LmmAttendance y)
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