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
	public static class TypeOfLetterManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TypeOfLetterCollection GetList()
		{
			TypeOfLetterCriteria typeofletter = new TypeOfLetterCriteria();
			return GetList(typeofletter, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfLetterCollection GetList(string sortExpression)
		{
			TypeOfLetterCriteria typeofletter = new TypeOfLetterCriteria();
			return GetList(typeofletter, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfLetterCollection GetList(int startRowIndex, int maximumRows)
		{
			TypeOfLetterCriteria typeofletter = new TypeOfLetterCriteria();
			return GetList(typeofletter, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfLetterCollection GetList(TypeOfLetterCriteria typeofletterCriteria)
		{
			return GetList(typeofletterCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfLetterCollection GetList(TypeOfLetterCriteria typeofletterCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			TypeOfLetterCollection myCollection = TypeOfLetterDB.GetList(typeofletterCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new TypeOfLetterComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new TypeOfLetterCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(TypeOfLetterCriteria typeofletterCriteria)
		{
			return TypeOfLetterDB.SelectCountForGetList(typeofletterCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfLetter GetItem(int id)
		{
			TypeOfLetter typeofletter = TypeOfLetterDB.GetItem(id);
			return typeofletter;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(TypeOfLetter myTypeOfLetter)
		{
			if (!myTypeOfLetter.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid typeofletter. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				//if(myTypeOfLetter.mId != 0)
				//	AuditUpdate(myTypeOfLetter);

				int id = TypeOfLetterDB.Save(myTypeOfLetter);
				if(myTypeOfLetter.mId == 0)
					AuditInsert(myTypeOfLetter, id);

				myTypeOfLetter.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(TypeOfLetter myTypeOfLetter)
		{
			if (TypeOfLetterDB.Delete(myTypeOfLetter.mId))
			{
				AuditDelete(myTypeOfLetter);
				return myTypeOfLetter.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(TypeOfLetter myTypeOfLetter, int id)
		{
			AuditManager.AuditInsert(false, myTypeOfLetter.mUserFullName,(int)(Tables.ptApi_TypeOfLetter),id,"Insert");
		}
		private static void AuditDelete( TypeOfLetter myTypeOfLetter)
		{
			AuditManager.AuditDelete(false, myTypeOfLetter.mUserFullName,(int)(Tables.ptApi_TypeOfLetter),myTypeOfLetter.mId,"Delete");
		}
		private static void AuditUpdate( TypeOfLetter myTypeOfLetter)
		{
			TypeOfLetter old_typeofletter = GetItem(myTypeOfLetter.mId);
			AuditCollection audit_collection = TypeOfLetterAudit.Audit(myTypeOfLetter, old_typeofletter);
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
		private class TypeOfLetterComparer : IComparer < TypeOfLetter >
		{
			private string _sortColumn;
			private bool _reverse;
			public TypeOfLetterComparer(string sortExpression)
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

			public int Compare(TypeOfLetter x, TypeOfLetter y)
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