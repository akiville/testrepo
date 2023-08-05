using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The LmmEntryCollection class is designed to work with lists of instances of LmmEntry.
	/// </summary>
	public class LmmEntryCollection : BusinessCollectionBase<LmmEntry>
	{
		/// <summary>
		/// Initializes a new instance of the LmmEntryCollection class.
		/// </summary>
		public LmmEntryCollection() { }
		/// <summary>
		/// Initializes a new instance of the LmmEntryCollection class.
		/// </summary>
		public LmmEntryCollection(IList<LmmEntry> initialList) : base(initialList) { }
	}
}