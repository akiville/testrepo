using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class HrLetterActualEnd : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mLetterId { get; set; }
		public DateTime mActualEnd { get; set; }
		public Int32 mUserId { get; set; }
		public DateTime mDateAdded { get; set; }
		public Int32 mIsRenewalHrLetterId { get; set; }
		public Int32 mTypeOfLetterId { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public DateTime mDurationFrom { get; set; }
		public DateTime mDurationTo { get; set; }
		public Int32 mRecordId { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}