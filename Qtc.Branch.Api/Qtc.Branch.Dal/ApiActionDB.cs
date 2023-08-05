using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class ApiActionDB
	{
		public static ApiAction GetItem(int apiactionId)
		{
			ApiAction apiaction = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spApiActionSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", apiactionId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						apiaction = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return apiaction;
		}

		public static ApiActionCollection GetList(ApiActionCriteria apiactionCriteria)
		{
			ApiActionCollection tempList = new ApiActionCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spApiActionSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", apiactionCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@action_taken", apiactionCriteria.mActionTaken);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new ApiActionCollection();
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

		public static int SelectCountForGetList(ApiActionCriteria apiactionCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spApiActionSearchList";

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

		public static int Save(ApiAction myApiAction)
		{
			if (!myApiAction.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a apiaction in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spApiActionInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@module_name", myApiAction.mModuleName);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myApiAction.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myApiAction.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@action_taken", myApiAction.mActionTaken);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@action_date", myApiAction.mActionDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myApiAction.mDatestamp);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myApiAction.mUserId);

				Helpers.SetSaveParameters(myCommand, myApiAction);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update apiaction as it has been updated by someone else");
				}
				//myApiAction.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spApiActionDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static ApiAction FillDataRecord(IDataRecord myDataRecord)
		{
			ApiAction apiaction = new ApiAction();

			apiaction.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			apiaction.mModuleName = myDataRecord.GetString(myDataRecord.GetOrdinal("module_name"));
			apiaction.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			apiaction.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			apiaction.mActionTaken = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("action_taken"));
			apiaction.mActionDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("action_date"));
			apiaction.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			apiaction.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			apiaction.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			//apiaction.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

			return apiaction;
		}
	}
}