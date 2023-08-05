using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingAgentLoginCollection class is designed to work with lists of instances of RovingAgentLogin.
	/// </summary>
	public class RovingAgentLoginCollection : BusinessCollectionBase<RovingAgentLogin>
	{
		/// <summary>
		/// Initializes a new instance of the RovingAgentLoginCollection class.
		/// </summary>
		public RovingAgentLoginCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingAgentLoginCollection class.
		/// </summary>
		public RovingAgentLoginCollection(IList<RovingAgentLogin> initialList) : base(initialList) { }
	}
}