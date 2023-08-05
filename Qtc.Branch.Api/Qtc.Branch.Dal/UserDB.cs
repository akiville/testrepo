using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class UserDB
	{
		public static User GetItem(int userId)
		{
			User user = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUserSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", userId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						user = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return user;
		}

		public static UserCollection GetList(UserCriteria userCriteria)
		{
			UserCollection tempList = new UserCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUserSearchList";

                Helpers.CreateParameter(myCommand, DbType.String, "@username", userCriteria.mUserName);
                Helpers.CreateParameter(myCommand, DbType.String, "@password", userCriteria.mPassword);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new UserCollection();
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

		public static int SelectCountForGetList(UserCriteria userCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUserSearchList";

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

		public static int Save(User myUser)
		{
			if (!myUser.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a user in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUserInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@first_name", myUser.mFirstName);
				Helpers.CreateParameter(myCommand, DbType.String, "@middle_name", myUser.mMiddleName);
				Helpers.CreateParameter(myCommand, DbType.String, "@last_name", myUser.mLastName);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@role", myUser.mRole);
				Helpers.CreateParameter(myCommand, DbType.String, "@user_name", myUser.mUserName);
				Helpers.CreateParameter(myCommand, DbType.String, "@password", myUser.mPassword);

				Helpers.SetSaveParameters(myCommand, myUser);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update user as it has been updated by someone else");
				}
				//myUser.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spUserDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static User FillDataRecord(IDataRecord myDataRecord)
		{
			User user = new User();

			user.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			user.mFirstName = myDataRecord.GetString(myDataRecord.GetOrdinal("first_name"));
			user.mMiddleName = myDataRecord.GetString(myDataRecord.GetOrdinal("middle_name"));
			user.mLastName = myDataRecord.GetString(myDataRecord.GetOrdinal("last_name"));
			user.mRole = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("role"));
			user.mUserName = myDataRecord.GetString(myDataRecord.GetOrdinal("user_name"));
			user.mPassword = myDataRecord.GetString(myDataRecord.GetOrdinal("password"));
			user.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return user;
		}
	}
}