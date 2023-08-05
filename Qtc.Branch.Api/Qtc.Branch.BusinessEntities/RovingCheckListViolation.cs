using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RovingCheckListViolation : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRovingChecklistId { get; set; }
		public Int32 mTypeOfViolationId { get; set; }
		public String mRemarks { get; set; }
        public String mTypeOfViolationName { get; set; }
        

        #endregion
    }
}