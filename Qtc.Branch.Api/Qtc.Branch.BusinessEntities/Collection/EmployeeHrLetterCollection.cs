using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The EmployeeHrLetterCollection class is designed to work with lists of instances of EmployeeHrLetter.
	/// </summary>
	public class EmployeeHrLetterCollection : BusinessCollectionBase<EmployeeHrLetter>
	{
		/// <summary>
		/// Initializes a new instance of the EmployeeHrLetterCollection class.
		/// </summary>
		public EmployeeHrLetterCollection() { }
		/// <summary>
		/// Initializes a new instance of the EmployeeHrLetterCollection class.
		/// </summary>
		public EmployeeHrLetterCollection(IList<EmployeeHrLetter> initialList) : base(initialList) { }
	}
}