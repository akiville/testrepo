using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The OdsCompanyCollection class is designed to work with lists of instances of OdsCompany.
	/// </summary>
	public class OdsCompanyCollection : BusinessCollectionBase<OdsCompany>
	{
		/// <summary>
		/// Initializes a new instance of the OdsCompanyCollection class.
		/// </summary>
		public OdsCompanyCollection() { }
		/// <summary>
		/// Initializes a new instance of the OdsCompanyCollection class.
		/// </summary>
		public OdsCompanyCollection(IList<OdsCompany> initialList) : base(initialList) { }
	}
}