using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class IbwDetail : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mIbwId { get; set; }
		public Int32 mProductId { get; set; }
		public Double mQuantity { get; set; }
		public Double mCheckedQuantity { get; set; }
		public String mNovNo { get; set; }
		public String mRemarks { get; set; }
        public String mProductName { get; set; }
        #endregion
    }
}