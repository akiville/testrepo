using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The NovExplanationCollection class is designed to work with lists of instances of NovExplanation.
	/// </summary>
	public class NovExplanationCollection : BusinessCollectionBase<NovExplanation>
	{
		/// <summary>
		/// Initializes a new instance of the NovExplanationCollection class.
		/// </summary>
		public NovExplanationCollection() { }
		/// <summary>
		/// Initializes a new instance of the NovExplanationCollection class.
		/// </summary>
		public NovExplanationCollection(IList<NovExplanation> initialList) : base(initialList) { }
	}
}