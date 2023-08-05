using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class DisseminatedLetter : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mHrLetterId { get; set; }
		public String mLmm { get; set; }
		public DateTime mDateCreated { get; set; }
		public String mAreaName { get; set; }
		public String mName { get; set; }
		public String mAgencyIdName { get; set; }
		public String mBranch { get; set; }
		public String mType { get; set; }
		public String mDuration { get; set; }
		public String mControlNo { get; set; }
		public DateTime mDateTrip { get; set; }
		public String mCourierName { get; set; }
		public Int32 mLmmId { get; set; }
        public Int32 mBranchId { get; set; }
        public DateTime mDurationFrom { get; set; }
        public DateTime mDurationTo { get; set; }
        public String mActualExpirationDate { get; set; }
		public String mResponseDate { get; set; }
		public String mRemarks { get; set; }
        public Int32 mUpdatedByLmm { get; set; }
        public DateTime mDateDisseminated { get; set; }
        public Int32 mRecordId { get; set; }
        public Int32 mDaysBeforeExpire { get; set; }
        public bool mIsExpired { get; set; }
        public Int32 mEmployeeId { get; set; }
        public String mRenewalRequestStatus { get; set; }
        public int mRenewalRequestId { get; set; }
        public Boolean mIsManual { get; set; }
        public int mBranchCode { get; set; }
        #endregion
    }
}