using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class LmmAttendanceUpdateDB
	{
		public static LmmAttendanceUpdate GetItem(int lmmattendanceupdateId)
		{
			LmmAttendanceUpdate lmmattendanceupdate = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmAttendanceUpdateSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", lmmattendanceupdateId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						lmmattendanceupdate = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return lmmattendanceupdate;
		}

		public static LmmAttendanceUpdateCollection GetList(LmmAttendanceUpdateCriteria lmmattendanceupdateCriteria)
		{
			LmmAttendanceUpdateCollection tempList = new LmmAttendanceUpdateCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmAttendanceUpdateSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", lmmattendanceupdateCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", lmmattendanceupdateCriteria.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@cutoff_id", lmmattendanceupdateCriteria.mCutoffId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new LmmAttendanceUpdateCollection();
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

		public static int SelectCountForGetList(LmmAttendanceUpdateCriteria lmmattendanceupdateCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmAttendanceUpdateSearchList";

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

		public static int Save(LmmAttendanceUpdate myLmmAttendanceUpdate)
		{
			if (!myLmmAttendanceUpdate.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a lmmattendanceupdate in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmAttendanceUpdateInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@ssu_id", myLmmAttendanceUpdate.mSsuId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@monday_date", myLmmAttendanceUpdate.mMondayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@monday_schedule_id", myLmmAttendanceUpdate.mMondayScheduleId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@tuesday_date", myLmmAttendanceUpdate.mTuesdayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@tuesday_schedule_id", myLmmAttendanceUpdate.mTuesdayScheduleId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@wednesday_date", myLmmAttendanceUpdate.mWednesdayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@wednesday_schedule_id", myLmmAttendanceUpdate.mWednesdayScheduleId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@thursday_date", myLmmAttendanceUpdate.mThursdayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@thursday_schedule_id", myLmmAttendanceUpdate.mThursdayScheduleId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@friday_date", myLmmAttendanceUpdate.mFridayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@friday_schedule_id", myLmmAttendanceUpdate.mFridayScheduleId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@saturday_date", myLmmAttendanceUpdate.mSaturdayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@saturday_schedule_id", myLmmAttendanceUpdate.mSaturdayScheduleId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@sunday_date", myLmmAttendanceUpdate.mSundayDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@sunday_schedule_id", myLmmAttendanceUpdate.mSundayScheduleId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myLmmAttendanceUpdate.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@cutoff_id", myLmmAttendanceUpdate.mCutoffId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myLmmAttendanceUpdate.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myLmmAttendanceUpdate.mRecordId);

				Helpers.SetSaveParameters(myCommand, myLmmAttendanceUpdate);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update lmmattendanceupdate as it has been updated by someone else");
				}
				//myLmmAttendanceUpdate.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spLmmAttendanceUpdateDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static LmmAttendanceUpdate FillDataRecord(IDataRecord myDataRecord)
		{
			LmmAttendanceUpdate lmmattendanceupdate = new LmmAttendanceUpdate();

			lmmattendanceupdate.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			lmmattendanceupdate.mSsuId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ssu_id"));
			lmmattendanceupdate.mMondayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("monday_date"));
			lmmattendanceupdate.mMondayScheduleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("monday_schedule_id"));
			lmmattendanceupdate.mTuesdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("tuesday_date"));
			lmmattendanceupdate.mTuesdayScheduleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("tuesday_schedule_id"));
			lmmattendanceupdate.mWednesdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("wednesday_date"));
			lmmattendanceupdate.mWednesdayScheduleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("wednesday_schedule_id"));
			lmmattendanceupdate.mThursdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("thursday_date"));
			lmmattendanceupdate.mThursdayScheduleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("thursday_schedule_id"));
			lmmattendanceupdate.mFridayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("friday_date"));
			lmmattendanceupdate.mFridayScheduleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("friday_schedule_id"));
			lmmattendanceupdate.mSaturdayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("saturday_date"));
			lmmattendanceupdate.mSaturdayScheduleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("saturday_schedule_id"));
			lmmattendanceupdate.mSundayDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sunday_date"));
			lmmattendanceupdate.mSundayScheduleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("sunday_schedule_id"));
			lmmattendanceupdate.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			lmmattendanceupdate.mCutoffId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("cutoff_id"));
			lmmattendanceupdate.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			lmmattendanceupdate.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			lmmattendanceupdate.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));

			return lmmattendanceupdate;
		}
	}
}