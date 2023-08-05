using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RovingPlanScheduledActualChecklist : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRecordId { get; set; }
		public DateTime mDateCreated { get; set; }
		public Int32 mRpsId { get; set; }
		public Int32 mRcId { get; set; }
		public Boolean mIsCheck { get; set; }
		public Boolean mLostSales { get; set; }
		public Boolean mWithViolation { get; set; }
		public String mActionTaken { get; set; }
		public Int32 mRpsaId { get; set; }
		public String mRemarks { get; set; }
        public Int32 mPoints { get; set; }
        public String mCategory { get; set; }
        public String mDescription { get; set; }
        public String mChecklist { get; set; }
        #endregion
    }
}