using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The UserCollection class is designed to work with lists of instances of User.
	/// </summary>
	public class UserCollection : BusinessCollectionBase<User>
	{
		/// <summary>
		/// Initializes a new instance of the UserCollection class.
		/// </summary>
		public UserCollection() { }
		/// <summary>
		/// Initializes a new instance of the UserCollection class.
		/// </summary>
		public UserCollection(IList<User> initialList) : base(initialList) { }
	}
}