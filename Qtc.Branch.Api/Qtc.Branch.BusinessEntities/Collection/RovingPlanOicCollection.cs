using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingPlanOicCollection class is designed to work with lists of instances of RovingPlanOic.
	/// </summary>
	public class RovingPlanOicCollection : BusinessCollectionBase<RovingPlanOic>
	{
		/// <summary>
		/// Initializes a new instance of the RovingPlanOicCollection class.
		/// </summary>
		public RovingPlanOicCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingPlanOicCollection class.
		/// </summary>
		public RovingPlanOicCollection(IList<RovingPlanOic> initialList) : base(initialList) { }
	}
}