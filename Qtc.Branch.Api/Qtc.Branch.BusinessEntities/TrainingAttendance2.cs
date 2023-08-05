using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class TrainingAttendance2 : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mSalesSchedulingId { get; set; }
		public Int32 mCutoffId { get; set; }
		public Int32 mBranch { get; set; }
		public String mLmm { get; set; }
		public String mType { get; set; }
		public String mBranchArea { get; set; }
		public String mAgency { get; set; }
		public Decimal mRate { get; set; }
		public Decimal mAmount { get; set; }
		public String mCurrentBranchId { get; set; }
		public String mIdExpired { get; set; }
		public DateTime mMondayDate { get; set; }
		public Int32 mMonday { get; set; }
		public Int32 mMondaySchedule { get; set; }
		public Boolean mMondayHasId { get; set; }
		public Boolean mMondayExpiredId { get; set; }
		public DateTime mTuesdayDate { get; set; }
		public Int32 mTuesday { get; set; }
		public Int32 mTuesdaySchedule { get; set; }
		public Boolean mTuesdayHasId { get; set; }
		public Boolean mTuesdayExpiredId { get; set; }
		public DateTime mWednesdayDate { get; set; }
		public Int32 mWednesday { get; set; }
		public Int32 mWednesdaySchedule { get; set; }
		public Boolean mWednesdayHasId { get; set; }
		public Boolean mWednesdayExpiredId { get; set; }
		public DateTime mThursdayDate { get; set; }
		public Int32 mThursday { get; set; }
		public Int32 mThursdaySchedule { get; set; }
		public Boolean mThursdayHasId { get; set; }
		public Boolean mThursdayExpiredId { get; set; }
		public DateTime mFridayDate { get; set; }
		public Int32 mFriday { get; set; }
		public Int32 mFridaySchedule { get; set; }
		public Boolean mFridayHasId { get; set; }
		public Boolean mFridayExpiredId { get; set; }
		public DateTime mSaturdayDate { get; set; }
		public Int32 mSaturday { get; set; }
		public Int32 mSaturdaySchedule { get; set; }
		public Boolean mSaturdayHasId { get; set; }
		public Boolean mSaturdayExpiredId { get; set; }
		public DateTime mSundayDate { get; set; }
		public Int32 mSunday { get; set; }
		public Int32 mSundaySchedule { get; set; }
		public Boolean mSundayHasId { get; set; }
		public Boolean mSundayExpiredId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Int32 mPositionScheduleId { get; set; }
		public String mTimeIn { get; set; }
		public String mBreakOut { get; set; }
		public String mBreakIn { get; set; }
		public String mTimeOut { get; set; }
		public Int32 mAgencyId { get; set; }
		public String mPosition { get; set; }
		public Int32 mPositionId { get; set; }
		public Int32 mBranchWorkable { get; set; }
		public Boolean mContinueToNextweek { get; set; }
		public Int32 mAssignTo { get; set; }
		public Boolean mAsIs { get; set; }
		public DateTime mStartDate { get; set; }
		public DateTime mEndDate { get; set; }
		public Boolean mIsConfirmed { get; set; }
		public Int32 mConfirmedBy { get; set; }
		public DateTime mConfirmationDate { get; set; }
		public String mConfirmationRemarks { get; set; }
		public String mRemarks { get; set; }
        public String mEmployee { get; set; }
        public String mMotherBranch { get; set; }
        public String mCellphoneNo { get; set; }
        public String mMon { get; set; }
        public String mTue { get; set; }
        public String mWed { get; set; }
        public String mThur { get; set; }
        public String mFri { get; set; }
        public String mSat { get; set; }
        public String mSun { get; set; }
        public String mMonFc { get; set; }
        public String mMonBc { get; set; }
        public String mTueFc { get; set; }
        public String mTueBc { get; set; }
        public String mWedFc { get; set; }
        public String mWedBc { get; set; }
        public String mThuFc { get; set; }
        public String mThuBc { get; set; }
        public String mFriFc { get; set; }
        public String mFriBc { get; set; }
        public String mSatFc { get; set; }
        public String mSatBc { get; set; }
        public String mSunFc { get; set; }
        public String mSunBc { get; set; }
        public DateTime mLastTrainingDate { get; set; }
        #endregion
    }
}