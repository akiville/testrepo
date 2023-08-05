using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RovingPlanScheduledActual : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public DateTime mDateCreated { get; set; }
		public Int32 mRpsId { get; set; }
		public Int32 mUserId { get; set; }
		public Int32 mRovingTaskId { get; set; }
		public Int32 mMcOnDutyId { get; set; }
		public DateTime mTimeIn { get; set; }
		public DateTime mTimeOut { get; set; }
		public String mLateExplain { get; set; }
		public Int32 mNovId { get; set; }
		public String mNovNo { get; set; }
		public Int32 mRfdId { get; set; }
		public String mRfdNo { get; set; }
		public Int32 mRpId { get; set; }
		public String mRpNo { get; set; }
		public Int32 mRmId { get; set; }
		public String mRmNo { get; set; }
		public Boolean mNoDeduction { get; set; }
		public Int32 mRecordId { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}