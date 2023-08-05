using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The NotifyLmmForTransferedSalesEmployeeCollection class is designed to work with lists of instances of NotifyLmmForTransferedSalesEmployee.
	/// </summary>
	public class NotifyLmmForTransferedSalesEmployeeCollection : BusinessCollectionBase<NotifyLmmForTransferedSalesEmployee>
	{
		/// <summary>
		/// Initializes a new instance of the NotifyLmmForTransferedSalesEmployeeCollection class.
		/// </summary>
		public NotifyLmmForTransferedSalesEmployeeCollection() { }
		/// <summary>
		/// Initializes a new instance of the NotifyLmmForTransferedSalesEmployeeCollection class.
		/// </summary>
		public NotifyLmmForTransferedSalesEmployeeCollection(IList<NotifyLmmForTransferedSalesEmployee> initialList) : base(initialList) { }
	}
}