using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingCheckListViolationPersonnelCollection class is designed to work with lists of instances of RovingCheckListViolationPersonnel.
	/// </summary>
	public class RovingCheckListViolationPersonnelCollection : BusinessCollectionBase<RovingCheckListViolationPersonnel>
	{
		/// <summary>
		/// Initializes a new instance of the RovingCheckListViolationPersonnelCollection class.
		/// </summary>
		public RovingCheckListViolationPersonnelCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingCheckListViolationPersonnelCollection class.
		/// </summary>
		public RovingCheckListViolationPersonnelCollection(IList<RovingCheckListViolationPersonnel> initialList) : base(initialList) { }
	}
}