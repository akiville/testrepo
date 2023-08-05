using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class EmployeeHrLetterAudit
	{
		public static AuditCollection Audit(EmployeeHrLetter employeehrletter,EmployeeHrLetter employeehrletterOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();
            
			if (employeehrletter.mTypeOfLetterId != employeehrletterOld.mTypeOfLetterId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "type_of_letter_id";
				audit.mOldValue = employeehrletterOld.mTypeOfLetterId.ToString();
				audit.mNewValue = employeehrletter.mTypeOfLetterId.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mControlNo != employeehrletterOld.mControlNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "control_no";
				audit.mOldValue = employeehrletterOld.mControlNo;
				audit.mNewValue = employeehrletter.mControlNo;
				audit_collection.Add(audit);
			}

			if (employeehrletter.mFormNo != employeehrletterOld.mFormNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "form_no";
				audit.mOldValue = employeehrletterOld.mFormNo;
				audit.mNewValue = employeehrletter.mFormNo;
				audit_collection.Add(audit);
			}

			if (employeehrletter.mEmployeeId != employeehrletterOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "employee_id";
				audit.mOldValue = employeehrletterOld.mEmployeeId.ToString();
				audit.mNewValue = employeehrletter.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mDateCreated != employeehrletterOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "date_created";
				audit.mOldValue = employeehrletterOld.mDateCreated.ToString();
				audit.mNewValue = employeehrletter.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mDurationFrom != employeehrletterOld.mDurationFrom)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "duration_from";
				audit.mOldValue = employeehrletterOld.mDurationFrom.ToString();
				audit.mNewValue = employeehrletter.mDurationFrom.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mDurationTo != employeehrletterOld.mDurationTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "duration_to";
				audit.mOldValue = employeehrletterOld.mDurationTo.ToString();
				audit.mNewValue = employeehrletter.mDurationTo.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mBranchId != employeehrletterOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "branch_id";
				audit.mOldValue = employeehrletterOld.mBranchId.ToString();
				audit.mNewValue = employeehrletter.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mBranchTo != employeehrletterOld.mBranchTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "branch_to";
				audit.mOldValue = employeehrletterOld.mBranchTo.ToString();
				audit.mNewValue = employeehrletter.mBranchTo.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mRemarks != employeehrletterOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "remarks";
				audit.mOldValue = employeehrletterOld.mRemarks;
				audit.mNewValue = employeehrletter.mRemarks;
				audit_collection.Add(audit);
			}

			if (employeehrletter.mNoCopies != employeehrletterOld.mNoCopies)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "no_copies";
				audit.mOldValue = employeehrletterOld.mNoCopies.ToString();
				audit.mNewValue = employeehrletter.mNoCopies.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mCancelled != employeehrletterOld.mCancelled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "cancelled";
				audit.mOldValue = employeehrletterOld.mCancelled.ToString();
				audit.mNewValue = employeehrletter.mCancelled.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mCancelledRemark != employeehrletterOld.mCancelledRemark)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "cancelled_remark";
				audit.mOldValue = employeehrletterOld.mCancelledRemark;
				audit.mNewValue = employeehrletter.mCancelledRemark;
				audit_collection.Add(audit);
			}

			if (employeehrletter.mApproved != employeehrletterOld.mApproved)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "approved";
				audit.mOldValue = employeehrletterOld.mApproved.ToString();
				audit.mNewValue = employeehrletter.mApproved.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mApprovedDate != employeehrletterOld.mApprovedDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "approved_date";
				audit.mOldValue = employeehrletterOld.mApprovedDate.ToString();
				audit.mNewValue = employeehrletter.mApprovedDate.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mApprovedBy != employeehrletterOld.mApprovedBy)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "approved_by";
				audit.mOldValue = employeehrletterOld.mApprovedBy.ToString();
				audit.mNewValue = employeehrletter.mApprovedBy.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mPrintNo != employeehrletterOld.mPrintNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "print_no";
				audit.mOldValue = employeehrletterOld.mPrintNo.ToString();
				audit.mNewValue = employeehrletter.mPrintNo.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mPrintDate != employeehrletterOld.mPrintDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "print_date";
				audit.mOldValue = employeehrletterOld.mPrintDate.ToString();
				audit.mNewValue = employeehrletter.mPrintDate.ToString();
				audit_collection.Add(audit);
			}

			
			if (employeehrletter.mReleasedDate != employeehrletterOld.mReleasedDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "released_date";
				audit.mOldValue = employeehrletterOld.mReleasedDate.ToString();
				audit.mNewValue = employeehrletter.mReleasedDate.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mReleasedById != employeehrletterOld.mReleasedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "released_by_id";
				audit.mOldValue = employeehrletterOld.mReleasedById.ToString();
				audit.mNewValue = employeehrletter.mReleasedById.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mReturnedDate != employeehrletterOld.mReturnedDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "returned_date";
				audit.mOldValue = employeehrletterOld.mReturnedDate.ToString();
				audit.mNewValue = employeehrletter.mReturnedDate.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mReturnedTo != employeehrletterOld.mReturnedTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "returned_to";
				audit.mOldValue = employeehrletterOld.mReturnedTo.ToString();
				audit.mNewValue = employeehrletter.mReturnedTo.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mRequestBy != employeehrletterOld.mRequestBy)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "request_by";
				audit.mOldValue = employeehrletterOld.mRequestBy.ToString();
				audit.mNewValue = employeehrletter.mRequestBy.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mReleasedCopies != employeehrletterOld.mReleasedCopies)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "released_copies";
				audit.mOldValue = employeehrletterOld.mReleasedCopies.ToString();
				audit.mNewValue = employeehrletter.mReleasedCopies.ToString();
				audit_collection.Add(audit);
			}

			

			if (employeehrletter.mReleasedNo != employeehrletterOld.mReleasedNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "released_no";
				audit.mOldValue = employeehrletterOld.mReleasedNo;
				audit.mNewValue = employeehrletter.mReleasedNo;
				audit_collection.Add(audit);
			}

			if (employeehrletter.mReleasedTo != employeehrletterOld.mReleasedTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "released_to";
				audit.mOldValue = employeehrletterOld.mReleasedTo.ToString();
				audit.mNewValue = employeehrletter.mReleasedTo.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mDurationFromActual != employeehrletterOld.mDurationFromActual)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "duration_from_actual";
				audit.mOldValue = employeehrletterOld.mDurationFromActual.ToString();
				audit.mNewValue = employeehrletter.mDurationFromActual.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mDurationToActual != employeehrletterOld.mDurationToActual)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "duration_to_actual";
				audit.mOldValue = employeehrletterOld.mDurationToActual.ToString();
				audit.mNewValue = employeehrletter.mDurationToActual.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mIntroLetterId != employeehrletterOld.mIntroLetterId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "intro_letter_id";
				audit.mOldValue = employeehrletterOld.mIntroLetterId.ToString();
				audit.mNewValue = employeehrletter.mIntroLetterId.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mPrintReleaseTo != employeehrletterOld.mPrintReleaseTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "print_release_to";
				audit.mOldValue = employeehrletterOld.mPrintReleaseTo.ToString();
				audit.mNewValue = employeehrletter.mPrintReleaseTo.ToString();
				audit_collection.Add(audit);
			}

			

			if (employeehrletter.mRequestRtw != employeehrletterOld.mRequestRtw)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "request_rtw";
				audit.mOldValue = employeehrletterOld.mRequestRtw.ToString();
				audit.mNewValue = employeehrletter.mRequestRtw.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mLmmName != employeehrletterOld.mLmmName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "lmm_name";
				audit.mOldValue = employeehrletterOld.mLmmName;
				audit.mNewValue = employeehrletter.mLmmName;
				audit_collection.Add(audit);
			}

			if (employeehrletter.mOicName != employeehrletterOld.mOicName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "oic_name";
				audit.mOldValue = employeehrletterOld.mOicName;
				audit.mNewValue = employeehrletter.mOicName;
				audit_collection.Add(audit);
			}

			if (employeehrletter.mReason != employeehrletterOld.mReason)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "reason";
				audit.mOldValue = employeehrletterOld.mReason;
				audit.mNewValue = employeehrletter.mReason;
				audit_collection.Add(audit);
			}

			if (employeehrletter.mWeekendLetter != employeehrletterOld.mWeekendLetter)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "weekend_letter";
				audit.mOldValue = employeehrletterOld.mWeekendLetter.ToString();
				audit.mNewValue = employeehrletter.mWeekendLetter.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mOpeningScheduled != employeehrletterOld.mOpeningScheduled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "opening_scheduled";
				audit.mOldValue = employeehrletterOld.mOpeningScheduled.ToString();
				audit.mNewValue = employeehrletter.mOpeningScheduled.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mReleasedDateRequest != employeehrletterOld.mReleasedDateRequest)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "released_date_request";
				audit.mOldValue = employeehrletterOld.mReleasedDateRequest.ToString();
				audit.mNewValue = employeehrletter.mReleasedDateRequest.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mEncodedById != employeehrletterOld.mEncodedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "encoded_by_id";
				audit.mOldValue = employeehrletterOld.mEncodedById.ToString();
				audit.mNewValue = employeehrletter.mEncodedById.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mHrLetterCategoryId != employeehrletterOld.mHrLetterCategoryId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "hr_letter_category_id";
				audit.mOldValue = employeehrletterOld.mHrLetterCategoryId.ToString();
				audit.mNewValue = employeehrletter.mHrLetterCategoryId.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mNotifyMcBag != employeehrletterOld.mNotifyMcBag)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "notify_mc_bag";
				audit.mOldValue = employeehrletterOld.mNotifyMcBag.ToString();
				audit.mNewValue = employeehrletter.mNotifyMcBag.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mNotifyKey != employeehrletterOld.mNotifyKey)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "notify_key";
				audit.mOldValue = employeehrletterOld.mNotifyKey.ToString();
				audit.mNewValue = employeehrletter.mNotifyKey.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mCourierName != employeehrletterOld.mCourierName)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "courier_name";
				audit.mOldValue = employeehrletterOld.mCourierName;
				audit.mNewValue = employeehrletter.mCourierName;
				audit_collection.Add(audit);
			}

			if (employeehrletter.mOtherType != employeehrletterOld.mOtherType)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "other_type";
				audit.mOldValue = employeehrletterOld.mOtherType.ToString();
				audit.mNewValue = employeehrletter.mOtherType.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mDateGenerated != employeehrletterOld.mDateGenerated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "date_generated";
				audit.mOldValue = employeehrletterOld.mDateGenerated.ToString();
				audit.mNewValue = employeehrletter.mDateGenerated.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mCancelledDate != employeehrletterOld.mCancelledDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "cancelled_date";
				audit.mOldValue = employeehrletterOld.mCancelledDate.ToString();
				audit.mNewValue = employeehrletter.mCancelledDate.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mFirstReliever != employeehrletterOld.mFirstReliever)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "first_reliever";
				audit.mOldValue = employeehrletterOld.mFirstReliever.ToString();
				audit.mNewValue = employeehrletter.mFirstReliever.ToString();
				audit_collection.Add(audit);
			}

			if (employeehrletter.mNeedToRelease != employeehrletterOld.mNeedToRelease)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeehrletter);
				audit.mField = "need_to_release";
				audit.mOldValue = employeehrletterOld.mNeedToRelease.ToString();
				audit.mNewValue = employeehrletter.mNeedToRelease.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, EmployeeHrLetter employeehrletter)
		{
			audit.mUserFullName = employeehrletter.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_EmployeeHrLetter);
			audit.mRowId = employeehrletter.mId;
			audit.mAction = 2;
		}
	}
}