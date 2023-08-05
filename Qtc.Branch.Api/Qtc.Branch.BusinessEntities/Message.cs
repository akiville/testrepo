using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Message : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public DateTime mMessageDate { get; set; }
		public Int32 mReceiverId { get; set; }
		public Int32 mSenderId { get; set; }
		public String mMessage { get; set; }
		public String mTitle { get; set; }
		public Int32 mReplyToId { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public String mReceiverName { get; set; }
        public String mSenderName { get; set; }
        public Boolean mIsAcknowledge { get; set; }
        public DateTime mAcknowledgeDate { get; set; }
        public Boolean mIsRead { get; set; }
        public String mImageLink { get; set; }
        public int mParentId { get; set; }

        #endregion
    }
}