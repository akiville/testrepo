using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RfscAudit
	{
		public static AuditCollection Audit(Rfsc rfsc,Rfsc rfscOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (rfsc.mDateFiled != rfscOld.mDateFiled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "date_filed";
				audit.mOldValue = rfscOld.mDateFiled.ToString();
				audit.mNewValue = rfsc.mDateFiled.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mCutoffDate != rfscOld.mCutoffDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "cutoff_date";
				audit.mOldValue = rfscOld.mCutoffDate.ToString();
				audit.mNewValue = rfsc.mCutoffDate.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mDateRequestedFrom != rfscOld.mDateRequestedFrom)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "date_requested_from";
				audit.mOldValue = rfscOld.mDateRequestedFrom.ToString();
				audit.mNewValue = rfsc.mDateRequestedFrom.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mDateRequestedTo != rfscOld.mDateRequestedTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "date_requested_to";
				audit.mOldValue = rfscOld.mDateRequestedTo.ToString();
				audit.mNewValue = rfsc.mDateRequestedTo.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mEncoderId != rfscOld.mEncoderId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "encoder_id";
				audit.mOldValue = rfscOld.mEncoderId.ToString();
				audit.mNewValue = rfsc.mEncoderId.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mEmployeeId != rfscOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "employee_id";
				audit.mOldValue = rfscOld.mEmployeeId.ToString();
				audit.mNewValue = rfsc.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mTypeId != rfscOld.mTypeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "type_id";
				audit.mOldValue = rfscOld.mTypeId.ToString();
				audit.mNewValue = rfsc.mTypeId.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mReason != rfscOld.mReason)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "reason";
				audit.mOldValue = rfscOld.mReason;
				audit.mNewValue = rfsc.mReason;
				audit_collection.Add(audit);
			}

			if (rfsc.mExplanation != rfscOld.mExplanation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "explanation";
				audit.mOldValue = rfscOld.mExplanation;
				audit.mNewValue = rfsc.mExplanation;
				audit_collection.Add(audit);
			}

			if (rfsc.mChangeWith != rfscOld.mChangeWith)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "change_with";
				audit.mOldValue = rfscOld.mChangeWith.ToString();
				audit.mNewValue = rfsc.mChangeWith.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mBranchFrom != rfscOld.mBranchFrom)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "branch_from";
				audit.mOldValue = rfscOld.mBranchFrom.ToString();
				audit.mNewValue = rfsc.mBranchFrom.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mBranchTo != rfscOld.mBranchTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "branch_to";
				audit.mOldValue = rfscOld.mBranchTo.ToString();
				audit.mNewValue = rfsc.mBranchTo.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mIsPlanned != rfscOld.mIsPlanned)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "is_planned";
				audit.mOldValue = rfscOld.mIsPlanned.ToString();
				audit.mNewValue = rfsc.mIsPlanned.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mIsApproved != rfscOld.mIsApproved)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "is_approved";
				audit.mOldValue = rfscOld.mIsApproved.ToString();
				audit.mNewValue = rfsc.mIsApproved.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mReconNumber != rfscOld.mReconNumber)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "recon_number";
				audit.mOldValue = rfscOld.mReconNumber;
				audit.mNewValue = rfsc.mReconNumber;
				audit_collection.Add(audit);
			}

			if (rfsc.mIsCancelled != rfscOld.mIsCancelled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "is_cancelled";
				audit.mOldValue = rfscOld.mIsCancelled.ToString();
				audit.mNewValue = rfsc.mIsCancelled.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mIsExecuted != rfscOld.mIsExecuted)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "is_executed";
				audit.mOldValue = rfscOld.mIsExecuted.ToString();
				audit.mNewValue = rfsc.mIsExecuted.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mIsPrinted != rfscOld.mIsPrinted)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "is_printed";
				audit.mOldValue = rfscOld.mIsPrinted.ToString();
				audit.mNewValue = rfsc.mIsPrinted.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mIsAcknowledge != rfscOld.mIsAcknowledge)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "is_acknowledge";
				audit.mOldValue = rfscOld.mIsAcknowledge.ToString();
				audit.mNewValue = rfsc.mIsAcknowledge.ToString();
				audit_collection.Add(audit);
			}

			if (rfsc.mAcknowledgeByUserId != rfscOld.mAcknowledgeByUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, rfsc);
				audit.mField = "acknowledge_by_user_id";
				audit.mOldValue = rfscOld.mAcknowledgeByUserId.ToString();
				audit.mNewValue = rfsc.mAcknowledgeByUserId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, Rfsc rfsc)
		{
			audit.mUserFullName = rfsc.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_Rfsc);
			audit.mRowId = rfsc.mId;
			audit.mAction = 2;
		}
	}
}