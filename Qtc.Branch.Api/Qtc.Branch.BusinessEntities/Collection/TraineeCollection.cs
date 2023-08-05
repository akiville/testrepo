using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The TraineeCollection class is designed to work with lists of instances of Trainee.
	/// </summary>
	public class TraineeCollection : BusinessCollectionBase<Trainee>
	{
		/// <summary>
		/// Initializes a new instance of the TraineeCollection class.
		/// </summary>
		public TraineeCollection() { }
		/// <summary>
		/// Initializes a new instance of the TraineeCollection class.
		/// </summary>
		public TraineeCollection(IList<Trainee> initialList) : base(initialList) { }
	}
}