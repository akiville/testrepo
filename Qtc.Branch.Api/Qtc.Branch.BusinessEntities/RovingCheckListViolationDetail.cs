using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RovingCheckListViolationDetail : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRovingChecklistViolationId { get; set; }
		public Int32 mRpsId { get; set; }
		public Int32 mRpsChklistId { get; set; }
		public Int32 mViolationId { get; set; }
		public String mImageFileName { get; set; }
		public Int32 mRecordId { get; set; }
		public String mRemarks { get; set; }
        public RovingCheckListViolationPersonnelCollection rovingCheckListViolationPersonnelCollection { get; set; }

        #endregion
    }
}