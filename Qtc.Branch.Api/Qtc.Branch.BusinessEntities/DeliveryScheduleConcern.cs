using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class DeliveryScheduleConcern : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mDeliveryScheduleId { get; set; }
		public String mExplanation { get; set; }
		public DateTime mDeliveryDate { get; set; }
		public Int32 mLmmId { get; set; }
		public DateTime mReportDate { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}