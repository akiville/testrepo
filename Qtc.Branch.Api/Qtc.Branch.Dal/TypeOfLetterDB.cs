using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class TypeOfLetterDB
	{
		public static TypeOfLetter GetItem(int typeofletterId)
		{
			TypeOfLetter typeofletter = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spTypeOfLetterSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", typeofletterId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						typeofletter = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return typeofletter;
		}

		public static TypeOfLetterCollection GetList(TypeOfLetterCriteria typeofletterCriteria)
		{
			TypeOfLetterCollection tempList = new TypeOfLetterCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spTypeOfLetterSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new TypeOfLetterCollection();
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

		public static int SelectCountForGetList(TypeOfLetterCriteria typeofletterCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spTypeOfLetterSearchList";

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

		public static int Save(TypeOfLetter myTypeOfLetter)
		{
			if (!myTypeOfLetter.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a typeofletter in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spTypeOfLetterInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@name", myTypeOfLetter.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myTypeOfLetter.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.String, "@code", myTypeOfLetter.mCode);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@submit_requirement", myTypeOfLetter.mSubmitRequirement);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@month_duration", myTypeOfLetter.mMonthDuration);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@stc_form", myTypeOfLetter.mStcForm);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@egress", myTypeOfLetter.mEgress);

				Helpers.SetSaveParameters(myCommand, myTypeOfLetter);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update typeofletter as it has been updated by someone else");
				}
				//myTypeOfLetter.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "ptApi_spTypeOfLetterDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static TypeOfLetter FillDataRecord(IDataRecord myDataRecord)
		{
			TypeOfLetter typeofletter = new TypeOfLetter();

			typeofletter.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			typeofletter.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			typeofletter.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			typeofletter.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			typeofletter.mCode = myDataRecord.GetString(myDataRecord.GetOrdinal("code"));
			typeofletter.mSubmitRequirement = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("submit_requirement"));
			typeofletter.mMonthDuration = myDataRecord.GetInt32(myDataRecord.GetOrdinal("month_duration"));
			typeofletter.mStcForm = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("stc_form"));
			typeofletter.mEgress = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("egress"));

			return typeofletter;
		}
	}
}