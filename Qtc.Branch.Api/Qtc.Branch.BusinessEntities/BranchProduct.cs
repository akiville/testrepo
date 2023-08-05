using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class BranchProduct : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mProductId { get; set; }
		public Int32 mUserId { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public Boolean mAllowIbw { get; set; }
        #endregion
    }
}