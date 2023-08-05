using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class DeviceConfiguration : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mVersion { get; set; }
		public String mWelcomeMessage { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mWeCareMessage { get; set; }
		public String mWeAchieveMessage { get; set; }
		public String mWeEnsureMessage { get; set; }
		public String mWeImproveMessage { get; set; }
		public String mWeProvideMessage { get; set; }
		public String mWeAlignMessage { get; set; }
		public String mRemarks { get; set; }
        public DateTime mLmmAttendanceEntryCutoff { get; set; }
        public DateTime mCashEntryCutoff { get; set; }
        public int mIntroLetterCutoff { get; set; }
        public int mOtherLetterCutoff { get; set; }

        #endregion
    }
}