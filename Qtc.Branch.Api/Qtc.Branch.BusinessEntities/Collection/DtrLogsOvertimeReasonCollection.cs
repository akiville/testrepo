using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DtrLogsOvertimeReasonCollection class is designed to work with lists of instances of DtrLogsOvertimeReason.
	/// </summary>
	public class DtrLogsOvertimeReasonCollection : BusinessCollectionBase<DtrLogsOvertimeReason>
	{
		/// <summary>
		/// Initializes a new instance of the DtrLogsOvertimeReasonCollection class.
		/// </summary>
		public DtrLogsOvertimeReasonCollection() { }
		/// <summary>
		/// Initializes a new instance of the DtrLogsOvertimeReasonCollection class.
		/// </summary>
		public DtrLogsOvertimeReasonCollection(IList<DtrLogsOvertimeReason> initialList) : base(initialList) { }
	}
}