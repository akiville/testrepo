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
	public static class OdsCompanyManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static OdsCompanyCollection GetList()
		{
			OdsCompanyCriteria odscompany = new OdsCompanyCriteria();
			return GetList(odscompany, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static OdsCompanyCollection GetList(string sortExpression)
		{
			OdsCompanyCriteria odscompany = new OdsCompanyCriteria();
			return GetList(odscompany, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static OdsCompanyCollection GetList(int startRowIndex, int maximumRows)
		{
			OdsCompanyCriteria odscompany = new OdsCompanyCriteria();
			return GetList(odscompany, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static OdsCompanyCollection GetList(OdsCompanyCriteria odscompanyCriteria)
		{
			return GetList(odscompanyCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static OdsCompanyCollection GetList(OdsCompanyCriteria odscompanyCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			OdsCompanyCollection myCollection = OdsCompanyDB.GetList(odscompanyCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new OdsCompanyComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new OdsCompanyCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(OdsCompanyCriteria odscompanyCriteria)
		{
			return OdsCompanyDB.SelectCountForGetList(odscompanyCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static OdsCompany GetItem(int id)
		{
			OdsCompany odscompany = OdsCompanyDB.GetItem(id);
			return odscompany;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(OdsCompany myOdsCompany)
		{
			if (!myOdsCompany.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid odscompany. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myOdsCompany.mId != 0)
					AuditUpdate(myOdsCompany);

				int id = OdsCompanyDB.Save(myOdsCompany);
				if(myOdsCompany.mId == 0)
					AuditInsert(myOdsCompany, id);

				myOdsCompany.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(OdsCompany myOdsCompany)
		{
			if (OdsCompanyDB.Delete(myOdsCompany.mId))
			{
				AuditDelete(myOdsCompany);
				return myOdsCompany.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(OdsCompany myOdsCompany, int id)
		{
			AuditManager.AuditInsert(false, myOdsCompany.mUserFullName,(int)(Tables.ptApi_OdsCompany),id,"Insert");
		}
		private static void AuditDelete( OdsCompany myOdsCompany)
		{
			AuditManager.AuditDelete(false, myOdsCompany.mUserFullName,(int)(Tables.ptApi_OdsCompany),myOdsCompany.mId,"Delete");
		}
		private static void AuditUpdate( OdsCompany myOdsCompany)
		{
			OdsCompany old_odscompany = GetItem(myOdsCompany.mId);
			AuditCollection audit_collection = OdsCompanyAudit.Audit(myOdsCompany, old_odscompany);
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
		private class OdsCompanyComparer : IComparer < OdsCompany >
		{
			private string _sortColumn;
			private bool _reverse;
			public OdsCompanyComparer(string sortExpression)
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

			public int Compare(OdsCompany x, OdsCompany y)
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