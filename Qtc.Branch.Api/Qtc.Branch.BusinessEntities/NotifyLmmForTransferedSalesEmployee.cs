using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class NotifyLmmForTransferedSalesEmployee : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mLmmFromId { get; set; }
		public Int32 mLmmToId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public DateTime mDate { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mBranchIdTo { get; set; }
		public Int32 mRecordId { get; set; }
		public String mRemarks { get; set; }
        public String mLmmFrom { get; set; }
        public String mLmmTo { get; set; }
        public String mBranch { get; set; }
        public String mBranchTo { get; set; }
        public String mEmployeeName { get; set; }
        #endregion
    }
}