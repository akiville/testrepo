using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The MessengerDetailCollection class is designed to work with lists of instances of MessengerDetail.
	/// </summary>
	public class MessengerDetailCollection : BusinessCollectionBase<MessengerDetail>
	{
		/// <summary>
		/// Initializes a new instance of the MessengerDetailCollection class.
		/// </summary>
		public MessengerDetailCollection() { }
		/// <summary>
		/// Initializes a new instance of the MessengerDetailCollection class.
		/// </summary>
		public MessengerDetailCollection(IList<MessengerDetail> initialList) : base(initialList) { }
	}
}