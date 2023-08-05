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
	public static class TrainingAttendance2Manager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TrainingAttendance2Collection GetList()
		{
			TrainingAttendance2Criteria trainingattendance2 = new TrainingAttendance2Criteria();
			return GetList(trainingattendance2, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendance2Collection GetList(string sortExpression)
		{
			TrainingAttendance2Criteria trainingattendance2 = new TrainingAttendance2Criteria();
			return GetList(trainingattendance2, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendance2Collection GetList(int startRowIndex, int maximumRows)
		{
			TrainingAttendance2Criteria trainingattendance2 = new TrainingAttendance2Criteria();
			return GetList(trainingattendance2, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendance2Collection GetList(TrainingAttendance2Criteria trainingattendance2Criteria)
		{
			return GetList(trainingattendance2Criteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendance2Collection GetList(TrainingAttendance2Criteria trainingattendance2Criteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			TrainingAttendance2Collection myCollection = TrainingAttendance2DB.GetList(trainingattendance2Criteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new TrainingAttendance2Comparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new TrainingAttendance2Collection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(TrainingAttendance2Criteria trainingattendance2Criteria)
		{
			return TrainingAttendance2DB.SelectCountForGetList(trainingattendance2Criteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TrainingAttendance2 GetItem(int id)
		{
			TrainingAttendance2 trainingattendance2 = TrainingAttendance2DB.GetItem(id);
			return trainingattendance2;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(TrainingAttendance2 myTrainingAttendance2)
		{
			if (!myTrainingAttendance2.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid trainingattendance2. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myTrainingAttendance2.mId != 0)
					AuditUpdate(myTrainingAttendance2);

				int id = TrainingAttendance2DB.Save(myTrainingAttendance2);
				if(myTrainingAttendance2.mId == 0)
					AuditInsert(myTrainingAttendance2, id);

				myTrainingAttendance2.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(TrainingAttendance2 myTrainingAttendance2)
		{
			if (TrainingAttendance2DB.Delete(myTrainingAttendance2.mId))
			{
				AuditDelete(myTrainingAttendance2);
				return myTrainingAttendance2.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(TrainingAttendance2 myTrainingAttendance2, int id)
		{
			AuditManager.AuditInsert(false, myTrainingAttendance2.mUserFullName,(int)(Tables.ptApi_TrainingAttendance2),id,"Insert");
		}
		private static void AuditDelete( TrainingAttendance2 myTrainingAttendance2)
		{
			AuditManager.AuditDelete(false, myTrainingAttendance2.mUserFullName,(int)(Tables.ptApi_TrainingAttendance2),myTrainingAttendance2.mId,"Delete");
		}
		private static void AuditUpdate( TrainingAttendance2 myTrainingAttendance2)
		{
			TrainingAttendance2 old_trainingattendance2 = GetItem(myTrainingAttendance2.mId);
			AuditCollection audit_collection = TrainingAttendance2Audit.Audit(myTrainingAttendance2, old_trainingattendance2);
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
		private class TrainingAttendance2Comparer : IComparer < TrainingAttendance2 >
		{
			private string _sortColumn;
			private bool _reverse;
			public TrainingAttendance2Comparer(string sortExpression)
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

			public int Compare(TrainingAttendance2 x, TrainingAttendance2 y)
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