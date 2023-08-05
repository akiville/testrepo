using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingPlanScheduledCollection class is designed to work with lists of instances of RovingPlanScheduled.
	/// </summary>
	public class RovingPlanScheduledCollection : BusinessCollectionBase<RovingPlanScheduled>
	{
		/// <summary>
		/// Initializes a new instance of the RovingPlanScheduledCollection class.
		/// </summary>
		public RovingPlanScheduledCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingPlanScheduledCollection class.
		/// </summary>
		public RovingPlanScheduledCollection(IList<RovingPlanScheduled> initialList) : base(initialList) { }
	}
}