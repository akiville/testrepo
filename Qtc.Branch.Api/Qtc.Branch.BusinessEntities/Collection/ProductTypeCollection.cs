using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The ProductTypeCollection class is designed to work with lists of instances of ProductType.
	/// </summary>
	public class ProductTypeCollection : BusinessCollectionBase<ProductType>
	{
		/// <summary>
		/// Initializes a new instance of the ProductTypeCollection class.
		/// </summary>
		public ProductTypeCollection() { }
		/// <summary>
		/// Initializes a new instance of the ProductTypeCollection class.
		/// </summary>
		public ProductTypeCollection(IList<ProductType> initialList) : base(initialList) { }
	}
}