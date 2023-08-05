using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Trainee : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Int32 mIsTrainee { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mLmmId { get; set; }
		public DateTime mStartDate { get; set; }
		public DateTime mEndDate { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}