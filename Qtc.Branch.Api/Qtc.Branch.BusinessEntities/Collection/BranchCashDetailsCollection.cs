using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The BranchCashDetailsCollection class is designed to work with lists of instances of BranchCashDetails.
	/// </summary>
	public class BranchCashDetailsCollection : BusinessCollectionBase<BranchCashDetails>
	{
		/// <summary>
		/// Initializes a new instance of the BranchCashDetailsCollection class.
		/// </summary>
		public BranchCashDetailsCollection() { }
		/// <summary>
		/// Initializes a new instance of the BranchCashDetailsCollection class.
		/// </summary>
		public BranchCashDetailsCollection(IList<BranchCashDetails> initialList) : base(initialList) { }
	}
}