using Qtc.Branch.BusinessEntities;

namespace Qtc.Branch.Audit
{
	public class RepairDetailAudit
	{
		public static AuditCollection Audit(RepairDetail repairdetail,RepairDetail repairdetailOld)
		{
			AuditCollection audit_collection = new AuditCollection();
			BusinessEntities.Audit audit = new BusinessEntities.Audit();


			if (repairdetail.mRequestRepairId != repairdetailOld.mRequestRepairId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "request_repair_id";
				audit.mOldValue = repairdetailOld.mRequestRepairId.ToString();
				audit.mNewValue = repairdetail.mRequestRepairId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mTimeOfCall != repairdetailOld.mTimeOfCall)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "time_of_call";
				audit.mOldValue = repairdetailOld.mTimeOfCall.ToString();
				audit.mNewValue = repairdetail.mTimeOfCall.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mDateNeeded != repairdetailOld.mDateNeeded)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "date_needed";
				audit.mOldValue = repairdetailOld.mDateNeeded.ToString();
				audit.mNewValue = repairdetail.mDateNeeded.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mPersonWhoCallId != repairdetailOld.mPersonWhoCallId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "person_who_call_id";
				audit.mOldValue = repairdetailOld.mPersonWhoCallId.ToString();
				audit.mNewValue = repairdetail.mPersonWhoCallId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mSecurityId != repairdetailOld.mSecurityId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "security_id";
				audit.mOldValue = repairdetailOld.mSecurityId.ToString();
				audit.mNewValue = repairdetail.mSecurityId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mTechnicianId != repairdetailOld.mTechnicianId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "technician_id";
				audit.mOldValue = repairdetailOld.mTechnicianId.ToString();
				audit.mNewValue = repairdetail.mTechnicianId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mRpAssistantId != repairdetailOld.mRpAssistantId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "rp_assistant_id";
				audit.mOldValue = repairdetailOld.mRpAssistantId.ToString();
				audit.mNewValue = repairdetail.mRpAssistantId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mContractorId != repairdetailOld.mContractorId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "contractor_id";
				audit.mOldValue = repairdetailOld.mContractorId.ToString();
				audit.mNewValue = repairdetail.mContractorId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mEmergencyLetterNo != repairdetailOld.mEmergencyLetterNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "emergency_letter_no";
				audit.mOldValue = repairdetailOld.mEmergencyLetterNo;
				audit.mNewValue = repairdetail.mEmergencyLetterNo;
				audit_collection.Add(audit);
			}

