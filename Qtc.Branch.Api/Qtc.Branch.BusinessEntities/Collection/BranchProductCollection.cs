using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The BranchProductCollection class is designed to work with lists of instances of BranchProduct.
	/// </summary>
	public class BranchProductCollection : BusinessCollectionBase<BranchProduct>
	{
		/// <summary>
		/// Initializes a new instance of the BranchProductCollection class.
		/// </summary>
		public BranchProductCollection() { }
		/// <summary>
		/// Initializes a new instance of the BranchProductCollection class.
		/// </summary>
		public BranchProductCollection(IList<BranchProduct> initialList) : base(initialList) { }
	}
}