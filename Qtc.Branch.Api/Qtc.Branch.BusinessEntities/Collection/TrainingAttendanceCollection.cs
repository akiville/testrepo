using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The TrainingAttendanceCollection class is designed to work with lists of instances of TrainingAttendance.
	/// </summary>
	public class TrainingAttendanceCollection : BusinessCollectionBase<TrainingAttendance>
	{
		/// <summary>
		/// Initializes a new instance of the TrainingAttendanceCollection class.
		/// </summary>
		public TrainingAttendanceCollection() { }
		/// <summary>
		/// Initializes a new instance of the TrainingAttendanceCollection class.
		/// </summary>
		public TrainingAttendanceCollection(IList<TrainingAttendance> initialList) : base(initialList) { }
	}
}