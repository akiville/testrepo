using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class AuditorLogin : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mUsername { get; set; }
		public String mPassword { get; set; }
		public String mFirstname { get; set; }
		public String mLastname { get; set; }
		public String mMiddlename { get; set; }
		public String mPositionName { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}