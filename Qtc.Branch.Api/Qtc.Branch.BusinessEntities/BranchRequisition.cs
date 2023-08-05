using System;
using System.ComponentModel;
using Qtc.Branch.Validation;
using System.ComponentModel.DataAnnotations;

namespace Qtc.Branch.BusinessEntities
{
	public class BranchRequisition : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime mSalesDate { get; set; }
		public Int32 mBranchId { get; set; }
		public Int32 mLmmId { get; set; }
        [Display(Name = "Employee Id")]
        public Int32 mEmployeeId { get; set; }
		public DateTime mDateCreated { get; set; }
		public DateTime mDateUpdated { get; set; }
		public String mLmmRemarks { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Remarks")]
        public String mEmployeeRemarks { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime mDatestamp { get; set; }
		public Int32 mUserId { get; set; }
        [Display(Name = "Code")]
        public String mCode { get; set; }
        [DataType(DataType.MultilineText)]
        public String mRemarks { get; set; }
        [Display(Name = "LMM")]
        public String mLmmName { get; set; }
        [Display(Name = "Employee")]
        public String mEmployeeName { get; set; }
        [Display(Name = "Branch")]
        public String mBranchName { get; set; }
        public string mReturnUrl { get; set; }

        public BranchRequisitionDetailsCollection mBranchRequisitionDetail { get; set; }
        #endregion
    }
}