using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Category : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mCategoryId { get; set; }
		public String mName { get; set; }
		public Boolean mRfs { get; set; }
		public Int16 mSorting { get; set; }
		public String mImageLink { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}