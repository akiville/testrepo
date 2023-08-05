using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RequestForIssuanceCollection class is designed to work with lists of instances of RequestForIssuance.
	/// </summary>
	public class RequestForIssuanceCollection : BusinessCollectionBase<RequestForIssuance>
	{
		/// <summary>
		/// Initializes a new instance of the RequestForIssuanceCollection class.
		/// </summary>
		public RequestForIssuanceCollection() { }
		/// <summary>
		/// Initializes a new instance of the RequestForIssuanceCollection class.
		/// </summary>
		public RequestForIssuanceCollection(IList<RequestForIssuance> initialList) : base(initialList) { }
	}
}