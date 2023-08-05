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
	public static class LoeManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static LoeCollection GetList()
		{
			LoeCriteria loe = new LoeCriteria();
			return GetList(loe, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LoeCollection GetList(string sortExpression)
		{
			LoeCriteria loe = new LoeCriteria();
			return GetList(loe, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LoeCollection GetList(int startRowIndex, int maximumRows)
		{
			LoeCriteria loe = new LoeCriteria();
			return GetList(loe, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LoeCollection GetList(LoeCriteria loeCriteria)
		{
			return GetList(loeCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static LoeCollection GetList(LoeCriteria loeCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			LoeCollection myCollection = LoeDB.GetList(loeCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new LoeComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new LoeCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(LoeCriteria loeCriteria)
		{
			return LoeDB.SelectCountForGetList(loeCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Loe GetItem(int id)
		{
			Loe loe = LoeDB.GetItem(id);
			return loe;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Loe myLoe)
		{
			if (!myLoe.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid loe. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myLoe.mId != 0)
					AuditUpdate(myLoe);

				int id = LoeDB.Save(myLoe);
				if(myLoe.mId == 0)
					AuditInsert(myLoe, id);

				myLoe.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Loe myLoe)
		{
			if (LoeDB.Delete(myLoe.mId))
			{
				AuditDelete(myLoe);
				return myLoe.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Loe myLoe, int id)
		{
			AuditManager.AuditInsert(false, myLoe.mUserFullName,(int)(Tables.ptApi_Loe),id,"Insert");
		}
		private static void AuditDelete( Loe myLoe)
		{
			AuditManager.AuditDelete(false, myLoe.mUserFullName,(int)(Tables.ptApi_Loe),myLoe.mId,"Delete");
		}
		private static void AuditUpdate( Loe myLoe)
		{
			Loe old_loe = GetItem(myLoe.mId);
			AuditCollection audit_collection = LoeAudit.Audit(myLoe, old_loe);
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
		private class LoeComparer : IComparer < Loe >
		{
			private string _sortColumn;
			private bool _reverse;
			public LoeComparer(string sortExpression)
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

			public int Compare(Loe x, Loe y)
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