using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingPlanScheduledActualChecklistCollection class is designed to work with lists of instances of RovingPlanScheduledActualChecklist.
	/// </summary>
	public class RovingPlanScheduledActualChecklistCollection : BusinessCollectionBase<RovingPlanScheduledActualChecklist>
	{
		/// <summary>
		/// Initializes a new instance of the RovingPlanScheduledActualChecklistCollection class.
		/// </summary>
		public RovingPlanScheduledActualChecklistCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingPlanScheduledActualChecklistCollection class.
		/// </summary>
		public RovingPlanScheduledActualChecklistCollection(IList<RovingPlanScheduledActualChecklist> initialList) : base(initialList) { }
	}
}