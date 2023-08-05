using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class Reason : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public String mName { get; set; }
		public Int32 mFormId { get; set; }
		public Boolean mRfd { get; set; }
		public Boolean mNcs { get; set; }
		public Boolean mInventory { get; set; }
		public Boolean mLogistics { get; set; }
		public Boolean mRepair { get; set; }
		public Boolean mActivity { get; set; }
		public Boolean mDdir { get; set; }
		public Int32 mOrdering { get; set; }
		public Boolean mDdirNo { get; set; }
		public Boolean mRpNo { get; set; }
		public Boolean mMcBag { get; set; }
		public Boolean mLoe { get; set; }
		public Boolean mRma { get; set; }
		public Boolean mOds { get; set; }
		public Boolean mDdd { get; set; }
		public Boolean mDddRequiredIncident { get; set; }
		public String mRemarks { get; set; }
		#endregion
	}
}