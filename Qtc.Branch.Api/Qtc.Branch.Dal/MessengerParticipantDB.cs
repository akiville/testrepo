using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class MessengerParticipantDB
	{
		public static MessengerParticipant GetItem(int messengerparticipantId)
		{
			MessengerParticipant messengerparticipant = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerParticipantSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", messengerparticipantId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						messengerparticipant = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return messengerparticipant;
		}

		public static MessengerParticipantCollection GetList(MessengerParticipantCriteria messengerparticipantCriteria)
		{
			MessengerParticipantCollection tempList = new MessengerParticipantCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerParticipantSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@messenger_id", messengerparticipantCriteria.mEmployeeId);
				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new MessengerParticipantCollection();
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

		public static int SelectCountForGetList(MessengerParticipantCriteria messengerparticipantCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerParticipantSearchList";

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

		public static int Save(MessengerParticipant myMessengerParticipant)
		{
			if (!myMessengerParticipant.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a messengerparticipant in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerParticipantInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myMessengerParticipant.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_admin", myMessengerParticipant.mIsAdmin);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@messenger_id", myMessengerParticipant.mMessengerId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myMessengerParticipant.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myMessengerParticipant);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update messengerparticipant as it has been updated by someone else");
				}
				//myMessengerParticipant.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spMessengerParticipantDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static MessengerParticipant FillDataRecord(IDataRecord myDataRecord)
		{
			MessengerParticipant messengerparticipant = new MessengerParticipant();

			messengerparticipant.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			messengerparticipant.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			messengerparticipant.mIsAdmin = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_admin"));
			messengerparticipant.mMessengerId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("messenger_id"));
			messengerparticipant.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			messengerparticipant.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));

			return messengerparticipant;
		}
	}
}