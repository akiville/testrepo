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
	public static class GreenSlipDetailManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static GreenSlipDetailCollection GetList()
		{
			GreenSlipDetailCriteria greenslipdetail = new GreenSlipDetailCriteria();
			return GetList(greenslipdetail, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipDetailCollection GetList(string sortExpression)
		{
			GreenSlipDetailCriteria greenslipdetail = new GreenSlipDetailCriteria();
			return GetList(greenslipdetail, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipDetailCollection GetList(int startRowIndex, int maximumRows)
		{
			GreenSlipDetailCriteria greenslipdetail = new GreenSlipDetailCriteria();
			return GetList(greenslipdetail, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipDetailCollection GetList(GreenSlipDetailCriteria greenslipdetailCriteria)
		{
			return GetList(greenslipdetailCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipDetailCollection GetList(GreenSlipDetailCriteria greenslipdetailCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			GreenSlipDetailCollection myCollection = GreenSlipDetailDB.GetList(greenslipdetailCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new GreenSlipDetailComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new GreenSlipDetailCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(GreenSlipDetailCriteria greenslipdetailCriteria)
		{
			return GreenSlipDetailDB.SelectCountForGetList(greenslipdetailCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipDetail GetItem(int id)
		{
			GreenSlipDetail greenslipdetail = GreenSlipDetailDB.GetItem(id);
			return greenslipdetail;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(GreenSlipDetail myGreenSlipDetail)
		{
			if (!myGreenSlipDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid greenslipdetail. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myGreenSlipDetail.mId != 0)
					AuditUpdate(myGreenSlipDetail);

				int id = GreenSlipDetailDB.Save(myGreenSlipDetail);
				if(myGreenSlipDetail.mId == 0)
					AuditInsert(myGreenSlipDetail, id);

				myGreenSlipDetail.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(GreenSlipDetail myGreenSlipDetail)
		{
			if (GreenSlipDetailDB.Delete(myGreenSlipDetail.mId))
			{
				AuditDelete(myGreenSlipDetail);
				return myGreenSlipDetail.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(GreenSlipDetail myGreenSlipDetail, int id)
		{
			AuditManager.AuditInsert(false, myGreenSlipDetail.mUserFullName,(int)(Tables.ptApi_GreenSlipDetail),id,"Insert");
		}
		private static void AuditDelete( GreenSlipDetail myGreenSlipDetail)
		{
			AuditManager.AuditDelete(false, myGreenSlipDetail.mUserFullName,(int)(Tables.ptApi_GreenSlipDetail),myGreenSlipDetail.mId,"Delete");
		}
		private static void AuditUpdate( GreenSlipDetail myGreenSlipDetail)
		{
			GreenSlipDetail old_greenslipdetail = GetItem(myGreenSlipDetail.mId);
			AuditCollection audit_collection = GreenSlipDetailAudit.Audit(myGreenSlipDetail, old_greenslipdetail);
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
		private class GreenSlipDetailComparer : IComparer < GreenSlipDetail >
		{
			private string _sortColumn;
			private bool _reverse;
			public GreenSlipDetailComparer(string sortExpression)
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

			public int Compare(GreenSlipDetail x, GreenSlipDetail y)
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