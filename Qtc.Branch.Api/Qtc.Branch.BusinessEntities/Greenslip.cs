using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Greenslip : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public DateTime mDate { get; set; }
		public DateTime mDateRequested { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mRequestedById { get; set; }
		public String mType { get; set; }
		public String mType2 { get; set; }
		public Int32 mGreenslipRequestId { get; set; }
		public String mExplanation { get; set; }
		public String mApprovedBy { get; set; }
		public Int32 mNumber { get; set; }
		public Boolean mDenied { get; set; }
		public Boolean mCancelled { get; set; }
		public DateTime mDeliveryDate { get; set; }
		public Boolean mIsDownloaded { get; set; }
		public DateTime mAppDownloadDate { get; set; }
		public String mRemarks { get; set; }
        public Boolean mTakenAction { get; set; }
        #endregion
    }
}