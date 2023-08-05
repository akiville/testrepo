using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingPlanScheduledActualCollection class is designed to work with lists of instances of RovingPlanScheduledActual.
	/// </summary>
	public class RovingPlanScheduledActualCollection : BusinessCollectionBase<RovingPlanScheduledActual>
	{
		/// <summary>
		/// Initializes a new instance of the RovingPlanScheduledActualCollection class.
		/// </summary>
		public RovingPlanScheduledActualCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingPlanScheduledActualCollection class.
		/// </summary>
		public RovingPlanScheduledActualCollection(IList<RovingPlanScheduledActual> initialList) : base(initialList) { }
	}
}