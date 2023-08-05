using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Lmm : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mLmmId { get; set; }
		public DateTime mSalesDate { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}