using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DeliveryScheduleConcernDetailCollection class is designed to work with lists of instances of DeliveryScheduleConcernDetail.
	/// </summary>
	public class DeliveryScheduleConcernDetailCollection : BusinessCollectionBase<DeliveryScheduleConcernDetail>
	{
		/// <summary>
		/// Initializes a new instance of the DeliveryScheduleConcernDetailCollection class.
		/// </summary>
		public DeliveryScheduleConcernDetailCollection() { }
		/// <summary>
		/// Initializes a new instance of the DeliveryScheduleConcernDetailCollection class.
		/// </summary>
		public DeliveryScheduleConcernDetailCollection(IList<DeliveryScheduleConcernDetail> initialList) : base(initialList) { }
	}
}