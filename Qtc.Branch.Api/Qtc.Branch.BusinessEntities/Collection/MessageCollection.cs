using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The MessageCollection class is designed to work with lists of instances of Message.
	/// </summary>
	public class MessageCollection : BusinessCollectionBase<Message>
	{
		/// <summary>
		/// Initializes a new instance of the MessageCollection class.
		/// </summary>
		public MessageCollection() { }
		/// <summary>
		/// Initializes a new instance of the MessageCollection class.
		/// </summary>
		public MessageCollection(IList<Message> initialList) : base(initialList) { }
	}
}