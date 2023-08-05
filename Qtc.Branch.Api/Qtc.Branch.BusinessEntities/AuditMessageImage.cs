using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class AuditMessageImage : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mAuditMessageDetailId { get; set; }
		public String mImageLink { get; set; }
		public String mImageTitle { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}