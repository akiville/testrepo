using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RovingSchedule : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRecordId { get; set; }
		public String mName { get; set; }
		public String mCode { get; set; }
		public String mBackColor { get; set; }
		public String mForeColor { get; set; }
		public Boolean mBlueSlipNo { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}