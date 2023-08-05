using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DeviceConfigurationDB
	{
		public static DeviceConfiguration GetItem(int deviceconfigurationId)
		{
			DeviceConfiguration deviceconfiguration = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeviceConfigurationSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", deviceconfigurationId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						deviceconfiguration = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return deviceconfiguration;
		}

		public static DeviceConfigurationCollection GetList(DeviceConfigurationCriteria deviceconfigurationCriteria)
		{
			DeviceConfigurationCollection tempList = new DeviceConfigurationCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeviceConfigurationSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DeviceConfigurationCollection();
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

		public static int SelectCountForGetList(DeviceConfigurationCriteria deviceconfigurationCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeviceConfigurationSearchList";

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

		public static int Save(DeviceConfiguration myDeviceConfiguration)
		{
			if (!myDeviceConfiguration.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a deviceconfiguration in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeviceConfigurationInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@version", myDeviceConfiguration.mVersion);
				Helpers.CreateParameter(myCommand, DbType.String, "@welcome_message", myDeviceConfiguration.mWelcomeMessage);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myDeviceConfiguration.mDatestamp);
				Helpers.CreateParameter(myCommand, DbType.String, "@we_care_message", myDeviceConfiguration.mWeCareMessage);
				Helpers.CreateParameter(myCommand, DbType.String, "@we_achieve_message", myDeviceConfiguration.mWeAchieveMessage);
				Helpers.CreateParameter(myCommand, DbType.String, "@we_ensure_message", myDeviceConfiguration.mWeEnsureMessage);
				Helpers.CreateParameter(myCommand, DbType.String, "@we_improve_message", myDeviceConfiguration.mWeImproveMessage);
				Helpers.CreateParameter(myCommand, DbType.String, "@we_provide_message", myDeviceConfiguration.mWeProvideMessage);
				Helpers.CreateParameter(myCommand, DbType.String, "@we_align_message", myDeviceConfiguration.mWeAlignMessage);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@lmm_attendance_entry_cutoff", myDeviceConfiguration.mLmmAttendanceEntryCutoff);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@intro_letter_cutoff", myDeviceConfiguration.mIntroLetterCutoff);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@other_letter_cutoff", myDeviceConfiguration.mOtherLetterCutoff);

				Helpers.SetSaveParameters(myCommand, myDeviceConfiguration);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update deviceconfiguration as it has been updated by someone else");
				}
				//myDeviceConfiguration.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDeviceConfigurationDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static DeviceConfiguration FillDataRecord(IDataRecord myDataRecord)
		{
			DeviceConfiguration deviceconfiguration = new DeviceConfiguration();

			deviceconfiguration.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			deviceconfiguration.mVersion = myDataRecord.GetInt32(myDataRecord.GetOrdinal("version"));
			deviceconfiguration.mWelcomeMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("welcome_message"));
			deviceconfiguration.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			deviceconfiguration.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			deviceconfiguration.mWeCareMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("we_care_message"));
			deviceconfiguration.mWeAchieveMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("we_achieve_message"));
			deviceconfiguration.mWeEnsureMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("we_ensure_message"));
			deviceconfiguration.mWeImproveMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("we_improve_message"));
			deviceconfiguration.mWeProvideMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("we_provide_message"));
			deviceconfiguration.mWeAlignMessage = myDataRecord.GetString(myDataRecord.GetOrdinal("we_align_message"));
            deviceconfiguration.mLmmAttendanceEntryCutoff = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("lmm_attendance_entry_cutoff"));
            deviceconfiguration.mCashEntryCutoff = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("cash_entry_cutoff"));
            deviceconfiguration.mIntroLetterCutoff = myDataRecord.GetInt32(myDataRecord.GetOrdinal("intro_letter_cutoff"));
            deviceconfiguration.mOtherLetterCutoff = myDataRecord.GetInt32(myDataRecord.GetOrdinal("other_letter_cutoff"));
            return deviceconfiguration;
		}
	}
}