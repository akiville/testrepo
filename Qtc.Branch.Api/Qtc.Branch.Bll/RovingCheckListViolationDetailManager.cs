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
	public static class RovingCheckListViolationDetailManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingCheckListViolationDetailCollection GetList()
		{
			RovingCheckListViolationDetailCriteria rovingchecklistviolationdetail = new RovingCheckListViolationDetailCriteria();
			return GetList(rovingchecklistviolationdetail, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationDetailCollection GetList(string sortExpression)
		{
			RovingCheckListViolationDetailCriteria rovingchecklistviolationdetail = new RovingCheckListViolationDetailCriteria();
			return GetList(rovingchecklistviolationdetail, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationDetailCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingCheckListViolationDetailCriteria rovingchecklistviolationdetail = new RovingCheckListViolationDetailCriteria();
			return GetList(rovingchecklistviolationdetail, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationDetailCollection GetList(RovingCheckListViolationDetailCriteria rovingchecklistviolationdetailCriteria)
		{
			return GetList(rovingchecklistviolationdetailCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationDetailCollection GetList(RovingCheckListViolationDetailCriteria rovingchecklistviolationdetailCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingCheckListViolationDetailCollection myCollection = RovingCheckListViolationDetailDB.GetList(rovingchecklistviolationdetailCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingCheckListViolationDetailComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingCheckListViolationDetailCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingCheckListViolationDetailCriteria rovingchecklistviolationdetailCriteria)
		{
			return RovingCheckListViolationDetailDB.SelectCountForGetList(rovingchecklistviolationdetailCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingCheckListViolationDetail GetItem(int id)
		{
			RovingCheckListViolationDetail rovingchecklistviolationdetail = RovingCheckListViolationDetailDB.GetItem(id);
			return rovingchecklistviolationdetail;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingCheckListViolationDetail myRovingCheckListViolationDetail)
		{
			if (!myRovingCheckListViolationDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingchecklistviolationdetail. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingCheckListViolationDetail.mId != 0)
					AuditUpdate(myRovingCheckListViolationDetail);

				int id = RovingCheckListViolationDetailDB.Save(myRovingCheckListViolationDetail);

                if (myRovingCheckListViolationDetail.rovingCheckListViolationPersonnelCollection != null)
                {
                    foreach(RovingCheckListViolationPersonnel item in myRovingCheckListViolationDetail.rovingCheckListViolationPersonnelCollection  ) {
                        item.mRclvdDetailId = id;
                        if (item.Validate())
                        {
                            int result;
                            result = RovingCheckListViolationPersonnelManager.Save(item);
                        }
                    }
                }

				if(myRovingCheckListViolationDetail.mId == 0)
					AuditInsert(myRovingCheckListViolationDetail, id);

				myRovingCheckListViolationDetail.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingCheckListViolationDetail myRovingCheckListViolationDetail)
		{
			if (RovingCheckListViolationDetailDB.Delete(myRovingCheckListViolationDetail.mId))
			{
				AuditDelete(myRovingCheckListViolationDetail);
				return myRovingCheckListViolationDetail.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingCheckListViolationDetail myRovingCheckListViolationDetail, int id)
		{
			AuditManager.AuditInsert(false, myRovingCheckListViolationDetail.mUserFullName,(int)(Tables.ptApi_RovingCheckListViolationDetail),id,"Insert");
		}
		private static void AuditDelete( RovingCheckListViolationDetail myRovingCheckListViolationDetail)
		{
			AuditManager.AuditDelete(false, myRovingCheckListViolationDetail.mUserFullName,(int)(Tables.ptApi_RovingCheckListViolationDetail),myRovingCheckListViolationDetail.mId,"Delete");
		}
		private static void AuditUpdate( RovingCheckListViolationDetail myRovingCheckListViolationDetail)
		{
			RovingCheckListViolationDetail old_rovingchecklistviolationdetail = GetItem(myRovingCheckListViolationDetail.mId);
			AuditCollection audit_collection = RovingCheckListViolationDetailAudit.Audit(myRovingCheckListViolationDetail, old_rovingchecklistviolationdetail);
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
		private class RovingCheckListViolationDetailComparer : IComparer < RovingCheckListViolationDetail >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingCheckListViolationDetailComparer(string sortExpression)
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

			public int Compare(RovingCheckListViolationDetail x, RovingCheckListViolationDetail y)
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