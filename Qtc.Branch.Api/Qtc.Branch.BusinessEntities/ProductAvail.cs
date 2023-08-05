using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class ProductAvail : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mBranchId { get; set; }
		public DateTime mInventoryDate { get; set; }
		public Int32 mProductId { get; set; }
		public Int32 mCategoryId { get; set; }
		public Int32 mPrior { get; set; }
		public Int32 mAvail { get; set; }
		public Int32 mAddBack { get; set; }
		public String mAddBackReason { get; set; }
		public Int32 mDispatch { get; set; }
		public Int32 mGreenslipAdd { get; set; }
		public Int32 mGreenslipCancel { get; set; }
		public Int32 mDdirOut { get; set; }
		public Int32 mDdirIn { get; set; }
		public Int32 mLoe { get; set; }
		public Int32 mIbwOut { get; set; }
		public Int32 mIbwIn { get; set; }
		public Int32 mRma { get; set; }
		public Int32 mEmployeeId { get; set; }
		public DateTime mUploadDate { get; set; }
		//public byte[] mSignature { get; set; }
		public String mRemarks { get; set; }
        public Int32 mProductAvailId { get; set; }
        public Boolean mIsHoDownloaded { get; set; }
        public String mName { get; set; }
        public String mCode { get; set; }
        public String mUnit { get; set; }
        #endregion
    }
}