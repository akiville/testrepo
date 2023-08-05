using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class ApiAction : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mModuleName { get; set; }
		public Int32 mRecordId { get; set; }
		public Int32 mBranchId { get; set; }
		public Boolean mActionTaken { get; set; }
		public DateTime mActionDate { get; set; }
		public DateTime mDatestamp { get; set; }
		public Int32 mUserId { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}