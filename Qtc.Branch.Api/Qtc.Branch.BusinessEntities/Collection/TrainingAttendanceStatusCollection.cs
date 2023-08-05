using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The TrainingAttendanceStatusCollection class is designed to work with lists of instances of TrainingAttendanceStatus.
	/// </summary>
	public class TrainingAttendanceStatusCollection : BusinessCollectionBase<TrainingAttendanceStatus>
	{
		/// <summary>
		/// Initializes a new instance of the TrainingAttendanceStatusCollection class.
		/// </summary>
		public TrainingAttendanceStatusCollection() { }
		/// <summary>
		/// Initializes a new instance of the TrainingAttendanceStatusCollection class.
		/// </summary>
		public TrainingAttendanceStatusCollection(IList<TrainingAttendanceStatus> initialList) : base(initialList) { }
	}
}