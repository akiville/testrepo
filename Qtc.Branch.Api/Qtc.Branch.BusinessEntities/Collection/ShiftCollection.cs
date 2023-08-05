using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The ShiftCollection class is designed to work with lists of instances of Shift.
	/// </summary>
	public class ShiftCollection : BusinessCollectionBase<Shift>
	{
		/// <summary>
		/// Initializes a new instance of the ShiftCollection class.
		/// </summary>
		public ShiftCollection() { }
		/// <summary>
		/// Initializes a new instance of the ShiftCollection class.
		/// </summary>
		public ShiftCollection(IList<Shift> initialList) : base(initialList) { }
	}
}