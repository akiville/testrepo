using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class EmployeeAttendance : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Int32 mAttendanceId { get; set; }
		public Int32 mAttendanceTrackingNo { get; set; }
		public Int32 mBranchCode { get; set; }
		public DateTime mAttendanceDate { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}