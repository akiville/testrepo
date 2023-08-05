using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The TypeOfLetterCollection class is designed to work with lists of instances of TypeOfLetter.
	/// </summary>
	public class TypeOfLetterCollection : BusinessCollectionBase<TypeOfLetter>
	{
		/// <summary>
		/// Initializes a new instance of the TypeOfLetterCollection class.
		/// </summary>
		public TypeOfLetterCollection() { }
		/// <summary>
		/// Initializes a new instance of the TypeOfLetterCollection class.
		/// </summary>
		public TypeOfLetterCollection(IList<TypeOfLetter> initialList) : base(initialList) { }
	}
}