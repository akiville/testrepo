using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RequestRepair : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRequestRepairId { get; set; }
		public Int32 mNumber { get; set; }
		public DateTime mDate { get; set; }
		public DateTime mDateIncident { get; set; }
		public Int32 mBranchId { get; set; }
		public Boolean mPlanned { get; set; }
		public Boolean mContractor { get; set; }
		public Int32 mRequestedById { get; set; }
		public Int32 mMcId { get; set; }
		public Int32 mApprovedById { get; set; }
		public Int32 mCodeId { get; set; }
		public Int32 mProductId { get; set; }
		public Int32 mProductSerialId { get; set; }
		public Int32 mReasonId { get; set; }
		public String mComplaint { get; set; }
		public String mRemarks { get; set; }
        public String mBranchName { get; set; }
        public String mProductName { get; set; }
        public String mRequestedBy { get; set; }
        public String mReasonName { get; set; }

        #endregion
    }
}