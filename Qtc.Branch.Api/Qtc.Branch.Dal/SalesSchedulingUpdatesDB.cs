using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class SalesSchedulingUpdatesDB
	{
		public static SalesSchedulingUpdates GetItem(int salesschedulingupdatesId)
		{
			SalesSchedulingUpdates salesschedulingupdates = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingUpdatesSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", salesschedulingupdatesId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						salesschedulingupdates = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return salesschedulingupdates;
		}

		public static SalesSchedulingUpdatesCollection GetList(SalesSchedulingUpdatesCriteria salesschedulingupdatesCriteria)
		{
			SalesSchedulingUpdatesCollection tempList = new SalesSchedulingUpdatesCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingUpdatesSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", salesschedulingupdatesCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@date", salesschedulingupdatesCriteria.mDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@sales_scheduling_id", salesschedulingupdatesCriteria.mSalesSchedulingId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new SalesSchedulingUpdatesCollection();
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

		public static int SelectCountForGetList(SalesSchedulingUpdatesCriteria salesschedulingupdatesCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingUpdatesSearchList";

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

		public static int Save(SalesSchedulingUpdates mySalesSchedulingUpdates)
		{
			if (!mySalesSchedulingUpdates.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a salesschedulingupdates in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingUpdatesInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@sales_scheduling_id", mySalesSchedulingUpdates.mSalesSchedulingId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@cutoff_id", mySalesSchedulingUpdates.mCutoffId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch", mySalesSchedulingUpdates.mBranch);
				Helpers.CreateParameter(myCommand, DbType.String, "@lmm", mySalesSchedulingUpdates.mLmm);
				Helpers.CreateParameter(myCommand, DbType.String, "@type", mySalesSchedulingUpdates.mType);
				Helpers.CreateParameter(myCommand, DbType.String, "@branch_area", mySalesSchedulingUpdates.mBranchArea);
				Helpers.CreateParameter(myCommand, DbType.String, "@agency", mySalesSchedulingUpdates.mAgency);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@rate", mySalesSchedulingUpdates.mRate);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@amount", mySalesSchedulingUpdates.mAmount);
				Helpers.CreateParameter(myCommand, DbType.String, "@current_branch_id", mySalesSchedulingUpdates.mCurrentBranchId);
				Helpers.CreateParameter(myCommand, DbType.String, "@id_expired", mySalesSchedulingUpdates.mIdExpired);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@monday_date", mySalesSchedulingUpdates.mMondayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@monday", mySalesSchedulingUpdates.mMonday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@monday_schedule", mySalesSchedulingUpdates.mMondaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@monday_has_id", mySalesSchedulingUpdates.mMondayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@monday_expired_id", mySalesSchedulingUpdates.mMondayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@tuesday_date", mySalesSchedulingUpdates.mTuesdayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@tuesday", mySalesSchedulingUpdates.mTuesday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@tuesday_schedule", mySalesSchedulingUpdates.mTuesdaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@tuesday_has_id", mySalesSchedulingUpdates.mTuesdayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@tuesday_expired_id", mySalesSchedulingUpdates.mTuesdayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@wednesday_date", mySalesSchedulingUpdates.mWednesdayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@wednesday", mySalesSchedulingUpdates.mWednesday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@wednesday_schedule", mySalesSchedulingUpdates.mWednesdaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@wednesday_has_id", mySalesSchedulingUpdates.mWednesdayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@wednesday_expired_id", mySalesSchedulingUpdates.mWednesdayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@thursday_date", mySalesSchedulingUpdates.mThursdayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@thursday", mySalesSchedulingUpdates.mThursday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@thursday_schedule", mySalesSchedulingUpdates.mThursdaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@thursday_has_id", mySalesSchedulingUpdates.mThursdayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@thursday_expired_id", mySalesSchedulingUpdates.mThursdayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@friday_date", mySalesSchedulingUpdates.mFridayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@friday", mySalesSchedulingUpdates.mFriday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@friday_schedule", mySalesSchedulingUpdates.mFridaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@friday_has_id", mySalesSchedulingUpdates.mFridayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@friday_expired_id", mySalesSchedulingUpdates.mFridayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@saturday_date", mySalesSchedulingUpdates.mSaturdayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@saturday", mySalesSchedulingUpdates.mSaturday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@saturday_schedule", mySalesSchedulingUpdates.mSaturdaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@saturday_has_id", mySalesSchedulingUpdates.mSaturdayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@saturday_expired_id", mySalesSchedulingUpdates.mSaturdayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@sunday_date", mySalesSchedulingUpdates.mSundayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@sunday", mySalesSchedulingUpdates.mSunday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@sunday_schedule", mySalesSchedulingUpdates.mSundaySchedule);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@sunday_has_id", mySalesSchedulingUpdates.mSundayHasId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@sunday_expired_id", mySalesSchedulingUpdates.mSundayExpiredId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", mySalesSchedulingUpdates.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@position_schedule_id", mySalesSchedulingUpdates.mPositionScheduleId);
				Helpers.CreateParameter(myCommand, DbType.String, "@time_in", mySalesSchedulingUpdates.mTimeIn);
				Helpers.CreateParameter(myCommand, DbType.String, "@break_out", mySalesSchedulingUpdates.mBreakOut);
				Helpers.CreateParameter(myCommand, DbType.String, "@break_in", mySalesSchedulingUpdates.mBreakIn);
				Helpers.CreateParameter(myCommand, DbType.String, "@time_out", mySalesSchedulingUpdates.mTimeOut);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@agency_id", mySalesSchedulingUpdates.mAgencyId);
				Helpers.CreateParameter(myCommand, DbType.String, "@position", mySalesSchedulingUpdates.mPosition);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@position_id", mySalesSchedulingUpdates.mPositionId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_workable", mySalesSchedulingUpdates.mBranchWorkable);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@continue_to_nextweek", mySalesSchedulingUpdates.mContinueToNextweek);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@assign_to", mySalesSchedulingUpdates.mAssignTo);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@as_is", mySalesSchedulingUpdates.mAsIs);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", mySalesSchedulingUpdates.mStartDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", mySalesSchedulingUpdates.mEndDate);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_confirmed", mySalesSchedulingUpdates.mIsConfirmed);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@confirmed_by", mySalesSchedulingUpdates.mConfirmedBy);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@confirmation_date", mySalesSchedulingUpdates.mConfirmationDate);
                Helpers.CreateParameter(myCommand, DbType.String, "@confirmation_remarks", mySalesSchedulingUpdates.mConfirmationDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", mySalesSchedulingUpdates.mRecordId);

				Helpers.SetSaveParameters(myCommand, mySalesSchedulingUpdates);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				//if (numberOfRecordsAffected == 0)
				//{
				//	throw new DBConcurrencyException("Can't update salesschedulingupdates as it has been updated by someone else");
				//}
				//mySalesSchedulingUpdates.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spSalesSchedulingUpdatesDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

        public static bool UpdateByDate(DateTime date)
        {
            int result = 0;
            using (DbCommand myCommand = AppConfiguration.CreateCommand())
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Qt_spSalesSchedulingUpdatesDeleteSingleItem";
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@date", date);
                myCommand.Connection.Open();
                result = myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();
            }
            return result > 0;
        }

		private static SalesSchedulingUpdates FillDataRecord(IDataRecord myDataRecord)
		{
			SalesSchedulingUpdates salesschedulingupdates = new SalesSchedulingUpdates();

			salesschedulingupdates.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			salesschedulingupdates.mSalesSchedulingId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("sales_scheduling_id"));
			salesschedulingupdates.mCutoffId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("cutoff_id"));
			salesschedulingupdates.mBranch = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch"));
			salesschedulingupdates.mLmm = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm"));
			salesschedulingupdates.mType = myDataRecord.GetString(myDataRecord.GetOrdinal("type"));
			salesschedulingupdates.mBranchArea = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_area"));
			salesschedulingupdates.mAgency = myDataRecord.GetString(myDataRecord.GetOrdinal("agency"));
			salesschedulingupdates.mRate = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("rate"));
			salesschedulingupdates.mAmount = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("amount"));
			salesschedulingupdates.mCurrentBranchId = myDataRecord.GetString(myDataRecord.GetOrdinal("current_branch_id"));
			salesschedulingupdates.mIdExpired = myDataRecord.GetString(myDataRecord.GetOrdinal("id_expired"));
			salesschedulingupdates.mMondayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("monday_date"));
			salesschedulingupdates.mMonday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("monday"));
			salesschedulingupdates.mMondaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("monday_schedule"));
			salesschedulingupdates.mMondayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("monday_has_id"));
			salesschedulingupdates.mMondayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("monday_expired_id"));
			salesschedulingupdates.mTuesdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("tuesday_date"));
			salesschedulingupdates.mTuesday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("tuesday"));
			salesschedulingupdates.mTuesdaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("tuesday_schedule"));
			salesschedulingupdates.mTuesdayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("tuesday_has_id"));
			salesschedulingupdates.mTuesdayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("tuesday_expired_id"));
			salesschedulingupdates.mWednesdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("wednesday_date"));
			salesschedulingupdates.mWednesday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("wednesday"));
			salesschedulingupdates.mWednesdaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("wednesday_schedule"));
			salesschedulingupdates.mWednesdayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("wednesday_has_id"));
			salesschedulingupdates.mWednesdayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("wednesday_expired_id"));
			salesschedulingupdates.mThursdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("thursday_date"));
			salesschedulingupdates.mThursday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("thursday"));
			salesschedulingupdates.mThursdaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("thursday_schedule"));
			salesschedulingupdates.mThursdayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("thursday_has_id"));
			salesschedulingupdates.mThursdayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("thursday_expired_id"));
			salesschedulingupdates.mFridayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("friday_date"));
			salesschedulingupdates.mFriday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("friday"));
			salesschedulingupdates.mFridaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("friday_schedule"));
			salesschedulingupdates.mFridayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("friday_has_id"));
			salesschedulingupdates.mFridayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("friday_expired_id"));
			salesschedulingupdates.mSaturdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("saturday_date"));
			salesschedulingupdates.mSaturday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("saturday"));
			salesschedulingupdates.mSaturdaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("saturday_schedule"));
			salesschedulingupdates.mSaturdayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("saturday_has_id"));
			salesschedulingupdates.mSaturdayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("saturday_expired_id"));
			salesschedulingupdates.mSundayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sunday_date"));
			salesschedulingupdates.mSunday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("sunday"));
			salesschedulingupdates.mSundaySchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("sunday_schedule"));
			salesschedulingupdates.mSundayHasId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("sunday_has_id"));
			salesschedulingupdates.mSundayExpiredId = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("sunday_expired_id"));
			salesschedulingupdates.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			salesschedulingupdates.mPositionScheduleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("position_schedule_id"));
			salesschedulingupdates.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			salesschedulingupdates.mTimeIn = myDataRecord.GetString(myDataRecord.GetOrdinal("time_in"));
			salesschedulingupdates.mBreakOut = myDataRecord.GetString(myDataRecord.GetOrdinal("break_out"));
			salesschedulingupdates.mBreakIn = myDataRecord.GetString(myDataRecord.GetOrdinal("break_in"));
			salesschedulingupdates.mTimeOut = myDataRecord.GetString(myDataRecord.GetOrdinal("time_out"));
			salesschedulingupdates.mAgencyId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("agency_id"));
			salesschedulingupdates.mPosition = myDataRecord.GetString(myDataRecord.GetOrdinal("position"));
			salesschedulingupdates.mPositionId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("position_id"));
			salesschedulingupdates.mBranchWorkable = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_workable"));
			salesschedulingupdates.mContinueToNextweek = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("continue_to_nextweek"));
			salesschedulingupdates.mAssignTo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("assign_to"));
			salesschedulingupdates.mAsIs = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("as_is"));
			salesschedulingupdates.mStartDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("start_date"));
			salesschedulingupdates.mEndDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("end_date"));
            salesschedulingupdates.mMonPer = myDataRecord.GetString(myDataRecord.GetOrdinal("mon_per"));
            salesschedulingupdates.mTuePer = myDataRecord.GetString(myDataRecord.GetOrdinal("tue_per"));
            salesschedulingupdates.mWedPer = myDataRecord.GetString(myDataRecord.GetOrdinal("wed_per"));
            salesschedulingupdates.mThuPer = myDataRecord.GetString(myDataRecord.GetOrdinal("thu_per"));
            salesschedulingupdates.mFriPer = myDataRecord.GetString(myDataRecord.GetOrdinal("fri_per"));
            salesschedulingupdates.mSatPer = myDataRecord.GetString(myDataRecord.GetOrdinal("sat_per"));
            salesschedulingupdates.mSunPer = myDataRecord.GetString(myDataRecord.GetOrdinal("sun_per"));
            salesschedulingupdates.mMonFc = myDataRecord.GetString(myDataRecord.GetOrdinal("mon_fc"));
            salesschedulingupdates.mMonBc = myDataRecord.GetString(myDataRecord.GetOrdinal("mon_bc"));
            salesschedulingupdates.mTueFc = myDataRecord.GetString(myDataRecord.GetOrdinal("tue_fc"));
            salesschedulingupdates.mTueBc = myDataRecord.GetString(myDataRecord.GetOrdinal("tue_bc"));
            salesschedulingupdates.mWedFc = myDataRecord.GetString(myDataRecord.GetOrdinal("wed_fc"));
            salesschedulingupdates.mWedBc = myDataRecord.GetString(myDataRecord.GetOrdinal("wed_bc"));
            salesschedulingupdates.mThuFc = myDataRecord.GetString(myDataRecord.GetOrdinal("thu_fc"));
            salesschedulingupdates.mThuBc = myDataRecord.GetString(myDataRecord.GetOrdinal("thu_bc"));
            salesschedulingupdates.mFriFc = myDataRecord.GetString(myDataRecord.GetOrdinal("fri_fc"));
            salesschedulingupdates.mFriBc = myDataRecord.GetString(myDataRecord.GetOrdinal("fri_bc"));
            salesschedulingupdates.mSatFc = myDataRecord.GetString(myDataRecord.GetOrdinal("sat_fc"));
            salesschedulingupdates.mSatBc = myDataRecord.GetString(myDataRecord.GetOrdinal("sat_bc"));
            salesschedulingupdates.mSunFc = myDataRecord.GetString(myDataRecord.GetOrdinal("sun_fc"));
            salesschedulingupdates.mSunBc = myDataRecord.GetString(myDataRecord.GetOrdinal("sun_bc"));
            salesschedulingupdates.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            salesschedulingupdates.mConcern = myDataRecord.GetString(myDataRecord.GetOrdinal("concern"));
            salesschedulingupdates.mConcernDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("concern_date"));
            salesschedulingupdates.mConcernStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("concern_status"));
            salesschedulingupdates.mIsConfirmed = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_confirmed"));
            salesschedulingupdates.mConfirmedBy = myDataRecord.GetInt32(myDataRecord.GetOrdinal("confirmed_by"));
            salesschedulingupdates.mConfirmationRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("confirmation_remarks"));
            salesschedulingupdates.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
            salesschedulingupdates.mConcernSelDate = myDataRecord.GetString(myDataRecord.GetOrdinal("concern_selected_date"));
            salesschedulingupdates.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
            salesschedulingupdates.mPersonnel = myDataRecord.GetString(myDataRecord.GetOrdinal("personnel"));
            return salesschedulingupdates;
		}
	}
}