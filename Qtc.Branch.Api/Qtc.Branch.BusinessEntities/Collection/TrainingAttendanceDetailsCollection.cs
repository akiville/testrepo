using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The TrainingAttendanceDetailsCollection class is designed to work with lists of instances of TrainingAttendanceDetails.
	/// </summary>
	public class TrainingAttendanceDetailsCollection : BusinessCollectionBase<TrainingAttendanceDetails>
	{
		/// <summary>
		/// Initializes a new instance of the TrainingAttendanceDetailsCollection class.
		/// </summary>
		public TrainingAttendanceDetailsCollection() { }
		/// <summary>
		/// Initializes a new instance of the TrainingAttendanceDetailsCollection class.
		/// </summary>
		public TrainingAttendanceDetailsCollection(IList<TrainingAttendanceDetails> initialList) : base(initialList) { }
	}
}