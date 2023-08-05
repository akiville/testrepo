using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The UrgentScheduleChangeCollection class is designed to work with lists of instances of UrgentScheduleChange.
	/// </summary>
	public class UrgentScheduleChangeCollection : BusinessCollectionBase<UrgentScheduleChange>
	{
		/// <summary>
		/// Initializes a new instance of the UrgentScheduleChangeCollection class.
		/// </summary>
		public UrgentScheduleChangeCollection() { }
		/// <summary>
		/// Initializes a new instance of the UrgentScheduleChangeCollection class.
		/// </summary>
		public UrgentScheduleChangeCollection(IList<UrgentScheduleChange> initialList) : base(initialList) { }
	}
}