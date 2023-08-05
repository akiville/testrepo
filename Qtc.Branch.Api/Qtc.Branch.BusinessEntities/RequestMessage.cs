using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RequestMessage : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mUserId { get; set; }
		public Int32 mTopicId { get; set; }
		public String mMessage { get; set; }
        public byte[] mPicture { get; set; }
        public Int32 mOriginalMessageId { get; set; }
		public Boolean mIsSeen { get; set; }
		public DateTime mDateSeen { get; set; }
		public DateTime mMessageDate { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public int mEmployeeId { get; set; }
        public String mBranchName { get; set; }
        public String mEmployeeName { get; set; }
        public String mLmmName { get; set; }
        public DateTime mIncidentDate { get; set; }
        #endregion
    }
}