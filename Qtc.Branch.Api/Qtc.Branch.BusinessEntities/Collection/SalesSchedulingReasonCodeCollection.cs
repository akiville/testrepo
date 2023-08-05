using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The SalesSchedulingReasonCodeCollection class is designed to work with lists of instances of SalesSchedulingReasonCode.
	/// </summary>
	public class SalesSchedulingReasonCodeCollection : BusinessCollectionBase<SalesSchedulingReasonCode>
	{
		/// <summary>
		/// Initializes a new instance of the SalesSchedulingReasonCodeCollection class.
		/// </summary>
		public SalesSchedulingReasonCodeCollection() { }
		/// <summary>
		/// Initializes a new instance of the SalesSchedulingReasonCodeCollection class.
		/// </summary>
		public SalesSchedulingReasonCodeCollection(IList<SalesSchedulingReasonCode> initialList) : base(initialList) { }
	}
}