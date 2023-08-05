using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RepairImageCollection class is designed to work with lists of instances of RepairImage.
	/// </summary>
	public class RepairImageCollection : BusinessCollectionBase<RepairImage>
	{
		/// <summary>
		/// Initializes a new instance of the RepairImageCollection class.
		/// </summary>
		public RepairImageCollection() { }
		/// <summary>
		/// Initializes a new instance of the RepairImageCollection class.
		/// </summary>
		public RepairImageCollection(IList<RepairImage> initialList) : base(initialList) { }
	}
}