using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The GreenSlipRequestCollection class is designed to work with lists of instances of GreenSlipRequest.
	/// </summary>
	public class GreenSlipRequestCollection : BusinessCollectionBase<GreenSlipRequest>
	{
		/// <summary>
		/// Initializes a new instance of the GreenSlipRequestCollection class.
		/// </summary>
		public GreenSlipRequestCollection() { }
		/// <summary>
		/// Initializes a new instance of the GreenSlipRequestCollection class.
		/// </summary>
		public GreenSlipRequestCollection(IList<GreenSlipRequest> initialList) : base(initialList) { }
	}
}