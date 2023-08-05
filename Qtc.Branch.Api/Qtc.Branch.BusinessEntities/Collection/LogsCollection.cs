using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The LogsCollection class is designed to work with lists of instances of Logs.
	/// </summary>
	public class LogsCollection : BusinessCollectionBase<Logs>
	{
		/// <summary>
		/// Initializes a new instance of the LogsCollection class.
		/// </summary>
		public LogsCollection() { }
		/// <summary>
		/// Initializes a new instance of the LogsCollection class.
		/// </summary>
		public LogsCollection(IList<Logs> initialList) : base(initialList) { }
	}
}