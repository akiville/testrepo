using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class BranchCash : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mBranchId { get; set; }
		public DateTime mSalesDate { get; set; }
		public Decimal mTotalAmount { get; set; }
		public Int32 mIsDeposited { get; set; }
		public String mCashExplanation { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Int32 mDepositedById { get; set; }
		public DateTime mDepositDate { get; set; }
		public Int32 mDepositValidated { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public String mDepositSlipImageName { get; set; }
        #endregion
    }
}