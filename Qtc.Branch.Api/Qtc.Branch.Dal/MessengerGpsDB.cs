using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class MessengerGpsDB
	{
		public static MessengerGps GetItem(int messengergpsId)
		{
			MessengerGps messengergps = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerGpsSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", messengergpsId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						messengergps = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return messengergps;
		}

		public static MessengerGpsCollection GetList(MessengerGpsCriteria messengergpsCriteria)
		{
			MessengerGpsCollection tempList = new MessengerGpsCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerGpsSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "user_id", messengergpsCriteria.mUserId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new MessengerGpsCollection();
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

		public static int SelectCountForGetList(MessengerGpsCriteria messengergpsCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerGpsSearchList";

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

		public static int Save(MessengerGps myMessengerGps)
		{
			if (!myMessengerGps.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a messengergps in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spMessengerGpsInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myMessengerGps.mUserId);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@latitude", myMessengerGps.mLatitude);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@longitude", myMessengerGps.mLongitude);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@device_date", myMessengerGps.mDeviceDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myMessengerGps.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myMessengerGps);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update messengergps as it has been updated by someone else");
				}
				//myMessengerGps.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spMessengerGpsDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static MessengerGps FillDataRecord(IDataRecord myDataRecord)
		{
			MessengerGps messengergps = new MessengerGps();

			messengergps.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			messengergps.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			messengergps.mLatitude = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("latitude"));
			messengergps.mLongitude = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("longitude"));
			messengergps.mDeviceDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("device_date"));
			messengergps.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			messengergps.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return messengergps;
		}
	}
}