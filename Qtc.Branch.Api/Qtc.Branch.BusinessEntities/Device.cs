using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Device : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mDeviceSerialNo { get; set; }
		public String mStatus { get; set; }
		public String mLastCoordinates { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public int mEmployeeId { get; set; }
        public DateTime mLastLogDate { get; set; }
        public String mEmployeeName { get; set; }
        public String mDeviceNotice { get; set; }
        public Boolean mIsLocked { get; set; }
        public Boolean mIsKiosk { get; set; }
        public String mWelcomeMessage { get; set; }
        #endregion
    }
}