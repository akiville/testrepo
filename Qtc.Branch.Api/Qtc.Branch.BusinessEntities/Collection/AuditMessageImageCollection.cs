using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The AuditMessageImageCollection class is designed to work with lists of instances of AuditMessageImage.
	/// </summary>
	public class AuditMessageImageCollection : BusinessCollectionBase<AuditMessageImage>
	{
		/// <summary>
		/// Initializes a new instance of the AuditMessageImageCollection class.
		/// </summary>
		public AuditMessageImageCollection() { }
		/// <summary>
		/// Initializes a new instance of the AuditMessageImageCollection class.
		/// </summary>
		public AuditMessageImageCollection(IList<AuditMessageImage> initialList) : base(initialList) { }
	}
}