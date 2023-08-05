using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class HrLetterHistoryMovement : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mHrLetterId { get; set; }
		public Int32 mSequence { get; set; }
		public String mAction { get; set; }
		public String mRider { get; set; }
		public String mSeriesNumber { get; set; }
		public String mDestination { get; set; }
		public String mDateTrip { get; set; }
		public String mBranchName { get; set; }
		public DateTime mDate { get; set; }
		public String mActionBy { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}