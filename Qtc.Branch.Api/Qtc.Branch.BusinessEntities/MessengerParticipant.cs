using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class MessengerParticipant : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Boolean mIsAdmin { get; set; }
		public Int32 mMessengerId { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}