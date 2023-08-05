using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The ReasonCollection class is designed to work with lists of instances of Reason.
	/// </summary>
	public class ReasonCollection : BusinessCollectionBase<Reason>
	{
		/// <summary>
		/// Initializes a new instance of the ReasonCollection class.
		/// </summary>
		public ReasonCollection() { }
		/// <summary>
		/// Initializes a new instance of the ReasonCollection class.
		/// </summary>
		public ReasonCollection(IList<Reason> initialList) : base(initialList) { }
	}
}