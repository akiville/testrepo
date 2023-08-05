using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The ProductGroupCollection class is designed to work with lists of instances of ProductGroup.
	/// </summary>
	public class ProductGroupCollection : BusinessCollectionBase<ProductGroup>
	{
		/// <summary>
		/// Initializes a new instance of the ProductGroupCollection class.
		/// </summary>
		public ProductGroupCollection() { }
		/// <summary>
		/// Initializes a new instance of the ProductGroupCollection class.
		/// </summary>
		public ProductGroupCollection(IList<ProductGroup> initialList) : base(initialList) { }
	}
}