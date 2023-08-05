using System;

namespace Qtc.Branch.BusinessEntities
{
	public class RequestRepairCriteria : RequestRepair
	{
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }
    }
}