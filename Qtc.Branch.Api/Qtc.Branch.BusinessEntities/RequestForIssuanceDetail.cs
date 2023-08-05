using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RequestForIssuanceDetail : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRequestForIssuanceId { get; set; }
		public Int32 mProductId { get; set; }
		public Decimal mRequestedQty { get; set; }
		public Decimal mApprovedQty { get; set; }
		public Decimal mForPurchaseQuantity { get; set; }
		public Decimal mApprovedQuantityForPurchase { get; set; }
		public Decimal mReleasedQuantity { get; set; }
		public Decimal mUsedQuantity { get; set; }
		public Decimal mCost { get; set; }
		public Boolean mCancelled { get; set; }
		public Int32 mProductSerialIdGatepass { get; set; }
		public Int32 mProductSerialIdIngress { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}