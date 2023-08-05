using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DisseminatedLetterCollection class is designed to work with lists of instances of DisseminatedLetter.
	/// </summary>
	public class DisseminatedLetterCollection : BusinessCollectionBase<DisseminatedLetter>
	{
		/// <summary>
		/// Initializes a new instance of the DisseminatedLetterCollection class.
		/// </summary>
		public DisseminatedLetterCollection() { }
		/// <summary>
		/// Initializes a new instance of the DisseminatedLetterCollection class.
		/// </summary>
		public DisseminatedLetterCollection(IList<DisseminatedLetter> initialList) : base(initialList) { }
	}
}