using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class LmmAttendanceScheduleTypeDB
	{
		public static LmmAttendanceScheduleType GetItem(int lmmattendancescheduletypeId)
		{
			LmmAttendanceScheduleType lmmattendancescheduletype = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmAttendanceScheduleTypeSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", lmmattendancescheduletypeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						lmmattendancescheduletype = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return lmmattendancescheduletype;
		}

		public static LmmAttendanceScheduleTypeCollection GetList(LmmAttendanceScheduleTypeCriteria lmmattendancescheduletypeCriteria)
		{
			LmmAttendanceScheduleTypeCollection tempList = new LmmAttendanceScheduleTypeCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmAttendanceScheduleTypeSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@for_urgent_schedule_changed", lmmattendancescheduletypeCriteria.mForUrgentScheduleChanged);


                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new LmmAttendanceScheduleTypeCollection();
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

		public static int SelectCountForGetList(LmmAttendanceScheduleTypeCriteria lmmattendancescheduletypeCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmAttendanceScheduleTypeSearchList";

               
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

		public static int Save(LmmAttendanceScheduleType myLmmAttendanceScheduleType)
		{
			if (!myLmmAttendanceScheduleType.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a lmmattendancescheduletype in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmAttendanceScheduleTypeInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@name", myLmmAttendanceScheduleType.mName);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_present", myLmmAttendanceScheduleType.mIsPresent);
				Helpers.CreateParameter(myCommand, DbType.String, "@back_color", myLmmAttendanceScheduleType.mBackColor);
				Helpers.CreateParameter(myCommand, DbType.String, "@fore_color", myLmmAttendanceScheduleType.mForeColor);
				Helpers.CreateParameter(myCommand, DbType.String, "@sms_code", myLmmAttendanceScheduleType.mSmsCode);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@for_urgent_schedule_changed", myLmmAttendanceScheduleType.mForUrgentScheduleChanged);
                Helpers.CreateParameter(myCommand, DbType.String, "@hex_fore_color", myLmmAttendanceScheduleType.mHexBackColor);
                Helpers.CreateParameter(myCommand, DbType.String, "@hex_back_color", myLmmAttendanceScheduleType.mHexBackColor);

				Helpers.SetSaveParameters(myCommand, myLmmAttendanceScheduleType);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update lmmattendancescheduletype as it has been updated by someone else");
				}
				//myLmmAttendanceScheduleType.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spLmmAttendanceScheduleTypeDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static LmmAttendanceScheduleType FillDataRecord(IDataRecord myDataRecord)
		{
			LmmAttendanceScheduleType lmmattendancescheduletype = new LmmAttendanceScheduleType();

			lmmattendancescheduletype.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			lmmattendancescheduletype.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			lmmattendancescheduletype.mIsPresent = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_present"));
			lmmattendancescheduletype.mBackColor = myDataRecord.GetString(myDataRecord.GetOrdinal("back_color"));
			lmmattendancescheduletype.mForeColor = myDataRecord.GetString(myDataRecord.GetOrdinal("fore_color"));
			lmmattendancescheduletype.mSmsCode = myDataRecord.GetString(myDataRecord.GetOrdinal("sms_code"));
			lmmattendancescheduletype.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            lmmattendancescheduletype.mForUrgentScheduleChanged = myDataRecord.GetInt32(myDataRecord.GetOrdinal("for_urgent_schedule_changed"));
            lmmattendancescheduletype.mHexForeColor = myDataRecord.GetString(myDataRecord.GetOrdinal("hex_fore_color"));
            lmmattendancescheduletype.mHexBackColor = myDataRecord.GetString(myDataRecord.GetOrdinal("hex_back_color"));

            return lmmattendancescheduletype;
		}
	}
}