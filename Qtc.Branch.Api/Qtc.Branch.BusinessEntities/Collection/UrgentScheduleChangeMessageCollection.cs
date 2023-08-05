using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The UrgentScheduleChangeMessageCollection class is designed to work with lists of instances of UrgentScheduleChangeMessage.
	/// </summary>
	public class UrgentScheduleChangeMessageCollection : BusinessCollectionBase<UrgentScheduleChangeMessage>
	{
		/// <summary>
		/// Initializes a new instance of the UrgentScheduleChangeMessageCollection class.
		/// </summary>
		public UrgentScheduleChangeMessageCollection() { }
		/// <summary>
		/// Initializes a new instance of the UrgentScheduleChangeMessageCollection class.
		/// </summary>
		public UrgentScheduleChangeMessageCollection(IList<UrgentScheduleChangeMessage> initialList) : base(initialList) { }
	}
}