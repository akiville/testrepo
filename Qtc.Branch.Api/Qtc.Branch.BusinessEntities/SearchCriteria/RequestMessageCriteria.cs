using System;

namespace Qtc.Branch.BusinessEntities
{
	public class RequestMessageCriteria : RequestMessage
	{
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }
    }
}