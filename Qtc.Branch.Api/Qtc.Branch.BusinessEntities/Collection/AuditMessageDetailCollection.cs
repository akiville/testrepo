using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The AuditMessageDetailCollection class is designed to work with lists of instances of AuditMessageDetail.
	/// </summary>
	public class AuditMessageDetailCollection : BusinessCollectionBase<AuditMessageDetail>
	{
		/// <summary>
		/// Initializes a new instance of the AuditMessageDetailCollection class.
		/// </summary>
		public AuditMessageDetailCollection() { }
		/// <summary>
		/// Initializes a new instance of the AuditMessageDetailCollection class.
		/// </summary>
		public AuditMessageDetailCollection(IList<AuditMessageDetail> initialList) : base(initialList) { }
	}
}