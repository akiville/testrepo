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
	public static class DailySalesSummaryManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static DailySalesSummaryCollection GetList()
		{
			DailySalesSummaryCriteria dailysalessummary = new DailySalesSummaryCriteria();
			return GetList(dailysalessummary, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DailySalesSummaryCollection GetList(string sortExpression)
		{
			DailySalesSummaryCriteria dailysalessummary = new DailySalesSummaryCriteria();
			return GetList(dailysalessummary, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DailySalesSummaryCollection GetList(int startRowIndex, int maximumRows)
		{
			DailySalesSummaryCriteria dailysalessummary = new DailySalesSummaryCriteria();
			return GetList(dailysalessummary, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DailySalesSummaryCollection GetList(DailySalesSummaryCriteria dailysalessummaryCriteria)
		{
			return GetList(dailysalessummaryCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DailySalesSummaryCollection GetList(DailySalesSummaryCriteria dailysalessummaryCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			DailySalesSummaryCollection myCollection = DailySalesSummaryDB.GetList(dailysalessummaryCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new DailySalesSummaryComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new DailySalesSummaryCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(DailySalesSummaryCriteria dailysalessummaryCriteria)
		{
			return DailySalesSummaryDB.SelectCountForGetList(dailysalessummaryCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static DailySalesSummary GetItem(int id)
		{
			DailySalesSummary dailysalessummary = DailySalesSummaryDB.GetItem(id);
			return dailysalessummary;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(DailySalesSummary myDailySalesSummary)
		{
			if (!myDailySalesSummary.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid dailysalessummary. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myDailySalesSummary.mId != 0)
					AuditUpdate(myDailySalesSummary);

				int id = DailySalesSummaryDB.Save(myDailySalesSummary);
				if(myDailySalesSummary.mId == 0)
					AuditInsert(myDailySalesSummary, id);

				myDailySalesSummary.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(DailySalesSummary myDailySalesSummary)
		{
			if (DailySalesSummaryDB.Delete(myDailySalesSummary.mId))
			{
				AuditDelete(myDailySalesSummary);
				return myDailySalesSummary.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(DailySalesSummary myDailySalesSummary, int id)
		{
			AuditManager.AuditInsert(false, myDailySalesSummary.mUserFullName,(int)(Tables.ptApi_DailySalesSummary),id,"Insert");
		}
		private static void AuditDelete( DailySalesSummary myDailySalesSummary)
		{
			AuditManager.AuditDelete(false, myDailySalesSummary.mUserFullName,(int)(Tables.ptApi_DailySalesSummary),myDailySalesSummary.mId,"Delete");
		}
		private static void AuditUpdate( DailySalesSummary myDailySalesSummary)
		{
			DailySalesSummary old_dailysalessummary = GetItem(myDailySalesSummary.mId);
			AuditCollection audit_collection = DailySalesSummaryAudit.Audit(myDailySalesSummary, old_dailysalessummary);
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
		private class DailySalesSummaryComparer : IComparer < DailySalesSummary >
		{
			private string _sortColumn;
			private bool _reverse;
			public DailySalesSummaryComparer(string sortExpression)
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

			public int Compare(DailySalesSummary x, DailySalesSummary y)
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