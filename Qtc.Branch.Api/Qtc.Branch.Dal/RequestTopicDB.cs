using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RequestTopicDB
	{
		public static RequestTopic GetItem(int requesttopicId)
		{
			RequestTopic requesttopic = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestTopicSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", requesttopicId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						requesttopic = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return requesttopic;
		}

		public static RequestTopicCollection GetList(RequestTopicCriteria requesttopicCriteria)
		{
			RequestTopicCollection tempList = new RequestTopicCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestTopicSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RequestTopicCollection();
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

		public static int SelectCountForGetList(RequestTopicCriteria requesttopicCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestTopicSearchList";

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

		public static int Save(RequestTopic myRequestTopic)
		{
			if (!myRequestTopic.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a requesttopic in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestTopicInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@name", myRequestTopic.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@description", myRequestTopic.mDescription);
				Helpers.CreateParameter(myCommand, DbType.String, "@category", myRequestTopic.mCategory);
				Helpers.CreateParameter(myCommand, DbType.String, "@welcome_message", myRequestTopic.mWelcomeMessage);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myRequestTopic.mUserId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myRequestTopic.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myRequestTopic);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update requesttopic as it has been updated by someone else");
				}
				//myRequestTopic.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRequestTopicDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RequestTopic FillDataRecord(IDataRecord myDataRecord)
		{
			RequestTopic requesttopic = new RequestTopic();

			requesttopic.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			requesttopic.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			requesttopic.mDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("description"));
			requesttopic.mCategory = myDataRecord.GetString(myDataRecord.GetOrdinal("category"));
			requesttopic.mWelcomeMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("welcome_message"));
			requesttopic.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			requesttopic.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			requesttopic.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));

			return requesttopic;
		}
	}
}