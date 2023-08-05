using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The DeviceConfigurationCollection class is designed to work with lists of instances of DeviceConfiguration.
	/// </summary>
	public class DeviceConfigurationCollection : BusinessCollectionBase<DeviceConfiguration>
	{
		/// <summary>
		/// Initializes a new instance of the DeviceConfigurationCollection class.
		/// </summary>
		public DeviceConfigurationCollection() { }
		/// <summary>
		/// Initializes a new instance of the DeviceConfigurationCollection class.
		/// </summary>
		public DeviceConfigurationCollection(IList<DeviceConfiguration> initialList) : base(initialList) { }
	}
}