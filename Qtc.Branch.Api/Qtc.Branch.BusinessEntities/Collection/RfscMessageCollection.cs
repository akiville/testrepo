using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RfscMessageCollection class is designed to work with lists of instances of RfscMessage.
	/// </summary>
	public class RfscMessageCollection : BusinessCollectionBase<RfscMessage>
	{
		/// <summary>
		/// Initializes a new instance of the RfscMessageCollection class.
		/// </summary>
		public RfscMessageCollection() { }
		/// <summary>
		/// Initializes a new instance of the RfscMessageCollection class.
		/// </summary>
		public RfscMessageCollection(IList<RfscMessage> initialList) : base(initialList) { }
	}
}