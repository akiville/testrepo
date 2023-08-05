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
	public static class RepairDetailManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RepairDetailCollection GetList()
		{
			RepairDetailCriteria repairdetail = new RepairDetailCriteria();
			return GetList(repairdetail, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RepairDetailCollection GetList(string sortExpression)
		{
			RepairDetailCriteria repairdetail = new RepairDetailCriteria();
			return GetList(repairdetail, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RepairDetailCollection GetList(int startRowIndex, int maximumRows)
		{
			RepairDetailCriteria repairdetail = new RepairDetailCriteria();
			return GetList(repairdetail, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RepairDetailCollection GetList(RepairDetailCriteria repairdetailCriteria)
		{
			return GetList(repairdetailCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RepairDetailCollection GetList(RepairDetailCriteria repairdetailCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RepairDetailCollection myCollection = RepairDetailDB.GetList(repairdetailCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RepairDetailComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RepairDetailCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RepairDetailCriteria repairdetailCriteria)
		{
			return RepairDetailDB.SelectCountForGetList(repairdetailCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RepairDetail GetItem(int id)
		{
			RepairDetail repairdetail = RepairDetailDB.GetItem(id);
			return repairdetail;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RepairDetail myRepairDetail)
		{
			if (!myRepairDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid repairdetail. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRepairDetail.mId != 0)
					AuditUpdate(myRepairDetail);

				int id = RepairDetailDB.Save(myRepairDetail);
				if(myRepairDetail.mId == 0)
					AuditInsert(myRepairDetail, id);

				myRepairDetail.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RepairDetail myRepairDetail)
		{
			if (RepairDetailDB.Delete(myRepairDetail.mId))
			{
				AuditDelete(myRepairDetail);
				return myRepairDetail.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RepairDetail myRepairDetail, int id)
		{
			AuditManager.AuditInsert(false, myRepairDetail.mUserFullName,(int)(Tables.ptApi_RepairDetail),id,"Insert");
		}
		private static void AuditDelete( RepairDetail myRepairDetail)
		{
			AuditManager.AuditDelete(false, myRepairDetail.mUserFullName,(int)(Tables.ptApi_RepairDetail),myRepairDetail.mId,"Delete");
		}
		private static void AuditUpdate( RepairDetail myRepairDetail)
		{
			RepairDetail old_repairdetail = GetItem(myRepairDetail.mId);
			AuditCollection audit_collection = RepairDetailAudit.Audit(myRepairDetail, old_repairdetail);
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
		private class RepairDetailComparer : IComparer < RepairDetail >
		{
			private string _sortColumn;
			private bool _reverse;
			public RepairDetailComparer(string sortExpression)
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

			public int Compare(RepairDetail x, RepairDetail y)
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