using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class SalesSchedulingConfirmation : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public DateTime mStartDate { get; set; }
		public DateTime mEndDate { get; set; }
		public Int32 mLmmId { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public int mCutoffId { get; set; }
        #endregion
    }
}