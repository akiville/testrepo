using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class BranchCashDetails : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public DateTime mSalesDate { get; set; }
		public Int32 mDenominationId { get; set; }
		public Int32 mQty { get; set; }
		public Decimal mTotalAmount { get; set; }
		public String mCashExplanation { get; set; }
		public Int32 mBranchCashId { get; set; }
		public Int32 mUserId { get; set; }
		public DateTime mUploadDate { get; set; }
		public String mRemarks { get; set; }
        public String  mName { get; set; }
        #endregion
    }
}