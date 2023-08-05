using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Ibw : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mIbwId { get; set; }
		public DateTime mDate { get; set; }
		public DateTime mTransactionDate { get; set; }
		public Int32 mNumber { get; set; }
		public Int32 mPlannerId { get; set; }
		public Int32 mMcId { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mToBranchId { get; set; }
		public Int32 mRequestedById { get; set; }
		public Int32 mApprovedById { get; set; }
		public Int32 mReasonId { get; set; }
		public String mNovNo { get; set; }
		public Int32 mCodeId { get; set; }
		public String mRemarks { get; set; }
        public String mBranchName { get; set; }
        public String mToBranchName { get; set; }
        public Boolean mFromBranchAccepted { get; set; }
        #endregion
    }
}