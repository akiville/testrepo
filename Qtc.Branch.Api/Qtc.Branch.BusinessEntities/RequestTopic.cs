using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RequestTopic : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mName { get; set; }
		public String mDescription { get; set; }
		public String mCategory { get; set; }
		public String mWelcomeMessage { get; set; }
		public Int32 mUserId { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}