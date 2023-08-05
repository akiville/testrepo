using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class MessengerStatus : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mMessengerDetailId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Boolean mIsSeen { get; set; }
		public Boolean mIsAcknowledge { get; set; }
		public Boolean mIsLiked { get; set; }
		public DateTime mDateSeen { get; set; }
		public DateTime mDateAcknowledge { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}