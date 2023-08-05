using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class MessageDB
	{
		public static Message GetItem(int messageId)
		{
			Message message = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessageSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", messageId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						message = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return message;
		}

		public static MessageCollection GetList(MessageCriteria messageCriteria)
		{
			MessageCollection tempList = new MessageCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessageSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@receiver_id", messageCriteria.mReceiverId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@sender_id", messageCriteria.mSenderId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@reply_to_id", messageCriteria.mReplyToId);


                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new MessageCollection();
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

		public static int SelectCountForGetList(MessageCriteria messageCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessageSearchList";

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

		public static int Save(Message myMessage)
		{
			if (!myMessage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a message in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessageInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@message_date", myMessage.mMessageDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@receiver_id", myMessage.mReceiverId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@sender_id", myMessage.mSenderId);
				Helpers.CreateParameter(myCommand, DbType.String, "@message", myMessage.mMessage);
				Helpers.CreateParameter(myCommand, DbType.String, "@title", myMessage.mTitle);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@reply_to_id", myMessage.mReplyToId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myMessage.mDatestamp);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_acknowledge", myMessage.mIsAcknowledge);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@acknowledge_date", myMessage.mAcknowledgeDate);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_read", myMessage.mIsRead);
                Helpers.CreateParameter(myCommand, DbType.String, "@image_link", myMessage.mImageLink);

				Helpers.SetSaveParameters(myCommand, myMessage);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update message as it has been updated by someone else");
				}
				//myMessage.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spMessageDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Message FillDataRecord(IDataRecord myDataRecord)
		{
			Message message = new Message();

			message.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			message.mMessageDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("message_date"));
			message.mReceiverId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("receiver_id"));
			message.mSenderId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("sender_id"));
			message.mMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("message"));
			message.mTitle = myDataRecord.GetString(myDataRecord.GetOrdinal("title"));
			message.mReplyToId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("reply_to_id"));
			message.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			message.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
            message.mSenderName = myDataRecord.GetString(myDataRecord.GetOrdinal("sender_name"));
            message.mReceiverName = myDataRecord.GetString(myDataRecord.GetOrdinal("receiver_name"));
            message.mIsAcknowledge = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_acknowledge"));
            message.mAcknowledgeDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("acknowledge_date"));
            message.mIsRead = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_read"));
            message.mImageLink = myDataRecord.GetString(myDataRecord.GetOrdinal("image_link"));
            message.mParentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("parent_id"));
            return message;
		}
	}
}