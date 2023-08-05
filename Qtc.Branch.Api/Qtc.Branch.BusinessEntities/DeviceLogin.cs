using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class DeviceLogin : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public String mDeviceSerial { get; set; }
		public DateTime mLoginDate { get; set; }
		public String mStatus { get; set; }
        public String mDeviceLocation { get; set; }
        public String mRemarks { get; set; }
        public String mEmployeeName { get; set; }
        public Decimal mLatitude { get; set; }
        public Decimal mLongitude { get; set; }
        #endregion
    }
}