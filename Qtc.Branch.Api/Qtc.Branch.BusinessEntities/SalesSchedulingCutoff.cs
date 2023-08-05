using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class SalesSchedulingCutoff : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public DateTime mStartDate { get; set; }
		public DateTime mEndDate { get; set; }
		public Boolean mIsFinal { get; set; }
		public Int32 mMirroredCutoffId { get; set; }
		public String mRemarks { get; set; }
        public int mCutOffId { get; set; }
        #endregion
    }
}