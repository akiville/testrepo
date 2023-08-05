using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Loe : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mLoeId { get; set; }
		public DateTime mSaleDate { get; set; }
		public Int32 mNumber { get; set; }
		public Int16 mAuditNumber { get; set; }
		public Int16 mRflNo { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mType { get; set; }
		public Int32 mMcId { get; set; }
		public Int32 mRequestedById { get; set; }
		public Int32 mTransactedById { get; set; }
		public Int32 mApprovedById { get; set; }
		public Int32 mCodeId { get; set; }
		public Int32 mAuditedById { get; set; }
        public String mRemarks { get; set; }
        public String mComment { get; set; }
		public Boolean mDisApproved { get; set; }
		public String mDisApprovedReason { get; set; }
		public Int32 mWitnessNo { get; set; }
		public String mWitnessName { get; set; }
		public Double mAmount { get; set; }
		public String mLoeNoMcBag { get; set; }
		public Int32 mReasonId { get; set; }
		public String mReconcillingFormNo { get; set; }
		public Boolean mAloe { get; set; }
		public Boolean mXaloe { get; set; }
		public Boolean mReject { get; set; }
		public Byte mGuestCount { get; set; }
        public String mRequestendByName { get; set; }
        #endregion
    }
}