using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Logs : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mModuleName { get; set; }
		public Int32 mEmployeeId { get; set; }
		public String mDeviceSerial { get; set; }
		public String mAction { get; set; }
		public String mDesription { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}