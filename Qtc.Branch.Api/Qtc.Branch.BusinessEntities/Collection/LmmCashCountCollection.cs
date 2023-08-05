using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The LmmCashCountCollection class is designed to work with lists of instances of LmmCashCount.
	/// </summary>
	public class LmmCashCountCollection : BusinessCollectionBase<LmmCashCount>
	{
		/// <summary>
		/// Initializes a new instance of the LmmCashCountCollection class.
		/// </summary>
		public LmmCashCountCollection() { }
		/// <summary>
		/// Initializes a new instance of the LmmCashCountCollection class.
		/// </summary>
		public LmmCashCountCollection(IList<LmmCashCount> initialList) : base(initialList) { }
	}
}