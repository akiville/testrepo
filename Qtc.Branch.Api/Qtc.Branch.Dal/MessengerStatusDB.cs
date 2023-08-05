using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class MessengerStatusDB
	{
		public static MessengerStatus GetItem(int messengerstatusId)
		{
			MessengerStatus messengerstatus = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerStatusSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", messengerstatusId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						messengerstatus = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return messengerstatus;
		}

		public static MessengerStatusCollection GetList(MessengerStatusCriteria messengerstatusCriteria)
		{
			MessengerStatusCollection tempList = new MessengerStatusCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerStatusSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@messenger_detail_id", messengerstatusCriteria.mMessengerDetailId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", messengerstatusCriteria.mEmployeeId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new MessengerStatusCollection();
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

		public static int SelectCountForGetList(MessengerStatusCriteria messengerstatusCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerStatusSearchList";

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

		public static int Save(MessengerStatus myMessengerStatus)
		{
			if (!myMessengerStatus.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a messengerstatus in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerStatusInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@messenger_detail_id", myMessengerStatus.mMessengerDetailId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myMessengerStatus.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_seen", myMessengerStatus.mIsSeen);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_acknowledge", myMessengerStatus.mIsAcknowledge);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_liked", myMessengerStatus.mIsLiked);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_seen", myMessengerStatus.mDateSeen);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_acknowledge", myMessengerStatus.mDateAcknowledge);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myMessengerStatus.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myMessengerStatus);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update messengerstatus as it has been updated by someone else");
				}
				//myMessengerStatus.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spMessengerStatusDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static MessengerStatus FillDataRecord(IDataRecord myDataRecord)
		{
			MessengerStatus messengerstatus = new MessengerStatus();

			messengerstatus.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			messengerstatus.mMessengerDetailId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("messenger_detail_id"));
			messengerstatus.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			messengerstatus.mIsSeen = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_seen"));
			messengerstatus.mIsAcknowledge = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_acknowledge"));
			messengerstatus.mIsLiked = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_liked"));
			messengerstatus.mDateSeen = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_seen"));
			messengerstatus.mDateAcknowledge = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_acknowledge"));
			messengerstatus.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			messengerstatus.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));

			return messengerstatus;
		}
	}
}