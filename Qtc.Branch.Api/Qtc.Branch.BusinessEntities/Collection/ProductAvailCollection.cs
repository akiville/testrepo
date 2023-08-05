using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The ProductAvailCollection class is designed to work with lists of instances of ProductAvail.
	/// </summary>
	public class ProductAvailCollection : BusinessCollectionBase<ProductAvail>
	{
		/// <summary>
		/// Initializes a new instance of the ProductAvailCollection class.
		/// </summary>
		public ProductAvailCollection() { }
		/// <summary>
		/// Initializes a new instance of the ProductAvailCollection class.
		/// </summary>
		public ProductAvailCollection(IList<ProductAvail> initialList) : base(initialList) { }
	}
}