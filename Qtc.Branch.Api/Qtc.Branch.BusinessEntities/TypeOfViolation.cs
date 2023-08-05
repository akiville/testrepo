using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class TypeOfViolation : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mName { get; set; }
		public Boolean mIsRfda { get; set; }
		public Int32 mTypeofsanctionId { get; set; }
		public String mRuleName { get; set; }
		public String mArticleNo { get; set; }
		public String mSectionNo { get; set; }
		public Int32 mTypeofvioaltionruleId { get; set; }
		public Int32 mOffenseLevel { get; set; }
		public Boolean mIsPerformance { get; set; }
		public Int32 mRecordId { get; set; }
		public String mRemarks { get; set; }
        public String mTypeOfViolationRuleIdName { get; set; }
        public String mTypeOfSanctionIdName { get; set; }
        #endregion
    }
}