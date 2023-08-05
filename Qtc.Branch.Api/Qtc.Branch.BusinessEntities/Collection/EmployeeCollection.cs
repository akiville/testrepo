using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The EmployeeCollection class is designed to work with lists of instances of Employee.
	/// </summary>
	public class EmployeeCollection : BusinessCollectionBase<Employee>
	{
		/// <summary>
		/// Initializes a new instance of the EmployeeCollection class.
		/// </summary>
		public EmployeeCollection() { }
		/// <summary>
		/// Initializes a new instance of the EmployeeCollection class.
		/// </summary>
		public EmployeeCollection(IList<Employee> initialList) : base(initialList) { }
	}
}