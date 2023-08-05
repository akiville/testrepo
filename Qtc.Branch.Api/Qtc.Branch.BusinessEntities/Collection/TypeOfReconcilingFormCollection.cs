using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The TypeOfReconcilingFormCollection class is designed to work with lists of instances of TypeOfReconcilingForm.
	/// </summary>
	public class TypeOfReconcilingFormCollection : BusinessCollectionBase<TypeOfReconcilingForm>
	{
		/// <summary>
		/// Initializes a new instance of the TypeOfReconcilingFormCollection class.
		/// </summary>
		public TypeOfReconcilingFormCollection() { }
		/// <summary>
		/// Initializes a new instance of the TypeOfReconcilingFormCollection class.
		/// </summary>
		public TypeOfReconcilingFormCollection(IList<TypeOfReconcilingForm> initialList) : base(initialList) { }
	}
}