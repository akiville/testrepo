using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class ApiVersionDB
	{
		public static ApiVersion GetItem(int apiversionId)
		{
			ApiVersion apiversion = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spApiVersionSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", apiversionId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						apiversion = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return apiversion;
		}

		public static ApiVersionCollection GetList(ApiVersionCriteria apiversionCriteria)
		{
			ApiVersionCollection tempList = new ApiVersionCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spApiVersionSearchList";

                Helpers.CreateParameter(myCommand, DbType.String, "@status", apiversionCriteria.mStatus );

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new ApiVersionCollection();
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

		public static int SelectCountForGetList(ApiVersionCriteria apiversionCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spApiVersionSearchList";

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

		public static int Save(ApiVersion myApiVersion)
		{
			if (!myApiVersion.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a apiversion in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spApiVersionInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@version_code", myApiVersion.mVersionCode);
				Helpers.CreateParameter(myCommand, DbType.String, "@url", myApiVersion.mUrl);
				Helpers.CreateParameter(myCommand, DbType.String, "@update_message", myApiVersion.mUpdateMessage);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myApiVersion.mUserId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myApiVersion.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myApiVersion);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update apiversion as it has been updated by someone else");
				}
				//myApiVersion.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spApiVersionDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static ApiVersion FillDataRecord(IDataRecord myDataRecord)
		{
			ApiVersion apiversion = new ApiVersion();

			apiversion.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			apiversion.mVersionCode = myDataRecord.GetInt32(myDataRecord.GetOrdinal("version_code"));
			apiversion.mUrl = myDataRecord.GetString(myDataRecord.GetOrdinal("url"));
			apiversion.mUpdateMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("update_message"));
			apiversion.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			apiversion.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			apiversion.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			//apiversion.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

			return apiversion;
		}
	}
}