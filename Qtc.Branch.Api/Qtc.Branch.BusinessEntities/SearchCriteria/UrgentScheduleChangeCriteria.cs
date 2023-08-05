using System;

namespace Qtc.Branch.BusinessEntities
{
	public class UrgentScheduleChangeCriteria : UrgentScheduleChange
	{
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }
        public int mToLmmId { get; set; }
    }
}