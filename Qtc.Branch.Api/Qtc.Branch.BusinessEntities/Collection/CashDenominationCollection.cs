using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The CashDenominationCollection class is designed to work with lists of instances of CashDenomination.
	/// </summary>
	public class CashDenominationCollection : BusinessCollectionBase<CashDenomination>
	{
		/// <summary>
		/// Initializes a new instance of the CashDenominationCollection class.
		/// </summary>
		public CashDenominationCollection() { }
		/// <summary>
		/// Initializes a new instance of the CashDenominationCollection class.
		/// </summary>
		public CashDenominationCollection(IList<CashDenomination> initialList) : base(initialList) { }
	}
}