using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The LmmAttendanceCollection class is designed to work with lists of instances of LmmAttendance.
	/// </summary>
	public class LmmAttendanceCollection : BusinessCollectionBase<LmmAttendance>
	{
		/// <summary>
		/// Initializes a new instance of the LmmAttendanceCollection class.
		/// </summary>
		public LmmAttendanceCollection() { }
		/// <summary>
		/// Initializes a new instance of the LmmAttendanceCollection class.
		/// </summary>
		public LmmAttendanceCollection(IList<LmmAttendance> initialList) : base(initialList) { }
	}
}