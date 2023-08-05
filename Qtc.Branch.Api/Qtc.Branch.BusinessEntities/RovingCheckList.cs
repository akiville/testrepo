using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class RovingCheckList : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRecordId { get; set; }
		public Int32 mChecklistCategoryId { get; set; }
		public String mName { get; set; }
		public String mRemarks { get; set; }

        public String mChecklistCategoryIdName { get; set; }
        public int mPoints { get; set; }
        #endregion
    }
}