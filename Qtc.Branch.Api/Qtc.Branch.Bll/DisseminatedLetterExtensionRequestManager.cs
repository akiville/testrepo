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
	public static class DisseminatedLetterExtensionRequestManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DisseminatedLetterExtensionRequestCollection GetList()
		{
			DisseminatedLetterExtensionRequestCriteria disseminatedletterextensionrequest = new DisseminatedLetterExtensionRequestCriteria();
			return GetList(disseminatedletterextensionrequest, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterExtensionRequestCollection GetList(string sortExpression)
		{
			DisseminatedLetterExtensionRequestCriteria disseminatedletterextensionrequest = new DisseminatedLetterExtensionRequestCriteria();
			return GetList(disseminatedletterextensionrequest, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterExtensionRequestCollection GetList(int startRowIndex, int maximumRows)
		{
			DisseminatedLetterExtensionRequestCriteria disseminatedletterextensionrequest = new DisseminatedLetterExtensionRequestCriteria();
			return GetList(disseminatedletterextensionrequest, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterExtensionRequestCollection GetList(DisseminatedLetterExtensionRequestCriteria disseminatedletterextensionrequestCriteria)
		{
			return GetList(disseminatedletterextensionrequestCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterExtensionRequestCollection GetList(DisseminatedLetterExtensionRequestCriteria disseminatedletterextensionrequestCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DisseminatedLetterExtensionRequestCollection myCollection = DisseminatedLetterExtensionRequestDB.GetList(disseminatedletterextensionrequestCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DisseminatedLetterExtensionRequestComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DisseminatedLetterExtensionRequestCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DisseminatedLetterExtensionRequestCriteria disseminatedletterextensionrequestCriteria)
		{
			return DisseminatedLetterExtensionRequestDB.SelectCountForGetList(disseminatedletterextensionrequestCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterExtensionRequest GetItem(int id)
		{
			DisseminatedLetterExtensionRequest disseminatedletterextensionrequest = DisseminatedLetterExtensionRequestDB.GetItem(id);
			return disseminatedletterextensionrequest;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(DisseminatedLetterExtensionRequest myDisseminatedLetterExtensionRequest)
		{
			if (!myDisseminatedLetterExtensionRequest.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid disseminatedletterextensionrequest. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDisseminatedLetterExtensionRequest.mId != 0)
					AuditUpdate(myDisseminatedLetterExtensionRequest);

				int id = DisseminatedLetterExtensionRequestDB.Save(myDisseminatedLetterExtensionRequest);
				if(myDisseminatedLetterExtensionRequest.mId == 0)
					AuditInsert(myDisseminatedLetterExtensionRequest, id);

				myDisseminatedLetterExtensionRequest.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(DisseminatedLetterExtensionRequest myDisseminatedLetterExtensionRequest)
		{
			if (DisseminatedLetterExtensionRequestDB.Delete(myDisseminatedLetterExtensionRequest.mId))
			{
				AuditDelete(myDisseminatedLetterExtensionRequest);
				return myDisseminatedLetterExtensionRequest.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(DisseminatedLetterExtensionRequest myDisseminatedLetterExtensionRequest, int id)
		{
			AuditManager.AuditInsert(false, myDisseminatedLetterExtensionRequest.mUserFullName,(int)(Tables.ptApi_DisseminatedLetterExtensionRequest),id,"Insert");
		}
		private static void AuditDelete( DisseminatedLetterExtensionRequest myDisseminatedLetterExtensionRequest)
		{
			AuditManager.AuditDelete(false, myDisseminatedLetterExtensionRequest.mUserFullName,(int)(Tables.ptApi_DisseminatedLetterExtensionRequest),myDisseminatedLetterExtensionRequest.mId,"Delete");
		}
		private static void AuditUpdate( DisseminatedLetterExtensionRequest myDisseminatedLetterExtensionRequest)
		{
			DisseminatedLetterExtensionRequest old_disseminatedletterextensionrequest = GetItem(myDisseminatedLetterExtensionRequest.mId);
			AuditCollection audit_collection = DisseminatedLetterExtensionRequestAudit.Audit(myDisseminatedLetterExtensionRequest, old_disseminatedletterextensionrequest);
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
		private class DisseminatedLetterExtensionRequestComparer : IComparer < DisseminatedLetterExtensionRequest >
		{
			private string _sortColumn;
			private bool _reverse;
			public DisseminatedLetterExtensionRequestComparer(string sortExpression)
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

			public int Compare(DisseminatedLetterExtensionRequest x, DisseminatedLetterExtensionRequest y)
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