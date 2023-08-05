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
	public static class BranchRequisitionManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static BranchRequisitionCollection GetList()
		{
			BranchRequisitionCriteria branchrequisition = new BranchRequisitionCriteria();
			return GetList(branchrequisition, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchRequisitionCollection GetList(string sortExpression)
		{
			BranchRequisitionCriteria branchrequisition = new BranchRequisitionCriteria();
			return GetList(branchrequisition, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchRequisitionCollection GetList(int startRowIndex, int maximumRows)
		{
			BranchRequisitionCriteria branchrequisition = new BranchRequisitionCriteria();
			return GetList(branchrequisition, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchRequisitionCollection GetList(BranchRequisitionCriteria branchrequisitionCriteria)
		{
			return GetList(branchrequisitionCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchRequisitionCollection GetList(BranchRequisitionCriteria branchrequisitionCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			BranchRequisitionCollection myCollection = BranchRequisitionDB.GetList(branchrequisitionCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new BranchRequisitionComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new BranchRequisitionCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(BranchRequisitionCriteria branchrequisitionCriteria)
		{
			return BranchRequisitionDB.SelectCountForGetList(branchrequisitionCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchRequisition GetItem(int id)
		{
			BranchRequisition branchrequisition = BranchRequisitionDB.GetItem(id);
			return branchrequisition;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(BranchRequisition myBranchRequisition)
		{
			if (!myBranchRequisition.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid branchrequisition. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myBranchRequisition.mId != 0)
					AuditUpdate(myBranchRequisition);

				int id = BranchRequisitionDB.Save(myBranchRequisition);
				if(myBranchRequisition.mId == 0)
					AuditInsert(myBranchRequisition, id);

                if (id > 0)
                {
                    if (myBranchRequisition.mBranchRequisitionDetail != null)
                    {
                        foreach(BranchRequisitionDetails item in myBranchRequisition.mBranchRequisitionDetail)
                        {
                            item.mBranchRequisitionId = id;
                            item.mRemarks = "";
                            item.mUserFullName = "";

                            BranchRequisitionDetailsManager.Save(item);
                        }
                    }
                }

				myBranchRequisition.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(BranchRequisition myBranchRequisition)
		{
			if (BranchRequisitionDB.Delete(myBranchRequisition.mId))
			{
				AuditDelete(myBranchRequisition);
				return myBranchRequisition.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(BranchRequisition myBranchRequisition, int id)
		{
			AuditManager.AuditInsert(false, myBranchRequisition.mUserFullName,(int)(Tables.ptApi_BranchRequisition),id,"Insert");
		}
		private static void AuditDelete( BranchRequisition myBranchRequisition)
		{
			AuditManager.AuditDelete(false, myBranchRequisition.mUserFullName,(int)(Tables.ptApi_BranchRequisition),myBranchRequisition.mId,"Delete");
		}
		private static void AuditUpdate( BranchRequisition myBranchRequisition)
		{
			BranchRequisition old_branchrequisition = GetItem(myBranchRequisition.mId);
			AuditCollection audit_collection = BranchRequisitionAudit.Audit(myBranchRequisition, old_branchrequisition);
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
		private class BranchRequisitionComparer : IComparer < BranchRequisition >
		{
			private string _sortColumn;
			private bool _reverse;
			public BranchRequisitionComparer(string sortExpression)
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

			public int Compare(BranchRequisition x, BranchRequisition y)
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