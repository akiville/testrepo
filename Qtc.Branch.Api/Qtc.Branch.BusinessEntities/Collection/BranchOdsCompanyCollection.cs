using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The BranchOdsCompanyCollection class is designed to work with lists of instances of BranchOdsCompany.
	/// </summary>
	public class BranchOdsCompanyCollection : BusinessCollectionBase<BranchOdsCompany>
	{
		/// <summary>
		/// Initializes a new instance of the BranchOdsCompanyCollection class.
		/// </summary>
		public BranchOdsCompanyCollection() { }
		/// <summary>
		/// Initializes a new instance of the BranchOdsCompanyCollection class.
		/// </summary>
		public BranchOdsCompanyCollection(IList<BranchOdsCompany> initialList) : base(initialList) { }
	}
}