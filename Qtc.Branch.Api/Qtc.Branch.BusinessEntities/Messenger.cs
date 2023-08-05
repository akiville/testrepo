using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Messenger : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mTitle { get; set; }
		public DateTime mDateCreated { get; set; }
		public Boolean mDisplayReplyToEveryone { get; set; }
		public Int32 mUserId { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public MessengerDetailCollection mMessengerDetailCollection { get; set; }
        public MessengerParticipantCollection mMessengerParticipantCollection { get; set; }
        #endregion
    }
}