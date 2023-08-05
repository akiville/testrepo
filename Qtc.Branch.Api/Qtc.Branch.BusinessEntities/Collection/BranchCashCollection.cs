using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The BranchCashCollection class is designed to work with lists of instances of BranchCash.
	/// </summary>
	public class BranchCashCollection : BusinessCollectionBase<BranchCash>
	{
		/// <summary>
		/// Initializes a new instance of the BranchCashCollection class.
		/// </summary>
		public BranchCashCollection() { }
		/// <summary>
		/// Initializes a new instance of the BranchCashCollection class.
		/// </summary>
		public BranchCashCollection(IList<BranchCash> initialList) : base(initialList) { }
	}
}