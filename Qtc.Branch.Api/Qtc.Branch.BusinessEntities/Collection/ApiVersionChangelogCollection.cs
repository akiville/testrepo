using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The ApiVersionChangelogCollection class is designed to work with lists of instances of ApiVersionChangelog.
	/// </summary>
	public class ApiVersionChangelogCollection : BusinessCollectionBase<ApiVersionChangelog>
	{
		/// <summary>
		/// Initializes a new instance of the ApiVersionChangelogCollection class.
		/// </summary>
		public ApiVersionChangelogCollection() { }
		/// <summary>
		/// Initializes a new instance of the ApiVersionChangelogCollection class.
		/// </summary>
		public ApiVersionChangelogCollection(IList<ApiVersionChangelog> initialList) : base(initialList) { }
	}
}