using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The LmmCollection class is designed to work with lists of instances of Lmm.
	/// </summary>
	public class LmmCollection : BusinessCollectionBase<Lmm>
	{
		/// <summary>
		/// Initializes a new instance of the LmmCollection class.
		/// </summary>
		public LmmCollection() { }
		/// <summary>
		/// Initializes a new instance of the LmmCollection class.
		/// </summary>
		public LmmCollection(IList<Lmm> initialList) : base(initialList) { }
	}
}