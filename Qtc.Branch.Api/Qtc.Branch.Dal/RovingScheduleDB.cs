using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingScheduleDB
	{
		public static RovingSchedule GetItem(int rovingscheduleId)
		{
			RovingSchedule rovingschedule = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingScheduleSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingscheduleId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingschedule = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingschedule;
		}

		public static RovingScheduleCollection GetList(RovingScheduleCriteria rovingscheduleCriteria)
		{
			RovingScheduleCollection tempList = new RovingScheduleCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingScheduleSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingScheduleCollection();
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

		public static int SelectCountForGetList(RovingScheduleCriteria rovingscheduleCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingScheduleSearchList";

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

		public static int Save(RovingSchedule myRovingSchedule)
		{
			if (!myRovingSchedule.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingschedule in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingScheduleInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRovingSchedule.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.String, "@name", myRovingSchedule.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myRovingSchedule.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.String, "@code", myRovingSchedule.mCode);
				Helpers.CreateParameter(myCommand, DbType.String, "@back_color", myRovingSchedule.mBackColor);
				Helpers.CreateParameter(myCommand, DbType.String, "@fore_color", myRovingSchedule.mForeColor);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@blue_slip_no", myRovingSchedule.mBlueSlipNo);

				Helpers.SetSaveParameters(myCommand, myRovingSchedule);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingschedule as it has been updated by someone else");
				}
				//myRovingSchedule.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRovingScheduleDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingSchedule FillDataRecord(IDataRecord myDataRecord)
		{
			RovingSchedule rovingschedule = new RovingSchedule();

			rovingschedule.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingschedule.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			rovingschedule.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			rovingschedule.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			rovingschedule.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			rovingschedule.mCode = myDataRecord.GetString(myDataRecord.GetOrdinal("code"));
			rovingschedule.mBackColor = myDataRecord.GetString(myDataRecord.GetOrdinal("back_color"));
			rovingschedule.mForeColor = myDataRecord.GetString(myDataRecord.GetOrdinal("fore_color"));
			rovingschedule.mBlueSlipNo = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("blue_slip_no"));

			return rovingschedule;
		}
	}
}