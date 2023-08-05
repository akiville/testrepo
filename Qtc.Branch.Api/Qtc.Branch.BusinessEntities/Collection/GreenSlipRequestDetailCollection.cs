using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The GreenSlipRequestDetailCollection class is designed to work with lists of instances of GreenSlipRequestDetail.
	/// </summary>
	public class GreenSlipRequestDetailCollection : BusinessCollectionBase<GreenSlipRequestDetail>
	{
		/// <summary>
		/// Initializes a new instance of the GreenSlipRequestDetailCollection class.
		/// </summary>
		public GreenSlipRequestDetailCollection() { }
		/// <summary>
		/// Initializes a new instance of the GreenSlipRequestDetailCollection class.
		/// </summary>
		public GreenSlipRequestDetailCollection(IList<GreenSlipRequestDetail> initialList) : base(initialList) { }
	}
}