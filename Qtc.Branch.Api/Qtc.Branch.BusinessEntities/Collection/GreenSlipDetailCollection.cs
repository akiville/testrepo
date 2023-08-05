using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The GreenSlipDetailCollection class is designed to work with lists of instances of GreenSlipDetail.
	/// </summary>
	public class GreenSlipDetailCollection : BusinessCollectionBase<GreenSlipDetail>
	{
		/// <summary>
		/// Initializes a new instance of the GreenSlipDetailCollection class.
		/// </summary>
		public GreenSlipDetailCollection() { }
		/// <summary>
		/// Initializes a new instance of the GreenSlipDetailCollection class.
		/// </summary>
		public GreenSlipDetailCollection(IList<GreenSlipDetail> initialList) : base(initialList) { }
	}
}