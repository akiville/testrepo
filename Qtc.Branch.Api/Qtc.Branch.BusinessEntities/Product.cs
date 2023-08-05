using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Product : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mProductGroupId { get; set; }
		public Int32 mCategoryId { get; set; }
		public Int32 mProductTypeId { get; set; }
		public Int32 mCategory2Id { get; set; }
		public String mName { get; set; }
		public String mCode { get; set; }
		public String mDescription { get; set; }
		public String mSize { get; set; }
		public Int32 mBrandId { get; set; }
		public String mBrand { get; set; }
		public Int32 mColorId { get; set; }
		public String mColor { get; set; }
		public String mDimension { get; set; }
		public String mSerial { get; set; }
		public String mModel { get; set; }
		public String mControlNo { get; set; }
		public String mUnit { get; set; }
		public String mMatchingItems { get; set; }
		public Int32 mOrdering { get; set; }
		public Boolean mPerishable { get; set; }
		public Boolean mRequiredDate { get; set; }
		public Boolean mChargeable { get; set; }
		public Boolean mBudget { get; set; }
		public Boolean mEquipment { get; set; }
		public Boolean mRepairParts { get; set; }
		public String mRemarks { get; set; }
        public Boolean mRpFiling { get; set; }
        #endregion
    }
}