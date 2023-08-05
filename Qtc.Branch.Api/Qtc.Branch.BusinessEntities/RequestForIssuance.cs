using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RequestForIssuance : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mIssuanceId { get; set; }
		public DateTime mDateRequested { get; set; }
		public DateTime mDateNeeded { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mNumber { get; set; }
		public Int32 mRequestedById { get; set; }
		public Int32 mProcessedById { get; set; }
		public Int32 mDepartmentId { get; set; }
		public Int32 mPurposeId { get; set; }
		public String mExplanation { get; set; }
		public Int32 mProductGroupId { get; set; }
		public Boolean mPlanned { get; set; }
		public Boolean mCancelled { get; set; }
		public String mCancelledReason { get; set; }
		public Int32 mIssuedToId { get; set; }
		public Int32 mRequestForRepairId { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public Boolean mIsHoDownloaded { get; set; }
        #endregion
    }
}