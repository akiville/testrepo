using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The PurposeCollection class is designed to work with lists of instances of Purpose.
	/// </summary>
	public class PurposeCollection : BusinessCollectionBase<Purpose>
	{
		/// <summary>
		/// Initializes a new instance of the PurposeCollection class.
		/// </summary>
		public PurposeCollection() { }
		/// <summary>
		/// Initializes a new instance of the PurposeCollection class.
		/// </summary>
		public PurposeCollection(IList<Purpose> initialList) : base(initialList) { }
	}
}