using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The ApiActionCollection class is designed to work with lists of instances of ApiAction.
	/// </summary>
	public class ApiActionCollection : BusinessCollectionBase<ApiAction>
	{
		/// <summary>
		/// Initializes a new instance of the ApiActionCollection class.
		/// </summary>
		public ApiActionCollection() { }
		/// <summary>
		/// Initializes a new instance of the ApiActionCollection class.
		/// </summary>
		public ApiActionCollection(IList<ApiAction> initialList) : base(initialList) { }
	}
}