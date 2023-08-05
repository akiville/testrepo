using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class User : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mFirstName { get; set; }
		public String mMiddleName { get; set; }
		public String mLastName { get; set; }
		public Boolean mRole { get; set; }
		public String mUserName { get; set; }
		public String mPassword { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}