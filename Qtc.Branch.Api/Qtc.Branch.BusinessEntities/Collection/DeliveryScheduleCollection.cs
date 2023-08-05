using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DeliveryScheduleCollection class is designed to work with lists of instances of DeliverySchedule.
	/// </summary>
	public class DeliveryScheduleCollection : BusinessCollectionBase<DeliverySchedule>
	{
		/// <summary>
		/// Initializes a new instance of the DeliveryScheduleCollection class.
		/// </summary>
		public DeliveryScheduleCollection() { }
		/// <summary>
		/// Initializes a new instance of the DeliveryScheduleCollection class.
		/// </summary>
		public DeliveryScheduleCollection(IList<DeliverySchedule> initialList) : base(initialList) { }
	}
}