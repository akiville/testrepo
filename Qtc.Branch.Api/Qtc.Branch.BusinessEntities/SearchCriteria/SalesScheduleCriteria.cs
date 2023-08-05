using System;

namespace Qtc.Branch.BusinessEntities
{
	public class SalesScheduleCriteria : SalesSchedule
	{
        public int mLmmId { get; set; }
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }

    }
}