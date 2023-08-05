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
	public static class RepairImageManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RepairImageCollection GetList()
		{
			RepairImageCriteria repairimage = new RepairImageCriteria();
			return GetList(repairimage, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RepairImageCollection GetList(string sortExpression)
		{
			RepairImageCriteria repairimage = new RepairImageCriteria();
			return GetList(repairimage, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RepairImageCollection GetList(int startRowIndex, int maximumRows)
		{
			RepairImageCriteria repairimage = new RepairImageCriteria();
			return GetList(repairimage, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RepairImageCollection GetList(RepairImageCriteria repairimageCriteria)
		{
			return GetList(repairimageCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RepairImageCollection GetList(RepairImageCriteria repairimageCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RepairImageCollection myCollection = RepairImageDB.GetList(repairimageCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RepairImageComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RepairImageCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RepairImageCriteria repairimageCriteria)
		{
			return RepairImageDB.SelectCountForGetList(repairimageCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RepairImage GetItem(int id)
		{
			RepairImage repairimage = RepairImageDB.GetItem(id);
			return repairimage;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RepairImage myRepairImage)
		{
			if (!myRepairImage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid repairimage. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRepairImage.mId != 0)
					AuditUpdate(myRepairImage);

				int id = RepairImageDB.Save(myRepairImage);
				if(myRepairImage.mId == 0)
					AuditInsert(myRepairImage, id);

				myRepairImage.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RepairImage myRepairImage)
		{
			if (RepairImageDB.Delete(myRepairImage.mId))
			{
				AuditDelete(myRepairImage);
				return myRepairImage.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RepairImage myRepairImage, int id)
		{
			AuditManager.AuditInsert(false, myRepairImage.mUserFullName,(int)(Tables.ptApi_RepairImage),id,"Insert");
		}
		private static void AuditDelete( RepairImage myRepairImage)
		{
			AuditManager.AuditDelete(false, myRepairImage.mUserFullName,(int)(Tables.ptApi_RepairImage),myRepairImage.mId,"Delete");
		}
		private static void AuditUpdate( RepairImage myRepairImage)
		{
			RepairImage old_repairimage = GetItem(myRepairImage.mId);
			AuditCollection audit_collection = RepairImageAudit.Audit(myRepairImage, old_repairimage);
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
		private class RepairImageComparer : IComparer < RepairImage >
		{
			private string _sortColumn;
			private bool _reverse;
			public RepairImageComparer(string sortExpression)
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

			public int Compare(RepairImage x, RepairImage y)
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