using System;

namespace Qtc.Branch.BusinessEntities
{
	public class DeviceLoginCriteria : DeviceLogin
	{
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }
    }
}