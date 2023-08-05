using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingCheckListViolationDetailCollection class is designed to work with lists of instances of RovingCheckListViolationDetail.
	/// </summary>
	public class RovingCheckListViolationDetailCollection : BusinessCollectionBase<RovingCheckListViolationDetail>
	{
		/// <summary>
		/// Initializes a new instance of the RovingCheckListViolationDetailCollection class.
		/// </summary>
		public RovingCheckListViolationDetailCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingCheckListViolationDetailCollection class.
		/// </summary>
		public RovingCheckListViolationDetailCollection(IList<RovingCheckListViolationDetail> initialList) : base(initialList) { }
	}
}