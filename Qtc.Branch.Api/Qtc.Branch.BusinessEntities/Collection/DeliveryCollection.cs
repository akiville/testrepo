using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DeliveryCollection class is designed to work with lists of instances of Delivery.
	/// </summary>
	public class DeliveryCollection : BusinessCollectionBase<Delivery>
	{
		/// <summary>
		/// Initializes a new instance of the DeliveryCollection class.
		/// </summary>
		public DeliveryCollection() { }
		/// <summary>
		/// Initializes a new instance of the DeliveryCollection class.
		/// </summary>
		public DeliveryCollection(IList<Delivery> initialList) : base(initialList) { }
	}
}