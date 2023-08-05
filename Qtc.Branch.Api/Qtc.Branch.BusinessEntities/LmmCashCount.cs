using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class LmmCashCount : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mBranchId { get; set; }
		public DateTime mSalesDate { get; set; }
		public Double mCash { get; set; }
		public Double mGrabFood { get; set; }
		public Double mFoodpanda { get; set; }
		public Double mTotalCash { get; set; }
		public Int32 mUserId { get; set; }
		public String mRemarks { get; set; }
        public String mName { get; set; }
        public String mOnDuty { get; set; }
        public String mLmmName { get; set; }
        public DateTime mDepositDate { get; set; }
        public Boolean mIsDeposited { get; set; }
        public String mDepositRemarks { get; set; }
        public Boolean mIsClosed { get; set; }
        public Boolean mIsDeposit { get; set; }
        public Boolean mIsPickup { get; set; }
        public String mDepositTransactionNo { get; set; }
        public String mPickUpReceiptNo { get; set; }
        public DateTime mPickUpDate { get; set; }
        public int mWithEntry { get; set; }
        public int mTotalItem { get; set; }
        public int mLmmId { get; set; }
        public int mFoodPandaId { get; set; }
        public int mGrabFoodId { get; set; }
        public Double mMaya { get; set; }
        public int mMayaId { get; set; }
        #endregion
    }
}