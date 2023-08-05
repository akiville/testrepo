using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RepairDetailDB
	{
		public static RepairDetail GetItem(int repairdetailId)
		{
			RepairDetail repairdetail = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRepairDetailSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", repairdetailId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						repairdetail = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return repairdetail;
		}

		public static RepairDetailCollection GetList(RepairDetailCriteria repairdetailCriteria)
		{
			RepairDetailCollection tempList = new RepairDetailCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRepairDetailSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RepairDetailCollection();
						while (myReader.Read())
						{
							tempList.Add(FillDataRecord(myReader));
						}
						myReader.Close();
					}
				}
				myCommand.Connection.Close();
			}

			return tempList;
		}

		public static int SelectCountForGetList(RepairDetailCriteria repairdetailCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRepairDetailSearchList";

				DbParameter idParam = myCommand.CreateParameter();
				idParam.DbType = DbType.Int32;
				idParam.Direction = ParameterDirection.InputOutput;
				idParam.ParameterName = "@record_count";
				idParam.Value = 0;
				myCommand.Parameters.Add(idParam);

				myCommand.Connection.Open();
				myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();

				return (int)myCommand.Parameters["@record_count"].Value;
			}
		}

		public static int Save(RepairDetail myRepairDetail)
		{
			if (!myRepairDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a repairdetail in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRepairDetailInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@request_repair_id", myRepairDetail.mRequestRepairId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@time_of_call", myRepairDetail.mTimeOfCall);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_needed", myRepairDetail.mDateNeeded);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@person_who_call_id", myRepairDetail.mPersonWhoCallId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@security_id", myRepairDetail.mSecurityId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@technician_id", myRepairDetail.mTechnicianId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rp_assistant_id", myRepairDetail.mRpAssistantId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@contractor_id", myRepairDetail.mContractorId);
				Helpers.CreateParameter(myCommand, DbType.String, "@emergency_letter_no", myRepairDetail.mEmergencyLetterNo);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@request_letter", myRepairDetail.mRequestLetter);
				Helpers.CreateParameter(myCommand, DbType.String, "@request_letter_staff", myRepairDetail.mRequestLetterStaff);
				Helpers.CreateParameter(myCommand, DbType.String, "@request_letter_details", myRepairDetail.mRequestLetterDetails);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@request_letter_date", myRepairDetail.mRequestLetterDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@request_letter_form_detail_id", myRepairDetail.mRequestLetterFormDetailId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@work_permit", myRepairDetail.mWorkPermit);
				Helpers.CreateParameter(myCommand, DbType.String, "@work_permit_staff", myRepairDetail.mWorkPermitStaff);
				Helpers.CreateParameter(myCommand, DbType.String, "@work_permit_details", myRepairDetail.mWorkPermitDetails);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@work_permit_date", myRepairDetail.mWorkPermitDate);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@work_permit_na", myRepairDetail.mWorkPermitNa);
				Helpers.CreateParameter(myCommand, DbType.String, "@work_permit_no", myRepairDetail.mWorkPermitNo);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@others", myRepairDetail.mOthers);
				Helpers.CreateParameter(myCommand, DbType.String, "@others_staff", myRepairDetail.mOthersStaff);
				Helpers.CreateParameter(myCommand, DbType.String, "@others_details", myRepairDetail.mOthersDetails);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@others_date", myRepairDetail.mOthersDate);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@departed", myRepairDetail.mDeparted);
				Helpers.CreateParameter(myCommand, DbType.String, "@departed_staff", myRepairDetail.mDepartedStaff);
				Helpers.CreateParameter(myCommand, DbType.String, "@departed_details", myRepairDetail.mDepartedDetails);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@departed_date", myRepairDetail.mDepartedDate);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@ce", myRepairDetail.mCe);
				Helpers.CreateParameter(myCommand, DbType.String, "@ce_staff", myRepairDetail.mCeStaff);
				Helpers.CreateParameter(myCommand, DbType.String, "@ce_details", myRepairDetail.mCeDetails);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@ce_date", myRepairDetail.mCeDate);
				Helpers.CreateParameter(myCommand, DbType.Double, "@ce_amount", myRepairDetail.mCeAmount);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@repaired", myRepairDetail.mRepaired);
				Helpers.CreateParameter(myCommand, DbType.String, "@repaired_staff", myRepairDetail.mRepairedStaff);
				Helpers.CreateParameter(myCommand, DbType.String, "@repaired_details", myRepairDetail.mRepairedDetails);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@repaired_date", myRepairDetail.mRepairedDate);
				Helpers.CreateParameter(myCommand, DbType.Double, "@cost_tech_fee", myRepairDetail.mCostTechFee);
				Helpers.CreateParameter(myCommand, DbType.Double, "@cost_ot", myRepairDetail.mCostOt);
				Helpers.CreateParameter(myCommand, DbType.String, "@cost_ot_remarks", myRepairDetail.mCostOtRemarks);
				Helpers.CreateParameter(myCommand, DbType.Double, "@cost_transpo_allowance", myRepairDetail.mCostTranspoAllowance);
				Helpers.CreateParameter(myCommand, DbType.Double, "@cost_meal_allowance", myRepairDetail.mCostMealAllowance);
				Helpers.CreateParameter(myCommand, DbType.Double, "@cost_supplier_fee", myRepairDetail.mCostSupplierFee);
				Helpers.CreateParameter(myCommand, DbType.Double, "@cost_other", myRepairDetail.mCostOther);
				Helpers.CreateParameter(myCommand, DbType.String, "@cost_its_no", myRepairDetail.mCostItsNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@cost_crm_no", myRepairDetail.mCostCrmNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@cost_dr_no_supplier", myRepairDetail.mCostDrNoSupplier);
				Helpers.CreateParameter(myCommand, DbType.String, "@cost_dr_no_other", myRepairDetail.mCostDrNoOther);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@chargeable", myRepairDetail.mChargeable);
				Helpers.CreateParameter(myCommand, DbType.String, "@daf_no", myRepairDetail.mDafNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@persons_caused_damage", myRepairDetail.mPersonsCausedDamage);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@for_reschedule", myRepairDetail.mForReschedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@for_backjob", myRepairDetail.mForBackjob);
				Helpers.CreateParameter(myCommand, DbType.String, "@rfsv_no", myRepairDetail.mRfsvNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@service_clearance_no", myRepairDetail.mServiceClearanceNo);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_filed", myRepairDetail.mDateFiled);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_scheduled", myRepairDetail.mDateScheduled);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_approved", myRepairDetail.mDateApproved);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@pending_materials", myRepairDetail.mPendingMaterials);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@pending_costing", myRepairDetail.mPendingCosting);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@pending_manner_of_repair", myRepairDetail.mPendingMannerOfRepair);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@pending_feedback", myRepairDetail.mPendingFeedback);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@pending_return", myRepairDetail.mPendingReturn);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@success", myRepairDetail.mSuccess);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@acknowledged", myRepairDetail.mAcknowledged);
				Helpers.CreateParameter(myCommand, DbType.String, "@category", myRepairDetail.mCategory);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rm2_id_service_unit", myRepairDetail.mRm2IdServiceUnit);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rm2_id_original_unit", myRepairDetail.mRm2IdOriginalUnit);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rm2_return_id", myRepairDetail.mRm2ReturnId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@original_schedule", myRepairDetail.mOriginalSchedule);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@pullout_original_unit_date", myRepairDetail.mPulloutOriginalUnitDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@pullout_service_unit_date", myRepairDetail.mPulloutServiceUnitDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@filed_by_id", myRepairDetail.mFiledById);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@filed_by_personnel_id", myRepairDetail.mFiledByPersonnelId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@cancelled", myRepairDetail.mCancelled);
				Helpers.CreateParameter(myCommand, DbType.String, "@cancelled_remarks", myRepairDetail.mCancelledRemarks);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@materials_date", myRepairDetail.mMaterialsDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@manner_of_repair_date", myRepairDetail.mMannerOfRepairDate);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@require_materials", myRepairDetail.mRequireMaterials);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@final_approved_by_id", myRepairDetail.mFinalApprovedById);
				Helpers.CreateParameter(myCommand, DbType.String, "@status2", myRepairDetail.mStatus2);
				Helpers.CreateParameter(myCommand, DbType.String, "@recommendation_remarks", myRepairDetail.mRecommendationRemarks);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@not_accomplished_reason_id", myRepairDetail.mNotAccomplishedReasonId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@backjob_reason_id", myRepairDetail.mBackjobReasonId);
				Helpers.CreateParameter(myCommand, DbType.Double, "@materials_used", myRepairDetail.mMaterialsUsed);
				Helpers.CreateParameter(myCommand, DbType.Double, "@materials_returned", myRepairDetail.mMaterialsReturned);
				Helpers.CreateParameter(myCommand, DbType.Byte, "@messages", myRepairDetail.mMessages);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@warranty", myRepairDetail.mWarranty);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@supplier_id", myRepairDetail.mSupplierId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@location_id", myRepairDetail.mLocationId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@unable_to_repair", myRepairDetail.mUnableToRepair);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@unable_to_repair_reason_id", myRepairDetail.mUnableToRepairReasonId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@gatepass_ho_id", myRepairDetail.mGatepassHoId);
				Helpers.CreateParameter(myCommand, DbType.Byte, "@technician_status", myRepairDetail.mTechnicianStatus);
				Helpers.CreateParameter(myCommand, DbType.String, "@nov_no", myRepairDetail.mNovNo);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@rework", myRepairDetail.mRework);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rework_reason_id", myRepairDetail.mReworkReasonId);
				Helpers.CreateParameter(myCommand, DbType.String, "@rework_reason_detail", myRepairDetail.mReworkReasonDetail);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@rework_review", myRepairDetail.mReworkReview);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@personnel_to_nov_id", myRepairDetail.mPersonnelToNovId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@lessor", myRepairDetail.mLessor);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@its_na", myRepairDetail.mItsNa);
				Helpers.CreateParameter(myCommand, DbType.String, "@its_na_remarks", myRepairDetail.mItsNaRemarks);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@repair_onsite", myRepairDetail.mRepairOnsite);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@priority", myRepairDetail.mPriority);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@archive", myRepairDetail.mArchive);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRepairDetail.mRecordId);

				Helpers.SetSaveParameters(myCommand, myRepairDetail);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update repairdetail as it has been updated by someone else");
				}
				//myRepairDetail.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
				result = Helpers.GetBusinessBaseId(myCommand);
				myCommand.Connection.Close();
			}
			return result;
		}

		public static bool Delete(int id)
		{
			int result = 0;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRepairDetailDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RepairDetail FillDataRecord(IDataRecord myDataRecord)
		{
			RepairDetail repairdetail = new RepairDetail();

			repairdetail.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			repairdetail.mRequestRepairId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("request_repair_id"));
			repairdetail.mTimeOfCall = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("time_of_call"));
			repairdetail.mDateNeeded = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_needed"));
			repairdetail.mPersonWhoCallId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("person_who_call_id"));
			repairdetail.mSecurityId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("security_id"));
			repairdetail.mTechnicianId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("technician_id"));
			repairdetail.mRpAssistantId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rp_assistant_id"));
			repairdetail.mContractorId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("contractor_id"));
			repairdetail.mEmergencyLetterNo = myDataRecord.GetString(myDataRecord.GetOrdinal("emergency_letter_no"));
			repairdetail.mRequestLetter = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("request_letter"));
			repairdetail.mRequestLetterStaff = myDataRecord.GetString(myDataRecord.GetOrdinal("request_letter_staff"));
			repairdetail.mRequestLetterDetails = myDataRecord.GetString(myDataRecord.GetOrdinal("request_letter_details"));
			repairdetail.mRequestLetterDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("request_letter_date"));
			repairdetail.mRequestLetterFormDetailId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("request_letter_form_detail_id"));
			repairdetail.mWorkPermit = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("work_permit"));
			repairdetail.mWorkPermitStaff = myDataRecord.GetString(myDataRecord.GetOrdinal("work_permit_staff"));
			repairdetail.mWorkPermitDetails = myDataRecord.GetString(myDataRecord.GetOrdinal("work_permit_details"));
			repairdetail.mWorkPermitDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("work_permit_date"));
			repairdetail.mWorkPermitNa = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("work_permit_na"));
			repairdetail.mWorkPermitNo = myDataRecord.GetString(myDataRecord.GetOrdinal("work_permit_no"));
			repairdetail.mOthers = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("others"));
			repairdetail.mOthersStaff = myDataRecord.GetString(myDataRecord.GetOrdinal("others_staff"));
			repairdetail.mOthersDetails = myDataRecord.GetString(myDataRecord.GetOrdinal("others_details"));
			repairdetail.mOthersDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("others_date"));
			repairdetail.mDeparted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("departed"));
			repairdetail.mDepartedStaff = myDataRecord.GetString(myDataRecord.GetOrdinal("departed_staff"));
			repairdetail.mDepartedDetails = myDataRecord.GetString(myDataRecord.GetOrdinal("departed_details"));
			repairdetail.mDepartedDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("departed_date"));
			repairdetail.mCe = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("ce"));
			repairdetail.mCeStaff = myDataRecord.GetString(myDataRecord.GetOrdinal("ce_staff"));
			repairdetail.mCeDetails = myDataRecord.GetString(myDataRecord.GetOrdinal("ce_details"));
			repairdetail.mCeDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ce_date"));
			repairdetail.mCeAmount = myDataRecord.GetDouble(myDataRecord.GetOrdinal("ce_amount"));
			repairdetail.mRepaired = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("repaired"));
			repairdetail.mRepairedStaff = myDataRecord.GetString(myDataRecord.GetOrdinal("repaired_staff"));
			repairdetail.mRepairedDetails = myDataRecord.GetString(myDataRecord.GetOrdinal("repaired_details"));
			repairdetail.mRepairedDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("repaired_date"));
			repairdetail.mCostTechFee = myDataRecord.GetDouble(myDataRecord.GetOrdinal("cost_tech_fee"));
			repairdetail.mCostOt = myDataRecord.GetDouble(myDataRecord.GetOrdinal("cost_ot"));
			repairdetail.mCostOtRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("cost_ot_remarks"));
			repairdetail.mCostTranspoAllowance = myDataRecord.GetDouble(myDataRecord.GetOrdinal("cost_transpo_allowance"));
			repairdetail.mCostMealAllowance = myDataRecord.GetDouble(myDataRecord.GetOrdinal("cost_meal_allowance"));
			repairdetail.mCostSupplierFee = myDataRecord.GetDouble(myDataRecord.GetOrdinal("cost_supplier_fee"));
			repairdetail.mCostOther = myDataRecord.GetDouble(myDataRecord.GetOrdinal("cost_other"));
			repairdetail.mCostItsNo = myDataRecord.GetString(myDataRecord.GetOrdinal("cost_its_no"));
			repairdetail.mCostCrmNo = myDataRecord.GetString(myDataRecord.GetOrdinal("cost_crm_no"));
			repairdetail.mCostDrNoSupplier = myDataRecord.GetString(myDataRecord.GetOrdinal("cost_dr_no_supplier"));
			repairdetail.mCostDrNoOther = myDataRecord.GetString(myDataRecord.GetOrdinal("cost_dr_no_other"));
			repairdetail.mChargeable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("chargeable"));
			repairdetail.mDafNo = myDataRecord.GetString(myDataRecord.GetOrdinal("daf_no"));
			repairdetail.mPersonsCausedDamage = myDataRecord.GetString(myDataRecord.GetOrdinal("persons_caused_damage"));
			repairdetail.mForReschedule = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("for_reschedule"));
			repairdetail.mForBackjob = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("for_backjob"));
			repairdetail.mRfsvNo = myDataRecord.GetString(myDataRecord.GetOrdinal("rfsv_no"));
			repairdetail.mServiceClearanceNo = myDataRecord.GetString(myDataRecord.GetOrdinal("service_clearance_no"));
			repairdetail.mDateFiled = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_filed"));
			repairdetail.mDateScheduled = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_scheduled"));
			repairdetail.mDateApproved = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_approved"));
			repairdetail.mPendingMaterials = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("pending_materials"));
			repairdetail.mPendingCosting = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("pending_costing"));
			repairdetail.mPendingMannerOfRepair = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("pending_manner_of_repair"));
			repairdetail.mPendingFeedback = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("pending_feedback"));
			repairdetail.mPendingReturn = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("pending_return"));
			repairdetail.mSuccess = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("success"));
			repairdetail.mAcknowledged = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("acknowledged"));
			repairdetail.mCategory = myDataRecord.GetString(myDataRecord.GetOrdinal("category"));
			repairdetail.mRm2IdServiceUnit = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rm2_id_service_unit"));
			repairdetail.mRm2IdOriginalUnit = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rm2_id_original_unit"));
			repairdetail.mRm2ReturnId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rm2_return_id"));
			repairdetail.mOriginalSchedule = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("original_schedule"));
			repairdetail.mPulloutOriginalUnitDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("pullout_original_unit_date"));
			repairdetail.mPulloutServiceUnitDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("pullout_service_unit_date"));
			repairdetail.mFiledById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("filed_by_id"));
			repairdetail.mFiledByPersonnelId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("filed_by_personnel_id"));
			repairdetail.mCancelled = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("cancelled"));
			repairdetail.mCancelledRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("cancelled_remarks"));
			repairdetail.mMaterialsDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("materials_date"));
			repairdetail.mMannerOfRepairDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("manner_of_repair_date"));
			repairdetail.mRequireMaterials = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("require_materials"));
			repairdetail.mFinalApprovedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("final_approved_by_id"));
			repairdetail.mStatus2 = myDataRecord.GetString(myDataRecord.GetOrdinal("status2"));
			repairdetail.mRecommendationRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("recommendation_remarks"));
			repairdetail.mNotAccomplishedReasonId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("not_accomplished_reason_id"));
			repairdetail.mBackjobReasonId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("backjob_reason_id"));
			repairdetail.mMaterialsUsed = myDataRecord.GetDouble(myDataRecord.GetOrdinal("materials_used"));
			repairdetail.mMaterialsReturned = myDataRecord.GetDouble(myDataRecord.GetOrdinal("materials_returned"));
			repairdetail.mMessages = myDataRecord.GetByte(myDataRecord.GetOrdinal("messages"));
			repairdetail.mWarranty = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("warranty"));
			repairdetail.mSupplierId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("supplier_id"));
			repairdetail.mLocationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("location_id"));
			repairdetail.mUnableToRepair = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("unable_to_repair"));
			repairdetail.mUnableToRepairReasonId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("unable_to_repair_reason_id"));
			repairdetail.mGatepassHoId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("gatepass_ho_id"));
			repairdetail.mTechnicianStatus = myDataRecord.GetByte(myDataRecord.GetOrdinal("technician_status"));
			repairdetail.mNovNo = myDataRecord.GetString(myDataRecord.GetOrdinal("nov_no"));
			repairdetail.mRework = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("rework"));
			repairdetail.mReworkReasonId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rework_reason_id"));
			repairdetail.mReworkReasonDetail = myDataRecord.GetString(myDataRecord.GetOrdinal("rework_reason_detail"));
			repairdetail.mReworkReview = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("rework_review"));
			repairdetail.mPersonnelToNovId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("personnel_to_nov_id"));
			repairdetail.mLessor = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("lessor"));
			repairdetail.mItsNa = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("its_na"));
			repairdetail.mItsNaRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("its_na_remarks"));
			repairdetail.mRepairOnsite = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("repair_onsite"));
			repairdetail.mPriority = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("priority"));
			repairdetail.mArchive = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("archive"));
			repairdetail.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			repairdetail.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));

			return repairdetail;
		}
	}
}