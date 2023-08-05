using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingAgentLoginDB
	{
		public static RovingAgentLogin GetItem(int rovingagentloginId)
		{
			RovingAgentLogin rovingagentlogin = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingAgentLoginSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingagentloginId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingagentlogin = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingagentlogin;
		}

		public static RovingAgentLoginCollection GetList(RovingAgentLoginCriteria rovingagentloginCriteria)
		{
			RovingAgentLoginCollection tempList = new RovingAgentLoginCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingAgentLoginSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_id", rovingagentloginCriteria.mRpsId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_agent_id", rovingagentloginCriteria.mRovingAgentId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", rovingagentloginCriteria.mBranchId);


				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingAgentLoginCollection();
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

		public static int SelectCountForGetList(RovingAgentLoginCriteria rovingagentloginCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingAgentLoginSearchList";

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

		public static int Save(RovingAgentLogin myRovingAgentLogin)
		{
			if (!myRovingAgentLogin.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingagentlogin in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingAgentLoginInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_agent_id", myRovingAgentLogin.mRovingAgentId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myRovingAgentLogin.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_id", myRovingAgentLogin.mRpsId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@time_in", myRovingAgentLogin.mTimeIn);
				Helpers.CreateParameter(myCommand, DbType.String, "@time_in_latitude", myRovingAgentLogin.mTimeInLatitude);
				Helpers.CreateParameter(myCommand, DbType.String, "@time_in_longitude", myRovingAgentLogin.mTimeInLongitude);
				Helpers.CreateParameter(myCommand, DbType.String, "@time_in_address", myRovingAgentLogin.mTimeInAddress);
				Helpers.CreateParameter(myCommand, DbType.String, "@time_out", myRovingAgentLogin.mTimeOut);
				Helpers.CreateParameter(myCommand, DbType.String, "@time_out_latitude", myRovingAgentLogin.mTimeOutLatitude);
				Helpers.CreateParameter(myCommand, DbType.String, "@time_out_longitude", myRovingAgentLogin.mTimeOutLongitude);
				Helpers.CreateParameter(myCommand, DbType.String, "@time_out_address", myRovingAgentLogin.mTimeOutAddress);

				Helpers.SetSaveParameters(myCommand, myRovingAgentLogin);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingagentlogin as it has been updated by someone else");
				}
				//myRovingAgentLogin.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "ptApi_spRovingAgentLoginDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingAgentLogin FillDataRecord(IDataRecord myDataRecord)
		{
			RovingAgentLogin rovingagentlogin = new RovingAgentLogin();

			rovingagentlogin.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingagentlogin.mRovingAgentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_agent_id"));
			rovingagentlogin.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			rovingagentlogin.mRpsId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rps_id"));
			rovingagentlogin.mTimeIn = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("time_in"));
			rovingagentlogin.mTimeInLatitude = myDataRecord.GetString(myDataRecord.GetOrdinal("time_in_latitude"));
			rovingagentlogin.mTimeInLongitude = myDataRecord.GetString(myDataRecord.GetOrdinal("time_in_longitude"));
			rovingagentlogin.mTimeInAddress = myDataRecord.GetString(myDataRecord.GetOrdinal("time_in_address"));
			rovingagentlogin.mTimeOut = myDataRecord.GetString(myDataRecord.GetOrdinal("time_out"));
			rovingagentlogin.mTimeOutLatitude = myDataRecord.GetString(myDataRecord.GetOrdinal("time_out_latitude"));
			rovingagentlogin.mTimeOutLongitude = myDataRecord.GetString(myDataRecord.GetOrdinal("time_out_longitude"));
			rovingagentlogin.mTimeOutAddress = myDataRecord.GetString(myDataRecord.GetOrdinal("time_out_address"));
			rovingagentlogin.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return rovingagentlogin;
		}
	}
}