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
	public static class RovingPlanOicManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static RovingPlanOicCollection GetList()
		{
			RovingPlanOicCriteria rovingplanoic = new RovingPlanOicCriteria();
			return GetList(rovingplanoic, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanOicCollection GetList(string sortExpression)
		{
			RovingPlanOicCriteria rovingplanoic = new RovingPlanOicCriteria();
			return GetList(rovingplanoic, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanOicCollection GetList(int startRowIndex, int maximumRows)
		{
			RovingPlanOicCriteria rovingplanoic = new RovingPlanOicCriteria();
			return GetList(rovingplanoic, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanOicCollection GetList(RovingPlanOicCriteria rovingplanoicCriteria)
		{
			return GetList(rovingplanoicCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanOicCollection GetList(RovingPlanOicCriteria rovingplanoicCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			RovingPlanOicCollection myCollection = RovingPlanOicDB.GetList(rovingplanoicCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new RovingPlanOicComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new RovingPlanOicCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(RovingPlanOicCriteria rovingplanoicCriteria)
		{
			return RovingPlanOicDB.SelectCountForGetList(rovingplanoicCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static RovingPlanOic GetItem(int id)
		{
			RovingPlanOic rovingplanoic = RovingPlanOicDB.GetItem(id);
			return rovingplanoic;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(RovingPlanOic myRovingPlanOic)
		{
			if (!myRovingPlanOic.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid rovingplanoic. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myRovingPlanOic.mId != 0)
					AuditUpdate(myRovingPlanOic);

				int id = RovingPlanOicDB.Save(myRovingPlanOic);
				if(myRovingPlanOic.mId == 0)
					AuditInsert(myRovingPlanOic, id);

				myRovingPlanOic.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(RovingPlanOic myRovingPlanOic)
		{
			if (RovingPlanOicDB.Delete(myRovingPlanOic.mId))
			{
				AuditDelete(myRovingPlanOic);
				return myRovingPlanOic.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(RovingPlanOic myRovingPlanOic, int id)
		{
			AuditManager.AuditInsert(false, myRovingPlanOic.mUserFullName,(int)(Tables.ptApi_RovingPlanOic),id,"Insert");
		}
		private static void AuditDelete( RovingPlanOic myRovingPlanOic)
		{
			AuditManager.AuditDelete(false, myRovingPlanOic.mUserFullName,(int)(Tables.ptApi_RovingPlanOic),myRovingPlanOic.mId,"Delete");
		}
		private static void AuditUpdate( RovingPlanOic myRovingPlanOic)
		{
			RovingPlanOic old_rovingplanoic = GetItem(myRovingPlanOic.mId);
			AuditCollection audit_collection = RovingPlanOicAudit.Audit(myRovingPlanOic, old_rovingplanoic);
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
		private class RovingPlanOicComparer : IComparer < RovingPlanOic >
		{
			private string _sortColumn;
			private bool _reverse;
			public RovingPlanOicComparer(string sortExpression)
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

			public int Compare(RovingPlanOic x, RovingPlanOic y)
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