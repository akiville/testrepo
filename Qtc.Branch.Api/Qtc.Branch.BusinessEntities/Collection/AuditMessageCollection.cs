using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The AuditMessageCollection class is designed to work with lists of instances of AuditMessage.
	/// </summary>
	public class AuditMessageCollection : BusinessCollectionBase<AuditMessage>
	{
		/// <summary>
		/// Initializes a new instance of the AuditMessageCollection class.
		/// </summary>
		public AuditMessageCollection() { }
		/// <summary>
		/// Initializes a new instance of the AuditMessageCollection class.
		/// </summary>
		public AuditMessageCollection(IList<AuditMessage> initialList) : base(initialList) { }
	}
}