using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The ApiVersionCollection class is designed to work with lists of instances of ApiVersion.
	/// </summary>
	public class ApiVersionCollection : BusinessCollectionBase<ApiVersion>
	{
		/// <summary>
		/// Initializes a new instance of the ApiVersionCollection class.
		/// </summary>
		public ApiVersionCollection() { }
		/// <summary>
		/// Initializes a new instance of the ApiVersionCollection class.
		/// </summary>
		public ApiVersionCollection(IList<ApiVersion> initialList) : base(initialList) { }
	}
}