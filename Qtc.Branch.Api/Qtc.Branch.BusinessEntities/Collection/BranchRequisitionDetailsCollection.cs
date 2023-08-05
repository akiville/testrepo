using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The BranchRequisitionDetailsCollection class is designed to work with lists of instances of BranchRequisitionDetails.
	/// </summary>
	public class BranchRequisitionDetailsCollection : BusinessCollectionBase<BranchRequisitionDetails>
	{
		/// <summary>
		/// Initializes a new instance of the BranchRequisitionDetailsCollection class.
		/// </summary>
		public BranchRequisitionDetailsCollection() { }
		/// <summary>
		/// Initializes a new instance of the BranchRequisitionDetailsCollection class.
		/// </summary>
		public BranchRequisitionDetailsCollection(IList<BranchRequisitionDetails> initialList) : base(initialList) { }
	}
}