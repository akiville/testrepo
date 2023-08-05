using System.ComponentModel;
using System;

namespace Qtc.Branch.BusinessEntities
{
    public class Audit : BusinessBase
    {
        [DataObjectFieldAttribute(true, true, false)]
        public override int mId { get; set; }
        public int mTableId { get; set; }
        public int mRowId { get; set; }
        public int mAction { get; set; }
        public string mField { get; set; }
        public string mOldValue { get; set; }
        public string mNewValue { get; set; }
        public DateTime mAuditDate = DateTime.Now;
        public bool mIsSubItem { get; set; }
        public DateTime mDateTime { get; set; }
        public string mDescription { get; set; }
        public int mUserId { get; set; }
        public string mModule { get; set; }
        public string mDateString { get; set; }
        public string mActionDescription { get; set; }

        public int mBranchId { get; set; }
        public DateTime mDeliveryDate { get; set; }
        public String mTransactionNo { get; set; }
        public String mInfo { get; set; }
    }
}