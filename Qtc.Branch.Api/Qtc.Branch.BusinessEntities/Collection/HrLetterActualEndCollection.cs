using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The HrLetterActualEndCollection class is designed to work with lists of instances of HrLetterActualEnd.
	/// </summary>
	public class HrLetterActualEndCollection : BusinessCollectionBase<HrLetterActualEnd>
	{
		/// <summary>
		/// Initializes a new instance of the HrLetterActualEndCollection class.
		/// </summary>
		public HrLetterActualEndCollection() { }
		/// <summary>
		/// Initializes a new instance of the HrLetterActualEndCollection class.
		/// </summary>
		public HrLetterActualEndCollection(IList<HrLetterActualEnd> initialList) : base(initialList) { }
	}
}