using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class MessengerGps : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mUserId { get; set; }
		public Decimal mLatitude { get; set; }
		public Decimal mLongitude { get; set; }
		public DateTime mDeviceDate { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}