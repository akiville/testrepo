using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The NonComplianceCollection class is designed to work with lists of instances of NonCompliance.
	/// </summary>
	public class NonComplianceCollection : BusinessCollectionBase<NonCompliance>
	{
		/// <summary>
		/// Initializes a new instance of the NonComplianceCollection class.
		/// </summary>
		public NonComplianceCollection() { }
		/// <summary>
		/// Initializes a new instance of the NonComplianceCollection class.
		/// </summary>
		public NonComplianceCollection(IList<NonCompliance> initialList) : base(initialList) { }
	}
}