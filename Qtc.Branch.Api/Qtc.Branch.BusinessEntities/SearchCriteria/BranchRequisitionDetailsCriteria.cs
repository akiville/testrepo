using System;

namespace Qtc.Branch.BusinessEntities
{
	public class BranchRequisitionDetailsCriteria : BranchRequisitionDetails
	{
        public int mBranchId { get; set; }
        public DateTime mSalesDate { get; set; }
    }
}