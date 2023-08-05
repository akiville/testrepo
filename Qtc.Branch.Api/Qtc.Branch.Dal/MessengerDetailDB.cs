using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class MessengerDetailDB
	{
		public static MessengerDetail GetItem(int messengerdetailId)
		{
			MessengerDetail messengerdetail = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerDetailSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", messengerdetailId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						messengerdetail = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return messengerdetail;
		}

		public static MessengerDetailCollection GetList(MessengerDetailCriteria messengerdetailCriteria)
		{
			MessengerDetailCollection tempList = new MessengerDetailCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerDetailSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new MessengerDetailCollection();
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

		public static int SelectCountForGetList(MessengerDetailCriteria messengerdetailCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerDetailSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@messenger_id", messengerdetailCriteria.mMessengerId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", messengerdetailCriteria.mEmployeeId);

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

		public static int Save(MessengerDetail myMessengerDetail)
		{
			if (!myMessengerDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a messengerdetail in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerDetailInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@messenger_id", myMessengerDetail.mMessengerId);
				Helpers.CreateParameter(myCommand, DbType.String, "@message", myMessengerDetail.mMessage);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myMessengerDetail.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myMessengerDetail.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myMessengerDetail.mDatestamp);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_whisper", myMessengerDetail.mIsWhisper);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@whisper_to", myMessengerDetail.mWhisperTo);

				Helpers.SetSaveParameters(myCommand, myMessengerDetail);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update messengerdetail as it has been updated by someone else");
				}
				//myMessengerDetail.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spMessengerDetailDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static MessengerDetail FillDataRecord(IDataRecord myDataRecord)
		{
			MessengerDetail messengerdetail = new MessengerDetail();

			messengerdetail.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			messengerdetail.mMessengerId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("messenger_id"));
			messengerdetail.mMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("message"));
			messengerdetail.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			messengerdetail.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			messengerdetail.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			messengerdetail.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			messengerdetail.mIsWhisper = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_whisper"));
			messengerdetail.mWhisperTo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("whisper_to"));

			return messengerdetail;
		}
	}
}