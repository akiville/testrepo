using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The AddBackCollection class is designed to work with lists of instances of AddBack.
	/// </summary>
	public class AddBackCollection : BusinessCollectionBase<AddBack>
	{
		/// <summary>
		/// Initializes a new instance of the AddBackCollection class.
		/// </summary>
		public AddBackCollection() { }
		/// <summary>
		/// Initializes a new instance of the AddBackCollection class.
		/// </summary>
		public AddBackCollection(IList<AddBack> initialList) : base(initialList) { }
	}
}