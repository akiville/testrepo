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
	public static class ContactManager
	{
		#region Public Methods
		[DataObjectMethod(DataObjectMethodType.Select, true)]
		public static ContactCollection GetList()
		{
			ContactCriteria contact = new ContactCriteria();
			return GetList(contact, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ContactCollection GetList(string sortExpression)
		{
			ContactCriteria contact = new ContactCriteria();
			return GetList(contact, sortExpression, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ContactCollection GetList(int startRowIndex, int maximumRows)
		{
			ContactCriteria contact = new ContactCriteria();
			return GetList(contact, string.Empty, startRowIndex, maximumRows);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ContactCollection GetList(ContactCriteria contactCriteria)
		{
			return GetList(contactCriteria, string.Empty, -1, -1);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static ContactCollection GetList(ContactCriteria contactCriteria, string sortExpression, int startRowIndex, int maximumRows)
		{
			ContactCollection myCollection = ContactDB.GetList(contactCriteria);
			if (!string.IsNullOrEmpty(sortExpression))
			{
				myCollection.Sort(new ContactComparer(sortExpression));
			}
			if (startRowIndex >= 0 && maximumRows > 0)
			{
				return new ContactCollection(myCollection.Skip(startRowIndex).Take(maximumRows).ToList());
			}
			return myCollection;
		}

		public static int SelectCountForGetList(ContactCriteria contactCriteria)
		{
			return ContactDB.SelectCountForGetList(contactCriteria);
		}

		[DataObjectMethod(DataObjectMethodType.Select, false)]
		public static Contact GetItem(int id)
		{
			Contact contact = ContactDB.GetItem(id);
			return contact;
		}

		[DataObjectMethod(DataObjectMethodType.Update, true)]
		public static int Save(Contact myContact)
		{
			if (!myContact.Validate())
			{
				throw new InvalidSaveOperationException("Can't save an invalid contact. Please make sure Validate() returns true before you call Save.");
			}
			using (TransactionScope myTransactionScope = new TransactionScope())
			{
				if(myContact.mId != 0)
					AuditUpdate(myContact);

				int id = ContactDB.Save(myContact);
				if(myContact.mId == 0)
					AuditInsert(myContact, id);

				myContact.mId = id;
				myTransactionScope.Complete();
				return id;
			}
		}

		[DataObjectMethod(DataObjectMethodType.Delete, true)]
		public static int Delete(Contact myContact)
		{
			if (ContactDB.Delete(myContact.mId))
			{
				AuditDelete(myContact);
				return myContact.mId;
			}
			else
				return 0;
		}
		#endregion

		#region Audit
		private static void AuditInsert(Contact myContact, int id)
		{
			AuditManager.AuditInsert(false, myContact.mUserFullName,(int)(Tables.ptApi_Contact),id,"Insert");
		}
		private static void AuditDelete( Contact myContact)
		{
			AuditManager.AuditDelete(false, myContact.mUserFullName,(int)(Tables.ptApi_Contact),myContact.mId,"Delete");
		}
		private static void AuditUpdate( Contact myContact)
		{
			Contact old_contact = GetItem(myContact.mId);
			AuditCollection audit_collection = ContactAudit.Audit(myContact, old_contact);
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
		private class ContactComparer : IComparer < Contact >
		{
			private string _sortColumn;
			private bool _reverse;
			public ContactComparer(string sortExpression)
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

			public int Compare(Contact x, Contact y)
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