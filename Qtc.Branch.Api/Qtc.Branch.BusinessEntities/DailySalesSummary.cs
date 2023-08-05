using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class DailySalesSummary : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public DateTime mInventoryDate { get; set; }
		public Int32 mBranchId { get; set; }
		//public Int32 mUserId { get; set; }
		public String mCashExplanation { get; set; }
		public String mInventoryExplanation { get; set; }
		//public byte[] mSignature { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public String mStatus { get; set; }
        #endregion
    }
}