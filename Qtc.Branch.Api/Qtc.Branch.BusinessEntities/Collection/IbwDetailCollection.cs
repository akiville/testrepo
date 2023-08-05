using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The IbwDetailCollection class is designed to work with lists of instances of IbwDetail.
	/// </summary>
	public class IbwDetailCollection : BusinessCollectionBase<IbwDetail>
	{
		/// <summary>
		/// Initializes a new instance of the IbwDetailCollection class.
		/// </summary>
		public IbwDetailCollection() { }
		/// <summary>
		/// Initializes a new instance of the IbwDetailCollection class.
		/// </summary>
		public IbwDetailCollection(IList<IbwDetail> initialList) : base(initialList) { }
	}
}