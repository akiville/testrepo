using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class SalesSchedule : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mScheduleId { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public DateTime mDate { get; set; }
		public Int32 mHasId { get; set; }
		public Int32 mExpiredId { get; set; }
		public Int32 mSchedule { get; set; }
		public String mTimeIn { get; set; }
		public String mBreakout { get; set; }
		public String mBreakin { get; set; }
		public String mTimeOut { get; set; }
		public String mRemarks { get; set; }
        public String mAttendanceStatus { get; set; }
        public String mAttendanceRemarks { get; set; }
        public String mName { get; set; }
        public String mBranch { get; set; }

        #endregion
    }
}