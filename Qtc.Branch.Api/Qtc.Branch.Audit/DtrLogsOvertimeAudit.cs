using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class DtrLogsOvertimeAudit
	{
		public static AuditCollection Audit(DtrLogsOvertime dtrlogsovertime,DtrLogsOvertime dtrlogsovertimeOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (dtrlogsovertime.mDateCreated != dtrlogsovertimeOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "date_created";
				audit.mOldValue = dtrlogsovertimeOld.mDateCreated.ToString();
				audit.mNewValue = dtrlogsovertime.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mNumber != dtrlogsovertimeOld.mNumber)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "number";
				audit.mOldValue = dtrlogsovertimeOld.mNumber.ToString();
				audit.mNewValue = dtrlogsovertime.mNumber.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mEmployeeId != dtrlogsovertimeOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "employee_id";
				audit.mOldValue = dtrlogsovertimeOld.mEmployeeId.ToString();
				audit.mNewValue = dtrlogsovertime.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mShiftId != dtrlogsovertimeOld.mShiftId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "shift_id";
				audit.mOldValue = dtrlogsovertimeOld.mShiftId.ToString();
				audit.mNewValue = dtrlogsovertime.mShiftId.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mFiledById != dtrlogsovertimeOld.mFiledById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "filed_by_id";
				audit.mOldValue = dtrlogsovertimeOld.mFiledById.ToString();
				audit.mNewValue = dtrlogsovertime.mFiledById.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mOtDate != dtrlogsovertimeOld.mOtDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "ot_date";
				audit.mOldValue = dtrlogsovertimeOld.mOtDate.ToString();
				audit.mNewValue = dtrlogsovertime.mOtDate.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mOtStart != dtrlogsovertimeOld.mOtStart)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "ot_start";
				audit.mOldValue = dtrlogsovertimeOld.mOtStart.ToString();
				audit.mNewValue = dtrlogsovertime.mOtStart.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mOtEnd != dtrlogsovertimeOld.mOtEnd)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "ot_end";
				audit.mOldValue = dtrlogsovertimeOld.mOtEnd.ToString();
				audit.mNewValue = dtrlogsovertime.mOtEnd.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mTotalHours != dtrlogsovertimeOld.mTotalHours)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "total_hours";
				audit.mOldValue = dtrlogsovertimeOld.mTotalHours.ToString();
				audit.mNewValue = dtrlogsovertime.mTotalHours.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mReason != dtrlogsovertimeOld.mReason)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "reason";
				audit.mOldValue = dtrlogsovertimeOld.mReason;
				audit.mNewValue = dtrlogsovertime.mReason;
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mExplanation != dtrlogsovertimeOld.mExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "explanation";
				audit.mOldValue = dtrlogsovertimeOld.mExplanation;
				audit.mNewValue = dtrlogsovertime.mExplanation;
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mApproved != dtrlogsovertimeOld.mApproved)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "approved";
				audit.mOldValue = dtrlogsovertimeOld.mApproved.ToString();
				audit.mNewValue = dtrlogsovertime.mApproved.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mApprovedBy != dtrlogsovertimeOld.mApprovedBy)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "approved_by";
				audit.mOldValue = dtrlogsovertimeOld.mApprovedBy.ToString();
				audit.mNewValue = dtrlogsovertime.mApprovedBy.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mApprovedDate != dtrlogsovertimeOld.mApprovedDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "approved_date";
				audit.mOldValue = dtrlogsovertimeOld.mApprovedDate.ToString();
				audit.mNewValue = dtrlogsovertime.mApprovedDate.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mApprovedNumber != dtrlogsovertimeOld.mApprovedNumber)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "approved_number";
				audit.mOldValue = dtrlogsovertimeOld.mApprovedNumber.ToString();
				audit.mNewValue = dtrlogsovertime.mApprovedNumber.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mCancelled != dtrlogsovertimeOld.mCancelled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "cancelled";
				audit.mOldValue = dtrlogsovertimeOld.mCancelled.ToString();
				audit.mNewValue = dtrlogsovertime.mCancelled.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mCancelledBy != dtrlogsovertimeOld.mCancelledBy)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "cancelled_by";
				audit.mOldValue = dtrlogsovertimeOld.mCancelledBy.ToString();
				audit.mNewValue = dtrlogsovertime.mCancelledBy.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mCancelledDate != dtrlogsovertimeOld.mCancelledDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "cancelled_date";
				audit.mOldValue = dtrlogsovertimeOld.mCancelledDate.ToString();
				audit.mNewValue = dtrlogsovertime.mCancelledDate.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mDatePrint != dtrlogsovertimeOld.mDatePrint)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "date_print";
				audit.mOldValue = dtrlogsovertimeOld.mDatePrint.ToString();
				audit.mNewValue = dtrlogsovertime.mDatePrint.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mBranchOt != dtrlogsovertimeOld.mBranchOt)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "branch_ot";
				audit.mOldValue = dtrlogsovertimeOld.mBranchOt.ToString();
				audit.mNewValue = dtrlogsovertime.mBranchOt.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mVerifiedBy != dtrlogsovertimeOld.mVerifiedBy)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "verified_by";
				audit.mOldValue = dtrlogsovertimeOld.mVerifiedBy.ToString();
				audit.mNewValue = dtrlogsovertime.mVerifiedBy.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mVerifiedByRemarks != dtrlogsovertimeOld.mVerifiedByRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "verified_by_remarks";
				audit.mOldValue = dtrlogsovertimeOld.mVerifiedByRemarks;
				audit.mNewValue = dtrlogsovertime.mVerifiedByRemarks;
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mVerifiedByDate != dtrlogsovertimeOld.mVerifiedByDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "verified_by_date";
				audit.mOldValue = dtrlogsovertimeOld.mVerifiedByDate.ToString();
				audit.mNewValue = dtrlogsovertime.mVerifiedByDate.ToString();
				audit_collection.Add(audit);
			}

			if (dtrlogsovertime.mRecordId != dtrlogsovertimeOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, dtrlogsovertime);
				audit.mField = "record_id";
				audit.mOldValue = dtrlogsovertimeOld.mRecordId.ToString();
				audit.mNewValue = dtrlogsovertime.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, DtrLogsOvertime dtrlogsovertime)
		{
			audit.mUserFullName = dtrlogsovertime.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_DtrLogsOvertime);
			audit.mRowId = dtrlogsovertime.mId;
			audit.mAction = 2;
		}
	}
}