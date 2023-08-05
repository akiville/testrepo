using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class AuditMessage : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mMessage { get; set; }
		public DateTime mDateCreated { get; set; }
		public Int32 mUserId { get; set; }
		public String mStatus { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}