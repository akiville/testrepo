using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingPlanCollection class is designed to work with lists of instances of RovingPlan.
	/// </summary>
	public class RovingPlanCollection : BusinessCollectionBase<RovingPlan>
	{
		/// <summary>
		/// Initializes a new instance of the RovingPlanCollection class.
		/// </summary>
		public RovingPlanCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingPlanCollection class.
		/// </summary>
		public RovingPlanCollection(IList<RovingPlan> initialList) : base(initialList) { }
	}
}