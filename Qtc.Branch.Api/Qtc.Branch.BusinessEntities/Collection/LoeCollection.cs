using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The LoeCollection class is designed to work with lists of instances of Loe.
	/// </summary>
	public class LoeCollection : BusinessCollectionBase<Loe>
	{
		/// <summary>
		/// Initializes a new instance of the LoeCollection class.
		/// </summary>
		public LoeCollection() { }
		/// <summary>
		/// Initializes a new instance of the LoeCollection class.
		/// </summary>
		public LoeCollection(IList<Loe> initialList) : base(initialList) { }
	}
}