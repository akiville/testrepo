using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The BranchsCollection class is designed to work with lists of instances of Branchs.
	/// </summary>
	public class BranchsCollection : BusinessCollectionBase<Branchs>
	{
		/// <summary>
		/// Initializes a new instance of the BranchsCollection class.
		/// </summary>
		public BranchsCollection() { }
		/// <summary>
		/// Initializes a new instance of the BranchsCollection class.
		/// </summary>
		public BranchsCollection(IList<Branchs> initialList) : base(initialList) { }
	}
}