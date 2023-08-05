using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class BranchRequisitionDetails : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mBranchRequisitionId { get; set; }
		public Int32 mProductId { get; set; }
		public Decimal mAvailQty { get; set; }
		public Int32 mUserId { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
        public String mName { get; set; }
        public String mUnit { get; set; }

        #endregion
    }
}