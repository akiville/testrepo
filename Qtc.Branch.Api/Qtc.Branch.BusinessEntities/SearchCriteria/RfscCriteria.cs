using System;

namespace Qtc.Branch.BusinessEntities
{
	public class RfscCriteria : Rfsc
	{
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }
        public String mIsApprovedValue { get; set; }
        public String mIsCancelledValue { get; set; }
        public String mIsAcknowledgeValue { get; set; }
        public int mStatusId { get; set; }
    }
}