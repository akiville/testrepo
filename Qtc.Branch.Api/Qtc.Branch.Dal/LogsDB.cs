using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class LogsDB
	{
		public static Logs GetItem(int logsId)
		{
			Logs logs = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLogsSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", logsId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						logs = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return logs;
		}

		public static LogsCollection GetList(LogsCriteria logsCriteria)
		{
			LogsCollection tempList = new LogsCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLogsSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new LogsCollection();
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

		public static int SelectCountForGetList(LogsCriteria logsCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLogsSearchList";

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

		public static int Save(Logs myLogs)
		{
			if (!myLogs.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a logs in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLogsInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@module_name", myLogs.mModuleName);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myLogs.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.String, "@device_serial", myLogs.mDeviceSerial);
				Helpers.CreateParameter(myCommand, DbType.String, "@action", myLogs.mAction);
				Helpers.CreateParameter(myCommand, DbType.String, "@desription", myLogs.mDesription);

				Helpers.SetSaveParameters(myCommand, myLogs);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update logs as it has been updated by someone else");
				}
				//myLogs.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spLogsDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Logs FillDataRecord(IDataRecord myDataRecord)
		{
			Logs logs = new Logs();

			logs.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			logs.mModuleName = myDataRecord.GetString(myDataRecord.GetOrdinal("module_name"));
			logs.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			logs.mDeviceSerial = myDataRecord.GetString(myDataRecord.GetOrdinal("device_serial"));
			logs.mAction = myDataRecord.GetString(myDataRecord.GetOrdinal("action"));
			logs.mDesription = myDataRecord.GetString(myDataRecord.GetOrdinal("desription"));
			logs.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return logs;
		}
	}
}