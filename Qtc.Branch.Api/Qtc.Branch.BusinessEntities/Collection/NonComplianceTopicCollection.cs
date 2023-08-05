using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The NonComplianceTopicCollection class is designed to work with lists of instances of NonComplianceTopic.
	/// </summary>
	public class NonComplianceTopicCollection : BusinessCollectionBase<NonComplianceTopic>
	{
		/// <summary>
		/// Initializes a new instance of the NonComplianceTopicCollection class.
		/// </summary>
		public NonComplianceTopicCollection() { }
		/// <summary>
		/// Initializes a new instance of the NonComplianceTopicCollection class.
		/// </summary>
		public NonComplianceTopicCollection(IList<NonComplianceTopic> initialList) : base(initialList) { }
	}
}