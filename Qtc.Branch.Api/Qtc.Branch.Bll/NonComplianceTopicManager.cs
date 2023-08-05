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
	public static class NonComplianceTopicManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static NonComplianceTopicCollection GetList()
		{
			NonComplianceTopicCriteria noncompliancetopic = new NonComplianceTopicCriteria();
			return GetList(noncompliancetopic, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NonComplianceTopicCollection GetList(string sortExpression)
		{
			NonComplianceTopicCriteria noncompliancetopic = new NonComplianceTopicCriteria();
			return GetList(noncompliancetopic, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NonComplianceTopicCollection GetList(int startRowIndex, int maximumRows)
		{
			NonComplianceTopicCriteria noncompliancetopic = new NonComplianceTopicCriteria();
			return GetList(noncompliancetopic, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NonComplianceTopicCollection GetList(NonComplianceTopicCriteria noncompliancetopicCriteria)
		{
			return GetList(noncompliancetopicCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NonComplianceTopicCollection GetList(NonComplianceTopicCriteria noncompliancetopicCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			NonComplianceTopicCollection myCollection = NonComplianceTopicDB.GetList(noncompliancetopicCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new NonComplianceTopicComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new NonComplianceTopicCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(NonComplianceTopicCriteria noncompliancetopicCriteria)
		{
			return NonComplianceTopicDB.SelectCountForGetList(noncompliancetopicCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static NonComplianceTopic GetItem(int id)
		{
			NonComplianceTopic noncompliancetopic = NonComplianceTopicDB.GetItem(id);
			return noncompliancetopic;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(NonComplianceTopic myNonComplianceTopic)
		{
			if (!myNonComplianceTopic.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid noncompliancetopic. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myNonComplianceTopic.mId != 0)
					AuditUpdate(myNonComplianceTopic);

				int id = NonComplianceTopicDB.Save(myNonComplianceTopic);
				if(myNonComplianceTopic.mId == 0)
					AuditInsert(myNonComplianceTopic, id);

				myNonComplianceTopic.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(NonComplianceTopic myNonComplianceTopic)
		{
			if (NonComplianceTopicDB.Delete(myNonComplianceTopic.mId))
			{
				AuditDelete(myNonComplianceTopic);
				return myNonComplianceTopic.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(NonComplianceTopic myNonComplianceTopic, int id)
		{
			AuditManager.AuditInsert(false, myNonComplianceTopic.mUserFullName,(int)(Tables.ptApi_NonComplianceTopic),id,"Insert");
		}
		private static void AuditDelete( NonComplianceTopic myNonComplianceTopic)
		{
			AuditManager.AuditDelete(false, myNonComplianceTopic.mUserFullName,(int)(Tables.ptApi_NonComplianceTopic),myNonComplianceTopic.mId,"Delete");
		}
		private static void AuditUpdate( NonComplianceTopic myNonComplianceTopic)
		{
			NonComplianceTopic old_noncompliancetopic = GetItem(myNonComplianceTopic.mId);
			AuditCollection audit_collection = NonComplianceTopicAudit.Audit(myNonComplianceTopic, old_noncompliancetopic);
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
		private class NonComplianceTopicComparer : IComparer < NonComplianceTopic >
		{
			private string _sortColumn;
			private bool _reverse;
			public NonComplianceTopicComparer(string sortExpression)
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

			public int Compare(NonComplianceTopic x, NonComplianceTopic y)
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