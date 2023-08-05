using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class UrgentScheduleChangeMessage : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mUrgentScheduleChangeId { get; set; }
		public Int32 mLmmId { get; set; }
		public Int32 mBranchId { get; set; }
		public DateTime mStartDate { get; set; }
		public DateTime mEndDate { get; set; }
		public Int32 mToLmmId { get; set; }
		public String mStatus { get; set; }
		public Int32 mPersonnelId { get; set; }
		public String mPersonnelName { get; set; }
		public DateTime mDatetime { get; set; }
		public String mRemarks { get; set; }
        public String mToLmmName { get; set; }
        public String mLmmName { get; set; }
        public int mAffectedBranchId { get; set; }
        public String mAffectedBranchName { get; set; }
        public String mBranchName { get; set; }
        public DateTime mDateFiled { get; set; }
        public String mConcernType { get; set; }
        #endregion
    }
}