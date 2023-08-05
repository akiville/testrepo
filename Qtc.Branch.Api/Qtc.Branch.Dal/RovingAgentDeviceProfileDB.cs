using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingAgentDeviceProfileDB
	{
		public static RovingAgentDeviceProfile GetItem(int rovingagentdeviceprofileId)
		{
			RovingAgentDeviceProfile rovingagentdeviceprofile = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingAgentDeviceProfileSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingagentdeviceprofileId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingagentdeviceprofile = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingagentdeviceprofile;
		}

		public static RovingAgentDeviceProfileCollection GetList(RovingAgentDeviceProfileCriteria rovingagentdeviceprofileCriteria)
		{
			RovingAgentDeviceProfileCollection tempList = new RovingAgentDeviceProfileCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingAgentDeviceProfileSearchList";

                Helpers.CreateParameter(myCommand, DbType.String, "@device_id", rovingagentdeviceprofileCriteria.mDeviceId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", rovingagentdeviceprofileCriteria.mUserId);


                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingAgentDeviceProfileCollection();
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

		public static int SelectCountForGetList(RovingAgentDeviceProfileCriteria rovingagentdeviceprofileCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingAgentDeviceProfileSearchList";

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

		public static int Save(RovingAgentDeviceProfile myRovingAgentDeviceProfile)
		{
			if (!myRovingAgentDeviceProfile.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingagentdeviceprofile in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingAgentDeviceProfileInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@device_id", myRovingAgentDeviceProfile.mDeviceId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@kiosk_mode", myRovingAgentDeviceProfile.mKioskMode);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_locked", myRovingAgentDeviceProfile.mIsLocked);
				Helpers.CreateParameter(myCommand, DbType.String, "@mobile_no", myRovingAgentDeviceProfile.mMobileNo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myRovingAgentDeviceProfile.mUserId);
				Helpers.CreateParameter(myCommand, DbType.String, "@last_know_location", myRovingAgentDeviceProfile.mLastKnowLocation);

				Helpers.SetSaveParameters(myCommand, myRovingAgentDeviceProfile);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingagentdeviceprofile as it has been updated by someone else");
				}
				//myRovingAgentDeviceProfile.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "ptApi_spRovingAgentDeviceProfileDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingAgentDeviceProfile FillDataRecord(IDataRecord myDataRecord)
		{
			RovingAgentDeviceProfile rovingagentdeviceprofile = new RovingAgentDeviceProfile();

			rovingagentdeviceprofile.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingagentdeviceprofile.mDeviceId = myDataRecord.GetString(myDataRecord.GetOrdinal("device_id"));
			rovingagentdeviceprofile.mKioskMode = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("kiosk_mode"));
			rovingagentdeviceprofile.mIsLocked = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_locked"));
			rovingagentdeviceprofile.mMobileNo = myDataRecord.GetString(myDataRecord.GetOrdinal("mobile_no"));
			rovingagentdeviceprofile.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			rovingagentdeviceprofile.mLastKnowLocation = myDataRecord.GetString(myDataRecord.GetOrdinal("last_know_location"));
			rovingagentdeviceprofile.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            rovingagentdeviceprofile.mEmployeeName = myDataRecord.GetString(myDataRecord.GetOrdinal("employee_name"));
            rovingagentdeviceprofile.mAgency = myDataRecord.GetString(myDataRecord.GetOrdinal("agency"));
            rovingagentdeviceprofile.mLatitude = myDataRecord.GetString(myDataRecord.GetOrdinal("latitude"));
            rovingagentdeviceprofile.mLongitude = myDataRecord.GetString(myDataRecord.GetOrdinal("longitude"));
            rovingagentdeviceprofile.mDeviceDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("device_date"));
            rovingagentdeviceprofile.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));

			return rovingagentdeviceprofile;
		}
	}
}