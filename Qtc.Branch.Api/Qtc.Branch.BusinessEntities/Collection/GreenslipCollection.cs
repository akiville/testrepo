using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The GreenslipCollection class is designed to work with lists of instances of Greenslip.
	/// </summary>
	public class GreenslipCollection : BusinessCollectionBase<Greenslip>
	{
		/// <summary>
		/// Initializes a new instance of the GreenslipCollection class.
		/// </summary>
		public GreenslipCollection() { }
		/// <summary>
		/// Initializes a new instance of the GreenslipCollection class.
		/// </summary>
		public GreenslipCollection(IList<Greenslip> initialList) : base(initialList) { }
	}
}