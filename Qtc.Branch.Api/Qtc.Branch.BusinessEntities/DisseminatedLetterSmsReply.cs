using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class DisseminatedLetterSmsReply : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mHrLetterId { get; set; }
		public DateTime mActualExpirationDate { get; set; }
		public DateTime mResponseDate { get; set; }
		public Int32 mLmmId { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}