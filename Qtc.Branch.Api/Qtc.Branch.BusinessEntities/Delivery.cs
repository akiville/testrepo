using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Delivery : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public DateTime mDeliveryDate { get; set; }
		public String mPlannedBy { get; set; }
		public String mTrucking { get; set; }
		public String mDriver { get; set; }
		public String mCrew { get; set; }
		public String mBranch { get; set; }
		public Int32 mBranchId { get; set; }
		public String mDeliverySchedule { get; set; }
		public String mEta { get; set; }
		public String mCrewToDrop { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public Boolean mIsDelivered { get; set; }
        #endregion
    }
}