using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The MessengerStatusCollection class is designed to work with lists of instances of MessengerStatus.
	/// </summary>
	public class MessengerStatusCollection : BusinessCollectionBase<MessengerStatus>
	{
		/// <summary>
		/// Initializes a new instance of the MessengerStatusCollection class.
		/// </summary>
		public MessengerStatusCollection() { }
		/// <summary>
		/// Initializes a new instance of the MessengerStatusCollection class.
		/// </summary>
		public MessengerStatusCollection(IList<MessengerStatus> initialList) : base(initialList) { }
	}
}