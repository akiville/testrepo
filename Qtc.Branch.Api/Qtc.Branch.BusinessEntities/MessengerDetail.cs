using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class MessengerDetail : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mMessengerId { get; set; }
		public String mMessage { get; set; }
		public Int32 mEmployeeId { get; set; }
		public DateTime mDateCreated { get; set; }
		public DateTime mDatestamp { get; set; }
		public Boolean mIsWhisper { get; set; }
		public Int32 mWhisperTo { get; set; }
		public String mRemarks { get; set; }

        public MessengerStatusCollection mMessengerStatus { get; set; }
        #endregion
    }
}