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
	public static class NonComplianceManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static NonComplianceCollection GetList()
		{
			NonComplianceCriteria noncompliance = new NonComplianceCriteria();
			return GetList(noncompliance, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NonComplianceCollection GetList(string sortExpression)
		{
			NonComplianceCriteria noncompliance = new NonComplianceCriteria();
			return GetList(noncompliance, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NonComplianceCollection GetList(int startRowIndex, int maximumRows)
		{
			NonComplianceCriteria noncompliance = new NonComplianceCriteria();
			return GetList(noncompliance, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NonComplianceCollection GetList(NonComplianceCriteria noncomplianceCriteria)
		{
			return GetList(noncomplianceCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NonComplianceCollection GetList(NonComplianceCriteria noncomplianceCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			NonComplianceCollection myCollection = NonComplianceDB.GetList(noncomplianceCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new NonComplianceComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new NonComplianceCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(NonComplianceCriteria noncomplianceCriteria)
		{
			return NonComplianceDB.SelectCountForGetList(noncomplianceCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NonCompliance GetItem(int id)
		{
			NonCompliance noncompliance = NonComplianceDB.GetItem(id);
			return noncompliance;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(NonCompliance myNonCompliance)
		{
			if (!myNonCompliance.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid noncompliance. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myNonCompliance.mId != 0)
					AuditUpdate(myNonCompliance);

				int id = NonComplianceDB.Save(myNonCompliance);
				if(myNonCompliance.mId == 0)
					AuditInsert(myNonCompliance, id);

				myNonCompliance.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(NonCompliance myNonCompliance)
		{
			if (NonComplianceDB.Delete(myNonCompliance.mId))
			{
				AuditDelete(myNonCompliance);
				return myNonCompliance.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(NonCompliance myNonCompliance, int id)
		{
			AuditManager.AuditInsert(false, myNonCompliance.mUserFullName,(int)(Tables.ptApi_NonCompliance),id,"Insert");
		}
		private static void AuditDelete( NonCompliance myNonCompliance)
		{
			AuditManager.AuditDelete(false, myNonCompliance.mUserFullName,(int)(Tables.ptApi_NonCompliance),myNonCompliance.mId,"Delete");
		}
		private static void AuditUpdate( NonCompliance myNonCompliance)
		{
			NonCompliance old_noncompliance = GetItem(myNonCompliance.mId);
			AuditCollection audit_collection = NonComplianceAudit.Audit(myNonCompliance, old_noncompliance);
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
		private class NonComplianceComparer : IComparer < NonCompliance >
		{
			private string _sortColumn;
			private bool _reverse;
			public NonComplianceComparer(string sortExpression)
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

			public int Compare(NonCompliance x, NonCompliance y)
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