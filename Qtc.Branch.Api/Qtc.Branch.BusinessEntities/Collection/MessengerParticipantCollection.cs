using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The MessengerParticipantCollection class is designed to work with lists of instances of MessengerParticipant.
	/// </summary>
	public class MessengerParticipantCollection : BusinessCollectionBase<MessengerParticipant>
	{
		/// <summary>
		/// Initializes a new instance of the MessengerParticipantCollection class.
		/// </summary>
		public MessengerParticipantCollection() { }
		/// <summary>
		/// Initializes a new instance of the MessengerParticipantCollection class.
		/// </summary>
		public MessengerParticipantCollection(IList<MessengerParticipant> initialList) : base(initialList) { }
	}
}