			if (repairdetail.mRequestLetter != repairdetailOld.mRequestLetter)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "request_letter";
				audit.mOldValue = repairdetailOld.mRequestLetter.ToString();
				audit.mNewValue = repairdetail.mRequestLetter.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mRequestLetterStaff != repairdetailOld.mRequestLetterStaff)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "request_letter_staff";
				audit.mOldValue = repairdetailOld.mRequestLetterStaff;
				audit.mNewValue = repairdetail.mRequestLetterStaff;
				audit_collection.Add(audit);
			}

			if (repairdetail.mRequestLetterDetails != repairdetailOld.mRequestLetterDetails)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "request_letter_details";
				audit.mOldValue = repairdetailOld.mRequestLetterDetails;
				audit.mNewValue = repairdetail.mRequestLetterDetails;
				audit_collection.Add(audit);
			}

			if (repairdetail.mRequestLetterDate != repairdetailOld.mRequestLetterDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "request_letter_date";
				audit.mOldValue = repairdetailOld.mRequestLetterDate.ToString();
				audit.mNewValue = repairdetail.mRequestLetterDate.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mRequestLetterFormDetailId != repairdetailOld.mRequestLetterFormDetailId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "request_letter_form_detail_id";
				audit.mOldValue = repairdetailOld.mRequestLetterFormDetailId.ToString();
				audit.mNewValue = repairdetail.mRequestLetterFormDetailId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mWorkPermit != repairdetailOld.mWorkPermit)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "work_permit";
				audit.mOldValue = repairdetailOld.mWorkPermit.ToString();
				audit.mNewValue = repairdetail.mWorkPermit.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mWorkPermitStaff != repairdetailOld.mWorkPermitStaff)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "work_permit_staff";
				audit.mOldValue = repairdetailOld.mWorkPermitStaff;
				audit.mNewValue = repairdetail.mWorkPermitStaff;
				audit_collection.Add(audit);
			}

			if (repairdetail.mWorkPermitDetails != repairdetailOld.mWorkPermitDetails)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "work_permit_details";
				audit.mOldValue = repairdetailOld.mWorkPermitDetails;
				audit.mNewValue = repairdetail.mWorkPermitDetails;
				audit_collection.Add(audit);
			}

			if (repairdetail.mWorkPermitDate != repairdetailOld.mWorkPermitDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "work_permit_date";
				audit.mOldValue = repairdetailOld.mWorkPermitDate.ToString();
				audit.mNewValue = repairdetail.mWorkPermitDate.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mWorkPermitNa != repairdetailOld.mWorkPermitNa)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "work_permit_na";
				audit.mOldValue = repairdetailOld.mWorkPermitNa.ToString();
				audit.mNewValue = repairdetail.mWorkPermitNa.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mWorkPermitNo != repairdetailOld.mWorkPermitNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "work_permit_no";
				audit.mOldValue = repairdetailOld.mWorkPermitNo;
				audit.mNewValue = repairdetail.mWorkPermitNo;
				audit_collection.Add(audit);
			}

			if (repairdetail.mOthers != repairdetailOld.mOthers)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "others";
				audit.mOldValue = repairdetailOld.mOthers.ToString();
				audit.mNewValue = repairdetail.mOthers.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mOthersStaff != repairdetailOld.mOthersStaff)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "others_staff";
				audit.mOldValue = repairdetailOld.mOthersStaff;
				audit.mNewValue = repairdetail.mOthersStaff;
				audit_collection.Add(audit);
			}

			if (repairdetail.mOthersDetails != repairdetailOld.mOthersDetails)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "others_details";
				audit.mOldValue = repairdetailOld.mOthersDetails;
				audit.mNewValue = repairdetail.mOthersDetails;
				audit_collection.Add(audit);
			}

			if (repairdetail.mOthersDate != repairdetailOld.mOthersDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "others_date";
				audit.mOldValue = repairdetailOld.mOthersDate.ToString();
				audit.mNewValue = repairdetail.mOthersDate.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mDeparted != repairdetailOld.mDeparted)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "departed";
				audit.mOldValue = repairdetailOld.mDeparted.ToString();
				audit.mNewValue = repairdetail.mDeparted.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mDepartedStaff != repairdetailOld.mDepartedStaff)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "departed_staff";
				audit.mOldValue = repairdetailOld.mDepartedStaff;
				audit.mNewValue = repairdetail.mDepartedStaff;
				audit_collection.Add(audit);
			}

			if (repairdetail.mDepartedDetails != repairdetailOld.mDepartedDetails)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "departed_details";
				audit.mOldValue = repairdetailOld.mDepartedDetails;
				audit.mNewValue = repairdetail.mDepartedDetails;
				audit_collection.Add(audit);
			}

			if (repairdetail.mDepartedDate != repairdetailOld.mDepartedDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "departed_date";
				audit.mOldValue = repairdetailOld.mDepartedDate.ToString();
				audit.mNewValue = repairdetail.mDepartedDate.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mCe != repairdetailOld.mCe)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "ce";
				audit.mOldValue = repairdetailOld.mCe.ToString();
				audit.mNewValue = repairdetail.mCe.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mCeStaff != repairdetailOld.mCeStaff)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "ce_staff";
				audit.mOldValue = repairdetailOld.mCeStaff;
				audit.mNewValue = repairdetail.mCeStaff;
				audit_collection.Add(audit);
			}

			if (repairdetail.mCeDetails != repairdetailOld.mCeDetails)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "ce_details";
				audit.mOldValue = repairdetailOld.mCeDetails;
				audit.mNewValue = repairdetail.mCeDetails;
				audit_collection.Add(audit);
			}

			if (repairdetail.mCeDate != repairdetailOld.mCeDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "ce_date";
				audit.mOldValue = repairdetailOld.mCeDate.ToString();
				audit.mNewValue = repairdetail.mCeDate.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mCeAmount != repairdetailOld.mCeAmount)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "ce_amount";
				audit.mOldValue = repairdetailOld.mCeAmount.ToString();
				audit.mNewValue = repairdetail.mCeAmount.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mRepaired != repairdetailOld.mRepaired)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "repaired";
				audit.mOldValue = repairdetailOld.mRepaired.ToString();
				audit.mNewValue = repairdetail.mRepaired.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mRepairedStaff != repairdetailOld.mRepairedStaff)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "repaired_staff";
				audit.mOldValue = repairdetailOld.mRepairedStaff;
				audit.mNewValue = repairdetail.mRepairedStaff;
				audit_collection.Add(audit);
			}

			if (repairdetail.mRepairedDetails != repairdetailOld.mRepairedDetails)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "repaired_details";
				audit.mOldValue = repairdetailOld.mRepairedDetails;
				audit.mNewValue = repairdetail.mRepairedDetails;
				audit_collection.Add(audit);
			}

			if (repairdetail.mRepairedDate != repairdetailOld.mRepairedDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "repaired_date";
				audit.mOldValue = repairdetailOld.mRepairedDate.ToString();
				audit.mNewValue = repairdetail.mRepairedDate.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mCostTechFee != repairdetailOld.mCostTechFee)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "cost_tech_fee";
				audit.mOldValue = repairdetailOld.mCostTechFee.ToString();
				audit.mNewValue = repairdetail.mCostTechFee.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mCostOt != repairdetailOld.mCostOt)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "cost_ot";
				audit.mOldValue = repairdetailOld.mCostOt.ToString();
				audit.mNewValue = repairdetail.mCostOt.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mCostOtRemarks != repairdetailOld.mCostOtRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "cost_ot_remarks";
				audit.mOldValue = repairdetailOld.mCostOtRemarks;
				audit.mNewValue = repairdetail.mCostOtRemarks;
				audit_collection.Add(audit);
			}

			if (repairdetail.mCostTranspoAllowance != repairdetailOld.mCostTranspoAllowance)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "cost_transpo_allowance";
				audit.mOldValue = repairdetailOld.mCostTranspoAllowance.ToString();
				audit.mNewValue = repairdetail.mCostTranspoAllowance.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mCostMealAllowance != repairdetailOld.mCostMealAllowance)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "cost_meal_allowance";
				audit.mOldValue = repairdetailOld.mCostMealAllowance.ToString();
				audit.mNewValue = repairdetail.mCostMealAllowance.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mCostSupplierFee != repairdetailOld.mCostSupplierFee)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "cost_supplier_fee";
				audit.mOldValue = repairdetailOld.mCostSupplierFee.ToString();
				audit.mNewValue = repairdetail.mCostSupplierFee.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mCostOther != repairdetailOld.mCostOther)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "cost_other";
				audit.mOldValue = repairdetailOld.mCostOther.ToString();
				audit.mNewValue = repairdetail.mCostOther.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mCostItsNo != repairdetailOld.mCostItsNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "cost_its_no";
				audit.mOldValue = repairdetailOld.mCostItsNo;
				audit.mNewValue = repairdetail.mCostItsNo;
				audit_collection.Add(audit);
			}

			if (repairdetail.mCostCrmNo != repairdetailOld.mCostCrmNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "cost_crm_no";
				audit.mOldValue = repairdetailOld.mCostCrmNo;
				audit.mNewValue = repairdetail.mCostCrmNo;
				audit_collection.Add(audit);
			}

			if (repairdetail.mCostDrNoSupplier != repairdetailOld.mCostDrNoSupplier)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "cost_dr_no_supplier";
				audit.mOldValue = repairdetailOld.mCostDrNoSupplier;
				audit.mNewValue = repairdetail.mCostDrNoSupplier;
				audit_collection.Add(audit);
			}

			if (repairdetail.mCostDrNoOther != repairdetailOld.mCostDrNoOther)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "cost_dr_no_other";
				audit.mOldValue = repairdetailOld.mCostDrNoOther;
				audit.mNewValue = repairdetail.mCostDrNoOther;
				audit_collection.Add(audit);
			}

			if (repairdetail.mChargeable != repairdetailOld.mChargeable)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "chargeable";
				audit.mOldValue = repairdetailOld.mChargeable.ToString();
				audit.mNewValue = repairdetail.mChargeable.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mDafNo != repairdetailOld.mDafNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "daf_no";
				audit.mOldValue = repairdetailOld.mDafNo;
				audit.mNewValue = repairdetail.mDafNo;
				audit_collection.Add(audit);
			}

			if (repairdetail.mPersonsCausedDamage != repairdetailOld.mPersonsCausedDamage)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "persons_caused_damage";
				audit.mOldValue = repairdetailOld.mPersonsCausedDamage;
				audit.mNewValue = repairdetail.mPersonsCausedDamage;
				audit_collection.Add(audit);
			}

			if (repairdetail.mForReschedule != repairdetailOld.mForReschedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "for_reschedule";
				audit.mOldValue = repairdetailOld.mForReschedule.ToString();
				audit.mNewValue = repairdetail.mForReschedule.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mForBackjob != repairdetailOld.mForBackjob)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "for_backjob";
				audit.mOldValue = repairdetailOld.mForBackjob.ToString();
				audit.mNewValue = repairdetail.mForBackjob.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mRfsvNo != repairdetailOld.mRfsvNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "rfsv_no";
				audit.mOldValue = repairdetailOld.mRfsvNo;
				audit.mNewValue = repairdetail.mRfsvNo;
				audit_collection.Add(audit);
			}

			if (repairdetail.mServiceClearanceNo != repairdetailOld.mServiceClearanceNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "service_clearance_no";
				audit.mOldValue = repairdetailOld.mServiceClearanceNo;
				audit.mNewValue = repairdetail.mServiceClearanceNo;
				audit_collection.Add(audit);
			}

			if (repairdetail.mDateFiled != repairdetailOld.mDateFiled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "date_filed";
				audit.mOldValue = repairdetailOld.mDateFiled.ToString();
				audit.mNewValue = repairdetail.mDateFiled.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mDateScheduled != repairdetailOld.mDateScheduled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "date_scheduled";
				audit.mOldValue = repairdetailOld.mDateScheduled.ToString();
				audit.mNewValue = repairdetail.mDateScheduled.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mDateApproved != repairdetailOld.mDateApproved)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "date_approved";
				audit.mOldValue = repairdetailOld.mDateApproved.ToString();
				audit.mNewValue = repairdetail.mDateApproved.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mPendingMaterials != repairdetailOld.mPendingMaterials)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "pending_materials";
				audit.mOldValue = repairdetailOld.mPendingMaterials.ToString();
				audit.mNewValue = repairdetail.mPendingMaterials.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mPendingCosting != repairdetailOld.mPendingCosting)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "pending_costing";
				audit.mOldValue = repairdetailOld.mPendingCosting.ToString();
				audit.mNewValue = repairdetail.mPendingCosting.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mPendingMannerOfRepair != repairdetailOld.mPendingMannerOfRepair)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "pending_manner_of_repair";
				audit.mOldValue = repairdetailOld.mPendingMannerOfRepair.ToString();
				audit.mNewValue = repairdetail.mPendingMannerOfRepair.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mPendingFeedback != repairdetailOld.mPendingFeedback)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "pending_feedback";
				audit.mOldValue = repairdetailOld.mPendingFeedback.ToString();
				audit.mNewValue = repairdetail.mPendingFeedback.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mPendingReturn != repairdetailOld.mPendingReturn)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "pending_return";
				audit.mOldValue = repairdetailOld.mPendingReturn.ToString();
				audit.mNewValue = repairdetail.mPendingReturn.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mSuccess != repairdetailOld.mSuccess)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "success";
				audit.mOldValue = repairdetailOld.mSuccess.ToString();
				audit.mNewValue = repairdetail.mSuccess.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mAcknowledged != repairdetailOld.mAcknowledged)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "acknowledged";
				audit.mOldValue = repairdetailOld.mAcknowledged.ToString();
				audit.mNewValue = repairdetail.mAcknowledged.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mCategory != repairdetailOld.mCategory)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "category";
				audit.mOldValue = repairdetailOld.mCategory;
				audit.mNewValue = repairdetail.mCategory;
				audit_collection.Add(audit);
			}

			if (repairdetail.mRm2IdServiceUnit != repairdetailOld.mRm2IdServiceUnit)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "rm2_id_service_unit";
				audit.mOldValue = repairdetailOld.mRm2IdServiceUnit.ToString();
				audit.mNewValue = repairdetail.mRm2IdServiceUnit.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mRm2IdOriginalUnit != repairdetailOld.mRm2IdOriginalUnit)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "rm2_id_original_unit";
				audit.mOldValue = repairdetailOld.mRm2IdOriginalUnit.ToString();
				audit.mNewValue = repairdetail.mRm2IdOriginalUnit.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mRm2ReturnId != repairdetailOld.mRm2ReturnId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "rm2_return_id";
				audit.mOldValue = repairdetailOld.mRm2ReturnId.ToString();
				audit.mNewValue = repairdetail.mRm2ReturnId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mOriginalSchedule != repairdetailOld.mOriginalSchedule)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "original_schedule";
				audit.mOldValue = repairdetailOld.mOriginalSchedule.ToString();
				audit.mNewValue = repairdetail.mOriginalSchedule.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mPulloutOriginalUnitDate != repairdetailOld.mPulloutOriginalUnitDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "pullout_original_unit_date";
				audit.mOldValue = repairdetailOld.mPulloutOriginalUnitDate.ToString();
				audit.mNewValue = repairdetail.mPulloutOriginalUnitDate.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mPulloutServiceUnitDate != repairdetailOld.mPulloutServiceUnitDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "pullout_service_unit_date";
				audit.mOldValue = repairdetailOld.mPulloutServiceUnitDate.ToString();
				audit.mNewValue = repairdetail.mPulloutServiceUnitDate.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mFiledById != repairdetailOld.mFiledById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "filed_by_id";
				audit.mOldValue = repairdetailOld.mFiledById.ToString();
				audit.mNewValue = repairdetail.mFiledById.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mFiledByPersonnelId != repairdetailOld.mFiledByPersonnelId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "filed_by_personnel_id";
				audit.mOldValue = repairdetailOld.mFiledByPersonnelId.ToString();
				audit.mNewValue = repairdetail.mFiledByPersonnelId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mCancelled != repairdetailOld.mCancelled)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "cancelled";
				audit.mOldValue = repairdetailOld.mCancelled.ToString();
				audit.mNewValue = repairdetail.mCancelled.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mCancelledRemarks != repairdetailOld.mCancelledRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "cancelled_remarks";
				audit.mOldValue = repairdetailOld.mCancelledRemarks;
				audit.mNewValue = repairdetail.mCancelledRemarks;
				audit_collection.Add(audit);
			}

			if (repairdetail.mMaterialsDate != repairdetailOld.mMaterialsDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "materials_date";
				audit.mOldValue = repairdetailOld.mMaterialsDate.ToString();
				audit.mNewValue = repairdetail.mMaterialsDate.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mMannerOfRepairDate != repairdetailOld.mMannerOfRepairDate)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "manner_of_repair_date";
				audit.mOldValue = repairdetailOld.mMannerOfRepairDate.ToString();
				audit.mNewValue = repairdetail.mMannerOfRepairDate.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mRequireMaterials != repairdetailOld.mRequireMaterials)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "require_materials";
				audit.mOldValue = repairdetailOld.mRequireMaterials.ToString();
				audit.mNewValue = repairdetail.mRequireMaterials.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mFinalApprovedById != repairdetailOld.mFinalApprovedById)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "final_approved_by_id";
				audit.mOldValue = repairdetailOld.mFinalApprovedById.ToString();
				audit.mNewValue = repairdetail.mFinalApprovedById.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mStatus2 != repairdetailOld.mStatus2)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "status2";
				audit.mOldValue = repairdetailOld.mStatus2;
				audit.mNewValue = repairdetail.mStatus2;
				audit_collection.Add(audit);
			}

			if (repairdetail.mRecommendationRemarks != repairdetailOld.mRecommendationRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "recommendation_remarks";
				audit.mOldValue = repairdetailOld.mRecommendationRemarks;
				audit.mNewValue = repairdetail.mRecommendationRemarks;
				audit_collection.Add(audit);
			}

			if (repairdetail.mNotAccomplishedReasonId != repairdetailOld.mNotAccomplishedReasonId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "not_accomplished_reason_id";
				audit.mOldValue = repairdetailOld.mNotAccomplishedReasonId.ToString();
				audit.mNewValue = repairdetail.mNotAccomplishedReasonId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mBackjobReasonId != repairdetailOld.mBackjobReasonId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "backjob_reason_id";
				audit.mOldValue = repairdetailOld.mBackjobReasonId.ToString();
				audit.mNewValue = repairdetail.mBackjobReasonId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mMaterialsUsed != repairdetailOld.mMaterialsUsed)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "materials_used";
				audit.mOldValue = repairdetailOld.mMaterialsUsed.ToString();
				audit.mNewValue = repairdetail.mMaterialsUsed.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mMaterialsReturned != repairdetailOld.mMaterialsReturned)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "materials_returned";
				audit.mOldValue = repairdetailOld.mMaterialsReturned.ToString();
				audit.mNewValue = repairdetail.mMaterialsReturned.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mMessages != repairdetailOld.mMessages)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "messages";
				audit.mOldValue = repairdetailOld.mMessages.ToString();
				audit.mNewValue = repairdetail.mMessages.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mWarranty != repairdetailOld.mWarranty)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "warranty";
				audit.mOldValue = repairdetailOld.mWarranty.ToString();
				audit.mNewValue = repairdetail.mWarranty.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mSupplierId != repairdetailOld.mSupplierId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "supplier_id";
				audit.mOldValue = repairdetailOld.mSupplierId.ToString();
				audit.mNewValue = repairdetail.mSupplierId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mLocationId != repairdetailOld.mLocationId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "location_id";
				audit.mOldValue = repairdetailOld.mLocationId.ToString();
				audit.mNewValue = repairdetail.mLocationId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mUnableToRepair != repairdetailOld.mUnableToRepair)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "unable_to_repair";
				audit.mOldValue = repairdetailOld.mUnableToRepair.ToString();
				audit.mNewValue = repairdetail.mUnableToRepair.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mUnableToRepairReasonId != repairdetailOld.mUnableToRepairReasonId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "unable_to_repair_reason_id";
				audit.mOldValue = repairdetailOld.mUnableToRepairReasonId.ToString();
				audit.mNewValue = repairdetail.mUnableToRepairReasonId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mGatepassHoId != repairdetailOld.mGatepassHoId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "gatepass_ho_id";
				audit.mOldValue = repairdetailOld.mGatepassHoId.ToString();
				audit.mNewValue = repairdetail.mGatepassHoId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mTechnicianStatus != repairdetailOld.mTechnicianStatus)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "technician_status";
				audit.mOldValue = repairdetailOld.mTechnicianStatus.ToString();
				audit.mNewValue = repairdetail.mTechnicianStatus.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mNovNo != repairdetailOld.mNovNo)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "nov_no";
				audit.mOldValue = repairdetailOld.mNovNo;
				audit.mNewValue = repairdetail.mNovNo;
				audit_collection.Add(audit);
			}

			if (repairdetail.mRework != repairdetailOld.mRework)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "rework";
				audit.mOldValue = repairdetailOld.mRework.ToString();
				audit.mNewValue = repairdetail.mRework.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mReworkReasonId != repairdetailOld.mReworkReasonId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "rework_reason_id";
				audit.mOldValue = repairdetailOld.mReworkReasonId.ToString();
				audit.mNewValue = repairdetail.mReworkReasonId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mReworkReasonDetail != repairdetailOld.mReworkReasonDetail)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "rework_reason_detail";
				audit.mOldValue = repairdetailOld.mReworkReasonDetail;
				audit.mNewValue = repairdetail.mReworkReasonDetail;
				audit_collection.Add(audit);
			}

			if (repairdetail.mReworkReview != repairdetailOld.mReworkReview)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "rework_review";
				audit.mOldValue = repairdetailOld.mReworkReview.ToString();
				audit.mNewValue = repairdetail.mReworkReview.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mPersonnelToNovId != repairdetailOld.mPersonnelToNovId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "personnel_to_nov_id";
				audit.mOldValue = repairdetailOld.mPersonnelToNovId.ToString();
				audit.mNewValue = repairdetail.mPersonnelToNovId.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mLessor != repairdetailOld.mLessor)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "lessor";
				audit.mOldValue = repairdetailOld.mLessor.ToString();
				audit.mNewValue = repairdetail.mLessor.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mItsNa != repairdetailOld.mItsNa)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "its_na";
				audit.mOldValue = repairdetailOld.mItsNa.ToString();
				audit.mNewValue = repairdetail.mItsNa.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mItsNaRemarks != repairdetailOld.mItsNaRemarks)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "its_na_remarks";
				audit.mOldValue = repairdetailOld.mItsNaRemarks;
				audit.mNewValue = repairdetail.mItsNaRemarks;
				audit_collection.Add(audit);
			}

			if (repairdetail.mRepairOnsite != repairdetailOld.mRepairOnsite)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "repair_onsite";
				audit.mOldValue = repairdetailOld.mRepairOnsite.ToString();
				audit.mNewValue = repairdetail.mRepairOnsite.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mPriority != repairdetailOld.mPriority)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "priority";
				audit.mOldValue = repairdetailOld.mPriority.ToString();
				audit.mNewValue = repairdetail.mPriority.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mArchive != repairdetailOld.mArchive)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "archive";
				audit.mOldValue = repairdetailOld.mArchive.ToString();
				audit.mNewValue = repairdetail.mArchive.ToString();
				audit_collection.Add(audit);
			}

			if (repairdetail.mRecordId != repairdetailOld.mRecordId)
			{
				audit = new BusinessEntities.Audit();
				LoadCommonData(ref audit, repairdetail);
				audit.mField = "record_id";
				audit.mOldValue = repairdetailOld.mRecordId.ToString();
				audit.mNewValue = repairdetail.mRecordId.ToString();
				audit_collection.Add(audit);
			}

			return audit_collection;
		}

		static void LoadCommonData(ref BusinessEntities.Audit audit, RepairDetail repairdetail)
		{
			audit.mUserFullName = repairdetail.mUserFullName;
			audit.mTableId = (int)(Tables.ptApi_RepairDetail);
			audit.mRowId = repairdetail.mId;
			audit.mAction = 2;
		}
	}
}