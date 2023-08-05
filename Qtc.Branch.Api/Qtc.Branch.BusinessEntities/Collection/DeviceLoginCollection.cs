using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DeviceLoginCollection class is designed to work with lists of instances of DeviceLogin.
	/// </summary>
	public class DeviceLoginCollection : BusinessCollectionBase<DeviceLogin>
	{
		/// <summary>
		/// Initializes a new instance of the DeviceLoginCollection class.
		/// </summary>
		public DeviceLoginCollection() { }
		/// <summary>
		/// Initializes a new instance of the DeviceLoginCollection class.
		/// </summary>
		public DeviceLoginCollection(IList<DeviceLogin> initialList) : base(initialList) { }
	}
}