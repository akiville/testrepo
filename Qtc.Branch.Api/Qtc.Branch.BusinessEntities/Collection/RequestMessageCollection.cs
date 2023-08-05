using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RequestMessageCollection class is designed to work with lists of instances of RequestMessage.
	/// </summary>
	public class RequestMessageCollection : BusinessCollectionBase<RequestMessage>
	{
		/// <summary>
		/// Initializes a new instance of the RequestMessageCollection class.
		/// </summary>
		public RequestMessageCollection() { }
		/// <summary>
		/// Initializes a new instance of the RequestMessageCollection class.
		/// </summary>
		public RequestMessageCollection(IList<RequestMessage> initialList) : base(initialList) { }
	}
}