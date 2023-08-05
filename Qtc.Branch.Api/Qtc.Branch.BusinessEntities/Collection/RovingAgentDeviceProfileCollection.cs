using System.Collections.Generic;

namespace Qtc.Branch.BusinessEntities
{
	/// <summary>
	/// The RovingAgentDeviceProfileCollection class is designed to work with lists of instances of RovingAgentDeviceProfile.
	/// </summary>
	public class RovingAgentDeviceProfileCollection : BusinessCollectionBase<RovingAgentDeviceProfile>
	{
		/// <summary>
		/// Initializes a new instance of the RovingAgentDeviceProfileCollection class.
		/// </summary>
		public RovingAgentDeviceProfileCollection() { }
		/// <summary>
		/// Initializes a new instance of the RovingAgentDeviceProfileCollection class.
		/// </summary>
		public RovingAgentDeviceProfileCollection(IList<RovingAgentDeviceProfile> initialList) : base(initialList) { }
	}
}