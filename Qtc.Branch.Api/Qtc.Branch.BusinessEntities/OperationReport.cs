using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class OperationReport : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public DateTime mConcernDate { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mLmId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public String mConcern { get; set; }
		public DateTime mDateFiled { get; set; }
		public String mImagePath { get; set; }
		public String mImageName { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}