using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The LoeDetailCollection class is designed to work with lists of instances of LoeDetail.
	/// </summary>
	public class LoeDetailCollection : BusinessCollectionBase<LoeDetail>
	{
		/// <summary>
		/// Initializes a new instance of the LoeDetailCollection class.
		/// </summary>
		public LoeDetailCollection() { }
		/// <summary>
		/// Initializes a new instance of the LoeDetailCollection class.
		/// </summary>
		public LoeDetailCollection(IList<LoeDetail> initialList) : base(initialList) { }
	}
}