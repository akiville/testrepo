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
	public static class TrainingAttendanceStatusManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TrainingAttendanceStatusCollection GetList()
		{
			TrainingAttendanceStatusCriteria trainingattendancestatus = new TrainingAttendanceStatusCriteria();
			return GetList(trainingattendancestatus, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceStatusCollection GetList(string sortExpression)
		{
			TrainingAttendanceStatusCriteria trainingattendancestatus = new TrainingAttendanceStatusCriteria();
			return GetList(trainingattendancestatus, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceStatusCollection GetList(int startRowIndex, int maximumRows)
		{
			TrainingAttendanceStatusCriteria trainingattendancestatus = new TrainingAttendanceStatusCriteria();
			return GetList(trainingattendancestatus, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceStatusCollection GetList(TrainingAttendanceStatusCriteria trainingattendancestatusCriteria)
		{
			return GetList(trainingattendancestatusCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceStatusCollection GetList(TrainingAttendanceStatusCriteria trainingattendancestatusCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			TrainingAttendanceStatusCollection myCollection = TrainingAttendanceStatusDB.GetList(trainingattendancestatusCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new TrainingAttendanceStatusComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new TrainingAttendanceStatusCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(TrainingAttendanceStatusCriteria trainingattendancestatusCriteria)
		{
			return TrainingAttendanceStatusDB.SelectCountForGetList(trainingattendancestatusCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceStatus GetItem(int id)
		{
			TrainingAttendanceStatus trainingattendancestatus = TrainingAttendanceStatusDB.GetItem(id);
			return trainingattendancestatus;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(TrainingAttendanceStatus myTrainingAttendanceStatus)
		{
			if (!myTrainingAttendanceStatus.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid trainingattendancestatus. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myTrainingAttendanceStatus.mId != 0)
					AuditUpdate(myTrainingAttendanceStatus);

				int id = TrainingAttendanceStatusDB.Save(myTrainingAttendanceStatus);
				if(myTrainingAttendanceStatus.mId == 0)
					AuditInsert(myTrainingAttendanceStatus, id);

				myTrainingAttendanceStatus.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(TrainingAttendanceStatus myTrainingAttendanceStatus)
		{
			if (TrainingAttendanceStatusDB.Delete(myTrainingAttendanceStatus.mId))
			{
				AuditDelete(myTrainingAttendanceStatus);
				return myTrainingAttendanceStatus.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(TrainingAttendanceStatus myTrainingAttendanceStatus, int id)
		{
			AuditManager.AuditInsert(false, myTrainingAttendanceStatus.mUserFullName,(int)(Tables.ptApi_TrainingAttendanceStatus),id,"Insert");
		}
		private static void AuditDelete( TrainingAttendanceStatus myTrainingAttendanceStatus)
		{
			AuditManager.AuditDelete(false, myTrainingAttendanceStatus.mUserFullName,(int)(Tables.ptApi_TrainingAttendanceStatus),myTrainingAttendanceStatus.mId,"Delete");
		}
		private static void AuditUpdate( TrainingAttendanceStatus myTrainingAttendanceStatus)
		{
			TrainingAttendanceStatus old_trainingattendancestatus = GetItem(myTrainingAttendanceStatus.mId);
			AuditCollection audit_collection = TrainingAttendanceStatusAudit.Audit(myTrainingAttendanceStatus, old_trainingattendancestatus);
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
		private class TrainingAttendanceStatusComparer : IComparer < TrainingAttendanceStatus >
		{
			private string _sortColumn;
			private bool _reverse;
			public TrainingAttendanceStatusComparer(string sortExpression)
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

			public int Compare(TrainingAttendanceStatus x, TrainingAttendanceStatus y)
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