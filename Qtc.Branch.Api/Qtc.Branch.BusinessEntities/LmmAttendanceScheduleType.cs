using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class LmmAttendanceScheduleType : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mName { get; set; }
		public Boolean mIsPresent { get; set; }
		public String mBackColor { get; set; }
		public String mForeColor { get; set; }
		public String mSmsCode { get; set; }
		public String mRemarks { get; set; }
        public int mForUrgentScheduleChanged { get; set; }
        public String mHexForeColor { get; set; }
        public String mHexBackColor { get; set; }

        #endregion
    }
}