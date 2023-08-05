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
	public static class TypeOfReconcilingFormManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static TypeOfReconcilingFormCollection GetList()
		{
			TypeOfReconcilingFormCriteria typeofreconcilingform = new TypeOfReconcilingFormCriteria();
			return GetList(typeofreconcilingform, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfReconcilingFormCollection GetList(string sortExpression)
		{
			TypeOfReconcilingFormCriteria typeofreconcilingform = new TypeOfReconcilingFormCriteria();
			return GetList(typeofreconcilingform, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfReconcilingFormCollection GetList(int startRowIndex, int maximumRows)
		{
			TypeOfReconcilingFormCriteria typeofreconcilingform = new TypeOfReconcilingFormCriteria();
			return GetList(typeofreconcilingform, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfReconcilingFormCollection GetList(TypeOfReconcilingFormCriteria typeofreconcilingformCriteria)
		{
			return GetList(typeofreconcilingformCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfReconcilingFormCollection GetList(TypeOfReconcilingFormCriteria typeofreconcilingformCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			TypeOfReconcilingFormCollection myCollection = TypeOfReconcilingFormDB.GetList(typeofreconcilingformCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new TypeOfReconcilingFormComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new TypeOfReconcilingFormCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(TypeOfReconcilingFormCriteria typeofreconcilingformCriteria)
		{
			return TypeOfReconcilingFormDB.SelectCountForGetList(typeofreconcilingformCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static TypeOfReconcilingForm GetItem(int id)
		{
			TypeOfReconcilingForm typeofreconcilingform = TypeOfReconcilingFormDB.GetItem(id);
			return typeofreconcilingform;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(TypeOfReconcilingForm myTypeOfReconcilingForm)
		{
			if (!myTypeOfReconcilingForm.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid typeofreconcilingform. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myTypeOfReconcilingForm.mId != 0)
					AuditUpdate(myTypeOfReconcilingForm);

				int id = TypeOfReconcilingFormDB.Save(myTypeOfReconcilingForm);
				if(myTypeOfReconcilingForm.mId == 0)
					AuditInsert(myTypeOfReconcilingForm, id);

				myTypeOfReconcilingForm.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(TypeOfReconcilingForm myTypeOfReconcilingForm)
		{
			if (TypeOfReconcilingFormDB.Delete(myTypeOfReconcilingForm.mId))
			{
				AuditDelete(myTypeOfReconcilingForm);
				return myTypeOfReconcilingForm.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(TypeOfReconcilingForm myTypeOfReconcilingForm, int id)
		{
			AuditManager.AuditInsert(false, myTypeOfReconcilingForm.mUserFullName,(int)(Tables.ptApi_TypeOfReconcilingForm),id,"Insert");
		}
		private static void AuditDelete( TypeOfReconcilingForm myTypeOfReconcilingForm)
		{
			AuditManager.AuditDelete(false, myTypeOfReconcilingForm.mUserFullName,(int)(Tables.ptApi_TypeOfReconcilingForm),myTypeOfReconcilingForm.mId,"Delete");
		}
		private static void AuditUpdate( TypeOfReconcilingForm myTypeOfReconcilingForm)
		{
			TypeOfReconcilingForm old_typeofreconcilingform = GetItem(myTypeOfReconcilingForm.mId);
			AuditCollection audit_collection = TypeOfReconcilingFormAudit.Audit(myTypeOfReconcilingForm, old_typeofreconcilingform);
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
		private class TypeOfReconcilingFormComparer : IComparer < TypeOfReconcilingForm >
		{
			private string _sortColumn;
			private bool _reverse;
			public TypeOfReconcilingFormComparer(string sortExpression)
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

			public int Compare(TypeOfReconcilingForm x, TypeOfReconcilingForm y)
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