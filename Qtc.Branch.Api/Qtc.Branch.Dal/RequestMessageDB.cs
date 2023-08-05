using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RequestMessageDB
	{
		public static RequestMessage GetItem(int requestmessageId)
		{
			RequestMessage requestmessage = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestMessageSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", requestmessageId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						requestmessage = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return requestmessage;
		}

		public static RequestMessageCollection GetList(RequestMessageCriteria requestmessageCriteria)
		{
			RequestMessageCollection tempList = new RequestMessageCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestMessageSearchList";

                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", requestmessageCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", requestmessageCriteria.mEndDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", requestmessageCriteria.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@topic_id", requestmessageCriteria.mTopicId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", requestmessageCriteria.mUserId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", requestmessageCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@original_message_id", requestmessageCriteria.mOriginalMessageId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RequestMessageCollection();
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

		public static int SelectCountForGetList(RequestMessageCriteria requestmessageCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestMessageSearchList";

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

		public static int Save(RequestMessage myRequestMessage)
		{
			if (!myRequestMessage.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a requestmessage in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestMessageInsertUpdateSingleItem";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myRequestMessage.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myRequestMessage.mUserId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@topic_id", myRequestMessage.mTopicId);
                Helpers.CreateParameter(myCommand, DbType.String, "@message", myRequestMessage.mMessage);
                Helpers.CreateParameter(myCommand, DbType.Binary, "@picture", myRequestMessage.mPicture);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@original_message_id", myRequestMessage.mOriginalMessageId);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_seen", myRequestMessage.mIsSeen);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_seen", myRequestMessage.mDateSeen);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@message_date", myRequestMessage.mMessageDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myRequestMessage.mDatestamp);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myRequestMessage.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@incident_date", myRequestMessage.mIncidentDate);

                Helpers.SetSaveParameters(myCommand, myRequestMessage);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update requestmessage as it has been updated by someone else");
				}
				//myRequestMessage.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRequestMessageDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RequestMessage FillDataRecord(IDataRecord myDataRecord)
		{
			RequestMessage requestmessage = new RequestMessage();

            requestmessage.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
            requestmessage.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
            requestmessage.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
            requestmessage.mTopicId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("topic_id"));
            requestmessage.mMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("message"));
            if (myDataRecord["picture"] != DBNull.Value)
                requestmessage.mPicture = (byte[])myDataRecord.GetValue(myDataRecord.GetOrdinal("picture"));
            else
                requestmessage.mPicture = null;
            //requestmessage.mPicture = myDataRecord.GetBinary(myDataRecord.GetOrdinal("picture"));
            requestmessage.mOriginalMessageId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("original_message_id"));
            requestmessage.mIsSeen = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_seen"));
            requestmessage.mDateSeen = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_seen"));
            requestmessage.mMessageDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("message_date"));
            requestmessage.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
            requestmessage.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            requestmessage.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
            requestmessage.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            requestmessage.mEmployeeName = myDataRecord.GetString(myDataRecord.GetOrdinal("employee_name"));
            requestmessage.mLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_name"));
            requestmessage.mIncidentDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("incident_date"));
            return requestmessage;
		}
	}
}