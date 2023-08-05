using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DeviceCollection class is designed to work with lists of instances of Device.
	/// </summary>
	public class DeviceCollection : BusinessCollectionBase<Device>
	{
		/// <summary>
		/// Initializes a new instance of the DeviceCollection class.
		/// </summary>
		public DeviceCollection() { }
		/// <summary>
		/// Initializes a new instance of the DeviceCollection class.
		/// </summary>
		public DeviceCollection(IList<Device> initialList) : base(initialList) { }
	}
}