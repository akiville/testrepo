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
	public static class BranchOdsCompanyManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static BranchOdsCompanyCollection GetList()
		{
			BranchOdsCompanyCriteria branchodscompany = new BranchOdsCompanyCriteria();
			return GetList(branchodscompany, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchOdsCompanyCollection GetList(string sortExpression)
		{
			BranchOdsCompanyCriteria branchodscompany = new BranchOdsCompanyCriteria();
			return GetList(branchodscompany, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchOdsCompanyCollection GetList(int startRowIndex, int maximumRows)
		{
			BranchOdsCompanyCriteria branchodscompany = new BranchOdsCompanyCriteria();
			return GetList(branchodscompany, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchOdsCompanyCollection GetList(BranchOdsCompanyCriteria branchodscompanyCriteria)
		{
			return GetList(branchodscompanyCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchOdsCompanyCollection GetList(BranchOdsCompanyCriteria branchodscompanyCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			BranchOdsCompanyCollection myCollection = BranchOdsCompanyDB.GetList(branchodscompanyCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new BranchOdsCompanyComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new BranchOdsCompanyCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(BranchOdsCompanyCriteria branchodscompanyCriteria)
		{
			return BranchOdsCompanyDB.SelectCountForGetList(branchodscompanyCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static BranchOdsCompany GetItem(int id)
		{
			BranchOdsCompany branchodscompany = BranchOdsCompanyDB.GetItem(id);
			return branchodscompany;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(BranchOdsCompany myBranchOdsCompany)
		{
			if (!myBranchOdsCompany.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid branchodscompany. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				//if(myBranchOdsCompany.mId != 0)
				//	AuditUpdate(myBranchOdsCompany);

				int id = BranchOdsCompanyDB.Save(myBranchOdsCompany);
				if(myBranchOdsCompany.mId == 0)
					AuditInsert(myBranchOdsCompany, id);

				myBranchOdsCompany.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(BranchOdsCompany myBranchOdsCompany)
		{
			if (BranchOdsCompanyDB.Delete(myBranchOdsCompany.mId))
			{
				AuditDelete(myBranchOdsCompany);
				return myBranchOdsCompany.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(BranchOdsCompany myBranchOdsCompany, int id)
		{
			AuditManager.AuditInsert(false, myBranchOdsCompany.mUserFullName,(int)(Tables.ptApi_BranchOdsCompany),id,"Insert");
		}
		private static void AuditDelete( BranchOdsCompany myBranchOdsCompany)
		{
			AuditManager.AuditDelete(false, myBranchOdsCompany.mUserFullName,(int)(Tables.ptApi_BranchOdsCompany),myBranchOdsCompany.mId,"Delete");
		}
		private static void AuditUpdate( BranchOdsCompany myBranchOdsCompany)
		{
			BranchOdsCompany old_branchodscompany = GetItem(myBranchOdsCompany.mId);
			AuditCollection audit_collection = BranchOdsCompanyAudit.Audit(myBranchOdsCompany, old_branchodscompany);
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
		private class BranchOdsCompanyComparer : IComparer < BranchOdsCompany >
		{
			private string _sortColumn;
			private bool _reverse;
			public BranchOdsCompanyComparer(string sortExpression)
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

			public int Compare(BranchOdsCompany x, BranchOdsCompany y)
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