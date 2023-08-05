using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class AddBack : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mProductAvailId { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mPersonnelId { get; set; }
		public Int32 mProductId { get; set; }
		public DateTime mSalesDate { get; set; }
		public Int32 mAddBackQty { get; set; }
		public String mAddBackReason { get; set; }
		public String mAddBackStatus { get; set; }
		public Int32 mPriorQty { get; set; }
		public Int32 mAvailQty { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public String mBranch { get; set; }
        public String mProduct { get; set; }
        public String mPersonnel { get; set; }
        public int mApprovedById { get; set; }
        public String mApprovalRemarks { get; set; }
        public DateTime mApprovalDate { get; set;  }
		#endregion
	}
}