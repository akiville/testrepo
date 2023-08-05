using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The TraineeAttendanceCollection class is designed to work with lists of instances of TraineeAttendance.
	/// </summary>
	public class TraineeAttendanceCollection : BusinessCollectionBase<TraineeAttendance>
	{
		/// <summary>
		/// Initializes a new instance of the TraineeAttendanceCollection class.
		/// </summary>
		public TraineeAttendanceCollection() { }
		/// <summary>
		/// Initializes a new instance of the TraineeAttendanceCollection class.
		/// </summary>
		public TraineeAttendanceCollection(IList<TraineeAttendance> initialList) : base(initialList) { }
	}
}