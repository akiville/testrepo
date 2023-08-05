using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The TypeOfViolationCollection class is designed to work with lists of instances of TypeOfViolation.
	/// </summary>
	public class TypeOfViolationCollection : BusinessCollectionBase<TypeOfViolation>
	{
		/// <summary>
		/// Initializes a new instance of the TypeOfViolationCollection class.
		/// </summary>
		public TypeOfViolationCollection() { }
		/// <summary>
		/// Initializes a new instance of the TypeOfViolationCollection class.
		/// </summary>
		public TypeOfViolationCollection(IList<TypeOfViolation> initialList) : base(initialList) { }
	}
}