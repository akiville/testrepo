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
	public static class ReasonManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ReasonCollection GetList()
		{
			ReasonCriteria reason = new ReasonCriteria();
			return GetList(reason, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ReasonCollection GetList(string sortExpression)
		{
			ReasonCriteria reason = new ReasonCriteria();
			return GetList(reason, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ReasonCollection GetList(int startRowIndex, int maximumRows)
		{
			ReasonCriteria reason = new ReasonCriteria();
			return GetList(reason, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ReasonCollection GetList(ReasonCriteria reasonCriteria)
		{
			return GetList(reasonCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ReasonCollection GetList(ReasonCriteria reasonCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			ReasonCollection myCollection = ReasonDB.GetList(reasonCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new ReasonComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new ReasonCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(ReasonCriteria reasonCriteria)
		{
			return ReasonDB.SelectCountForGetList(reasonCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Reason GetItem(int id)
		{
			Reason reason = ReasonDB.GetItem(id);
			return reason;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Reason myReason)
		{
			if (!myReason.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid reason. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				//if(myReason.mId != 0)
				//	AuditUpdate(myReason);

				int id = ReasonDB.Save(myReason);
				//if(myReason.mId == 0)
				//	AuditInsert(myReason, id);

				myReason.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Reason myReason)
		{
			if (ReasonDB.Delete(myReason.mId))
			{
				AuditDelete(myReason);
				return myReason.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Reason myReason, int id)
		{
			AuditManager.AuditInsert(false, myReason.mUserFullName,(int)(Tables.ptApi_Reason),id,"Insert");
		}
		private static void AuditDelete( Reason myReason)
		{
			AuditManager.AuditDelete(false, myReason.mUserFullName,(int)(Tables.ptApi_Reason),myReason.mId,"Delete");
		}
		private static void AuditUpdate( Reason myReason)
		{
			Reason old_reason = GetItem(myReason.mId);
			AuditCollection audit_collection = ReasonAudit.Audit(myReason, old_reason);
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
		private class ReasonComparer : IComparer < Reason >
		{
			private string _sortColumn;
			private bool _reverse;
			public ReasonComparer(string sortExpression)
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

			public int Compare(Reason x, Reason y)
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