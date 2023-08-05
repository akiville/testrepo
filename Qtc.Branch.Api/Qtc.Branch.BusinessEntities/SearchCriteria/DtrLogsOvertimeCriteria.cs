using System;

namespace Qtc.Branch.BusinessEntities
{
	public class DtrLogsOvertimeCriteria : DtrLogsOvertime
	{
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }
        public Boolean mIsSales { get; set; }
        public int mLmmId { get; set; }
    }
}