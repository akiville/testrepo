using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The SalesSchedulingColorPlannedCollection class is designed to work with lists of instances of SalesSchedulingColorPlanned.
	/// </summary>
	public class SalesSchedulingColorPlannedCollection : BusinessCollectionBase<SalesSchedulingColorPlanned>
	{
		/// <summary>
		/// Initializes a new instance of the SalesSchedulingColorPlannedCollection class.
		/// </summary>
		public SalesSchedulingColorPlannedCollection() { }
		/// <summary>
		/// Initializes a new instance of the SalesSchedulingColorPlannedCollection class.
		/// </summary>
		public SalesSchedulingColorPlannedCollection(IList<SalesSchedulingColorPlanned> initialList) : base(initialList) { }
	}
}