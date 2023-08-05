using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DtrLogsOvertimeCollection class is designed to work with lists of instances of DtrLogsOvertime.
	/// </summary>
	public class DtrLogsOvertimeCollection : BusinessCollectionBase<DtrLogsOvertime>
	{
		/// <summary>
		/// Initializes a new instance of the DtrLogsOvertimeCollection class.
		/// </summary>
		public DtrLogsOvertimeCollection() { }
		/// <summary>
		/// Initializes a new instance of the DtrLogsOvertimeCollection class.
		/// </summary>
		public DtrLogsOvertimeCollection(IList<DtrLogsOvertime> initialList) : base(initialList) { }
	}
}