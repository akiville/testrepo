using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The OperationReportCollection class is designed to work with lists of instances of OperationReport.
	/// </summary>
	public class OperationReportCollection : BusinessCollectionBase<OperationReport>
	{
		/// <summary>
		/// Initializes a new instance of the OperationReportCollection class.
		/// </summary>
		public OperationReportCollection() { }
		/// <summary>
		/// Initializes a new instance of the OperationReportCollection class.
		/// </summary>
		public OperationReportCollection(IList<OperationReport> initialList) : base(initialList) { }
	}
}