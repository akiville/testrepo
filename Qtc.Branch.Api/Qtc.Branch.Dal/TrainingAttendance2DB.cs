using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class TrainingAttendance2DB
	{
		public static TrainingAttendance2 GetItem(int trainingattendance2Id)
		{
			TrainingAttendance2 trainingattendance2 = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendance2SelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", trainingattendance2Id);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						trainingattendance2 = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return trainingattendance2;
		}

		public static TrainingAttendance2Collection GetList(TrainingAttendance2Criteria trainingattendance2Criteria)
		{
			TrainingAttendance2Collection tempList = new TrainingAttendance2Collection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendance2SearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", trainingattendance2Criteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@sales_date", trainingattendance2Criteria.mSalesDate);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new TrainingAttendance2Collection();
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

		public static int SelectCountForGetList(TrainingAttendance2Criteria trainingattendance2Criteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendance2SearchList";

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

		public static int Save(TrainingAttendance2 myTrainingAttendance2)
		{
			if (!myTrainingAttendance2.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a trainingattendance2 in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTrainingAttendance2InsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@sales_scheduling_id", myTrainingAttendance2.mSalesSchedulingId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@cutoff_id", myTrainingAttendance2.mCutoffId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch", myTrainingAttendance2.mBranch);
				Helpers.CreateParameter(myCommand, DbType.String, "@lmm", myTrainingAttendance2.mLmm);
				Helpers.CreateParameter(myCommand, DbType.String, "@type", myTrainingAttendance2.mType);
				Helpers.CreateParameter(myCommand, DbType.String, "@branch_area", myTrainingAttendance2.mBranchArea);
				Helpers.CreateParameter(myCommand, DbType.String, "@agency", myTrainingAttendance2.mAgency);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@rate", myTrainingAttendance2.mRate);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@amount", myTrainingAttendance2.mAmount);
				Helpers.CreateParameter(myCommand, DbType.String, "@current_branch_id", myTrainingAttendance2.mCurrentBranchId);
				Helpers.CreateParameter(myCommand, DbType.String, "@id_expired", myTrainingAttendance2.mIdExpired);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@monday_date", myTrainingAttendance2.mMondayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@monday", myTrainingAttendance2.mMonday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@monday_schedule", myTrainingAttendance2.mMondaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@monday_has_id", myTrainingAttendance2.mMondayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@monday_expired_id", myTrainingAttendance2.mMondayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@tuesday_date", myTrainingAttendance2.mTuesdayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@tuesday", myTrainingAttendance2.mTuesday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@tuesday_schedule", myTrainingAttendance2.mTuesdaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@tuesday_has_id", myTrainingAttendance2.mTuesdayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@tuesday_expired_id", myTrainingAttendance2.mTuesdayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@wednesday_date", myTrainingAttendance2.mWednesdayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@wednesday", myTrainingAttendance2.mWednesday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@wednesday_schedule", myTrainingAttendance2.mWednesdaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@wednesday_has_id", myTrainingAttendance2.mWednesdayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@wednesday_expired_id", myTrainingAttendance2.mWednesdayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@thursday_date", myTrainingAttendance2.mThursdayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@thursday", myTrainingAttendance2.mThursday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@thursday_schedule", myTrainingAttendance2.mThursdaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@thursday_has_id", myTrainingAttendance2.mThursdayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@thursday_expired_id", myTrainingAttendance2.mThursdayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@friday_date", myTrainingAttendance2.mFridayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@friday", myTrainingAttendance2.mFriday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@friday_schedule", myTrainingAttendance2.mFridaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@friday_has_id", myTrainingAttendance2.mFridayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@friday_expired_id", myTrainingAttendance2.mFridayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@saturday_date", myTrainingAttendance2.mSaturdayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@saturday", myTrainingAttendance2.mSaturday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@saturday_schedule", myTrainingAttendance2.mSaturdaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@saturday_has_id", myTrainingAttendance2.mSaturdayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@saturday_expired_id", myTrainingAttendance2.mSaturdayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@sunday_date", myTrainingAttendance2.mSundayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@sunday", myTrainingAttendance2.mSunday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@sunday_schedule", myTrainingAttendance2.mSundaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@sunday_has_id", myTrainingAttendance2.mSundayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@sunday_expired_id", myTrainingAttendance2.mSundayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myTrainingAttendance2.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@position_schedule_id", myTrainingAttendance2.mPositionScheduleId);
				Helpers.CreateParameter(myCommand, DbType.String, "@time_in", myTrainingAttendance2.mTimeIn);
				Helpers.CreateParameter(myCommand, DbType.String, "@break_out", myTrainingAttendance2.mBreakOut);
				Helpers.CreateParameter(myCommand, DbType.String, "@break_in", myTrainingAttendance2.mBreakIn);
				Helpers.CreateParameter(myCommand, DbType.String, "@time_out", myTrainingAttendance2.mTimeOut);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@agency_id", myTrainingAttendance2.mAgencyId);
				Helpers.CreateParameter(myCommand, DbType.String, "@position", myTrainingAttendance2.mPosition);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@position_id", myTrainingAttendance2.mPositionId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_workable", myTrainingAttendance2.mBranchWorkable);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@continue_to_nextweek", myTrainingAttendance2.mContinueToNextweek);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@assign_to", myTrainingAttendance2.mAssignTo);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@as_is", myTrainingAttendance2.mAsIs);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", myTrainingAttendance2.mStartDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", myTrainingAttendance2.mEndDate);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_confirmed", myTrainingAttendance2.mIsConfirmed);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@confirmed_by", myTrainingAttendance2.mConfirmedBy);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@confirmation_date", myTrainingAttendance2.mConfirmationDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@confirmation_remarks", myTrainingAttendance2.mConfirmationRemarks);

				Helpers.SetSaveParameters(myCommand, myTrainingAttendance2);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update trainingattendance2 as it has been updated by someone else");
				}
				//myTrainingAttendance2.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spTrainingAttendance2DeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static TrainingAttendance2 FillDataRecord(IDataRecord myDataRecord)
		{
			TrainingAttendance2 trainingattendance2 = new TrainingAttendance2();

			trainingattendance2.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			trainingattendance2.mSalesSchedulingId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("sales_scheduling_id"));
			trainingattendance2.mCutoffId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("cutoff_id"));
			trainingattendance2.mBranch = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch"));
			trainingattendance2.mLmm = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm"));
			trainingattendance2.mType = myDataRecord.GetString(myDataRecord.GetOrdinal("type"));
			trainingattendance2.mBranchArea = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_area"));
			trainingattendance2.mAgency = myDataRecord.GetString(myDataRecord.GetOrdinal("agency"));
			trainingattendance2.mRate = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("rate"));
			trainingattendance2.mAmount = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("amount"));
			trainingattendance2.mCurrentBranchId = myDataRecord.GetString(myDataRecord.GetOrdinal("current_branch_id"));
			trainingattendance2.mIdExpired = myDataRecord.GetString(myDataRecord.GetOrdinal("id_expired"));
			trainingattendance2.mMondayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("monday_date"));
			trainingattendance2.mMonday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("monday"));
			trainingattendance2.mMondaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("monday_schedule"));
			trainingattendance2.mMondayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("monday_has_id"));
			trainingattendance2.mMondayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("monday_expired_id"));
			trainingattendance2.mTuesdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("tuesday_date"));
			trainingattendance2.mTuesday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("tuesday"));
			trainingattendance2.mTuesdaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("tuesday_schedule"));
			trainingattendance2.mTuesdayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("tuesday_has_id"));
			trainingattendance2.mTuesdayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("tuesday_expired_id"));
			trainingattendance2.mWednesdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("wednesday_date"));
			trainingattendance2.mWednesday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("wednesday"));
			trainingattendance2.mWednesdaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("wednesday_schedule"));
			trainingattendance2.mWednesdayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("wednesday_has_id"));
			trainingattendance2.mWednesdayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("wednesday_expired_id"));
			trainingattendance2.mThursdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("thursday_date"));
			trainingattendance2.mThursday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("thursday"));
			trainingattendance2.mThursdaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("thursday_schedule"));
			trainingattendance2.mThursdayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("thursday_has_id"));
			trainingattendance2.mThursdayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("thursday_expired_id"));
			trainingattendance2.mFridayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("friday_date"));
			trainingattendance2.mFriday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("friday"));
			trainingattendance2.mFridaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("friday_schedule"));
			trainingattendance2.mFridayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("friday_has_id"));
			trainingattendance2.mFridayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("friday_expired_id"));
			trainingattendance2.mSaturdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("saturday_date"));
			trainingattendance2.mSaturday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("saturday"));
			trainingattendance2.mSaturdaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("saturday_schedule"));
			trainingattendance2.mSaturdayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("saturday_has_id"));
			trainingattendance2.mSaturdayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("saturday_expired_id"));
			trainingattendance2.mSundayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sunday_date"));
			trainingattendance2.mSunday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("sunday"));
			trainingattendance2.mSundaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("sunday_schedule"));
			trainingattendance2.mSundayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("sunday_has_id"));
			trainingattendance2.mSundayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("sunday_expired_id"));
			trainingattendance2.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			trainingattendance2.mPositionScheduleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("position_schedule_id"));
			trainingattendance2.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			trainingattendance2.mTimeIn = myDataRecord.GetString(myDataRecord.GetOrdinal("time_in"));
			trainingattendance2.mBreakOut = myDataRecord.GetString(myDataRecord.GetOrdinal("break_out"));
			trainingattendance2.mBreakIn = myDataRecord.GetString(myDataRecord.GetOrdinal("break_in"));
			trainingattendance2.mTimeOut = myDataRecord.GetString(myDataRecord.GetOrdinal("time_out"));
			trainingattendance2.mAgencyId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("agency_id"));
			trainingattendance2.mPosition = myDataRecord.GetString(myDataRecord.GetOrdinal("position"));
			trainingattendance2.mPositionId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("position_id"));
			trainingattendance2.mBranchWorkable = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_workable"));
			trainingattendance2.mContinueToNextweek = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("continue_to_nextweek"));
			trainingattendance2.mAssignTo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("assign_to"));
			trainingattendance2.mAsIs = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("as_is"));
			trainingattendance2.mStartDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("start_date"));
			trainingattendance2.mEndDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("end_date"));
			trainingattendance2.mIsConfirmed = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_confirmed"));
			trainingattendance2.mConfirmedBy = myDataRecord.GetInt32(myDataRecord.GetOrdinal("confirmed_by"));
			trainingattendance2.mConfirmationDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("confirmation_date"));
			trainingattendance2.mConfirmationRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("confirmation_remarks"));
            trainingattendance2.mEmployee = myDataRecord.GetString(myDataRecord.GetOrdinal("employee"));
            trainingattendance2.mMotherBranch = myDataRecord.GetString(myDataRecord.GetOrdinal("mother_branch"));
            trainingattendance2.mCellphoneNo = myDataRecord.GetString(myDataRecord.GetOrdinal("cellphone_no"));
            trainingattendance2.mMon = myDataRecord.GetString(myDataRecord.GetOrdinal("mon"));
            trainingattendance2.mTue = myDataRecord.GetString(myDataRecord.GetOrdinal("tue"));
            trainingattendance2.mWed = myDataRecord.GetString(myDataRecord.GetOrdinal("wed"));
            trainingattendance2.mThur = myDataRecord.GetString(myDataRecord.GetOrdinal("thu"));
            trainingattendance2.mFri = myDataRecord.GetString(myDataRecord.GetOrdinal("fri"));
            trainingattendance2.mSat = myDataRecord.GetString(myDataRecord.GetOrdinal("sat"));
            trainingattendance2.mSun = myDataRecord.GetString(myDataRecord.GetOrdinal("sun"));
            trainingattendance2.mMonFc = myDataRecord.GetString(myDataRecord.GetOrdinal("mon_fc"));
            trainingattendance2.mMonBc = myDataRecord.GetString(myDataRecord.GetOrdinal("mon_bc"));
            trainingattendance2.mTueFc = myDataRecord.GetString(myDataRecord.GetOrdinal("tue_fc"));
            trainingattendance2.mTueBc = myDataRecord.GetString(myDataRecord.GetOrdinal("tue_bc"));
            trainingattendance2.mWedFc = myDataRecord.GetString(myDataRecord.GetOrdinal("wed_fc"));
            trainingattendance2.mWedBc = myDataRecord.GetString(myDataRecord.GetOrdinal("wed_bc"));
            trainingattendance2.mThuFc = myDataRecord.GetString(myDataRecord.GetOrdinal("thu_fc"));
            trainingattendance2.mThuBc = myDataRecord.GetString(myDataRecord.GetOrdinal("thu_bc"));
            trainingattendance2.mFriFc = myDataRecord.GetString(myDataRecord.GetOrdinal("fri_fc"));
            trainingattendance2.mFriBc = myDataRecord.GetString(myDataRecord.GetOrdinal("fri_bc"));
            trainingattendance2.mSatFc = myDataRecord.GetString(myDataRecord.GetOrdinal("sat_fc"));
            trainingattendance2.mSatBc = myDataRecord.GetString(myDataRecord.GetOrdinal("sat_bc"));
            trainingattendance2.mSunFc = myDataRecord.GetString(myDataRecord.GetOrdinal("sun_fc"));
            trainingattendance2.mSunBc = myDataRecord.GetString(myDataRecord.GetOrdinal("sun_bc"));
            trainingattendance2.mLastTrainingDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("last_training_date"));
            trainingattendance2.mCutoffId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("cutoff_id"));
            return trainingattendance2;
		}
	}
}