using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The HrLetterRequestCollection class is designed to work with lists of instances of HrLetterRequest.
	/// </summary>
	public class HrLetterRequestCollection : BusinessCollectionBase<HrLetterRequest>
	{
		/// <summary>
		/// Initializes a new instance of the HrLetterRequestCollection class.
		/// </summary>
		public HrLetterRequestCollection() { }
		/// <summary>
		/// Initializes a new instance of the HrLetterRequestCollection class.
		/// </summary>
		public HrLetterRequestCollection(IList<HrLetterRequest> initialList) : base(initialList) { }
	}
}