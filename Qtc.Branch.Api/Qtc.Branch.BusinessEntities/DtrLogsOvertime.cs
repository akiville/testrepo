using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
    public class DtrLogsOvertime : BusinessBase
    {
        #region Public Properties
        public override Int32 mId { get; set; }
        public DateTime mDateCreated { get; set; }
        public Int32 mNumber { get; set; }
        public Int32 mEmployeeId { get; set; }
        public Int32 mShiftId { get; set; }
        public Int32 mFiledById { get; set; }
        public DateTime mOtDate { get; set; }
        public DateTime mOtStart { get; set; }
        public DateTime mOtEnd { get; set; }
        public Double mTotalHours { get; set; }
        public String mReason { get; set; }
        public String mExplanation { get; set; }
        public Boolean mApproved { get; set; }
        public Int32 mApprovedBy { get; set; }
        public DateTime mApprovedDate { get; set; }
        public Int32 mApprovedNumber { get; set; }
        public Boolean mCancelled { get; set; }
        public Int32 mCancelledBy { get; set; }
        public DateTime mCancelledDate { get; set; }
        public DateTime mDatePrint { get; set; }
        public Int32 mBranchOt { get; set; }
        public Int32 mVerifiedBy { get; set; }
        public String mVerifiedByRemarks { get; set; }
        public DateTime mVerifiedByDate { get; set; }
        public Int32 mRecordId { get; set; }
        public String mRemarks { get; set; }
        public String mBranchName { get; set; }
        public String mEmployeeName { get; set; }
        public String mFiledByName { get; set; }
        public Boolean mFromLocal { get; set; }
        public String mFiledByIdName { get; set; }
        public String mApprovedNumberComplete { get; set; }
        public String mPositionName { get; set; }
        #endregion
    }
}