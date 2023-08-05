using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The AuditorLoginCollection class is designed to work with lists of instances of AuditorLogin.
	/// </summary>
	public class AuditorLoginCollection : BusinessCollectionBase<AuditorLogin>
	{
		/// <summary>
		/// Initializes a new instance of the AuditorLoginCollection class.
		/// </summary>
		public AuditorLoginCollection() { }
		/// <summary>
		/// Initializes a new instance of the AuditorLoginCollection class.
		/// </summary>
		public AuditorLoginCollection(IList<AuditorLogin> initialList) : base(initialList) { }
	}
}