using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RovingPlanScheduled : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRecordId { get; set; }
		public DateTime mDateCreated { get; set; }
		public Int32 mRovingRequestId { get; set; }
		public Int32 mRovingPlanId { get; set; }
		public Int32 mRovingPlanDatesId { get; set; }
		public Int32 mRovingPlanOicId { get; set; }
		public Int32 mRovingPlanScheduledId { get; set; }
		public Int32 mBranchAreaId { get; set; }
		public Int32 mBranchId { get; set; }
		public DateTime mStartDate { get; set; }
		public DateTime mEndDate { get; set; }
		public Boolean mVisited { get; set; }
		public String mVisitedRemarks { get; set; }
		public Boolean mPost { get; set; }
		public Boolean mAlreadyEgress { get; set; }
		public String mRemarks { get; set; }

        public String mBranchIdName { get; set; }
        public String mBackColor { get; set; }
        public String mForeColor { get; set; }
        public String mRovingPlanOicEmployeeIdName { get; set; }
        public Boolean mIsActual { get; set; }
        public String mActualReport { get; set; }
        public int mEmployeeId { get; set; }

        public int mRovingTaskId { get; set; }
        public String mRovingTaskName { get; set; }
        public String mAreaName { get; set; }
        public int mRovingPlanActualId { get; set; }

        public String mActualRemarks { get; set; }
        public DateTime mActualTimeIn { get; set; }
        public DateTime mActualTimeOut { get; set; }
        public String mLateExplain { get; set; }
        public int mMcOnDutyId { get; set; }
        public String mMcOnDuty { get; set; }

        #endregion
    }
}