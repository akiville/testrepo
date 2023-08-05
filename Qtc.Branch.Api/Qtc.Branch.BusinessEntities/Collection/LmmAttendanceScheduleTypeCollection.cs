using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The LmmAttendanceScheduleTypeCollection class is designed to work with lists of instances of LmmAttendanceScheduleType.
	/// </summary>
	public class LmmAttendanceScheduleTypeCollection : BusinessCollectionBase<LmmAttendanceScheduleType>
	{
		/// <summary>
		/// Initializes a new instance of the LmmAttendanceScheduleTypeCollection class.
		/// </summary>
		public LmmAttendanceScheduleTypeCollection() { }
		/// <summary>
		/// Initializes a new instance of the LmmAttendanceScheduleTypeCollection class.
		/// </summary>
		public LmmAttendanceScheduleTypeCollection(IList<LmmAttendanceScheduleType> initialList) : base(initialList) { }
	}
}