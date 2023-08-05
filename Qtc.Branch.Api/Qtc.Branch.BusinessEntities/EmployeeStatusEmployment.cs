using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class EmployeeStatusEmployment : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public DateTime mDateCreated { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Int32 mStatusId { get; set; }
		public Int32 mPositionId { get; set; }
		public Int32 mDepartmentId { get; set; }
		public Int32 mSectionId { get; set; }
		public Int32 mLinesId { get; set; }
		public Int32 mGroupHeadCountId { get; set; }
		public Int32 mGroupPayrollId { get; set; }
		public Int32 mBatchNoId { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mTaxId { get; set; }
		public DateTime mDateApplied { get; set; }
		public DateTime mDateHired { get; set; }
		public DateTime mDateRegularized { get; set; }
		public DateTime mDateOrientation { get; set; }
		public DateTime mDateOrientationTo { get; set; }
		public DateTime mDateExtention { get; set; }
		public DateTime mDateLastAttendance { get; set; }
		public DateTime mDateClearance { get; set; }
		public DateTime mDateSeparated { get; set; }
		public Int32 mEmployeeTypeId { get; set; }
		public Int32 mAgencyId { get; set; }
		public Int32 mProcessTypeId { get; set; }
		public Int32 mShuttleId { get; set; }
		public Int32 mSeparatedTypeId { get; set; }
		public Int32 mApplicantCategoryId { get; set; }
		public Boolean mIsCurrent { get; set; }
		public Int32 mReasonCodeId { get; set; }
		public Int32 mReasonLeavingId { get; set; }
		public Int32 mAgencyIdRef { get; set; }
		public Int32 mRecordId { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}