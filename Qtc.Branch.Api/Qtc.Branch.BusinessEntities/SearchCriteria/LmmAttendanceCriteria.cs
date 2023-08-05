using System;

namespace Qtc.Branch.BusinessEntities
{
	public class LmmAttendanceCriteria : LmmAttendance
	{
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }
    }
}