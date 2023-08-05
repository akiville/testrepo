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
	public static class HrLetterRequestManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static HrLetterRequestCollection GetList()
		{
			HrLetterRequestCriteria hrletterrequest = new HrLetterRequestCriteria();
			return GetList(hrletterrequest, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterRequestCollection GetList(string sortExpression)
		{
			HrLetterRequestCriteria hrletterrequest = new HrLetterRequestCriteria();
			return GetList(hrletterrequest, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterRequestCollection GetList(int startRowIndex, int maximumRows)
		{
			HrLetterRequestCriteria hrletterrequest = new HrLetterRequestCriteria();
			return GetList(hrletterrequest, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterRequestCollection GetList(HrLetterRequestCriteria hrletterrequestCriteria)
		{
			return GetList(hrletterrequestCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterRequestCollection GetList(HrLetterRequestCriteria hrletterrequestCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			HrLetterRequestCollection myCollection = HrLetterRequestDB.GetList(hrletterrequestCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new HrLetterRequestComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new HrLetterRequestCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(HrLetterRequestCriteria hrletterrequestCriteria)
		{
			return HrLetterRequestDB.SelectCountForGetList(hrletterrequestCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static HrLetterRequest GetItem(int id)
		{
			HrLetterRequest hrletterrequest = HrLetterRequestDB.GetItem(id);
			return hrletterrequest;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(HrLetterRequest myHrLetterRequest)
		{
			if (!myHrLetterRequest.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid hrletterrequest. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myHrLetterRequest.mId != 0)
					AuditUpdate(myHrLetterRequest);

				int id = HrLetterRequestDB.Save(myHrLetterRequest);
				if(myHrLetterRequest.mId == 0)
					AuditInsert(myHrLetterRequest, id);

				myHrLetterRequest.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(HrLetterRequest myHrLetterRequest)
		{
			if (HrLetterRequestDB.Delete(myHrLetterRequest.mId))
			{
				AuditDelete(myHrLetterRequest);
				return myHrLetterRequest.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(HrLetterRequest myHrLetterRequest, int id)
		{
			AuditManager.AuditInsert(false, myHrLetterRequest.mUserFullName,(int)(Tables.ptApi_HrLetterRequest),id,"Insert");
		}
		private static void AuditDelete( HrLetterRequest myHrLetterRequest)
		{
			AuditManager.AuditDelete(false, myHrLetterRequest.mUserFullName,(int)(Tables.ptApi_HrLetterRequest),myHrLetterRequest.mId,"Delete");
		}
		private static void AuditUpdate( HrLetterRequest myHrLetterRequest)
		{
			HrLetterRequest old_hrletterrequest = GetItem(myHrLetterRequest.mId);
			AuditCollection audit_collection = HrLetterRequestAudit.Audit(myHrLetterRequest, old_hrletterrequest);
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
		private class HrLetterRequestComparer : IComparer < HrLetterRequest >
		{
			private string _sortColumn;
			private bool _reverse;
			public HrLetterRequestComparer(string sortExpression)
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

			public int Compare(HrLetterRequest x, HrLetterRequest y)
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