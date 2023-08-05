using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class LmmAttendanceUpdate : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mSsuId { get; set; }
		public DateTime mMondayDate { get; set; }
		public Int32 mMondayScheduleId { get; set; }
		public DateTime mTuesdayDate { get; set; }
		public Int32 mTuesdayScheduleId { get; set; }
		public DateTime mWednesdayDate { get; set; }
		public Int32 mWednesdayScheduleId { get; set; }
		public DateTime mThursdayDate { get; set; }
		public Int32 mThursdayScheduleId { get; set; }
		public DateTime mFridayDate { get; set; }
		public Int32 mFridayScheduleId { get; set; }
		public DateTime mSaturdayDate { get; set; }
		public Int32 mSaturdayScheduleId { get; set; }
		public DateTime mSundayDate { get; set; }
		public Int32 mSundayScheduleId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Int32 mCutoffId { get; set; }
		public Int32 mLmmId { get; set; }
		public Int32 mRecordId { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}