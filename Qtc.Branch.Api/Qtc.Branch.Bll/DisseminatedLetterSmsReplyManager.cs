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
	public static class DisseminatedLetterSmsReplyManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DisseminatedLetterSmsReplyCollection GetList()
		{
			DisseminatedLetterSmsReplyCriteria disseminatedlettersmsreply = new DisseminatedLetterSmsReplyCriteria();
			return GetList(disseminatedlettersmsreply, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterSmsReplyCollection GetList(string sortExpression)
		{
			DisseminatedLetterSmsReplyCriteria disseminatedlettersmsreply = new DisseminatedLetterSmsReplyCriteria();
			return GetList(disseminatedlettersmsreply, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterSmsReplyCollection GetList(int startRowIndex, int maximumRows)
		{
			DisseminatedLetterSmsReplyCriteria disseminatedlettersmsreply = new DisseminatedLetterSmsReplyCriteria();
			return GetList(disseminatedlettersmsreply, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterSmsReplyCollection GetList(DisseminatedLetterSmsReplyCriteria disseminatedlettersmsreplyCriteria)
		{
			return GetList(disseminatedlettersmsreplyCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterSmsReplyCollection GetList(DisseminatedLetterSmsReplyCriteria disseminatedlettersmsreplyCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DisseminatedLetterSmsReplyCollection myCollection = DisseminatedLetterSmsReplyDB.GetList(disseminatedlettersmsreplyCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DisseminatedLetterSmsReplyComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DisseminatedLetterSmsReplyCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DisseminatedLetterSmsReplyCriteria disseminatedlettersmsreplyCriteria)
		{
			return DisseminatedLetterSmsReplyDB.SelectCountForGetList(disseminatedlettersmsreplyCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DisseminatedLetterSmsReply GetItem(int id)
		{
			DisseminatedLetterSmsReply disseminatedlettersmsreply = DisseminatedLetterSmsReplyDB.GetItem(id);
			return disseminatedlettersmsreply;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(DisseminatedLetterSmsReply myDisseminatedLetterSmsReply)
		{
			if (!myDisseminatedLetterSmsReply.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid disseminatedlettersmsreply. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				//if(myDisseminatedLetterSmsReply.mId != 0)
				//	AuditUpdate(myDisseminatedLetterSmsReply);

				int id = DisseminatedLetterSmsReplyDB.Save(myDisseminatedLetterSmsReply);
				if(myDisseminatedLetterSmsReply.mId == 0)
					AuditInsert(myDisseminatedLetterSmsReply, id);

				myDisseminatedLetterSmsReply.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(DisseminatedLetterSmsReply myDisseminatedLetterSmsReply)
		{
			if (DisseminatedLetterSmsReplyDB.Delete(myDisseminatedLetterSmsReply.mId))
			{
				AuditDelete(myDisseminatedLetterSmsReply);
				return myDisseminatedLetterSmsReply.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(DisseminatedLetterSmsReply myDisseminatedLetterSmsReply, int id)
		{
			AuditManager.AuditInsert(false, myDisseminatedLetterSmsReply.mUserFullName,(int)(Tables.ptApi_DisseminatedLetterSmsReply),id,"Insert");
		}
		private static void AuditDelete( DisseminatedLetterSmsReply myDisseminatedLetterSmsReply)
		{
			AuditManager.AuditDelete(false, myDisseminatedLetterSmsReply.mUserFullName,(int)(Tables.ptApi_DisseminatedLetterSmsReply),myDisseminatedLetterSmsReply.mId,"Delete");
		}
		private static void AuditUpdate( DisseminatedLetterSmsReply myDisseminatedLetterSmsReply)
		{
			DisseminatedLetterSmsReply old_disseminatedlettersmsreply = GetItem(myDisseminatedLetterSmsReply.mId);
			AuditCollection audit_collection = DisseminatedLetterSmsReplyAudit.Audit(myDisseminatedLetterSmsReply, old_disseminatedlettersmsreply);
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
		private class DisseminatedLetterSmsReplyComparer : IComparer < DisseminatedLetterSmsReply >
		{
			private string _sortColumn;
			private bool _reverse;
			public DisseminatedLetterSmsReplyComparer(string sortExpression)
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

			public int Compare(DisseminatedLetterSmsReply x, DisseminatedLetterSmsReply y)
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