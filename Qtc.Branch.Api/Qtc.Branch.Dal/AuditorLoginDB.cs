using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class AuditorLoginDB
	{
		public static AuditorLogin GetItem(int auditorloginId)
		{
			AuditorLogin auditorlogin = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditorLoginSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", auditorloginId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						auditorlogin = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return auditorlogin;
		}

		public static AuditorLoginCollection GetList(AuditorLoginCriteria auditorloginCriteria)
		{
			AuditorLoginCollection tempList = new AuditorLoginCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditorLoginSearchList";

                Helpers.CreateParameter(myCommand, DbType.String, "@username", auditorloginCriteria.mUsername);
                Helpers.CreateParameter(myCommand, DbType.String, "@password", auditorloginCriteria.mPassword);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new AuditorLoginCollection();
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

		public static int SelectCountForGetList(AuditorLoginCriteria auditorloginCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditorLoginSearchList";

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

		public static int Save(AuditorLogin myAuditorLogin)
		{
			if (!myAuditorLogin.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a auditorlogin in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spAuditorLoginInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@username", myAuditorLogin.mUsername);
				Helpers.CreateParameter(myCommand, DbType.String, "@password", myAuditorLogin.mPassword);
				Helpers.CreateParameter(myCommand, DbType.String, "@firstname", myAuditorLogin.mFirstname);
				Helpers.CreateParameter(myCommand, DbType.String, "@lastname", myAuditorLogin.mLastname);
				Helpers.CreateParameter(myCommand, DbType.String, "@middlename", myAuditorLogin.mMiddlename);
				Helpers.CreateParameter(myCommand, DbType.String, "@position_name", myAuditorLogin.mPositionName);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myAuditorLogin.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myAuditorLogin);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update auditorlogin as it has been updated by someone else");
				}
				//myAuditorLogin.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spAuditorLoginDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static AuditorLogin FillDataRecord(IDataRecord myDataRecord)
		{
			AuditorLogin auditorlogin = new AuditorLogin();

			auditorlogin.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			auditorlogin.mUsername = myDataRecord.GetString(myDataRecord.GetOrdinal("username"));
			auditorlogin.mPassword = myDataRecord.GetString(myDataRecord.GetOrdinal("password"));
			auditorlogin.mFirstname = myDataRecord.GetString(myDataRecord.GetOrdinal("firstname"));
			auditorlogin.mLastname = myDataRecord.GetString(myDataRecord.GetOrdinal("lastname"));
			auditorlogin.mMiddlename = myDataRecord.GetString(myDataRecord.GetOrdinal("middlename"));
			auditorlogin.mPositionName = myDataRecord.GetString(myDataRecord.GetOrdinal("position_name"));
			auditorlogin.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			auditorlogin.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));

			return auditorlogin;
		}
	}
}