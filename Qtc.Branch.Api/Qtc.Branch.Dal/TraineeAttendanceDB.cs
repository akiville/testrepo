using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class TraineeAttendanceDB
	{
		public static TraineeAttendance GetItem(int traineeattendanceId)
		{
			TraineeAttendance traineeattendance = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTraineeAttendanceSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", traineeattendanceId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						traineeattendance = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return traineeattendance;
		}

		public static TraineeAttendanceCollection GetList(TraineeAttendanceCriteria traineeattendanceCriteria)
		{
			TraineeAttendanceCollection tempList = new TraineeAttendanceCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTraineeAttendanceSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", traineeattendanceCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", traineeattendanceCriteria.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@date", traineeattendanceCriteria.mDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", traineeattendanceCriteria.mBranchId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new TraineeAttendanceCollection();
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

		public static int SelectCountForGetList(TraineeAttendanceCriteria traineeattendanceCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTraineeAttendanceSearchList";

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

		public static int Save(TraineeAttendance myTraineeAttendance)
		{
			if (!myTraineeAttendance.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a traineeattendance in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spTraineeAttendanceInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myTraineeAttendance.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myTraineeAttendance.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myTraineeAttendance.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@monday_date", myTraineeAttendance.mMondayDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@monday_status", myTraineeAttendance.mMondayStatus);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@tuesday_date", myTraineeAttendance.mTuesdayDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@tuesday_status", myTraineeAttendance.mTuesdayStatus);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@wednesday_date", myTraineeAttendance.mWednesdayDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@wednesday_status", myTraineeAttendance.mWednesdayStatus);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@thursday_date", myTraineeAttendance.mThursdayDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@thursday_status", myTraineeAttendance.mThursdayStatus);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@friday_date", myTraineeAttendance.mFridayDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@friday_status", myTraineeAttendance.mFridayStatus);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@saturday_date", myTraineeAttendance.mSaturdayDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@saturday_status", myTraineeAttendance.mSaturdayStatus);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@sunday_date", myTraineeAttendance.mSundayDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@sunday_status", myTraineeAttendance.mSundayStatus);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@sales_schedule_id", myTraineeAttendance.mSalesScheduleId);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myTraineeAttendance.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myTraineeAttendance.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myTraineeAttendance);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update traineeattendance as it has been updated by someone else");
				}
				//myTraineeAttendance.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spTraineeAttendanceDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static TraineeAttendance FillDataRecord(IDataRecord myDataRecord)
		{
			TraineeAttendance traineeattendance = new TraineeAttendance();

			traineeattendance.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			traineeattendance.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			traineeattendance.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			traineeattendance.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			traineeattendance.mMondayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("monday_date"));
			traineeattendance.mMondayStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("monday_status"));
			traineeattendance.mTuesdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("tuesday_date"));
			traineeattendance.mTuesdayStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("tuesday_status"));
			traineeattendance.mWednesdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("wednesday_date"));
			traineeattendance.mWednesdayStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("wednesday_status"));
			traineeattendance.mThursdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("thursday_date"));
			traineeattendance.mThursdayStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("thursday_status"));
			traineeattendance.mFridayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("friday_date"));
			traineeattendance.mFridayStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("friday_status"));
			traineeattendance.mSaturdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("saturday_date"));
			traineeattendance.mSaturdayStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("saturday_status"));
			traineeattendance.mSundayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sunday_date"));
			traineeattendance.mSundayStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("sunday_status"));
			traineeattendance.mSalesScheduleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("sales_schedule_id"));
			traineeattendance.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			traineeattendance.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			traineeattendance.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
            traineeattendance.mEmployeeName = myDataRecord.GetString(myDataRecord.GetOrdinal("employee_name"));
            traineeattendance.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            traineeattendance.mAgency = myDataRecord.GetString(myDataRecord.GetOrdinal("agency"));
            traineeattendance.mLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_name"));

            return traineeattendance;
		}
	}
}