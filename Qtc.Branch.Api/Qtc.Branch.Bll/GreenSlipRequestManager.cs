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
	public static class GreenSlipRequestManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static GreenSlipRequestCollection GetList()
		{
			GreenSlipRequestCriteria greensliprequest = new GreenSlipRequestCriteria();
			return GetList(greensliprequest, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipRequestCollection GetList(string sortExpression)
		{
			GreenSlipRequestCriteria greensliprequest = new GreenSlipRequestCriteria();
			return GetList(greensliprequest, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipRequestCollection GetList(int startRowIndex, int maximumRows)
		{
			GreenSlipRequestCriteria greensliprequest = new GreenSlipRequestCriteria();
			return GetList(greensliprequest, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipRequestCollection GetList(GreenSlipRequestCriteria greensliprequestCriteria)
		{
			return GetList(greensliprequestCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipRequestCollection GetList(GreenSlipRequestCriteria greensliprequestCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			GreenSlipRequestCollection myCollection = GreenSlipRequestDB.GetList(greensliprequestCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new GreenSlipRequestComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new GreenSlipRequestCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(GreenSlipRequestCriteria greensliprequestCriteria)
		{
			return GreenSlipRequestDB.SelectCountForGetList(greensliprequestCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenSlipRequest GetItem(int id)
		{
			GreenSlipRequest greensliprequest = GreenSlipRequestDB.GetItem(id);
			return greensliprequest;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(GreenSlipRequest myGreenSlipRequest)
		{
			if (!myGreenSlipRequest.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid greensliprequest. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myGreenSlipRequest.mId != 0)
					AuditUpdate(myGreenSlipRequest);

				int id = GreenSlipRequestDB.Save(myGreenSlipRequest);
				if(myGreenSlipRequest.mId == 0)
					AuditInsert(myGreenSlipRequest, id);

				myGreenSlipRequest.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(GreenSlipRequest myGreenSlipRequest)
		{
			if (GreenSlipRequestDB.Delete(myGreenSlipRequest.mId))
			{
				AuditDelete(myGreenSlipRequest);
				return myGreenSlipRequest.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(GreenSlipRequest myGreenSlipRequest, int id)
		{
			AuditManager.AuditInsert(false, myGreenSlipRequest.mUserFullName,(int)(Tables.ptApi_GreenSlipRequest),id,"Insert");
		}
		private static void AuditDelete( GreenSlipRequest myGreenSlipRequest)
		{
			AuditManager.AuditDelete(false, myGreenSlipRequest.mUserFullName,(int)(Tables.ptApi_GreenSlipRequest),myGreenSlipRequest.mId,"Delete");
		}
		private static void AuditUpdate( GreenSlipRequest myGreenSlipRequest)
		{
			GreenSlipRequest old_greensliprequest = GetItem(myGreenSlipRequest.mId);
			AuditCollection audit_collection = GreenSlipRequestAudit.Audit(myGreenSlipRequest, old_greensliprequest);
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
		private class GreenSlipRequestComparer : IComparer < GreenSlipRequest >
		{
			private string _sortColumn;
			private bool _reverse;
			public GreenSlipRequestComparer(string sortExpression)
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

			public int Compare(GreenSlipRequest x, GreenSlipRequest y)
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