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
	public static class DisseminatedLetterManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DisseminatedLetterCollection GetList()
		{
			DisseminatedLetterCriteria disseminatedletter = new DisseminatedLetterCriteria();
			return GetList(disseminatedletter, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterCollection GetList(string sortExpression)
		{
			DisseminatedLetterCriteria disseminatedletter = new DisseminatedLetterCriteria();
			return GetList(disseminatedletter, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterCollection GetList(int startRowIndex, int maximumRows)
		{
			DisseminatedLetterCriteria disseminatedletter = new DisseminatedLetterCriteria();
			return GetList(disseminatedletter, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterCollection GetList(DisseminatedLetterCriteria disseminatedletterCriteria)
		{
			return GetList(disseminatedletterCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterCollection GetList(DisseminatedLetterCriteria disseminatedletterCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DisseminatedLetterCollection myCollection = DisseminatedLetterDB.GetList(disseminatedletterCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DisseminatedLetterComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DisseminatedLetterCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DisseminatedLetterCriteria disseminatedletterCriteria)
		{
			return DisseminatedLetterDB.SelectCountForGetList(disseminatedletterCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetter GetItem(int id)
		{
			DisseminatedLetter disseminatedletter = DisseminatedLetterDB.GetItem(id);
			return disseminatedletter;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(DisseminatedLetter myDisseminatedLetter)
		{
			if (!myDisseminatedLetter.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid disseminatedletter. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDisseminatedLetter.mId != 0)
					AuditUpdate(myDisseminatedLetter);

				int id = DisseminatedLetterDB.Save(myDisseminatedLetter);
				if(myDisseminatedLetter.mId == 0)
					AuditInsert(myDisseminatedLetter, id);

				myDisseminatedLetter.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(DisseminatedLetter myDisseminatedLetter)
		{
			if (DisseminatedLetterDB.Delete(myDisseminatedLetter.mId))
			{
				AuditDelete(myDisseminatedLetter);
				return myDisseminatedLetter.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(DisseminatedLetter myDisseminatedLetter, int id)
		{
			AuditManager.AuditInsert(false, myDisseminatedLetter.mUserFullName,(int)(Tables.ptApi_DisseminatedLetter),id,"Insert");
		}
		private static void AuditDelete( DisseminatedLetter myDisseminatedLetter)
		{
			AuditManager.AuditDelete(false, myDisseminatedLetter.mUserFullName,(int)(Tables.ptApi_DisseminatedLetter),myDisseminatedLetter.mId,"Delete");
		}
		private static void AuditUpdate( DisseminatedLetter myDisseminatedLetter)
		{
			DisseminatedLetter old_disseminatedletter = GetItem(myDisseminatedLetter.mId);
			AuditCollection audit_collection = DisseminatedLetterAudit.Audit(myDisseminatedLetter, old_disseminatedletter);
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
		private class DisseminatedLetterComparer : IComparer < DisseminatedLetter >
		{
			private string _sortColumn;
			private bool _reverse;
			public DisseminatedLetterComparer(string sortExpression)
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

			public int Compare(DisseminatedLetter x, DisseminatedLetter y)
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