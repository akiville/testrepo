using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingPlanScheduledActualChecklistImageCollection class is designed to work with lists of instances of RovingPlanScheduledActualChecklistImage.
	/// </summary>
	public class RovingPlanScheduledActualChecklistImageCollection : BusinessCollectionBase<RovingPlanScheduledActualChecklistImage>
	{
		/// <summary>
		/// Initializes a new instance of the RovingPlanScheduledActualChecklistImageCollection class.
		/// </summary>
		public RovingPlanScheduledActualChecklistImageCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingPlanScheduledActualChecklistImageCollection class.
		/// </summary>
		public RovingPlanScheduledActualChecklistImageCollection(IList<RovingPlanScheduledActualChecklistImage> initialList) : base(initialList) { }
	}
}