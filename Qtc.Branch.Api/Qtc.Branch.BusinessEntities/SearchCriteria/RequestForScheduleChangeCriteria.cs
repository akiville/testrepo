using System;

namespace Qtc.Branch.BusinessEntities
{
	public class RequestForScheduleChangeCriteria : RequestForScheduleChange
	{
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }
    }
}