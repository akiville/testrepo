using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The SalesSchedulingUpdatesCollection class is designed to work with lists of instances of SalesSchedulingUpdates.
	/// </summary>
	public class SalesSchedulingUpdatesCollection : BusinessCollectionBase<SalesSchedulingUpdates>
	{
		/// <summary>
		/// Initializes a new instance of the SalesSchedulingUpdatesCollection class.
		/// </summary>
		public SalesSchedulingUpdatesCollection() { }
		/// <summary>
		/// Initializes a new instance of the SalesSchedulingUpdatesCollection class.
		/// </summary>
		public SalesSchedulingUpdatesCollection(IList<SalesSchedulingUpdates> initialList) : base(initialList) { }
	}
}