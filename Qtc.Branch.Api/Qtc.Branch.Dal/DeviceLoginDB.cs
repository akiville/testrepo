using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DeviceLoginDB
	{
		public static DeviceLogin GetItem(int deviceloginId)
		{
			DeviceLogin devicelogin = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeviceLoginSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", deviceloginId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						devicelogin = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return devicelogin;
		}

		public static DeviceLoginCollection GetList(DeviceLoginCriteria deviceloginCriteria)
		{
			DeviceLoginCollection tempList = new DeviceLoginCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeviceLoginSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@id", deviceloginCriteria.mId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", deviceloginCriteria.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.String, "@status", deviceloginCriteria.mStatus);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", deviceloginCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", deviceloginCriteria.mEndDate);

            
				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DeviceLoginCollection();
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

		public static int SelectCountForGetList(DeviceLoginCriteria deviceloginCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeviceLoginSearchList";

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

		public static int Save(DeviceLogin myDeviceLogin)
		{
			if (!myDeviceLogin.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a devicelogin in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeviceLoginInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myDeviceLogin.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.String, "@device_serial", myDeviceLogin.mDeviceSerial);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@login_date", myDeviceLogin.mLoginDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@status", myDeviceLogin.mStatus);
                Helpers.CreateParameter(myCommand, DbType.String, "@device_location", myDeviceLogin.mDeviceLocation);
                Helpers.CreateParameter(myCommand, DbType.Decimal, "@latitude", myDeviceLogin.mLatitude);
                Helpers.CreateParameter(myCommand, DbType.Decimal, "@longitude", myDeviceLogin.mLongitude);

                Helpers.SetSaveParameters(myCommand, myDeviceLogin);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update devicelogin as it has been updated by someone else");
				}
				//myDeviceLogin.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDeviceLoginDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static DeviceLogin FillDataRecord(IDataRecord myDataRecord)
		{
			DeviceLogin devicelogin = new DeviceLogin();

			devicelogin.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id")); 
			devicelogin.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			devicelogin.mDeviceSerial = myDataRecord.GetString(myDataRecord.GetOrdinal("device_serial"));
			devicelogin.mLoginDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("login_date"));
			devicelogin.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
			devicelogin.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            devicelogin.mDeviceLocation = myDataRecord.GetString(myDataRecord.GetOrdinal("device_location"));
            devicelogin.mEmployeeName = myDataRecord.GetString(myDataRecord.GetOrdinal("employee_name"));
            devicelogin.mLatitude = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("latitude"));
            devicelogin.mLongitude = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("longitude"));

            return devicelogin;
		}
	}
}