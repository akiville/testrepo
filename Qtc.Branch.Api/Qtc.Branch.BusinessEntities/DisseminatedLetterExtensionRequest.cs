using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class DisseminatedLetterExtensionRequest : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mDisseminatedLetterId { get; set; }
		public Int32 mLmmId { get; set; }
		public DateTime mRequestDate { get; set; }
		public String mRemarks { get; set; }
        public String mStatus { get; set; }
        public int mEmployeeId { get; set; }
        #endregion
    }
}