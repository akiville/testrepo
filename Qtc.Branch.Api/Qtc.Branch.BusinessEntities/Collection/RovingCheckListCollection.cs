using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingCheckListCollection class is designed to work with lists of instances of RovingCheckList.
	/// </summary>
	public class RovingCheckListCollection : BusinessCollectionBase<RovingCheckList>
	{
		/// <summary>
		/// Initializes a new instance of the RovingCheckListCollection class.
		/// </summary>
		public RovingCheckListCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingCheckListCollection class.
		/// </summary>
		public RovingCheckListCollection(IList<RovingCheckList> initialList) : base(initialList) { }
	}
}