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
	public static class TraineeManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TraineeCollection GetList()
		{
			TraineeCriteria trainee = new TraineeCriteria();
			return GetList(trainee, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TraineeCollection GetList(string sortExpression)
		{
			TraineeCriteria trainee = new TraineeCriteria();
			return GetList(trainee, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TraineeCollection GetList(int startRowIndex, int maximumRows)
		{
			TraineeCriteria trainee = new TraineeCriteria();
			return GetList(trainee, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TraineeCollection GetList(TraineeCriteria traineeCriteria)
		{
			return GetList(traineeCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TraineeCollection GetList(TraineeCriteria traineeCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			TraineeCollection myCollection = TraineeDB.GetList(traineeCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new TraineeComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new TraineeCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(TraineeCriteria traineeCriteria)
		{
			return TraineeDB.SelectCountForGetList(traineeCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Trainee GetItem(int id)
		{
			Trainee trainee = TraineeDB.GetItem(id);
			return trainee;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Trainee myTrainee)
		{
			if (!myTrainee.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid trainee. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myTrainee.mId != 0)
					AuditUpdate(myTrainee);

				int id = TraineeDB.Save(myTrainee);
				if(myTrainee.mId == 0)
					AuditInsert(myTrainee, id);

				myTrainee.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Trainee myTrainee)
		{
			if (TraineeDB.Delete(myTrainee.mId))
			{
				AuditDelete(myTrainee);
				return myTrainee.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Trainee myTrainee, int id)
		{
			AuditManager.AuditInsert(false, myTrainee.mUserFullName,(int)(Tables.ptApi_Trainee),id,"Insert");
		}
		private static void AuditDelete( Trainee myTrainee)
		{
			AuditManager.AuditDelete(false, myTrainee.mUserFullName,(int)(Tables.ptApi_Trainee),myTrainee.mId,"Delete");
		}
		private static void AuditUpdate( Trainee myTrainee)
		{
			Trainee old_trainee = GetItem(myTrainee.mId);
			AuditCollection audit_collection = TraineeAudit.Audit(myTrainee, old_trainee);
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
		private class TraineeComparer : IComparer < Trainee >
		{
			private string _sortColumn;
			private bool _reverse;
			public TraineeComparer(string sortExpression)
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

			public int Compare(Trainee x, Trainee y)
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