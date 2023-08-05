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
	public static class TraineeAttendanceManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TraineeAttendanceCollection GetList()
		{
			TraineeAttendanceCriteria traineeattendance = new TraineeAttendanceCriteria();
			return GetList(traineeattendance, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TraineeAttendanceCollection GetList(string sortExpression)
		{
			TraineeAttendanceCriteria traineeattendance = new TraineeAttendanceCriteria();
			return GetList(traineeattendance, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TraineeAttendanceCollection GetList(int startRowIndex, int maximumRows)
		{
			TraineeAttendanceCriteria traineeattendance = new TraineeAttendanceCriteria();
			return GetList(traineeattendance, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TraineeAttendanceCollection GetList(TraineeAttendanceCriteria traineeattendanceCriteria)
		{
			return GetList(traineeattendanceCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TraineeAttendanceCollection GetList(TraineeAttendanceCriteria traineeattendanceCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			TraineeAttendanceCollection myCollection = TraineeAttendanceDB.GetList(traineeattendanceCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new TraineeAttendanceComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new TraineeAttendanceCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(TraineeAttendanceCriteria traineeattendanceCriteria)
		{
			return TraineeAttendanceDB.SelectCountForGetList(traineeattendanceCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TraineeAttendance GetItem(int id)
		{
			TraineeAttendance traineeattendance = TraineeAttendanceDB.GetItem(id);
			return traineeattendance;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(TraineeAttendance myTraineeAttendance)
		{
			if (!myTraineeAttendance.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid traineeattendance. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myTraineeAttendance.mId != 0)
					AuditUpdate(myTraineeAttendance);

				int id = TraineeAttendanceDB.Save(myTraineeAttendance);
				if(myTraineeAttendance.mId == 0)
					AuditInsert(myTraineeAttendance, id);

				myTraineeAttendance.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(TraineeAttendance myTraineeAttendance)
		{
			if (TraineeAttendanceDB.Delete(myTraineeAttendance.mId))
			{
				AuditDelete(myTraineeAttendance);
				return myTraineeAttendance.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(TraineeAttendance myTraineeAttendance, int id)
		{
			AuditManager.AuditInsert(false, myTraineeAttendance.mUserFullName,(int)(Tables.ptApi_TraineeAttendance),id,"Insert");
		}
		private static void AuditDelete( TraineeAttendance myTraineeAttendance)
		{
			AuditManager.AuditDelete(false, myTraineeAttendance.mUserFullName,(int)(Tables.ptApi_TraineeAttendance),myTraineeAttendance.mId,"Delete");
		}
		private static void AuditUpdate( TraineeAttendance myTraineeAttendance)
		{
			TraineeAttendance old_traineeattendance = GetItem(myTraineeAttendance.mId);
			AuditCollection audit_collection = TraineeAttendanceAudit.Audit(myTraineeAttendance, old_traineeattendance);
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
		private class TraineeAttendanceComparer : IComparer < TraineeAttendance >
		{
			private string _sortColumn;
			private bool _reverse;
			public TraineeAttendanceComparer(string sortExpression)
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

			public int Compare(TraineeAttendance x, TraineeAttendance y)
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