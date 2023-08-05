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
	public static class AuditMessageDetailManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static AuditMessageDetailCollection GetList()
		{
			AuditMessageDetailCriteria auditmessagedetail = new AuditMessageDetailCriteria();
			return GetList(auditmessagedetail, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditMessageDetailCollection GetList(string sortExpression)
		{
			AuditMessageDetailCriteria auditmessagedetail = new AuditMessageDetailCriteria();
			return GetList(auditmessagedetail, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditMessageDetailCollection GetList(int startRowIndex, int maximumRows)
		{
			AuditMessageDetailCriteria auditmessagedetail = new AuditMessageDetailCriteria();
			return GetList(auditmessagedetail, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditMessageDetailCollection GetList(AuditMessageDetailCriteria auditmessagedetailCriteria)
		{
			return GetList(auditmessagedetailCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditMessageDetailCollection GetList(AuditMessageDetailCriteria auditmessagedetailCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			AuditMessageDetailCollection myCollection = AuditMessageDetailDB.GetList(auditmessagedetailCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new AuditMessageDetailComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new AuditMessageDetailCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(AuditMessageDetailCriteria auditmessagedetailCriteria)
		{
			return AuditMessageDetailDB.SelectCountForGetList(auditmessagedetailCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static AuditMessageDetail GetItem(int id)
		{
			AuditMessageDetail auditmessagedetail = AuditMessageDetailDB.GetItem(id);
			return auditmessagedetail;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(AuditMessageDetail myAuditMessageDetail)
		{
			if (!myAuditMessageDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid auditmessagedetail. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myAuditMessageDetail.mId != 0)
					AuditUpdate(myAuditMessageDetail);

				int id = AuditMessageDetailDB.Save(myAuditMessageDetail);
				if(myAuditMessageDetail.mId == 0)
					AuditInsert(myAuditMessageDetail, id);

				myAuditMessageDetail.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(AuditMessageDetail myAuditMessageDetail)
		{
			if (AuditMessageDetailDB.Delete(myAuditMessageDetail.mId))
			{
				AuditDelete(myAuditMessageDetail);
				return myAuditMessageDetail.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(AuditMessageDetail myAuditMessageDetail, int id)
		{
			AuditManager.AuditInsert(false, myAuditMessageDetail.mUserFullName,(int)(Tables.ptApi_AuditMessageDetail),id,"Insert");
		}
		private static void AuditDelete( AuditMessageDetail myAuditMessageDetail)
		{
			AuditManager.AuditDelete(false, myAuditMessageDetail.mUserFullName,(int)(Tables.ptApi_AuditMessageDetail),myAuditMessageDetail.mId,"Delete");
		}
		private static void AuditUpdate( AuditMessageDetail myAuditMessageDetail)
		{
			AuditMessageDetail old_auditmessagedetail = GetItem(myAuditMessageDetail.mId);
			AuditCollection audit_collection = AuditMessageDetailAudit.Audit(myAuditMessageDetail, old_auditmessagedetail);
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
		private class AuditMessageDetailComparer : IComparer < AuditMessageDetail >
		{
			private string _sortColumn;
			private bool _reverse;
			public AuditMessageDetailComparer(string sortExpression)
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

			public int Compare(AuditMessageDetail x, AuditMessageDetail y)
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