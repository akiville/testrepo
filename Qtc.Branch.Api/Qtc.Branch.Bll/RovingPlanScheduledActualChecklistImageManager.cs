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
	public static class RovingPlanScheduledActualChecklistImageManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingPlanScheduledActualChecklistImageCollection GetList()
		{
			RovingPlanScheduledActualChecklistImageCriteria rovingplanscheduledactualchecklistimage = new RovingPlanScheduledActualChecklistImageCriteria();
			return GetList(rovingplanscheduledactualchecklistimage, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistImageCollection GetList(string sortExpression)
		{
			RovingPlanScheduledActualChecklistImageCriteria rovingplanscheduledactualchecklistimage = new RovingPlanScheduledActualChecklistImageCriteria();
			return GetList(rovingplanscheduledactualchecklistimage, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistImageCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingPlanScheduledActualChecklistImageCriteria rovingplanscheduledactualchecklistimage = new RovingPlanScheduledActualChecklistImageCriteria();
			return GetList(rovingplanscheduledactualchecklistimage, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistImageCollection GetList(RovingPlanScheduledActualChecklistImageCriteria rovingplanscheduledactualchecklistimageCriteria)
		{
			return GetList(rovingplanscheduledactualchecklistimageCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistImageCollection GetList(RovingPlanScheduledActualChecklistImageCriteria rovingplanscheduledactualchecklistimageCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingPlanScheduledActualChecklistImageCollection myCollection = RovingPlanScheduledActualChecklistImageDB.GetList(rovingplanscheduledactualchecklistimageCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingPlanScheduledActualChecklistImageComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingPlanScheduledActualChecklistImageCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingPlanScheduledActualChecklistImageCriteria rovingplanscheduledactualchecklistimageCriteria)
		{
			return RovingPlanScheduledActualChecklistImageDB.SelectCountForGetList(rovingplanscheduledactualchecklistimageCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistImage GetItem(int id)
		{
			RovingPlanScheduledActualChecklistImage rovingplanscheduledactualchecklistimage = RovingPlanScheduledActualChecklistImageDB.GetItem(id);
			return rovingplanscheduledactualchecklistimage;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingPlanScheduledActualChecklistImage myRovingPlanScheduledActualChecklistImage)
		{
			if (!myRovingPlanScheduledActualChecklistImage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingplanscheduledactualchecklistimage. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingPlanScheduledActualChecklistImage.mId != 0)
					AuditUpdate(myRovingPlanScheduledActualChecklistImage);

				int id = RovingPlanScheduledActualChecklistImageDB.Save(myRovingPlanScheduledActualChecklistImage);
				if(myRovingPlanScheduledActualChecklistImage.mId == 0)
					AuditInsert(myRovingPlanScheduledActualChecklistImage, id);

				myRovingPlanScheduledActualChecklistImage.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingPlanScheduledActualChecklistImage myRovingPlanScheduledActualChecklistImage)
		{
			if (RovingPlanScheduledActualChecklistImageDB.Delete(myRovingPlanScheduledActualChecklistImage.mId))
			{
				AuditDelete(myRovingPlanScheduledActualChecklistImage);
				return myRovingPlanScheduledActualChecklistImage.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingPlanScheduledActualChecklistImage myRovingPlanScheduledActualChecklistImage, int id)
		{
			AuditManager.AuditInsert(false, myRovingPlanScheduledActualChecklistImage.mUserFullName,(int)(Tables.ptApi_RovingPlanScheduledActualChecklistImage),id,"Insert");
		}
		private static void AuditDelete( RovingPlanScheduledActualChecklistImage myRovingPlanScheduledActualChecklistImage)
		{
			AuditManager.AuditDelete(false, myRovingPlanScheduledActualChecklistImage.mUserFullName,(int)(Tables.ptApi_RovingPlanScheduledActualChecklistImage),myRovingPlanScheduledActualChecklistImage.mId,"Delete");
		}
		private static void AuditUpdate( RovingPlanScheduledActualChecklistImage myRovingPlanScheduledActualChecklistImage)
		{
			RovingPlanScheduledActualChecklistImage old_rovingplanscheduledactualchecklistimage = GetItem(myRovingPlanScheduledActualChecklistImage.mId);
			AuditCollection audit_collection = RovingPlanScheduledActualChecklistImageAudit.Audit(myRovingPlanScheduledActualChecklistImage, old_rovingplanscheduledactualchecklistimage);
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
		private class RovingPlanScheduledActualChecklistImageComparer : IComparer < RovingPlanScheduledActualChecklistImage >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingPlanScheduledActualChecklistImageComparer(string sortExpression)
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

			public int Compare(RovingPlanScheduledActualChecklistImage x, RovingPlanScheduledActualChecklistImage y)
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