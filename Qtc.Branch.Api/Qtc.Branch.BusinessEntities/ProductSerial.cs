using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class ProductSerial : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mProductId { get; set; }
		public String mBrand { get; set; }
		public String mDescription { get; set; }
		public String mStickerNo { get; set; }
		public String mControlNo { get; set; }
		public String mSerialNo { get; set; }
		public String mModelNo { get; set; }
		public String mDimension { get; set; }
		public Boolean mService { get; set; }
		public Int32 mBranchIdLocation { get; set; }
		public Int32 mBranchId { get; set; }
		public Double mCost { get; set; }
		public String mSupplier { get; set; }
		public Int32 mRm2Id { get; set; }
		public Int32 mRpId { get; set; }
		public Int32 mProductModelId { get; set; }
		public Boolean mForInventory { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}