using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DeliveryDetailsCollection class is designed to work with lists of instances of DeliveryDetails.
	/// </summary>
	public class DeliveryDetailsCollection : BusinessCollectionBase<DeliveryDetails>
	{
		/// <summary>
		/// Initializes a new instance of the DeliveryDetailsCollection class.
		/// </summary>
		public DeliveryDetailsCollection() { }
		/// <summary>
		/// Initializes a new instance of the DeliveryDetailsCollection class.
		/// </summary>
		public DeliveryDetailsCollection(IList<DeliveryDetails> initialList) : base(initialList) { }
	}
}