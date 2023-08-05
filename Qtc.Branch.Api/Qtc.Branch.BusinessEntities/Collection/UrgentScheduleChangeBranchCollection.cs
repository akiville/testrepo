using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The UrgentScheduleChangeBranchCollection class is designed to work with lists of instances of UrgentScheduleChangeBranch.
	/// </summary>
	public class UrgentScheduleChangeBranchCollection : BusinessCollectionBase<UrgentScheduleChangeBranch>
	{
		/// <summary>
		/// Initializes a new instance of the UrgentScheduleChangeBranchCollection class.
		/// </summary>
		public UrgentScheduleChangeBranchCollection() { }
		/// <summary>
		/// Initializes a new instance of the UrgentScheduleChangeBranchCollection class.
		/// </summary>
		public UrgentScheduleChangeBranchCollection(IList<UrgentScheduleChangeBranch> initialList) : base(initialList) { }
	}
}