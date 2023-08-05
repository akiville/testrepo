using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class TraineeAttendance : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mLmmId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Int32 mBranchId { get; set; }
		public DateTime mMondayDate { get; set; }
		public String mMondayStatus { get; set; }
		public DateTime mTuesdayDate { get; set; }
		public String mTuesdayStatus { get; set; }
		public DateTime mWednesdayDate { get; set; }
		public String mWednesdayStatus { get; set; }
		public DateTime mThursdayDate { get; set; }
		public String mThursdayStatus { get; set; }
		public DateTime mFridayDate { get; set; }
		public String mFridayStatus { get; set; }
		public DateTime mSaturdayDate { get; set; }
		public String mSaturdayStatus { get; set; }
		public DateTime mSundayDate { get; set; }
		public String mSundayStatus { get; set; }
		public Int32 mSalesScheduleId { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public String mEmployeeName { get; set; }
        public String mBranchName { get; set; }
        public String mAgency { get; set; }
    
        public String mLmmName { get; set; }

        #endregion
    }
}