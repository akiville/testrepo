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
	public static class GreenSlipRequestDetailManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static GreenSlipRequestDetailCollection GetList()
		{
			GreenSlipRequestDetailCriteria greensliprequestdetail = new GreenSlipRequestDetailCriteria();
			return GetList(greensliprequestdetail, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipRequestDetailCollection GetList(string sortExpression)
		{
			GreenSlipRequestDetailCriteria greensliprequestdetail = new GreenSlipRequestDetailCriteria();
			return GetList(greensliprequestdetail, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipRequestDetailCollection GetList(int startRowIndex, int maximumRows)
		{
			GreenSlipRequestDetailCriteria greensliprequestdetail = new GreenSlipRequestDetailCriteria();
			return GetList(greensliprequestdetail, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipRequestDetailCollection GetList(GreenSlipRequestDetailCriteria greensliprequestdetailCriteria)
		{
			return GetList(greensliprequestdetailCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipRequestDetailCollection GetList(GreenSlipRequestDetailCriteria greensliprequestdetailCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			GreenSlipRequestDetailCollection myCollection = GreenSlipRequestDetailDB.GetList(greensliprequestdetailCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new GreenSlipRequestDetailComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new GreenSlipRequestDetailCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(GreenSlipRequestDetailCriteria greensliprequestdetailCriteria)
		{
			return GreenSlipRequestDetailDB.SelectCountForGetList(greensliprequestdetailCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipRequestDetail GetItem(int id)
		{
			GreenSlipRequestDetail greensliprequestdetail = GreenSlipRequestDetailDB.GetItem(id);
			return greensliprequestdetail;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(GreenSlipRequestDetail myGreenSlipRequestDetail)
		{
			if (!myGreenSlipRequestDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid greensliprequestdetail. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myGreenSlipRequestDetail.mId != 0)
					AuditUpdate(myGreenSlipRequestDetail);

				int id = GreenSlipRequestDetailDB.Save(myGreenSlipRequestDetail);
				if(myGreenSlipRequestDetail.mId == 0)
					AuditInsert(myGreenSlipRequestDetail, id);

				myGreenSlipRequestDetail.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(GreenSlipRequestDetail myGreenSlipRequestDetail)
		{
			if (GreenSlipRequestDetailDB.Delete(myGreenSlipRequestDetail.mId))
			{
				AuditDelete(myGreenSlipRequestDetail);
				return myGreenSlipRequestDetail.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(GreenSlipRequestDetail myGreenSlipRequestDetail, int id)
		{
			AuditManager.AuditInsert(false, myGreenSlipRequestDetail.mUserFullName,(int)(Tables.ptApi_GreenSlipRequestDetail),id,"Insert");
		}
		private static void AuditDelete( GreenSlipRequestDetail myGreenSlipRequestDetail)
		{
			AuditManager.AuditDelete(false, myGreenSlipRequestDetail.mUserFullName,(int)(Tables.ptApi_GreenSlipRequestDetail),myGreenSlipRequestDetail.mId,"Delete");
		}
		private static void AuditUpdate( GreenSlipRequestDetail myGreenSlipRequestDetail)
		{
			GreenSlipRequestDetail old_greensliprequestdetail = GetItem(myGreenSlipRequestDetail.mId);
			AuditCollection audit_collection = GreenSlipRequestDetailAudit.Audit(myGreenSlipRequestDetail, old_greensliprequestdetail);
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
		private class GreenSlipRequestDetailComparer : IComparer < GreenSlipRequestDetail >
		{
			private string _sortColumn;
			private bool _reverse;
			public GreenSlipRequestDetailComparer(string sortExpression)
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

			public int Compare(GreenSlipRequestDetail x, GreenSlipRequestDetail y)
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