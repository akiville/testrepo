using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RepairDetailCollection class is designed to work with lists of instances of RepairDetail.
	/// </summary>
	public class RepairDetailCollection : BusinessCollectionBase<RepairDetail>
	{
		/// <summary>
		/// Initializes a new instance of the RepairDetailCollection class.
		/// </summary>
		public RepairDetailCollection() { }
		/// <summary>
		/// Initializes a new instance of the RepairDetailCollection class.
		/// </summary>
		public RepairDetailCollection(IList<RepairDetail> initialList) : base(initialList) { }
	}
}