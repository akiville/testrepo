using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingTasksDB
	{
		public static RovingTasks GetItem(int rovingtasksId)
		{
			RovingTasks rovingtasks = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingTasksSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingtasksId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingtasks = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingtasks;
		}

		public static RovingTasksCollection GetList(RovingTasksCriteria rovingtasksCriteria)
		{
			RovingTasksCollection tempList = new RovingTasksCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingTasksSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingTasksCollection();
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

		public static int SelectCountForGetList(RovingTasksCriteria rovingtasksCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingTasksSearchList";

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

		public static int Save(RovingTasks myRovingTasks)
		{
			if (!myRovingTasks.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingtasks in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingTasksInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRovingTasks.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.String, "@name", myRovingTasks.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myRovingTasks.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.String, "@code", myRovingTasks.mCode);

				Helpers.SetSaveParameters(myCommand, myRovingTasks);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingtasks as it has been updated by someone else");
				}
				//myRovingTasks.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRovingTasksDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingTasks FillDataRecord(IDataRecord myDataRecord)
		{
			RovingTasks rovingtasks = new RovingTasks();

			rovingtasks.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingtasks.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			rovingtasks.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			rovingtasks.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			rovingtasks.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			rovingtasks.mCode = myDataRecord.GetString(myDataRecord.GetOrdinal("code"));

			return rovingtasks;
		}
	}
}