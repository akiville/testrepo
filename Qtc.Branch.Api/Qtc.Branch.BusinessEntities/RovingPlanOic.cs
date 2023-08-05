using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RovingPlanOic : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRecordId { get; set; }
		public Int32 mRovingPlanId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public String mRemarks { get; set; }

        public Boolean mDisable { get; set; }
        #endregion
    }
}