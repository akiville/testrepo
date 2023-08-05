using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The EmployeeStatusEmploymentCollection class is designed to work with lists of instances of EmployeeStatusEmployment.
	/// </summary>
	public class EmployeeStatusEmploymentCollection : BusinessCollectionBase<EmployeeStatusEmployment>
	{
		/// <summary>
		/// Initializes a new instance of the EmployeeStatusEmploymentCollection class.
		/// </summary>
		public EmployeeStatusEmploymentCollection() { }
		/// <summary>
		/// Initializes a new instance of the EmployeeStatusEmploymentCollection class.
		/// </summary>
		public EmployeeStatusEmploymentCollection(IList<EmployeeStatusEmployment> initialList) : base(initialList) { }
	}
}