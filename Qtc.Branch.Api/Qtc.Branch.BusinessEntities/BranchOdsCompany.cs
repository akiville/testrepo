using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class BranchOdsCompany : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRecordId { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mOdcCompanyId { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}