using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The BranchRequisitionCollection class is designed to work with lists of instances of BranchRequisition.
	/// </summary>
	public class BranchRequisitionCollection : BusinessCollectionBase<BranchRequisition>
	{
		/// <summary>
		/// Initializes a new instance of the BranchRequisitionCollection class.
		/// </summary>
		public BranchRequisitionCollection() { }
		/// <summary>
		/// Initializes a new instance of the BranchRequisitionCollection class.
		/// </summary>
		public BranchRequisitionCollection(IList<BranchRequisition> initialList) : base(initialList) { }
	}
}