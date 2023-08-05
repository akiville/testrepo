using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The ProductSerialCollection class is designed to work with lists of instances of ProductSerial.
	/// </summary>
	public class ProductSerialCollection : BusinessCollectionBase<ProductSerial>
	{
		/// <summary>
		/// Initializes a new instance of the ProductSerialCollection class.
		/// </summary>
		public ProductSerialCollection() { }
		/// <summary>
		/// Initializes a new instance of the ProductSerialCollection class.
		/// </summary>
		public ProductSerialCollection(IList<ProductSerial> initialList) : base(initialList) { }
	}
}