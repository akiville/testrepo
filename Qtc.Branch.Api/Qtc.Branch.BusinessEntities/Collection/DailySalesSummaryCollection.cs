using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DailySalesSummaryCollection class is designed to work with lists of instances of DailySalesSummary.
	/// </summary>
	public class DailySalesSummaryCollection : BusinessCollectionBase<DailySalesSummary>
	{
		/// <summary>
		/// Initializes a new instance of the DailySalesSummaryCollection class.
		/// </summary>
		public DailySalesSummaryCollection() { }
		/// <summary>
		/// Initializes a new instance of the DailySalesSummaryCollection class.
		/// </summary>
		public DailySalesSummaryCollection(IList<DailySalesSummary> initialList) : base(initialList) { }
	}
}