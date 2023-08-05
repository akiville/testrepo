using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class EmployeeStatusEmploymentAudit
	{
		public static AuditCollection Audit(EmployeeStatusEmployment employeestatusemployment,EmployeeStatusEmployment employeestatusemploymentOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (employeestatusemployment.mDateCreated != employeestatusemploymentOld.mDateCreated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "date_created";
				audit.mOldValue = employeestatusemploymentOld.mDateCreated.ToString();
				audit.mNewValue = employeestatusemployment.mDateCreated.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mEmployeeId != employeestatusemploymentOld.mEmployeeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "employee_id";
				audit.mOldValue = employeestatusemploymentOld.mEmployeeId.ToString();
				audit.mNewValue = employeestatusemployment.mEmployeeId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mStatusId != employeestatusemploymentOld.mStatusId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "status_id";
				audit.mOldValue = employeestatusemploymentOld.mStatusId.ToString();
				audit.mNewValue = employeestatusemployment.mStatusId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mPositionId != employeestatusemploymentOld.mPositionId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "position_id";
				audit.mOldValue = employeestatusemploymentOld.mPositionId.ToString();
				audit.mNewValue = employeestatusemployment.mPositionId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mDepartmentId != employeestatusemploymentOld.mDepartmentId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "department_id";
				audit.mOldValue = employeestatusemploymentOld.mDepartmentId.ToString();
				audit.mNewValue = employeestatusemployment.mDepartmentId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mSectionId != employeestatusemploymentOld.mSectionId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "section_id";
				audit.mOldValue = employeestatusemploymentOld.mSectionId.ToString();
				audit.mNewValue = employeestatusemployment.mSectionId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mLinesId != employeestatusemploymentOld.mLinesId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "lines_id";
				audit.mOldValue = employeestatusemploymentOld.mLinesId.ToString();
				audit.mNewValue = employeestatusemployment.mLinesId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mGroupHeadCountId != employeestatusemploymentOld.mGroupHeadCountId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "group_head_count_id";
				audit.mOldValue = employeestatusemploymentOld.mGroupHeadCountId.ToString();
				audit.mNewValue = employeestatusemployment.mGroupHeadCountId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mGroupPayrollId != employeestatusemploymentOld.mGroupPayrollId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "group_payroll_id";
				audit.mOldValue = employeestatusemploymentOld.mGroupPayrollId.ToString();
				audit.mNewValue = employeestatusemployment.mGroupPayrollId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mBatchNoId != employeestatusemploymentOld.mBatchNoId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "batch_no_id";
				audit.mOldValue = employeestatusemploymentOld.mBatchNoId.ToString();
				audit.mNewValue = employeestatusemployment.mBatchNoId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mBranchId != employeestatusemploymentOld.mBranchId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "branch_id";
				audit.mOldValue = employeestatusemploymentOld.mBranchId.ToString();
				audit.mNewValue = employeestatusemployment.mBranchId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mTaxId != employeestatusemploymentOld.mTaxId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "tax_id";
				audit.mOldValue = employeestatusemploymentOld.mTaxId.ToString();
				audit.mNewValue = employeestatusemployment.mTaxId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mDateApplied != employeestatusemploymentOld.mDateApplied)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "date_applied";
				audit.mOldValue = employeestatusemploymentOld.mDateApplied.ToString();
				audit.mNewValue = employeestatusemployment.mDateApplied.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mDateHired != employeestatusemploymentOld.mDateHired)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "date_hired";
				audit.mOldValue = employeestatusemploymentOld.mDateHired.ToString();
				audit.mNewValue = employeestatusemployment.mDateHired.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mDateRegularized != employeestatusemploymentOld.mDateRegularized)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "date_regularized";
				audit.mOldValue = employeestatusemploymentOld.mDateRegularized.ToString();
				audit.mNewValue = employeestatusemployment.mDateRegularized.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mDateOrientation != employeestatusemploymentOld.mDateOrientation)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "date_orientation";
				audit.mOldValue = employeestatusemploymentOld.mDateOrientation.ToString();
				audit.mNewValue = employeestatusemployment.mDateOrientation.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mDateOrientationTo != employeestatusemploymentOld.mDateOrientationTo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "date_orientation_to";
				audit.mOldValue = employeestatusemploymentOld.mDateOrientationTo.ToString();
				audit.mNewValue = employeestatusemployment.mDateOrientationTo.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mDateExtention != employeestatusemploymentOld.mDateExtention)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "date_extention";
				audit.mOldValue = employeestatusemploymentOld.mDateExtention.ToString();
				audit.mNewValue = employeestatusemployment.mDateExtention.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mDateLastAttendance != employeestatusemploymentOld.mDateLastAttendance)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "date_last_attendance";
				audit.mOldValue = employeestatusemploymentOld.mDateLastAttendance.ToString();
				audit.mNewValue = employeestatusemployment.mDateLastAttendance.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mDateClearance != employeestatusemploymentOld.mDateClearance)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "date_clearance";
				audit.mOldValue = employeestatusemploymentOld.mDateClearance.ToString();
				audit.mNewValue = employeestatusemployment.mDateClearance.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mDateSeparated != employeestatusemploymentOld.mDateSeparated)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "date_separated";
				audit.mOldValue = employeestatusemploymentOld.mDateSeparated.ToString();
				audit.mNewValue = employeestatusemployment.mDateSeparated.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mEmployeeTypeId != employeestatusemploymentOld.mEmployeeTypeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "employee_type_id";
				audit.mOldValue = employeestatusemploymentOld.mEmployeeTypeId.ToString();
				audit.mNewValue = employeestatusemployment.mEmployeeTypeId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mAgencyId != employeestatusemploymentOld.mAgencyId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "agency_id";
				audit.mOldValue = employeestatusemploymentOld.mAgencyId.ToString();
				audit.mNewValue = employeestatusemployment.mAgencyId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mProcessTypeId != employeestatusemploymentOld.mProcessTypeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "process_type_id";
				audit.mOldValue = employeestatusemploymentOld.mProcessTypeId.ToString();
				audit.mNewValue = employeestatusemployment.mProcessTypeId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mShuttleId != employeestatusemploymentOld.mShuttleId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "shuttle_id";
				audit.mOldValue = employeestatusemploymentOld.mShuttleId.ToString();
				audit.mNewValue = employeestatusemployment.mShuttleId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mSeparatedTypeId != employeestatusemploymentOld.mSeparatedTypeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "separated_type_id";
				audit.mOldValue = employeestatusemploymentOld.mSeparatedTypeId.ToString();
				audit.mNewValue = employeestatusemployment.mSeparatedTypeId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mApplicantCategoryId != employeestatusemploymentOld.mApplicantCategoryId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "applicant_category_id";
				audit.mOldValue = employeestatusemploymentOld.mApplicantCategoryId.ToString();
				audit.mNewValue = employeestatusemployment.mApplicantCategoryId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mIsCurrent != employeestatusemploymentOld.mIsCurrent)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "is_current";
				audit.mOldValue = employeestatusemploymentOld.mIsCurrent.ToString();
				audit.mNewValue = employeestatusemployment.mIsCurrent.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mReasonCodeId != employeestatusemploymentOld.mReasonCodeId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "reason_code_id";
				audit.mOldValue = employeestatusemploymentOld.mReasonCodeId.ToString();
				audit.mNewValue = employeestatusemployment.mReasonCodeId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mRemarks != employeestatusemploymentOld.mRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "remarks";
				audit.mOldValue = employeestatusemploymentOld.mRemarks;
				audit.mNewValue = employeestatusemployment.mRemarks;
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mReasonLeavingId != employeestatusemploymentOld.mReasonLeavingId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "reason_leaving_id";
				audit.mOldValue = employeestatusemploymentOld.mReasonLeavingId.ToString();
				audit.mNewValue = employeestatusemployment.mReasonLeavingId.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mAgencyIdRef != employeestatusemploymentOld.mAgencyIdRef)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "agency_id_ref";
				audit.mOldValue = employeestatusemploymentOld.mAgencyIdRef.ToString();
				audit.mNewValue = employeestatusemployment.mAgencyIdRef.ToString();
				audit_collection.Add(audit);
			}

			if (employeestatusemployment.mRecordId != employeestatusemploymentOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, employeestatusemployment);
				audit.mField = "record_id";
				audit.mOldValue = employeestatusemploymentOld.mRecordId.ToString();
				audit.mNewValue = employeestatusemployment.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, EmployeeStatusEmployment employeestatusemployment)
		{
			audit.mUserFullName = employeestatusemployment.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_EmployeeStatusEmployment);
			audit.mRowId = employeestatusemployment.mId;
			audit.mAction = 2;
		}
	}
}