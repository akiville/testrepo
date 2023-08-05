using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Announcement : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mMessage { get; set; }
		public String mHeader { get; set; }
		public String mFooter { get; set; }
		public DateTime mDateSent { get; set; }
		public Int32 mUserId { get; set; }
		public DateTime mDatetime { get; set; }
		public Boolean mIsPosted { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}