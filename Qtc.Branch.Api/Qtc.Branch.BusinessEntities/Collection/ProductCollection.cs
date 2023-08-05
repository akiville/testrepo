using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The ProductCollection class is designed to work with lists of instances of Product.
	/// </summary>
	public class ProductCollection : BusinessCollectionBase<Product>
	{
		/// <summary>
		/// Initializes a new instance of the ProductCollection class.
		/// </summary>
		public ProductCollection() { }
		/// <summary>
		/// Initializes a new instance of the ProductCollection class.
		/// </summary>
		public ProductCollection(IList<Product> initialList) : base(initialList) { }
	}
}