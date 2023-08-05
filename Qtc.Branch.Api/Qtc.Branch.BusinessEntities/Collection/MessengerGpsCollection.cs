using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The MessengerGpsCollection class is designed to work with lists of instances of MessengerGps.
	/// </summary>
	public class MessengerGpsCollection : BusinessCollectionBase<MessengerGps>
	{
		/// <summary>
		/// Initializes a new instance of the MessengerGpsCollection class.
		/// </summary>
		public MessengerGpsCollection() { }
		/// <summary>
		/// Initializes a new instance of the MessengerGpsCollection class.
		/// </summary>
		public MessengerGpsCollection(IList<MessengerGps> initialList) : base(initialList) { }
	}
}