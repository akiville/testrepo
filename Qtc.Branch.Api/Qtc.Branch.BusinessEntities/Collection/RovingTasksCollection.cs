using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingTasksCollection class is designed to work with lists of instances of RovingTasks.
	/// </summary>
	public class RovingTasksCollection : BusinessCollectionBase<RovingTasks>
	{
		/// <summary>
		/// Initializes a new instance of the RovingTasksCollection class.
		/// </summary>
		public RovingTasksCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingTasksCollection class.
		/// </summary>
		public RovingTasksCollection(IList<RovingTasks> initialList) : base(initialList) { }
	}
}