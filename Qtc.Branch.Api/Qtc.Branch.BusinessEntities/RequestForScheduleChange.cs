using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RequestForScheduleChange : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRequestForScheduleChangeId { get; set; }
		public Boolean mIsApproved { get; set; }
		public Boolean mIsCancelled { get; set; }
		public Boolean mIsPostponed { get; set; }
		public Int32 mRequestedById { get; set; }
		public Int32 mEmployeeId { get; set; }
		public String mAgency { get; set; }
		public String mBranch { get; set; }
		public String mPosition { get; set; }
		public String mArea { get; set; }
		public Int32 mLmmId { get; set; }
		public String mLmm { get; set; }
		public DateTime mStartDate { get; set; }
		public DateTime mEndDate { get; set; }
		public Int32 mReasonId { get; set; }
		public Int32 mPersonToRelieveId { get; set; }
		public String mRelieverBranch { get; set; }
		public Int32 mReplacementId { get; set; }
		public DateTime mPostponedTo { get; set; }
		public String mReconNumber { get; set; }
		public String mReason { get; set; }
		public String mExplanation { get; set; }
		public Int32 mChangeWith { get; set; }
		public Int32 mBranchFrom { get; set; }
		public Int32 mBranchTo { get; set; }
		public Boolean mIsPlanned { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}