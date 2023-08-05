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
	public static class BranchsManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static BranchsCollection GetList()
		{
			BranchsCriteria branchs = new BranchsCriteria();
			return GetList(branchs, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchsCollection GetList(string sortExpression)
		{
			BranchsCriteria branchs = new BranchsCriteria();
			return GetList(branchs, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchsCollection GetList(int startRowIndex, int maximumRows)
		{
			BranchsCriteria branchs = new BranchsCriteria();
			return GetList(branchs, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchsCollection GetList(BranchsCriteria branchsCriteria)
		{
			return GetList(branchsCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchsCollection GetList(BranchsCriteria branchsCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			BranchsCollection myCollection = BranchsDB.GetList(branchsCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new BranchsComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new BranchsCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(BranchsCriteria branchsCriteria)
		{
			return BranchsDB.SelectCountForGetList(branchsCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Branchs GetItem(int id)
		{
			Branchs branchs = BranchsDB.GetItem(id);
			return branchs;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Branchs myBranchs)
		{
			if (!myBranchs.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid branchs. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myBranchs.mId != 0)
					AuditUpdate(myBranchs);

				int id = BranchsDB.Save(myBranchs);
				if(myBranchs.mId == 0)
					AuditInsert(myBranchs, id);

				myBranchs.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Branchs myBranchs)
		{
			if (BranchsDB.Delete(myBranchs.mId))
			{
				AuditDelete(myBranchs);
				return myBranchs.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Branchs myBranchs, int id)
		{
			AuditManager.AuditInsert(false, myBranchs.mUserFullName,(int)(Tables.ptApi_Branchs),id,"Insert");
		}
		private static void AuditDelete( Branchs myBranchs)
		{
			AuditManager.AuditDelete(false, myBranchs.mUserFullName,(int)(Tables.ptApi_Branchs),myBranchs.mId,"Delete");
		}
		private static void AuditUpdate( Branchs myBranchs)
		{
			Branchs old_branchs = GetItem(myBranchs.mId);
			AuditCollection audit_collection = BranchsAudit.Audit(myBranchs, old_branchs);
			if (audit_collection != null)
			{
				foreach (BusinessEntities.Audit audit in audit_collection)
				{
					AuditManager.Save( audit);
				}
			}
		}
		#endregion

		#region IComparable
		private class BranchsComparer : IComparer < Branchs >
		{
			private string _sortColumn;
			private bool _reverse;
			public BranchsComparer(string sortExpression)
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

			public int Compare(Branchs x, Branchs y)
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