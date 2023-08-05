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
	public static class PurposeManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static PurposeCollection GetList()
		{
			PurposeCriteria purpose = new PurposeCriteria();
			return GetList(purpose, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static PurposeCollection GetList(string sortExpression)
		{
			PurposeCriteria purpose = new PurposeCriteria();
			return GetList(purpose, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static PurposeCollection GetList(int startRowIndex, int maximumRows)
		{
			PurposeCriteria purpose = new PurposeCriteria();
			return GetList(purpose, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static PurposeCollection GetList(PurposeCriteria purposeCriteria)
		{
			return GetList(purposeCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static PurposeCollection GetList(PurposeCriteria purposeCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			PurposeCollection myCollection = PurposeDB.GetList(purposeCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new PurposeComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new PurposeCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(PurposeCriteria purposeCriteria)
		{
			return PurposeDB.SelectCountForGetList(purposeCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Purpose GetItem(int id)
		{
			Purpose purpose = PurposeDB.GetItem(id);
			return purpose;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Purpose myPurpose)
		{
			if (!myPurpose.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid purpose. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myPurpose.mId != 0)
					AuditUpdate(myPurpose);

				int id = PurposeDB.Save(myPurpose);
				if(myPurpose.mId == 0)
					AuditInsert(myPurpose, id);

				myPurpose.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Purpose myPurpose)
		{
			if (PurposeDB.Delete(myPurpose.mId))
			{
				AuditDelete(myPurpose);
				return myPurpose.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Purpose myPurpose, int id)
		{
			AuditManager.AuditInsert(false, myPurpose.mUserFullName,(int)(Tables.ptApi_Purpose),id,"Insert");
		}
		private static void AuditDelete( Purpose myPurpose)
		{
			AuditManager.AuditDelete(false, myPurpose.mUserFullName,(int)(Tables.ptApi_Purpose),myPurpose.mId,"Delete");
		}
		private static void AuditUpdate( Purpose myPurpose)
		{
			Purpose old_purpose = GetItem(myPurpose.mId);
			AuditCollection audit_collection = PurposeAudit.Audit(myPurpose, old_purpose);
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
		private class PurposeComparer : IComparer < Purpose >
		{
			private string _sortColumn;
			private bool _reverse;
			public PurposeComparer(string sortExpression)
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

			public int Compare(Purpose x, Purpose y)
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