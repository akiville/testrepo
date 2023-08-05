using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class LmmEntry : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mType { get; set; }
		public Int32 mLmmId { get; set; }
		public Int32 mWithEntry { get; set; }
		public Int32 mTotalItem { get; set; }
		public String mLmmName { get; set; }
		public DateTime mSalesDate { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}