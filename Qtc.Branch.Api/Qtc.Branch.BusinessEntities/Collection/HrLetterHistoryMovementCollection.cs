using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The HrLetterHistoryMovementCollection class is designed to work with lists of instances of HrLetterHistoryMovement.
	/// </summary>
	public class HrLetterHistoryMovementCollection : BusinessCollectionBase<HrLetterHistoryMovement>
	{
		/// <summary>
		/// Initializes a new instance of the HrLetterHistoryMovementCollection class.
		/// </summary>
		public HrLetterHistoryMovementCollection() { }
		/// <summary>
		/// Initializes a new instance of the HrLetterHistoryMovementCollection class.
		/// </summary>
		public HrLetterHistoryMovementCollection(IList<HrLetterHistoryMovement> initialList) : base(initialList) { }
	}
}