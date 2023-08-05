using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The CategoryCollection class is designed to work with lists of instances of Category.
	/// </summary>
	public class CategoryCollection : BusinessCollectionBase<Category>
	{
		/// <summary>
		/// Initializes a new instance of the CategoryCollection class.
		/// </summary>
		public CategoryCollection() { }
		/// <summary>
		/// Initializes a new instance of the CategoryCollection class.
		/// </summary>
		public CategoryCollection(IList<Category> initialList) : base(initialList) { }
	}
}