using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class DeliverySchedule : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mLmmId { get; set; }
		public Int32 mDeliveryScheduleId { get; set; }
		public Int32 mBranchId { get; set; }
		public DateTime mEta { get; set; }
		public DateTime mDeliveryDate { get; set; }
		public DateTime mDeliveryTime { get; set; }
		public Int32 mRecordId { get; set; }
		public DateTime mDatetime { get; set; }
		public String mRemarks { get; set; }
        public String mBranchName { get; set; }
        public int mConcernId { get; set; }
        #endregion
    }
}