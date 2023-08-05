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
	public static class TrainingAttendanceDetailsManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TrainingAttendanceDetailsCollection GetList()
		{
			TrainingAttendanceDetailsCriteria trainingattendancedetails = new TrainingAttendanceDetailsCriteria();
			return GetList(trainingattendancedetails, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceDetailsCollection GetList(string sortExpression)
		{
			TrainingAttendanceDetailsCriteria trainingattendancedetails = new TrainingAttendanceDetailsCriteria();
			return GetList(trainingattendancedetails, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceDetailsCollection GetList(int startRowIndex, int maximumRows)
		{
			TrainingAttendanceDetailsCriteria trainingattendancedetails = new TrainingAttendanceDetailsCriteria();
			return GetList(trainingattendancedetails, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceDetailsCollection GetList(TrainingAttendanceDetailsCriteria trainingattendancedetailsCriteria)
		{
			return GetList(trainingattendancedetailsCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceDetailsCollection GetList(TrainingAttendanceDetailsCriteria trainingattendancedetailsCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			TrainingAttendanceDetailsCollection myCollection = TrainingAttendanceDetailsDB.GetList(trainingattendancedetailsCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new TrainingAttendanceDetailsComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new TrainingAttendanceDetailsCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(TrainingAttendanceDetailsCriteria trainingattendancedetailsCriteria)
		{
			return TrainingAttendanceDetailsDB.SelectCountForGetList(trainingattendancedetailsCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendanceDetails GetItem(int id)
		{
			TrainingAttendanceDetails trainingattendancedetails = TrainingAttendanceDetailsDB.GetItem(id);
			return trainingattendancedetails;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(TrainingAttendanceDetails myTrainingAttendanceDetails)
		{
			if (!myTrainingAttendanceDetails.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid trainingattendancedetails. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				//if(myTrainingAttendanceDetails.mId != 0)
				//	AuditUpdate(myTrainingAttendanceDetails);

				int id = TrainingAttendanceDetailsDB.Save(myTrainingAttendanceDetails);
				if(myTrainingAttendanceDetails.mId == 0)
					AuditInsert(myTrainingAttendanceDetails, id);

				myTrainingAttendanceDetails.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(TrainingAttendanceDetails myTrainingAttendanceDetails)
		{
			if (TrainingAttendanceDetailsDB.Delete(myTrainingAttendanceDetails.mId))
			{
				AuditDelete(myTrainingAttendanceDetails);
				return myTrainingAttendanceDetails.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(TrainingAttendanceDetails myTrainingAttendanceDetails, int id)
		{
			AuditManager.AuditInsert(false, myTrainingAttendanceDetails.mUserFullName,(int)(Tables.ptApi_TrainingAttendanceDetails),id,"Insert");
		}
		private static void AuditDelete( TrainingAttendanceDetails myTrainingAttendanceDetails)
		{
			AuditManager.AuditDelete(false, myTrainingAttendanceDetails.mUserFullName,(int)(Tables.ptApi_TrainingAttendanceDetails),myTrainingAttendanceDetails.mId,"Delete");
		}
		private static void AuditUpdate( TrainingAttendanceDetails myTrainingAttendanceDetails)
		{
			TrainingAttendanceDetails old_trainingattendancedetails = GetItem(myTrainingAttendanceDetails.mId);
			AuditCollection audit_collection = TrainingAttendanceDetailsAudit.Audit(myTrainingAttendanceDetails, old_trainingattendancedetails);
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
		private class TrainingAttendanceDetailsComparer : IComparer < TrainingAttendanceDetails >
		{
			private string _sortColumn;
			private bool _reverse;
			public TrainingAttendanceDetailsComparer(string sortExpression)
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

			public int Compare(TrainingAttendanceDetails x, TrainingAttendanceDetails y)
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