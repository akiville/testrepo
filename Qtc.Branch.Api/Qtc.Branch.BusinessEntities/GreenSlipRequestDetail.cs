using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class GreenSlipRequestDetail : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mGreenSlipId { get; set; }
		public Int32 mProductId { get; set; }
		public Double mOtherQuantity { get; set; }
		public Double mQuantity { get; set; }
		public Double mApprovedQuantity { get; set; }
		public Double mOtherCancel { get; set; }
		public Double mCancel { get; set; }
		public Double mApprovedCancel { get; set; }
		public Double mDispatch { get; set; }
		public Double mReceivedQuantity { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}