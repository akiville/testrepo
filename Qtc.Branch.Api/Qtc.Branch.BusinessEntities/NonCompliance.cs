using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class NonCompliance : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mName { get; set; }
		public Boolean mRequiredDetails { get; set; }
		public Boolean mRequiredExplanation { get; set; }
		public String mRemarks { get; set; }
        public String mTopicIdName { get; set; }
        #endregion
    }
}