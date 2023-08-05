using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The AnnouncementCollection class is designed to work with lists of instances of Announcement.
	/// </summary>
	public class AnnouncementCollection : BusinessCollectionBase<Announcement>
	{
		/// <summary>
		/// Initializes a new instance of the AnnouncementCollection class.
		/// </summary>
		public AnnouncementCollection() { }
		/// <summary>
		/// Initializes a new instance of the AnnouncementCollection class.
		/// </summary>
		public AnnouncementCollection(IList<Announcement> initialList) : base(initialList) { }
	}
}