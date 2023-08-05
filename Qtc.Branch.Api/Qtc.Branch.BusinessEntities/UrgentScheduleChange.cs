using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class UrgentScheduleChange : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mLmmId { get; set; }
		public DateTime mDateFiled { get; set; }
		public DateTime mIncidentDate { get; set; }
		public Int32 mEmployeeId { get; set; }
		public String mConcern { get; set; }
		public Int32 mLmmAttendanceScheduleTypeId { get; set; }
		public String mExplanation { get; set; }
		public Int32 mReasonCodeId { get; set; }
		public Int32 mAffectedPersonnelId { get; set; }
		public Int32 mAffectedBranchId { get; set; }
		public DateTime mDatestamp { get; set; }
		public Int32 mUserId { get; set; }
		public String mRemarks { get; set; }
        public String mAction { get; set; }
        public String mStatus { get; set; }
        public String mConcernType { get; set; }
        public String mLmmName { get; set; }
        public String mToLmmName { get; set; }
        public String mAffectedPersonnelName { get; set; }
        public String mAffectedBranchName { get; set; }
        public String mPersonnelName { get; set; }
        public int mRecordId { get; set; }
        public int mBranchId { get; set; }
        public int mMessageId { get; set; }
        public String mMessageStatus { get; set; }
        public String mBorrowedPersonnelName { get; set; }
        public String mBranchName { get; set; }
        public int mToLmmId { get; set; }
        public int mBorrowedPersonnelId { get; set; }

        public UrgentScheduleChangeBranchCollection mUrgentScheduleChangeBranchCollection { get; set; }
        public UrgentScheduleChangeMessageCollection mUrgentScheduleChangeMessageCollection { get; set; }
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }

        #endregion
    }
}