using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RfscCollection class is designed to work with lists of instances of Rfsc.
	/// </summary>
	public class RfscCollection : BusinessCollectionBase<Rfsc>
	{
		/// <summary>
		/// Initializes a new instance of the RfscCollection class.
		/// </summary>
		public RfscCollection() { }
		/// <summary>
		/// Initializes a new instance of the RfscCollection class.
		/// </summary>
		public RfscCollection(IList<Rfsc> initialList) : base(initialList) { }
	}
}