using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Employee : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public String mLastname { get; set; }
		public String mFirstname { get; set; }
		public String mMiddlename { get; set; }
		public String mPositionName { get; set; }
		public String mCellphoneNo { get; set; }
		public String mResidentialAddress { get; set; }
		public String mPassword { get; set; }
		public String mRemarks { get; set; }
        public Int32 mTrackingNo { get; set; }
        public Int32 mBranchCode { get; set; }
        public Int32 mIsLoggedIn { get; set; }
        public String mLastLogin { get; set; }
        public String mBranchName { get; set; }
        public String mAgency { get; set; }
        public String mAreaName { get; set; }
        public int mLmmId { get; set; }
        public String mTimeIn { get; set; }
        public String mTimeOut { get; set; }
        public int mAgencyId { get; set; }
        #endregion
    }
}