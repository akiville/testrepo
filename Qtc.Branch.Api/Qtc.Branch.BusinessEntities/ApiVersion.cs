using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class ApiVersion : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mVersionCode { get; set; }
		public String mUrl { get; set; }
		public String mUpdateMessage { get; set; }
		public Int32 mUserId { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}