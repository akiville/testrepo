using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class SalesSchedulingColorPlanned : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mOrderBy { get; set; }
		public String mName { get; set; }
		public String mBackColor { get; set; }
		public String mForeColor { get; set; }
		public Boolean mRequiredRtwNo { get; set; }
		public Boolean mRequiredNov { get; set; }
		public Int32 mIsWorking { get; set; }
		public Int32 mManPowerCount { get; set; }
		public Boolean mIsUnlimited { get; set; }
		public Int32 mNumberOfUsePerWeek { get; set; }
		public Boolean mIsPaid { get; set; }
		public String mBackColorHex { get; set; }
		public String mForColorHex { get; set; }
		public Boolean mIsActive { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}