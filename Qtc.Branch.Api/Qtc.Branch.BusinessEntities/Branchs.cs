using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Branchs : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mBranchId { get; set; }
		public String mCode { get; set; }
		public String mName { get; set; }
		public String mCode2 { get; set; }
		public String mClassification { get; set; }
		public String mCompany { get; set; }
		public String mTin { get; set; }
		public Int32 mAreaId { get; set; }
		public String mAddress { get; set; }
		public Int32 mMcId { get; set; }
		public String mBank { get; set; }
		public String mContactNo { get; set; }
		public String mStoreHourStart { get; set; }
		public String mStoreHourEnd { get; set; }
		public DateTime mDeliveryTime { get; set; }
		public Int32 mSunday { get; set; }
		public Int32 mMonday { get; set; }
		public Int32 mTuesday { get; set; }
		public Int32 mWednesday { get; set; }
		public Int32 mThursday { get; set; }
		public Int32 mFriday { get; set; }
		public Int32 mSaturday { get; set; }
		public Int32 mBankId { get; set; }
		public Int32 mRpAssistantId { get; set; }
		public String mRemarks { get; set; }
        public Int32 mIsLoggedIn { get; set; }
        public Int32 mMclId { get; set; }
        public Int32 mPmisBranchId { get; set; }
        public Int32 mDeliveryId { get; set; }
        public String mEta { get; set; }
        public float mCash { get; set; }
        public float mGrabFood { get; set; }
        public float mFoodPanda { get; set; }
        public float mTotalCash { get; set; }
        public int mLmmCashId { get; set; }
        public String mLmmCashRemarks { get; set; }
        public int mLmmId { get; set; }
        #endregion
    }
}