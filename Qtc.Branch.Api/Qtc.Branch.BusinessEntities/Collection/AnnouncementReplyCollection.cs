using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The AnnouncementReplyCollection class is designed to work with lists of instances of AnnouncementReply.
	/// </summary>
	public class AnnouncementReplyCollection : BusinessCollectionBase<AnnouncementReply>
	{
		/// <summary>
		/// Initializes a new instance of the AnnouncementReplyCollection class.
		/// </summary>
		public AnnouncementReplyCollection() { }
		/// <summary>
		/// Initializes a new instance of the AnnouncementReplyCollection class.
		/// </summary>
		public AnnouncementReplyCollection(IList<AnnouncementReply> initialList) : base(initialList) { }
	}
}