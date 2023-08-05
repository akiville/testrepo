using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DtrLogsOvertimeReasonDB
	{
		public static DtrLogsOvertimeReason GetItem(int dtrlogsovertimereasonId)
		{
			DtrLogsOvertimeReason dtrlogsovertimereason = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDtrLogsOvertimeReasonSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", dtrlogsovertimereasonId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						dtrlogsovertimereason = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return dtrlogsovertimereason;
		}

		public static DtrLogsOvertimeReasonCollection GetList(DtrLogsOvertimeReasonCriteria dtrlogsovertimereasonCriteria)
		{
			DtrLogsOvertimeReasonCollection tempList = new DtrLogsOvertimeReasonCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDtrLogsOvertimeReasonSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DtrLogsOvertimeReasonCollection();
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

		public static int SelectCountForGetList(DtrLogsOvertimeReasonCriteria dtrlogsovertimereasonCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDtrLogsOvertimeReasonSearchList";

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

		public static int Save(DtrLogsOvertimeReason myDtrLogsOvertimeReason)
		{
			if (!myDtrLogsOvertimeReason.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a dtrlogsovertimereason in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDtrLogsOvertimeReasonInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@name", myDtrLogsOvertimeReason.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myDtrLogsOvertimeReason.mRemarks);

				Helpers.SetSaveParameters(myCommand, myDtrLogsOvertimeReason);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update dtrlogsovertimereason as it has been updated by someone else");
				}
				//myDtrLogsOvertimeReason.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDtrLogsOvertimeReasonDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static DtrLogsOvertimeReason FillDataRecord(IDataRecord myDataRecord)
		{
			DtrLogsOvertimeReason dtrlogsovertimereason = new DtrLogsOvertimeReason();

			dtrlogsovertimereason.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			dtrlogsovertimereason.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			dtrlogsovertimereason.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			dtrlogsovertimereason.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return dtrlogsovertimereason;
		}
	}
}