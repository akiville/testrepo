using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class TrainingAttendanceDetails : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
        public Int32 mRecordId { get; set; }
        public Int32 mTrainingAttendanceId { get; set; }
		public DateTime mDateCreated { get; set; }
		public String mTypeStatus { get; set; }
		public Int32 mAttendanceId { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}