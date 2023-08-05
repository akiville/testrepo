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
	public static class IbwDetailManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static IbwDetailCollection GetList()
		{
			IbwDetailCriteria ibwdetail = new IbwDetailCriteria();
			return GetList(ibwdetail, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static IbwDetailCollection GetList(string sortExpression)
		{
			IbwDetailCriteria ibwdetail = new IbwDetailCriteria();
			return GetList(ibwdetail, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static IbwDetailCollection GetList(int startRowIndex, int maximumRows)
		{
			IbwDetailCriteria ibwdetail = new IbwDetailCriteria();
			return GetList(ibwdetail, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static IbwDetailCollection GetList(IbwDetailCriteria ibwdetailCriteria)
		{
			return GetList(ibwdetailCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static IbwDetailCollection GetList(IbwDetailCriteria ibwdetailCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			IbwDetailCollection myCollection = IbwDetailDB.GetList(ibwdetailCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new IbwDetailComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new IbwDetailCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(IbwDetailCriteria ibwdetailCriteria)
		{
			return IbwDetailDB.SelectCountForGetList(ibwdetailCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static IbwDetail GetItem(int id)
		{
			IbwDetail ibwdetail = IbwDetailDB.GetItem(id);
			return ibwdetail;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(IbwDetail myIbwDetail)
		{
			if (!myIbwDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid ibwdetail. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myIbwDetail.mId != 0)
					AuditUpdate(myIbwDetail);

				int id = IbwDetailDB.Save(myIbwDetail);
				if(myIbwDetail.mId == 0)
					AuditInsert(myIbwDetail, id);

				myIbwDetail.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(IbwDetail myIbwDetail)
		{
			if (IbwDetailDB.Delete(myIbwDetail.mId))
			{
				AuditDelete(myIbwDetail);
				return myIbwDetail.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(IbwDetail myIbwDetail, int id)
		{
			AuditManager.AuditInsert(false, myIbwDetail.mUserFullName,(int)(Tables.ptApi_IbwDetail),id,"Insert");
		}
		private static void AuditDelete( IbwDetail myIbwDetail)
		{
			AuditManager.AuditDelete(false, myIbwDetail.mUserFullName,(int)(Tables.ptApi_IbwDetail),myIbwDetail.mId,"Delete");
		}
		private static void AuditUpdate( IbwDetail myIbwDetail)
		{
			IbwDetail old_ibwdetail = GetItem(myIbwDetail.mId);
			AuditCollection audit_collection = IbwDetailAudit.Audit(myIbwDetail, old_ibwdetail);
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
		private class IbwDetailComparer : IComparer < IbwDetail >
		{
			private string _sortColumn;
			private bool _reverse;
			public IbwDetailComparer(string sortExpression)
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

			public int Compare(IbwDetail x, IbwDetail y)
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