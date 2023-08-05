using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class DeliveryDetails : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mDispatchId { get; set; }
		public Int32 mProductId { get; set; }
		public String mProduct { get; set; }
		public String mCode { get; set; }
		public Int32 mPlanned { get; set; }
		public Int32 mAdditional { get; set; }
		public Int32 mCancel { get; set; }
		public Int32 mDdir { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}