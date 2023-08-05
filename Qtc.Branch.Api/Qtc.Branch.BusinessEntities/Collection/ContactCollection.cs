using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The ContactCollection class is designed to work with lists of instances of Contact.
	/// </summary>
	public class ContactCollection : BusinessCollectionBase<Contact>
	{
		/// <summary>
		/// Initializes a new instance of the ContactCollection class.
		/// </summary>
		public ContactCollection() { }
		/// <summary>
		/// Initializes a new instance of the ContactCollection class.
		/// </summary>
		public ContactCollection(IList<Contact> initialList) : base(initialList) { }
	}
}