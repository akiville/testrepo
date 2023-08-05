using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DeliveryScheduleConcernCollection class is designed to work with lists of instances of DeliveryScheduleConcern.
	/// </summary>
	public class DeliveryScheduleConcernCollection : BusinessCollectionBase<DeliveryScheduleConcern>
	{
		/// <summary>
		/// Initializes a new instance of the DeliveryScheduleConcernCollection class.
		/// </summary>
		public DeliveryScheduleConcernCollection() { }
		/// <summary>
		/// Initializes a new instance of the DeliveryScheduleConcernCollection class.
		/// </summary>
		public DeliveryScheduleConcernCollection(IList<DeliveryScheduleConcern> initialList) : base(initialList) { }
	}
}