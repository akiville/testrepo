using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DeliveryScheduleDetailCollection class is designed to work with lists of instances of DeliveryScheduleDetail.
	/// </summary>
	public class DeliveryScheduleDetailCollection : BusinessCollectionBase<DeliveryScheduleDetail>
	{
		/// <summary>
		/// Initializes a new instance of the DeliveryScheduleDetailCollection class.
		/// </summary>
		public DeliveryScheduleDetailCollection() { }
		/// <summary>
		/// Initializes a new instance of the DeliveryScheduleDetailCollection class.
		/// </summary>
		public DeliveryScheduleDetailCollection(IList<DeliveryScheduleDetail> initialList) : base(initialList) { }
	}
}