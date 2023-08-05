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
	public static class LmmAttendanceUpdateManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static LmmAttendanceUpdateCollection GetList()
		{
			LmmAttendanceUpdateCriteria lmmattendanceupdate = new LmmAttendanceUpdateCriteria();
			return GetList(lmmattendanceupdate, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceUpdateCollection GetList(string sortExpression)
		{
			LmmAttendanceUpdateCriteria lmmattendanceupdate = new LmmAttendanceUpdateCriteria();
			return GetList(lmmattendanceupdate, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceUpdateCollection GetList(int startRowIndex, int maximumRows)
		{
			LmmAttendanceUpdateCriteria lmmattendanceupdate = new LmmAttendanceUpdateCriteria();
			return GetList(lmmattendanceupdate, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceUpdateCollection GetList(LmmAttendanceUpdateCriteria lmmattendanceupdateCriteria)
		{
			return GetList(lmmattendanceupdateCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceUpdateCollection GetList(LmmAttendanceUpdateCriteria lmmattendanceupdateCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			LmmAttendanceUpdateCollection myCollection = LmmAttendanceUpdateDB.GetList(lmmattendanceupdateCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new LmmAttendanceUpdateComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new LmmAttendanceUpdateCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(LmmAttendanceUpdateCriteria lmmattendanceupdateCriteria)
		{
			return LmmAttendanceUpdateDB.SelectCountForGetList(lmmattendanceupdateCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LmmAttendanceUpdate GetItem(int id)
		{
			LmmAttendanceUpdate lmmattendanceupdate = LmmAttendanceUpdateDB.GetItem(id);
			return lmmattendanceupdate;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(LmmAttendanceUpdate myLmmAttendanceUpdate)
		{
			if (!myLmmAttendanceUpdate.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid lmmattendanceupdate. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myLmmAttendanceUpdate.mId != 0)
					AuditUpdate(myLmmAttendanceUpdate);

				int id = LmmAttendanceUpdateDB.Save(myLmmAttendanceUpdate);
				if(myLmmAttendanceUpdate.mId == 0)
					AuditInsert(myLmmAttendanceUpdate, id);

				myLmmAttendanceUpdate.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(LmmAttendanceUpdate myLmmAttendanceUpdate)
		{
			if (LmmAttendanceUpdateDB.Delete(myLmmAttendanceUpdate.mId))
			{
				AuditDelete(myLmmAttendanceUpdate);
				return myLmmAttendanceUpdate.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(LmmAttendanceUpdate myLmmAttendanceUpdate, int id)
		{
			AuditManager.AuditInsert(false, myLmmAttendanceUpdate.mUserFullName,(int)(Tables.ptApi_LmmAttendanceUpdate),id,"Insert");
		}
		private static void AuditDelete( LmmAttendanceUpdate myLmmAttendanceUpdate)
		{
			AuditManager.AuditDelete(false, myLmmAttendanceUpdate.mUserFullName,(int)(Tables.ptApi_LmmAttendanceUpdate),myLmmAttendanceUpdate.mId,"Delete");
		}
		private static void AuditUpdate( LmmAttendanceUpdate myLmmAttendanceUpdate)
		{
			LmmAttendanceUpdate old_lmmattendanceupdate = GetItem(myLmmAttendanceUpdate.mId);
			AuditCollection audit_collection = LmmAttendanceUpdateAudit.Audit(myLmmAttendanceUpdate, old_lmmattendanceupdate);
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
		private class LmmAttendanceUpdateComparer : IComparer < LmmAttendanceUpdate >
		{
			private string _sortColumn;
			private bool _reverse;
			public LmmAttendanceUpdateComparer(string sortExpression)
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

			public int Compare(LmmAttendanceUpdate x, LmmAttendanceUpdate y)
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