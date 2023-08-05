using System;

namespace Qtc.Branch.BusinessEntities
{
	public class NotifyLmmForTransferedSalesEmployeeCriteria : NotifyLmmForTransferedSalesEmployee
	{
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }
    }
}