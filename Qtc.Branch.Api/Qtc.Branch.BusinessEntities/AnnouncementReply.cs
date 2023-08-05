using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class AnnouncementReply : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mAnnouncementId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Boolean mIsAcknowledge { get; set; }
		public DateTime mAknowledgementDate { get; set; }
		public DateTime mDatetime { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}