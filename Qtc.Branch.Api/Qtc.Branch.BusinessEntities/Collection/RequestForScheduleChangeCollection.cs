using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RequestForScheduleChangeCollection class is designed to work with lists of instances of RequestForScheduleChange.
	/// </summary>
	public class RequestForScheduleChangeCollection : BusinessCollectionBase<RequestForScheduleChange>
	{
		/// <summary>
		/// Initializes a new instance of the RequestForScheduleChangeCollection class.
		/// </summary>
		public RequestForScheduleChangeCollection() { }
		/// <summary>
		/// Initializes a new instance of the RequestForScheduleChangeCollection class.
		/// </summary>
		public RequestForScheduleChangeCollection(IList<RequestForScheduleChange> initialList) : base(initialList) { }
	}
}