using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The LmmAttendanceUpdateCollection class is designed to work with lists of instances of LmmAttendanceUpdate.
	/// </summary>
	public class LmmAttendanceUpdateCollection : BusinessCollectionBase<LmmAttendanceUpdate>
	{
		/// <summary>
		/// Initializes a new instance of the LmmAttendanceUpdateCollection class.
		/// </summary>
		public LmmAttendanceUpdateCollection() { }
		/// <summary>
		/// Initializes a new instance of the LmmAttendanceUpdateCollection class.
		/// </summary>
		public LmmAttendanceUpdateCollection(IList<LmmAttendanceUpdate> initialList) : base(initialList) { }
	}
}