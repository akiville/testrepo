using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DeviceDB
	{
		public static Device GetItem(int deviceId)
		{
			Device device = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeviceSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", deviceId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						device = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return device;
		}

		public static DeviceCollection GetList(DeviceCriteria deviceCriteria)
		{
			DeviceCollection tempList = new DeviceCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeviceSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", deviceCriteria.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.String, "@status", deviceCriteria.mStatus);
                Helpers.CreateParameter(myCommand, DbType.String, "@device_serial_no", deviceCriteria.mDeviceSerialNo);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DeviceCollection();
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

		public static int SelectCountForGetList(DeviceCriteria deviceCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeviceSearchList";

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

		public static int Save(Device myDevice)
		{
			if (!myDevice.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a device in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeviceInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@device_serial_no", myDevice.mDeviceSerialNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@status", myDevice.mStatus);
				Helpers.CreateParameter(myCommand, DbType.String, "@last_coordinates", myDevice.mLastCoordinates);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myDevice.mDatestamp);
                Helpers.CreateParameter(myCommand, DbType.String, "@employee_id", myDevice.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@last_log_date", myDevice.mLastLogDate);
                Helpers.CreateParameter(myCommand, DbType.String, "@device_notice", myDevice.mDeviceNotice);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_locked", myDevice.mIsLocked);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_kiosk", myDevice.mIsKiosk);
                Helpers.CreateParameter(myCommand, DbType.String, "@welcome_message", myDevice.mWelcomeMessage);

                Helpers.SetSaveParameters(myCommand, myDevice);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update device as it has been updated by someone else");
				}
				//myDevice.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDeviceDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Device FillDataRecord(IDataRecord myDataRecord)
		{
			Device device = new Device();

			device.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			device.mDeviceSerialNo = myDataRecord.GetString(myDataRecord.GetOrdinal("device_serial_no"));
			device.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
			device.mLastCoordinates = myDataRecord.GetString(myDataRecord.GetOrdinal("last_coordinates"));
			device.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			device.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            device.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
            device.mLastLogDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("last_log_date"));
            device.mEmployeeName = myDataRecord.GetString(myDataRecord.GetOrdinal("employee_name"));
            device.mDeviceNotice = myDataRecord.GetString(myDataRecord.GetOrdinal("device_notice"));
            device.mIsLocked = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_locked"));
            device.mIsKiosk = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_kiosk"));
            device.mWelcomeMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("welcome_message"));
            //device.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

            return device;
		}
	}
}