using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingCheckListViolationCollection class is designed to work with lists of instances of RovingCheckListViolation.
	/// </summary>
	public class RovingCheckListViolationCollection : BusinessCollectionBase<RovingCheckListViolation>
	{
		/// <summary>
		/// Initializes a new instance of the RovingCheckListViolationCollection class.
		/// </summary>
		public RovingCheckListViolationCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingCheckListViolationCollection class.
		/// </summary>
		public RovingCheckListViolationCollection(IList<RovingCheckListViolation> initialList) : base(initialList) { }
	}
}