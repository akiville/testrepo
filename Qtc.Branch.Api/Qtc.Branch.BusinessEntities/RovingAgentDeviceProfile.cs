using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RovingAgentDeviceProfile : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mDeviceId { get; set; }
		public Boolean mKioskMode { get; set; }
		public Boolean mIsLocked { get; set; }
		public String mMobileNo { get; set; }
		public Int32 mUserId { get; set; }
		public String mLastKnowLocation { get; set; }
		public String mRemarks { get; set; }
        public String mEmployeeName { get; set; }
        public String mAgency { get; set; }
        public String mLatitude { get; set; }
        public String mLongitude { get; set; }
        public DateTime mDeviceDate { get; set; }
        public DateTime mDatestamp { get; set; }
        #endregion
    }
}