using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Rfsc : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public DateTime mDateFiled { get; set; }
		public DateTime mCutoffDate { get; set; }
		public DateTime mDateRequestedFrom { get; set; }
		public DateTime mDateRequestedTo { get; set; }
		public Int32 mEncoderId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Int32 mTypeId { get; set; }
		public String mReason { get; set; }
		public String mExplanation { get; set; }
		public Int32 mChangeWith { get; set; }
		public Int32 mBranchFrom { get; set; }
		public Int32 mBranchTo { get; set; }
		public Boolean mIsPlanned { get; set; }
		public Boolean mIsApproved { get; set; }
		public String mReconNumber { get; set; }
		public Boolean mIsCancelled { get; set; }
		public Boolean mIsExecuted { get; set; }
		public Boolean mIsPrinted { get; set; }
		public Boolean mIsAcknowledge { get; set; }
		public Int32 mAcknowledgeByUserId { get; set; }
		public String mRemarks { get; set; }
        public String mRequestedBy { get; set; }
        public String mEmployeeName { get; set; }
        public String mFromBranchName { get; set; }
        public String mToBranchName { get; set; }
        public String mChangeWithName { get; set; }
        public String mStatus { get; set; }
        public String mAcknowledgeBy { get; set; }
        public String mType { get; set; }
        public int mRecordId { get; set; }
        #endregion
    }
}