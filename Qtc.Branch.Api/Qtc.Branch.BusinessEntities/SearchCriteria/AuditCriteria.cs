using System;
namespace Qtc.Branch.BusinessEntities
{
    public class AuditCriteria
    {
        public string mRowIds { get; set; }

        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }
    }
}
