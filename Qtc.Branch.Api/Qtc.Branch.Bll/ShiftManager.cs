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
	public static class ShiftManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ShiftCollection GetList()
		{
			ShiftCriteria shift = new ShiftCriteria();
			return GetList(shift, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ShiftCollection GetList(string sortExpression)
		{
			ShiftCriteria shift = new ShiftCriteria();
			return GetList(shift, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ShiftCollection GetList(int startRowIndex, int maximumRows)
		{
			ShiftCriteria shift = new ShiftCriteria();
			return GetList(shift, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ShiftCollection GetList(ShiftCriteria shiftCriteria)
		{
			return GetList(shiftCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ShiftCollection GetList(ShiftCriteria shiftCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			ShiftCollection myCollection = ShiftDB.GetList(shiftCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new ShiftComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new ShiftCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(ShiftCriteria shiftCriteria)
		{
			return ShiftDB.SelectCountForGetList(shiftCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Shift GetItem(int id)
		{
			Shift shift = ShiftDB.GetItem(id);
			return shift;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Shift myShift)
		{
			if (!myShift.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid shift. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myShift.mId != 0)
					AuditUpdate(myShift);

				int id = ShiftDB.Save(myShift);
				if(myShift.mId == 0)
					AuditInsert(myShift, id);

				myShift.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Shift myShift)
		{
			if (ShiftDB.Delete(myShift.mId))
			{
				AuditDelete(myShift);
				return myShift.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Shift myShift, int id)
		{
			AuditManager.AuditInsert(false, myShift.mUserFullName,(int)(Tables.ptApi_Shift),id,"Insert");
		}
		private static void AuditDelete( Shift myShift)
		{
			AuditManager.AuditDelete(false, myShift.mUserFullName,(int)(Tables.ptApi_Shift),myShift.mId,"Delete");
		}
		private static void AuditUpdate( Shift myShift)
		{
			Shift old_shift = GetItem(myShift.mId);
			AuditCollection audit_collection = ShiftAudit.Audit(myShift, old_shift);
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
		private class ShiftComparer : IComparer < Shift >
		{
			private string _sortColumn;
			private bool _reverse;
			public ShiftComparer(string sortExpression)
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

			public int Compare(Shift x, Shift y)
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