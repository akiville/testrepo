using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RequestRepairCollection class is designed to work with lists of instances of RequestRepair.
	/// </summary>
	public class RequestRepairCollection : BusinessCollectionBase<RequestRepair>
	{
		/// <summary>
		/// Initializes a new instance of the RequestRepairCollection class.
		/// </summary>
		public RequestRepairCollection() { }
		/// <summary>
		/// Initializes a new instance of the RequestRepairCollection class.
		/// </summary>
		public RequestRepairCollection(IList<RequestRepair> initialList) : base(initialList) { }
	}
}