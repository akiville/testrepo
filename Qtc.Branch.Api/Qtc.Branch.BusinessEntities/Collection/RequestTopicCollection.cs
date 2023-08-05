using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RequestTopicCollection class is designed to work with lists of instances of RequestTopic.
	/// </summary>
	public class RequestTopicCollection : BusinessCollectionBase<RequestTopic>
	{
		/// <summary>
		/// Initializes a new instance of the RequestTopicCollection class.
		/// </summary>
		public RequestTopicCollection() { }
		/// <summary>
		/// Initializes a new instance of the RequestTopicCollection class.
		/// </summary>
		public RequestTopicCollection(IList<RequestTopic> initialList) : base(initialList) { }
	}
}