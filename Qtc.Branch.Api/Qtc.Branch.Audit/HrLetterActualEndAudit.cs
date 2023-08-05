using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class HrLetterActualEndAudit
	{
		public static AuditCollection Audit(HrLetterActualEnd hrletteractualend,HrLetterActualEnd hrletteractualendOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (hrletteractualend.mLetterId != hrletteractualendOld.mLetterId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletteractualend);
				audit.mField = "letter_id";
				audit.mOldValue = hrletteractualendOld.mLetterId.ToString();
				audit.mNewValue = hrletteractualend.mLetterId.ToString();
				audit_collection.Add(audit);
			}

			if (hrletteractualend.mActualEnd != hrletteractualendOld.mActualEnd)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletteractualend);
				audit.mField = "actual_end";
				audit.mOldValue = hrletteractualendOld.mActualEnd.ToString();
				audit.mNewValue = hrletteractualend.mActualEnd.ToString();
				audit_collection.Add(audit);
			}

			if (hrletteractualend.mUserId != hrletteractualendOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletteractualend);
				audit.mField = "user_id";
				audit.mOldValue = hrletteractualendOld.mUserId.ToString();
				audit.mNewValue = hrletteractualend.mUserId.ToString();
				audit_collection.Add(audit);
			}

			if (hrletteractualend.mDateAdded != hrletteractualendOld.mDateAdded)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletteractualend);
				audit.mField = "date_added";
				audit.mOldValue = hrletteractualendOld.mDateAdded.ToString();
				audit.mNewValue = hrletteractualend.mDateAdded.ToString();
				audit_collection.Add(audit);
			}

			if (hrletteractualend.mIsRenewalHrLetterId != hrletteractualendOld.mIsRenewalHrLetterId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletteractualend);
				audit.mField = "is_renewal_hr_letter_id";
				audit.mOldValue = hrletteractualendOld.mIsRenewalHrLetterId.ToString();
				audit.mNewValue = hrletteractualend.mIsRenewalHrLetterId.ToString();
				audit_collection.Add(audit);
			}

			if (hrletteractualend.mTypeOfLetterId != hrletteractualendOld.mTypeOfLetterId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletteractualend);
				audit.mField = "type_of_letter_id";
				audit.mOldValue = hrletteractualendOld.mTypeOfLetterId.ToString();
				audit.mNewValue = hrletteractualend.mTypeOfLetterId.ToString();
				audit_collection.Add(audit);
			}

			if (hrletteractualend.mBranchId != hrletteractualendOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletteractualend);
				audit.mField = "branch_id";
				audit.mOldValue = hrletteractualendOld.mBranchId.ToString();
				audit.mNewValue = hrletteractualend.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (hrletteractualend.mEmployeeId != hrletteractualendOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletteractualend);
				audit.mField = "employee_id";
				audit.mOldValue = hrletteractualendOld.mEmployeeId.ToString();
				audit.mNewValue = hrletteractualend.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (hrletteractualend.mDurationFrom != hrletteractualendOld.mDurationFrom)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletteractualend);
				audit.mField = "duration_from";
				audit.mOldValue = hrletteractualendOld.mDurationFrom.ToString();
				audit.mNewValue = hrletteractualend.mDurationFrom.ToString();
				audit_collection.Add(audit);
			}

			if (hrletteractualend.mDurationTo != hrletteractualendOld.mDurationTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletteractualend);
				audit.mField = "duration_to";
				audit.mOldValue = hrletteractualendOld.mDurationTo.ToString();
				audit.mNewValue = hrletteractualend.mDurationTo.ToString();
				audit_collection.Add(audit);
			}

			if (hrletteractualend.mRecordId != hrletteractualendOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletteractualend);
				audit.mField = "record_id";
				audit.mOldValue = hrletteractualendOld.mRecordId.ToString();
				audit.mNewValue = hrletteractualend.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, HrLetterActualEnd hrletteractualend)
		{
			audit.mUserFullName = hrletteractualend.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_HrLetterActualEnd);
			audit.mRowId = hrletteractualend.mId;
			audit.mAction = 2;
		}
	}
}