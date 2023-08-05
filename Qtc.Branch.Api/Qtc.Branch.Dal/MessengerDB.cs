using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class MessengerDB
	{
		public static Messenger GetItem(int messengerId)
		{
			Messenger messenger = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", messengerId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						messenger = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return messenger;
		}

		public static MessengerCollection GetList(MessengerCriteria messengerCriteria)
		{
			MessengerCollection tempList = new MessengerCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerSearchList";

                Helpers.CreateParameter(myCommand, DbType.String, "@title", messengerCriteria.mTitle);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", messengerCriteria.mEmployeeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new MessengerCollection();
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

		public static int SelectCountForGetList(MessengerCriteria messengerCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerSearchList";

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

		public static int Save(Messenger myMessenger)
		{
			if (!myMessenger.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a messenger in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@title", myMessenger.mTitle);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myMessenger.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myMessenger.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@display_reply_to_everyone", myMessenger.mDisplayReplyToEveryone);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myMessenger.mUserId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myMessenger.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myMessenger);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update messenger as it has been updated by someone else");
				}
				//myMessenger.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spMessengerDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Messenger FillDataRecord(IDataRecord myDataRecord)
		{
			Messenger messenger = new Messenger();

			messenger.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			messenger.mTitle = myDataRecord.GetString(myDataRecord.GetOrdinal("title"));
			messenger.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			messenger.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			messenger.mDisplayReplyToEveryone = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("display_reply_to_everyone"));
			messenger.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			messenger.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			messenger.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));

			return messenger;
		}
	}
}