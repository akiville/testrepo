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
	public static class GreenslipManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static GreenslipCollection GetList()
		{
			GreenslipCriteria greenslip = new GreenslipCriteria();
			return GetList(greenslip, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenslipCollection GetList(string sortExpression)
		{
			GreenslipCriteria greenslip = new GreenslipCriteria();
			return GetList(greenslip, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenslipCollection GetList(int startRowIndex, int maximumRows)
		{
			GreenslipCriteria greenslip = new GreenslipCriteria();
			return GetList(greenslip, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenslipCollection GetList(GreenslipCriteria greenslipCriteria)
		{
			return GetList(greenslipCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static GreenslipCollection GetList(GreenslipCriteria greenslipCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			GreenslipCollection myCollection = GreenslipDB.GetList(greenslipCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new GreenslipComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new GreenslipCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(GreenslipCriteria greenslipCriteria)
		{
			return GreenslipDB.SelectCountForGetList(greenslipCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Greenslip GetItem(int id)
		{
			Greenslip greenslip = GreenslipDB.GetItem(id);
			return greenslip;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Greenslip myGreenslip)
		{
			if (!myGreenslip.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid greenslip. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myGreenslip.mId != 0)
					AuditUpdate(myGreenslip);

				int id = GreenslipDB.Save(myGreenslip);
				if(myGreenslip.mId == 0)
					AuditInsert(myGreenslip, id);

				myGreenslip.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Greenslip myGreenslip)
		{
			if (GreenslipDB.Delete(myGreenslip.mId))
			{
				AuditDelete(myGreenslip);
				return myGreenslip.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Greenslip myGreenslip, int id)
		{
			AuditManager.AuditInsert(false, myGreenslip.mUserFullName,(int)(Tables.ptApi_Greenslip),id,"Insert");
		}
		private static void AuditDelete( Greenslip myGreenslip)
		{
			AuditManager.AuditDelete(false, myGreenslip.mUserFullName,(int)(Tables.ptApi_Greenslip),myGreenslip.mId,"Delete");
		}
		private static void AuditUpdate( Greenslip myGreenslip)
		{
			Greenslip old_greenslip = GetItem(myGreenslip.mId);
			AuditCollection audit_collection = GreenslipAudit.Audit(myGreenslip, old_greenslip);
			if (audit_collection != null)
			{
				foreach (BusinessEntities.Audit audit in audit_collection)
				{
					AuditManager.Save( audit);
				}
			}
		}
		#endregion

		#region IComparable
		private class GreenslipComparer : IComparer < Greenslip >
		{
			private string _sortColumn;
			private bool _reverse;
			public GreenslipComparer(string sortExpression)
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

			public int Compare(Greenslip x, Greenslip y)
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