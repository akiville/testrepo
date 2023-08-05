using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The IbwCollection class is designed to work with lists of instances of Ibw.
	/// </summary>
	public class IbwCollection : BusinessCollectionBase<Ibw>
	{
		/// <summary>
		/// Initializes a new instance of the IbwCollection class.
		/// </summary>
		public IbwCollection() { }
		/// <summary>
		/// Initializes a new instance of the IbwCollection class.
		/// </summary>
		public IbwCollection(IList<Ibw> initialList) : base(initialList) { }
	}
}