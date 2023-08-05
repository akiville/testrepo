using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Shift : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mCode { get; set; }
		public String mIn { get; set; }
		public String mBreakOut { get; set; }
		public String mBreakIn { get; set; }
		public String mOut { get; set; }
		public Boolean mNShift { get; set; }
		public Int32 mMinutesNeed { get; set; }
		public DateTime mInSchedule { get; set; }
		public DateTime mBreakOutSchedule { get; set; }
		public DateTime mBreakInSchedule { get; set; }
		public DateTime mOutSchedule { get; set; }
		public Boolean mFlexiTime { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}