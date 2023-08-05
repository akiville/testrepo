using System;

namespace Qtc.Branch.BusinessEntities
{
	public class LmmCashCountCriteria : LmmCashCount
	{
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }
        public int mLmmId { get; set; }
    }
}