using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class UrgentScheduleChangeBranch : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mUrgentScheduleChangeId { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mUserId { get; set; }
		public String mRemarks { get; set; }
        public String mName { get; set; }
        #endregion
    }
}