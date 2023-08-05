using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class TypeOfReconcilingForm : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mName { get; set; }
		public Boolean mRequiredCheckPrevious { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}