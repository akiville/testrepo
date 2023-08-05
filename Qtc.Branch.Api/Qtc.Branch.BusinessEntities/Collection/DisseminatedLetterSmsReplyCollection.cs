using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DisseminatedLetterSmsReplyCollection class is designed to work with lists of instances of DisseminatedLetterSmsReply.
	/// </summary>
	public class DisseminatedLetterSmsReplyCollection : BusinessCollectionBase<DisseminatedLetterSmsReply>
	{
		/// <summary>
		/// Initializes a new instance of the DisseminatedLetterSmsReplyCollection class.
		/// </summary>
		public DisseminatedLetterSmsReplyCollection() { }
		/// <summary>
		/// Initializes a new instance of the DisseminatedLetterSmsReplyCollection class.
		/// </summary>
		public DisseminatedLetterSmsReplyCollection(IList<DisseminatedLetterSmsReply> initialList) : base(initialList) { }
	}
}