using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RovingPlanScheduledActualChecklistImage : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRovingChecklistActualId { get; set; }
		public String mImageUrl { get; set; }
		public DateTime mDatestamp { get; set; }
		public Int32 mRecordId { get; set; }
		public Int32 mRpsId { get; set; }
		public Int32 mRcId { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}