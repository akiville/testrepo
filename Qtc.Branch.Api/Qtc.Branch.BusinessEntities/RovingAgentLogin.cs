using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RovingAgentLogin : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRovingAgentId { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mRpsId { get; set; }
		public DateTime mTimeIn { get; set; }
		public String mTimeInLatitude { get; set; }
		public String mTimeInLongitude { get; set; }
		public String mTimeInAddress { get; set; }
		public String mTimeOut { get; set; }
		public String mTimeOutLatitude { get; set; }
		public String mTimeOutLongitude { get; set; }
		public String mTimeOutAddress { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}