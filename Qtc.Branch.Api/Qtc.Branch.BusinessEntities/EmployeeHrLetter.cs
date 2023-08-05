using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class EmployeeHrLetter : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mTypeOfLetterId { get; set; }
		public String mControlNo { get; set; }
		public String mFormNo { get; set; }
		public Int32 mEmployeeId { get; set; }
		public DateTime mDateCreated { get; set; }
		public DateTime mDurationFrom { get; set; }
		public DateTime mDurationTo { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mBranchTo { get; set; }
		public Int32 mNoCopies { get; set; }
		public Boolean mCancelled { get; set; }
		public String mCancelledRemark { get; set; }
		public Boolean mApproved { get; set; }
		public DateTime mApprovedDate { get; set; }
		public Int32 mApprovedBy { get; set; }
		public Int32 mPrintNo { get; set; }
		public DateTime mPrintDate { get; set; }
		public DateTime mReleasedDate { get; set; }
		public Int32 mReleasedById { get; set; }
		public DateTime mReturnedDate { get; set; }
		public Int32 mReturnedTo { get; set; }
		public Int32 mRequestBy { get; set; }
		public Int32 mReleasedCopies { get; set; }
		public String mReleasedNo { get; set; }
		public Int32 mReleasedTo { get; set; }
		public DateTime mDurationFromActual { get; set; }
		public DateTime mDurationToActual { get; set; }
		public Int32 mIntroLetterId { get; set; }
		public Int32 mPrintReleaseTo { get; set; }
		public Boolean mRequestRtw { get; set; }
		public String mLmmName { get; set; }
		public String mOicName { get; set; }
		public String mReason { get; set; }
		public Boolean mWeekendLetter { get; set; }
		public Boolean mOpeningScheduled { get; set; }
		public DateTime mReleasedDateRequest { get; set; }
		public Int32 mEncodedById { get; set; }
		public Int32 mHrLetterCategoryId { get; set; }
		public Int32 mNotifyMcBag { get; set; }
		public Int32 mNotifyKey { get; set; }
		public String mCourierName { get; set; }
		public Int32 mOtherType { get; set; }
		public DateTime mDateGenerated { get; set; }
		public DateTime mCancelledDate { get; set; }
		public Boolean mFirstReliever { get; set; }
		public Boolean mNeedToRelease { get; set; }
		public String mRemarks { get; set; }
        public String mEmployeeName { get; set; }
        public String mBranchName { get; set; }
        public String mBranchToName { get; set; }
        public String mRequestedByName { get; set; }
        public String mTypeOfLetter { get; set; }

        public HrLetterHistoryMovementCollection mHrLetterHistoryMovementCollection { get; set; }
        #endregion
    }
}