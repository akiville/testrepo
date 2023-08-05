using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The EmployeeAttendanceCollection class is designed to work with lists of instances of EmployeeAttendance.
	/// </summary>
	public class EmployeeAttendanceCollection : BusinessCollectionBase<EmployeeAttendance>
	{
		/// <summary>
		/// Initializes a new instance of the EmployeeAttendanceCollection class.
		/// </summary>
		public EmployeeAttendanceCollection() { }
		/// <summary>
		/// Initializes a new instance of the EmployeeAttendanceCollection class.
		/// </summary>
		public EmployeeAttendanceCollection(IList<EmployeeAttendance> initialList) : base(initialList) { }
	}
}