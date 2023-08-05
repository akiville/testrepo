using System;
using System.ComponentModel;
using Qtc.Branch.Validation;

namespace Qtc.Branch.BusinessEntities
{
	public class TrainingAttendance : BusinessBase
	{
		#region Public Properties
		public override Int32 mId { get; set; }
		public Int32 mRecordId { get; set; }
		public DateTime mDateCreated { get; set; }
		public Int32 mEmployeeId { get; set; }
		public Int32 mLmmId { get; set; }
		public Int32 mBranchId { get; set; }
		public String mOrientationDate { get; set; }
		public String mRemarks { get; set; }
        public String mDay1 { get; set; }
        public String mDay2 { get; set; }
        public String mDay3 { get; set; }
        public String mDay4 { get; set; }
        public String mDay5 { get; set; }
        public String mDay6 { get; set; }
        public String mDay7 { get; set; }
        public int mDay1Id { get; set; }
        public int mDay2Id { get; set; }
        public int mDay3Id { get; set; }
        public int mDay4Id { get; set; }
        public int mDay5Id { get; set; }
        public int mDay6Id { get; set; }
        public int mDay7Id { get; set; }
        public String mBranchName { get; set; }
        public String mEmployeeName { get; set; }
        public String mCellphoneNo { get; set; }
        public String mAgency { get; set; }
        public DateTime mDay1Date { get; set; }
        public DateTime mDay2Date { get; set; }
        public DateTime mDay3Date { get; set; }
        public DateTime mDay4Date { get; set; }
        public DateTime mDay5Date { get; set; }
        public DateTime mDay6Date { get; set; }
        public DateTime mDay7Date { get; set; }
        public String mMotherBranch { get; set; }
        public int mMotherBranchId { get; set; }
        public int mCutOffId { get; set; }


        #endregion
    }
}