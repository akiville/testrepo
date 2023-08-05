using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The SalesSchedulingConcernCollection class is designed to work with lists of instances of SalesSchedulingConcern.
	/// </summary>
	public class SalesSchedulingConcernCollection : BusinessCollectionBase<SalesSchedulingConcern>
	{
		/// <summary>
		/// Initializes a new instance of the SalesSchedulingConcernCollection class.
		/// </summary>
		public SalesSchedulingConcernCollection() { }
		/// <summary>
		/// Initializes a new instance of the SalesSchedulingConcernCollection class.
		/// </summary>
		public SalesSchedulingConcernCollection(IList<SalesSchedulingConcern> initialList) : base(initialList) { }
	}
}