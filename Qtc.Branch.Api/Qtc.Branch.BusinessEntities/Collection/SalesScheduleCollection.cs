using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The SalesScheduleCollection class is designed to work with lists of instances of SalesSchedule.
	/// </summary>
	public class SalesScheduleCollection : BusinessCollectionBase<SalesSchedule>
	{
		/// <summary>
		/// Initializes a new instance of the SalesScheduleCollection class.
		/// </summary>
		public SalesScheduleCollection() { }
		/// <summary>
		/// Initializes a new instance of the SalesScheduleCollection class.
		/// </summary>
		public SalesScheduleCollection(IList<SalesSchedule> initialList) : base(initialList) { }
	}
}