using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RepairDetail : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRequestRepairId { get; set; }
		public DateTime mTimeOfCall { get; set; }
		public DateTime mDateNeeded { get; set; }
		public Int32 mPersonWhoCallId { get; set; }
		public Int32 mSecurityId { get; set; }
		public Int32 mTechnicianId { get; set; }
		public Int32 mRpAssistantId { get; set; }
		public Int32 mContractorId { get; set; }
		public String mEmergencyLetterNo { get; set; }
		public Boolean mRequestLetter { get; set; }
		public String mRequestLetterStaff { get; set; }
		public String mRequestLetterDetails { get; set; }
		public DateTime mRequestLetterDate { get; set; }
		public Int32 mRequestLetterFormDetailId { get; set; }
		public Boolean mWorkPermit { get; set; }
		public String mWorkPermitStaff { get; set; }
		public String mWorkPermitDetails { get; set; }
		public DateTime mWorkPermitDate { get; set; }
		public Boolean mWorkPermitNa { get; set; }
		public String mWorkPermitNo { get; set; }
		public Boolean mOthers { get; set; }
		public String mOthersStaff { get; set; }
		public String mOthersDetails { get; set; }
		public DateTime mOthersDate { get; set; }
		public Boolean mDeparted { get; set; }
		public String mDepartedStaff { get; set; }
		public String mDepartedDetails { get; set; }
		public DateTime mDepartedDate { get; set; }
		public Boolean mCe { get; set; }
		public String mCeStaff { get; set; }
		public String mCeDetails { get; set; }
		public DateTime mCeDate { get; set; }
		public Double mCeAmount { get; set; }
		public Boolean mRepaired { get; set; }
		public String mRepairedStaff { get; set; }
		public String mRepairedDetails { get; set; }
		public DateTime mRepairedDate { get; set; }
		public Double mCostTechFee { get; set; }
		public Double mCostOt { get; set; }
		public String mCostOtRemarks { get; set; }
		public Double mCostTranspoAllowance { get; set; }
		public Double mCostMealAllowance { get; set; }
		public Double mCostSupplierFee { get; set; }
		public Double mCostOther { get; set; }
		public String mCostItsNo { get; set; }
		public String mCostCrmNo { get; set; }
		public String mCostDrNoSupplier { get; set; }
		public String mCostDrNoOther { get; set; }
		public Boolean mChargeable { get; set; }
		public String mDafNo { get; set; }
		public String mPersonsCausedDamage { get; set; }
		public Boolean mForReschedule { get; set; }
		public Boolean mForBackjob { get; set; }
		public String mRfsvNo { get; set; }
		public String mServiceClearanceNo { get; set; }
		public DateTime mDateFiled { get; set; }
		public DateTime mDateScheduled { get; set; }
		public DateTime mDateApproved { get; set; }
		public Boolean mPendingMaterials { get; set; }
		public Boolean mPendingCosting { get; set; }
		public Boolean mPendingMannerOfRepair { get; set; }
		public Boolean mPendingFeedback { get; set; }
		public Boolean mPendingReturn { get; set; }
		public Boolean mSuccess { get; set; }
		public Boolean mAcknowledged { get; set; }
		public String mCategory { get; set; }
		public Int32 mRm2IdServiceUnit { get; set; }
		public Int32 mRm2IdOriginalUnit { get; set; }
		public Int32 mRm2ReturnId { get; set; }
		public DateTime mOriginalSchedule { get; set; }
		public DateTime mPulloutOriginalUnitDate { get; set; }
		public DateTime mPulloutServiceUnitDate { get; set; }
		public Int32 mFiledById { get; set; }
		public Int32 mFiledByPersonnelId { get; set; }
		public Boolean mCancelled { get; set; }
		public String mCancelledRemarks { get; set; }
		public DateTime mMaterialsDate { get; set; }
		public DateTime mMannerOfRepairDate { get; set; }
		public Boolean mRequireMaterials { get; set; }
		public Int32 mFinalApprovedById { get; set; }
		public String mStatus2 { get; set; }
		public String mRecommendationRemarks { get; set; }
		public Int32 mNotAccomplishedReasonId { get; set; }
		public Int32 mBackjobReasonId { get; set; }
		public Double mMaterialsUsed { get; set; }
		public Double mMaterialsReturned { get; set; }
		public Byte mMessages { get; set; }
		public Boolean mWarranty { get; set; }
		public Int32 mSupplierId { get; set; }
		public Int32 mLocationId { get; set; }
		public Boolean mUnableToRepair { get; set; }
		public Int32 mUnableToRepairReasonId { get; set; }
		public Int32 mGatepassHoId { get; set; }
		public Byte mTechnicianStatus { get; set; }
		public String mNovNo { get; set; }
		public Boolean mRework { get; set; }
		public Int32 mReworkReasonId { get; set; }
		public String mReworkReasonDetail { get; set; }
		public Boolean mReworkReview { get; set; }
		public Int32 mPersonnelToNovId { get; set; }
		public Boolean mLessor { get; set; }
		public Boolean mItsNa { get; set; }
		public String mItsNaRemarks { get; set; }
		public Boolean mRepairOnsite { get; set; }
		public Boolean mPriority { get; set; }
		public Boolean mArchive { get; set; }
		public Int32 mRecordId { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}