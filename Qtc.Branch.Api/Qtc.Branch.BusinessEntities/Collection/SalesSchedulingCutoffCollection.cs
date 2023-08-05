using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The SalesSchedulingCutoffCollection class is designed to work with lists of instances of SalesSchedulingCutoff.
	/// </summary>
	public class SalesSchedulingCutoffCollection : BusinessCollectionBase<SalesSchedulingCutoff>
	{
		/// <summary>
		/// Initializes a new instance of the SalesSchedulingCutoffCollection class.
		/// </summary>
		public SalesSchedulingCutoffCollection() { }
		/// <summary>
		/// Initializes a new instance of the SalesSchedulingCutoffCollection class.
		/// </summary>
		public SalesSchedulingCutoffCollection(IList<SalesSchedulingCutoff> initialList) : base(initialList) { }
	}
}