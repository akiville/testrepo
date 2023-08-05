using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class HrLetterRequest : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Int32 mBranchId { get; set; }

		public DateTime mDurationFrom { get; set; }
		public DateTime mDurationTo { get; set; }
		public DateTime mStartDate { get; set; }
		public DateTime mEndDate { get; set; }
		public Int32 mTypeOfLetterId { get; set; }
		public Int32 mHrLetterCategoryId { get; set; }
		public Int32 mUserId { get; set; }
		public String mRemarks { get; set; }
        public String mStatus { get; set; }
        public String mHasExistingHrLetter { get; set; }
        public String mHasValidIntroLetter { get; set; }
        public String mHasValidIntroLetterForAdditionalOutlet { get; set; }
        public int mBranchIdTo { get; set; }
        public int mCopiesCount { get; set; }
        public int mRequestedBy { get; set; }
        public DateTime mRequestReleasedDate { get; set; }
        public int mAgencyId { get; set; }
        public String mFileName { get; set; }
        public String mLastName { get; set; }
        public String mFirstName { get; set; }
        public String mTypeOfLetter { get; set; }
        public String mBranchName { get; set; }
        public String mBranchNameTo { get; set; }

        #endregion
    }
}