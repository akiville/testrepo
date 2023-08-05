using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The SalesSchedulingConfirmationCollection class is designed to work with lists of instances of SalesSchedulingConfirmation.
	/// </summary>
	public class SalesSchedulingConfirmationCollection : BusinessCollectionBase<SalesSchedulingConfirmation>
	{
		/// <summary>
		/// Initializes a new instance of the SalesSchedulingConfirmationCollection class.
		/// </summary>
		public SalesSchedulingConfirmationCollection() { }
		/// <summary>
		/// Initializes a new instance of the SalesSchedulingConfirmationCollection class.
		/// </summary>
		public SalesSchedulingConfirmationCollection(IList<SalesSchedulingConfirmation> initialList) : base(initialList) { }
	}
}