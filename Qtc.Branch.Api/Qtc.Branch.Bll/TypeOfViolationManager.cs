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
	public static class TypeOfViolationManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TypeOfViolationCollection GetList()
		{
			TypeOfViolationCriteria typeofviolation = new TypeOfViolationCriteria();
			return GetList(typeofviolation, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfViolationCollection GetList(string sortExpression)
		{
			TypeOfViolationCriteria typeofviolation = new TypeOfViolationCriteria();
			return GetList(typeofviolation, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfViolationCollection GetList(int startRowIndex, int maximumRows)
		{
			TypeOfViolationCriteria typeofviolation = new TypeOfViolationCriteria();
			return GetList(typeofviolation, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfViolationCollection GetList(TypeOfViolationCriteria typeofviolationCriteria)
		{
			return GetList(typeofviolationCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfViolationCollection GetList(TypeOfViolationCriteria typeofviolationCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			TypeOfViolationCollection myCollection = TypeOfViolationDB.GetList(typeofviolationCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new TypeOfViolationComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new TypeOfViolationCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(TypeOfViolationCriteria typeofviolationCriteria)
		{
			return TypeOfViolationDB.SelectCountForGetList(typeofviolationCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfViolation GetItem(int id)
		{
			TypeOfViolation typeofviolation = TypeOfViolationDB.GetItem(id);
			return typeofviolation;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(TypeOfViolation myTypeOfViolation)
		{
			if (!myTypeOfViolation.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid typeofviolation. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myTypeOfViolation.mId != 0)
					AuditUpdate(myTypeOfViolation);

				int id = TypeOfViolationDB.Save(myTypeOfViolation);
				if(myTypeOfViolation.mId == 0)
					AuditInsert(myTypeOfViolation, id);

				myTypeOfViolation.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(TypeOfViolation myTypeOfViolation)
		{
			if (TypeOfViolationDB.Delete(myTypeOfViolation.mId))
			{
				AuditDelete(myTypeOfViolation);
				return myTypeOfViolation.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(TypeOfViolation myTypeOfViolation, int id)
		{
			AuditManager.AuditInsert(false, myTypeOfViolation.mUserFullName,(int)(Tables.ptApi_TypeOfViolation),id,"Insert");
		}
		private static void AuditDelete( TypeOfViolation myTypeOfViolation)
		{
			AuditManager.AuditDelete(false, myTypeOfViolation.mUserFullName,(int)(Tables.ptApi_TypeOfViolation),myTypeOfViolation.mId,"Delete");
		}
		private static void AuditUpdate( TypeOfViolation myTypeOfViolation)
		{
			TypeOfViolation old_typeofviolation = GetItem(myTypeOfViolation.mId);
			AuditCollection audit_collection = TypeOfViolationAudit.Audit(myTypeOfViolation, old_typeofviolation);
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
		private class TypeOfViolationComparer : IComparer < TypeOfViolation >
		{
			private string _sortColumn;
			private bool _reverse;
			public TypeOfViolationComparer(string sortExpression)
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

			public int Compare(TypeOfViolation x, TypeOfViolation y)
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