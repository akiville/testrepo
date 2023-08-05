using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class SalesSchedulingConcern : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mSalesSchedulingId { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mLmmId { get; set; }
		public String mConcern { get; set; }
		public String mStatus { get; set; }
		public DateTime mDateSubmitted { get; set; }
		public Int32 mCheckedBy { get; set; }
		public DateTime mCheckedDate { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public String mLmmName { get; set; }
        public String mBranchName { get; set; }
        public Int32 mCutoffId { get; set; }
        public DateTime mStartDate { get; set; }
        public DateTime mEndDate { get; set; }
        public String mConcernDate { get; set; }
        public String mEmployeeName { get; set; }
        #endregion
    }
}