using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class TypeOfLetter : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mName { get; set; }
		public String mCode { get; set; }
		public Boolean mSubmitRequirement { get; set; }
		public Int32 mMonthDuration { get; set; }
		public Boolean mStcForm { get; set; }
		public Boolean mEgress { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}