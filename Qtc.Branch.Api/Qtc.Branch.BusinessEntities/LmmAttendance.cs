using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class LmmAttendance : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mLmmId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Int32 mCutoffId { get; set; }
		public Int32 mAttendanceTypeId { get; set; }
		public Int32 mBranchId { get; set; }
		public DateTime mAttendanceDate { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public String mAttendanceTypeName { get; set; }
        public String mEmployeeName { get; set; }
        public String mAgencyName { get; set; }
        public String mBranchName { get; set; }
        public Int32 mCode { get; set; }
        public String mLmmName { get; set; }
        public int mWithEntry { get; set; }
        public int mTotalItem { get; set; }
        #endregion
    }
}