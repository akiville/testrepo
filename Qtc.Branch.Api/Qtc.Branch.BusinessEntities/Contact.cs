using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Contact : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mContactNo { get; set; }
		public String mTitle { get; set; }
		public String mGroupName { get; set; }
		public DateTime mDatestamp { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}