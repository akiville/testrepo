using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RovingPlanScheduledActualChecklistLSV : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRecordId { get; set; }
		public Int32 mRpsId { get; set; }
		public Int32 mRpsChklistId { get; set; }
		public Int32 mViolationId { get; set; }
		public Boolean mWithViolation { get; set; }
		public Boolean mLostSales { get; set; }
		public String mExplanation { get; set; }
		public String mRemarks { get; set; }
        public String mName { get; set; }

        public RovingCheckListViolationDetailCollection mRovingCheckListViolationDetailCollection { get; set; }
        #endregion
    }
}