using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The MessengerCollection class is designed to work with lists of instances of Messenger.
	/// </summary>
	public class MessengerCollection : BusinessCollectionBase<Messenger>
	{
		/// <summary>
		/// Initializes a new instance of the MessengerCollection class.
		/// </summary>
		public MessengerCollection() { }
		/// <summary>
		/// Initializes a new instance of the MessengerCollection class.
		/// </summary>
		public MessengerCollection(IList<Messenger> initialList) : base(initialList) { }
	}
}