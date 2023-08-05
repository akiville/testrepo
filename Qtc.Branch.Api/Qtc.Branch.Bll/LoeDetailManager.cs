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
	public static class LoeDetailManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static LoeDetailCollection GetList()
		{
			LoeDetailCriteria loedetail = new LoeDetailCriteria();
			return GetList(loedetail, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LoeDetailCollection GetList(string sortExpression)
		{
			LoeDetailCriteria loedetail = new LoeDetailCriteria();
			return GetList(loedetail, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LoeDetailCollection GetList(int startRowIndex, int maximumRows)
		{
			LoeDetailCriteria loedetail = new LoeDetailCriteria();
			return GetList(loedetail, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LoeDetailCollection GetList(LoeDetailCriteria loedetailCriteria)
		{
			return GetList(loedetailCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LoeDetailCollection GetList(LoeDetailCriteria loedetailCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			LoeDetailCollection myCollection = LoeDetailDB.GetList(loedetailCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new LoeDetailComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new LoeDetailCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(LoeDetailCriteria loedetailCriteria)
		{
			return LoeDetailDB.SelectCountForGetList(loedetailCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LoeDetail GetItem(int id)
		{
			LoeDetail loedetail = LoeDetailDB.GetItem(id);
			return loedetail;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(LoeDetail myLoeDetail)
		{
			if (!myLoeDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid loedetail. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myLoeDetail.mId != 0)
					AuditUpdate(myLoeDetail);

				int id = LoeDetailDB.Save(myLoeDetail);
				if(myLoeDetail.mId == 0)
					AuditInsert(myLoeDetail, id);

				myLoeDetail.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(LoeDetail myLoeDetail)
		{
			if (LoeDetailDB.Delete(myLoeDetail.mId))
			{
				AuditDelete(myLoeDetail);
				return myLoeDetail.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(LoeDetail myLoeDetail, int id)
		{
			AuditManager.AuditInsert(false, myLoeDetail.mUserFullName,(int)(Tables.ptApi_LoeDetail),id,"Insert");
		}
		private static void AuditDelete( LoeDetail myLoeDetail)
		{
			AuditManager.AuditDelete(false, myLoeDetail.mUserFullName,(int)(Tables.ptApi_LoeDetail),myLoeDetail.mId,"Delete");
		}
		private static void AuditUpdate( LoeDetail myLoeDetail)
		{
			LoeDetail old_loedetail = GetItem(myLoeDetail.mId);
			AuditCollection audit_collection = LoeDetailAudit.Audit(myLoeDetail, old_loedetail);
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
		private class LoeDetailComparer : IComparer < LoeDetail >
		{
			private string _sortColumn;
			private bool _reverse;
			public LoeDetailComparer(string sortExpression)
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

			public int Compare(LoeDetail x, LoeDetail y)
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