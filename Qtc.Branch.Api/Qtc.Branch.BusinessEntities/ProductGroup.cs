using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class ProductGroup : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mProductGroupId { get; set; }
		public String mName { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}