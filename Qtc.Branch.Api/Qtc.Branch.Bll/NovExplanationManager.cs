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
	public static class NovExplanationManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static NovExplanationCollection GetList()
		{
			NovExplanationCriteria novexplanation = new NovExplanationCriteria();
			return GetList(novexplanation, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NovExplanationCollection GetList(string sortExpression)
		{
			NovExplanationCriteria novexplanation = new NovExplanationCriteria();
			return GetList(novexplanation, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NovExplanationCollection GetList(int startRowIndex, int maximumRows)
		{
			NovExplanationCriteria novexplanation = new NovExplanationCriteria();
			return GetList(novexplanation, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NovExplanationCollection GetList(NovExplanationCriteria novexplanationCriteria)
		{
			return GetList(novexplanationCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NovExplanationCollection GetList(NovExplanationCriteria novexplanationCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			NovExplanationCollection myCollection = NovExplanationDB.GetList(novexplanationCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new NovExplanationComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new NovExplanationCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(NovExplanationCriteria novexplanationCriteria)
		{
			return NovExplanationDB.SelectCountForGetList(novexplanationCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NovExplanation GetItem(int id)
		{
			NovExplanation novexplanation = NovExplanationDB.GetItem(id);
			return novexplanation;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(NovExplanation myNovExplanation)
		{
			if (!myNovExplanation.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid novexplanation. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myNovExplanation.mId != 0)
					AuditUpdate(myNovExplanation);

				int id = NovExplanationDB.Save(myNovExplanation);
				if(myNovExplanation.mId == 0)
					AuditInsert(myNovExplanation, id);

				myNovExplanation.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(NovExplanation myNovExplanation)
		{
			if (NovExplanationDB.Delete(myNovExplanation.mId))
			{
				AuditDelete(myNovExplanation);
				return myNovExplanation.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(NovExplanation myNovExplanation, int id)
		{
			AuditManager.AuditInsert(false, myNovExplanation.mUserFullName,(int)(Tables.ptApi_NovExplanation),id,"Insert");
		}
		private static void AuditDelete( NovExplanation myNovExplanation)
		{
			AuditManager.AuditDelete(false, myNovExplanation.mUserFullName,(int)(Tables.ptApi_NovExplanation),myNovExplanation.mId,"Delete");
		}
		private static void AuditUpdate( NovExplanation myNovExplanation)
		{
			NovExplanation old_novexplanation = GetItem(myNovExplanation.mId);
			AuditCollection audit_collection = NovExplanationAudit.Audit(myNovExplanation, old_novexplanation);
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
		private class NovExplanationComparer : IComparer < NovExplanation >
		{
			private string _sortColumn;
			private bool _reverse;
			public NovExplanationComparer(string sortExpression)
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

			public int Compare(NovExplanation x, NovExplanation y)
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