using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class LoeDetail : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mLoeId { get; set; }
		public Double mQuantity { get; set; }
		public Int32 mApprovedQuantity { get; set; }
		public Int32 mProductId { get; set; }
		public Double mPrice { get; set; }
		public Double mDiscount { get; set; }
		public Int32 mEmployeeId { get; set; }
		public String mUnit { get; set; }
		public String mProduct { get; set; }
		public String mEditedQuantity { get; set; }
		public String mEditedPrice { get; set; }
		public String mEditedDiscount { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}