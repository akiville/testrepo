using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingScheduleCollection class is designed to work with lists of instances of RovingSchedule.
	/// </summary>
	public class RovingScheduleCollection : BusinessCollectionBase<RovingSchedule>
	{
		/// <summary>
		/// Initializes a new instance of the RovingScheduleCollection class.
		/// </summary>
		public RovingScheduleCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingScheduleCollection class.
		/// </summary>
		public RovingScheduleCollection(IList<RovingSchedule> initialList) : base(initialList) { }
	}
}