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
	public static class RovingPlanScheduledActualChecklistLSVManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingPlanScheduledActualChecklistLSVCollection GetList()
		{
			RovingPlanScheduledActualChecklistLSVCriteria rovingplanscheduledactualchecklistlsv = new RovingPlanScheduledActualChecklistLSVCriteria();
			return GetList(rovingplanscheduledactualchecklistlsv, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistLSVCollection GetList(string sortExpression)
		{
			RovingPlanScheduledActualChecklistLSVCriteria rovingplanscheduledactualchecklistlsv = new RovingPlanScheduledActualChecklistLSVCriteria();
			return GetList(rovingplanscheduledactualchecklistlsv, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistLSVCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingPlanScheduledActualChecklistLSVCriteria rovingplanscheduledactualchecklistlsv = new RovingPlanScheduledActualChecklistLSVCriteria();
			return GetList(rovingplanscheduledactualchecklistlsv, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistLSVCollection GetList(RovingPlanScheduledActualChecklistLSVCriteria rovingplanscheduledactualchecklistlsvCriteria)
		{
			return GetList(rovingplanscheduledactualchecklistlsvCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistLSVCollection GetList(RovingPlanScheduledActualChecklistLSVCriteria rovingplanscheduledactualchecklistlsvCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingPlanScheduledActualChecklistLSVCollection myCollection = RovingPlanScheduledActualChecklistLSVDB.GetList(rovingplanscheduledactualchecklistlsvCriteria);

            foreach (RovingPlanScheduledActualChecklistLSV item in myCollection)
            {
                RovingCheckListViolationDetailCollection myRovingCheckListViolationDetailCollection = new RovingCheckListViolationDetailCollection();
                RovingCheckListViolationDetailCriteria myRovingCheckListViolationDetailCriteria = new RovingCheckListViolationDetailCriteria();

                myRovingCheckListViolationDetailCriteria.mRpsChklistId = item.mRpsChklistId;
                myRovingCheckListViolationDetailCriteria.mRpsId = item.mRpsId;
                myRovingCheckListViolationDetailCriteria.mViolationId = item.mViolationId;

                item.mRovingCheckListViolationDetailCollection = RovingCheckListViolationDetailManager.GetList(myRovingCheckListViolationDetailCriteria);

            }

            if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingPlanScheduledActualChecklistLSVComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingPlanScheduledActualChecklistLSVCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingPlanScheduledActualChecklistLSVCriteria rovingplanscheduledactualchecklistlsvCriteria)
		{
			return RovingPlanScheduledActualChecklistLSVDB.SelectCountForGetList(rovingplanscheduledactualchecklistlsvCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanScheduledActualChecklistLSV GetItem(int id)
		{
			RovingPlanScheduledActualChecklistLSV rovingplanscheduledactualchecklistlsv = RovingPlanScheduledActualChecklistLSVDB.GetItem(id);
			return rovingplanscheduledactualchecklistlsv;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingPlanScheduledActualChecklistLSV myRovingPlanScheduledActualChecklistLSV)
		{
			if (!myRovingPlanScheduledActualChecklistLSV.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingplanscheduledactualchecklistlsv. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingPlanScheduledActualChecklistLSV.mId != 0)
					AuditUpdate(myRovingPlanScheduledActualChecklistLSV);

				int id = RovingPlanScheduledActualChecklistLSVDB.Save(myRovingPlanScheduledActualChecklistLSV);
				if(myRovingPlanScheduledActualChecklistLSV.mId == 0)
					AuditInsert(myRovingPlanScheduledActualChecklistLSV, id);

				myRovingPlanScheduledActualChecklistLSV.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingPlanScheduledActualChecklistLSV myRovingPlanScheduledActualChecklistLSV)
		{
			if (RovingPlanScheduledActualChecklistLSVDB.Delete(myRovingPlanScheduledActualChecklistLSV.mId))
			{
				AuditDelete(myRovingPlanScheduledActualChecklistLSV);
				return myRovingPlanScheduledActualChecklistLSV.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingPlanScheduledActualChecklistLSV myRovingPlanScheduledActualChecklistLSV, int id)
		{
			AuditManager.AuditInsert(false, myRovingPlanScheduledActualChecklistLSV.mUserFullName,(int)(Tables.ptApi_RovingPlanScheduledActualChecklistLSV),id,"Insert");
		}
		private static void AuditDelete( RovingPlanScheduledActualChecklistLSV myRovingPlanScheduledActualChecklistLSV)
		{
			AuditManager.AuditDelete(false, myRovingPlanScheduledActualChecklistLSV.mUserFullName,(int)(Tables.ptApi_RovingPlanScheduledActualChecklistLSV),myRovingPlanScheduledActualChecklistLSV.mId,"Delete");
		}
		private static void AuditUpdate( RovingPlanScheduledActualChecklistLSV myRovingPlanScheduledActualChecklistLSV)
		{
			RovingPlanScheduledActualChecklistLSV old_rovingplanscheduledactualchecklistlsv = GetItem(myRovingPlanScheduledActualChecklistLSV.mId);
			AuditCollection audit_collection = RovingPlanScheduledActualChecklistLSVAudit.Audit(myRovingPlanScheduledActualChecklistLSV, old_rovingplanscheduledactualchecklistlsv);
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
		private class RovingPlanScheduledActualChecklistLSVComparer : IComparer < RovingPlanScheduledActualChecklistLSV >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingPlanScheduledActualChecklistLSVComparer(string sortExpression)
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

			public int Compare(RovingPlanScheduledActualChecklistLSV x, RovingPlanScheduledActualChecklistLSV y)
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