using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class HrLetterRequestAudit
	{
		public static AuditCollection Audit(HrLetterRequest hrletterrequest,HrLetterRequest hrletterrequestOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (hrletterrequest.mEmployeeId != hrletterrequestOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterrequest);
				audit.mField = "employee_id";
				audit.mOldValue = hrletterrequestOld.mEmployeeId.ToString();
				audit.mNewValue = hrletterrequest.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (hrletterrequest.mBranchId != hrletterrequestOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterrequest);
				audit.mField = "branch_id";
				audit.mOldValue = hrletterrequestOld.mBranchId.ToString();
				audit.mNewValue = hrletterrequest.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (hrletterrequest.mDurationFrom != hrletterrequestOld.mDurationFrom)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterrequest);
				audit.mField = "duration_from";
				audit.mOldValue = hrletterrequestOld.mDurationFrom.ToString();
				audit.mNewValue = hrletterrequest.mDurationFrom.ToString();
				audit_collection.Add(audit);
			}

			if (hrletterrequest.mDurationTo != hrletterrequestOld.mDurationTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterrequest);
				audit.mField = "duration_to";
				audit.mOldValue = hrletterrequestOld.mDurationTo.ToString();
				audit.mNewValue = hrletterrequest.mDurationTo.ToString();
				audit_collection.Add(audit);
			}

			if (hrletterrequest.mStartDate != hrletterrequestOld.mStartDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterrequest);
				audit.mField = "start_date";
				audit.mOldValue = hrletterrequestOld.mStartDate.ToString();
				audit.mNewValue = hrletterrequest.mStartDate.ToString();
				audit_collection.Add(audit);
			}

			if (hrletterrequest.mEndDate != hrletterrequestOld.mEndDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterrequest);
				audit.mField = "end_date";
				audit.mOldValue = hrletterrequestOld.mEndDate.ToString();
				audit.mNewValue = hrletterrequest.mEndDate.ToString();
				audit_collection.Add(audit);
			}

			if (hrletterrequest.mTypeOfLetterId != hrletterrequestOld.mTypeOfLetterId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterrequest);
				audit.mField = "type_of_letter_id";
				audit.mOldValue = hrletterrequestOld.mTypeOfLetterId.ToString();
				audit.mNewValue = hrletterrequest.mTypeOfLetterId.ToString();
				audit_collection.Add(audit);
			}

			if (hrletterrequest.mHrLetterCategoryId != hrletterrequestOld.mHrLetterCategoryId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterrequest);
				audit.mField = "hr_letter_category_id";
				audit.mOldValue = hrletterrequestOld.mHrLetterCategoryId.ToString();
				audit.mNewValue = hrletterrequest.mHrLetterCategoryId.ToString();
				audit_collection.Add(audit);
			}

			if (hrletterrequest.mUserId != hrletterrequestOld.mUserId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, hrletterrequest);
				audit.mField = "user_id";
				audit.mOldValue = hrletterrequestOld.mUserId.ToString();
				audit.mNewValue = hrletterrequest.mUserId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, HrLetterRequest hrletterrequest)
		{
			audit.mUserFullName = hrletterrequest.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_HrLetterRequest);
			audit.mRowId = hrletterrequest.mId;
			audit.mAction = 2;
		}
	}
}