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
	public static class TrainingAttendanceManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TrainingAttendanceCollection GetList()
		{
			TrainingAttendanceCriteria trainingattendance = new TrainingAttendanceCriteria();
			return GetList(trainingattendance, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceCollection GetList(string sortExpression)
		{
			TrainingAttendanceCriteria trainingattendance = new TrainingAttendanceCriteria();
			return GetList(trainingattendance, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceCollection GetList(int startRowIndex, int maximumRows)
		{
			TrainingAttendanceCriteria trainingattendance = new TrainingAttendanceCriteria();
			return GetList(trainingattendance, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceCollection GetList(TrainingAttendanceCriteria trainingattendanceCriteria)
		{
			return GetList(trainingattendanceCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceCollection GetList(TrainingAttendanceCriteria trainingattendanceCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			TrainingAttendanceCollection myCollection = TrainingAttendanceDB.GetList(trainingattendanceCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new TrainingAttendanceComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new TrainingAttendanceCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(TrainingAttendanceCriteria trainingattendanceCriteria)
		{
			return TrainingAttendanceDB.SelectCountForGetList(trainingattendanceCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendance GetItem(int id)
		{
			TrainingAttendance trainingattendance = TrainingAttendanceDB.GetItem(id);
			return trainingattendance;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(TrainingAttendance myTrainingAttendance)
		{
			if (!myTrainingAttendance.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid trainingattendance. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				//if(myTrainingAttendance.mId != 0)
				//	AuditUpdate(myTrainingAttendance);

				int id = TrainingAttendanceDB.Save(myTrainingAttendance);
				if(myTrainingAttendance.mId == 0)
					AuditInsert(myTrainingAttendance, id);

				myTrainingAttendance.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(TrainingAttendance myTrainingAttendance)
		{
			if (TrainingAttendanceDB.Delete(myTrainingAttendance.mId))
			{
				AuditDelete(myTrainingAttendance);
				return myTrainingAttendance.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(TrainingAttendance myTrainingAttendance, int id)
		{
			AuditManager.AuditInsert(false, myTrainingAttendance.mUserFullName,(int)(Tables.ptApi_TrainingAttendance),id,"Insert");
		}
		private static void AuditDelete( TrainingAttendance myTrainingAttendance)
		{
			AuditManager.AuditDelete(false, myTrainingAttendance.mUserFullName,(int)(Tables.ptApi_TrainingAttendance),myTrainingAttendance.mId,"Delete");
		}
		private static void AuditUpdate( TrainingAttendance myTrainingAttendance)
		{
			TrainingAttendance old_trainingattendance = GetItem(myTrainingAttendance.mId);
			AuditCollection audit_collection = TrainingAttendanceAudit.Audit(myTrainingAttendance, old_trainingattendance);
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
		private class TrainingAttendanceComparer : IComparer < TrainingAttendance >
		{
			private string _sortColumn;
			private bool _reverse;
			public TrainingAttendanceComparer(string sortExpression)
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

			public int Compare(TrainingAttendance x, TrainingAttendance y)
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