using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RequestForIssuanceDetailCollection class is designed to work with lists of instances of RequestForIssuanceDetail.
	/// </summary>
	public class RequestForIssuanceDetailCollection : BusinessCollectionBase<RequestForIssuanceDetail>
	{
		/// <summary>
		/// Initializes a new instance of the RequestForIssuanceDetailCollection class.
		/// </summary>
		public RequestForIssuanceDetailCollection() { }
		/// <summary>
		/// Initializes a new instance of the RequestForIssuanceDetailCollection class.
		/// </summary>
		public RequestForIssuanceDetailCollection(IList<RequestForIssuanceDetail> initialList) : base(initialList) { }
	}
}