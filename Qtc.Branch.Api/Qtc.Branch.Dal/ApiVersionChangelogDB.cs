using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class ApiVersionChangelogDB
	{
		public static ApiVersionChangelog GetItem(int apiversionchangelogId)
		{
			ApiVersionChangelog apiversionchangelog = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spApiVersionChangelogSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", apiversionchangelogId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						apiversionchangelog = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return apiversionchangelog;
		}

		public static ApiVersionChangelogCollection GetList(ApiVersionChangelogCriteria apiversionchangelogCriteria)
		{
			ApiVersionChangelogCollection tempList = new ApiVersionChangelogCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spApiVersionChangelogSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@version_id", apiversionchangelogCriteria.mVersionId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new ApiVersionChangelogCollection();
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

		public static int SelectCountForGetList(ApiVersionChangelogCriteria apiversionchangelogCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spApiVersionChangelogSearchList";

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

		public static int Save(ApiVersionChangelog myApiVersionChangelog)
		{
			if (!myApiVersionChangelog.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a apiversionchangelog in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spApiVersionChangelogInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@version_id", myApiVersionChangelog.mVersionId);
				Helpers.CreateParameter(myCommand, DbType.String, "@changelog", myApiVersionChangelog.mChangelog);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myApiVersionChangelog.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.String, "@status", myApiVersionChangelog.mStatus);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myApiVersionChangelog.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myApiVersionChangelog);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update apiversionchangelog as it has been updated by someone else");
				}
				//myApiVersionChangelog.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spApiVersionChangelogDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static ApiVersionChangelog FillDataRecord(IDataRecord myDataRecord)
		{
			ApiVersionChangelog apiversionchangelog = new ApiVersionChangelog();

			apiversionchangelog.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			apiversionchangelog.mVersionId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("version_id"));
			apiversionchangelog.mChangelog = myDataRecord.GetString(myDataRecord.GetOrdinal("changelog"));
			apiversionchangelog.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			apiversionchangelog.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
			apiversionchangelog.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			apiversionchangelog.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			//apiversionchangelog.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

			return apiversionchangelog;
		}
	}
}