using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class ApiVersionChangelog : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mVersionId { get; set; }
		public String mChangelog { get; set; }
		public DateTime mDateCreated { get; set; }
		public String mStatus { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}