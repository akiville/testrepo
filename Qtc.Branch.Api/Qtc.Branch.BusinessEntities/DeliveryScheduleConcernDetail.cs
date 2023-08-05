using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class DeliveryScheduleConcernDetail : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mDeliveryScheduleConcernId { get; set; }
		public Int32 mDeliveryScheduleDetailId { get; set; }
		public Int32 mActualItemQtyReceived { get; set; }
		public Int32 mLmmId { get; set; }
		public String mRemarks { get; set; }
        public int mProductId { get; set; }
        public String mName { get; set; }
        #endregion
    }
}