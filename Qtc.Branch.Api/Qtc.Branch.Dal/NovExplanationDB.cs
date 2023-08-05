using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class NovExplanationDB
	{
		public static NovExplanation GetItem(int novexplanationId)
		{
			NovExplanation novexplanation = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNovExplanationSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", novexplanationId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						novexplanation = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return novexplanation;
		}

		public static NovExplanationCollection GetList(NovExplanationCriteria novexplanationCriteria)
		{
			NovExplanationCollection tempList = new NovExplanationCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNovExplanationSearchList";

                Helpers.CreateParameter(myCommand, DbType.String, "@module_name", novexplanationCriteria.mModuleName);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", novexplanationCriteria.mRecordId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", novexplanationCriteria.mEmployeeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new NovExplanationCollection();
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

		public static int SelectCountForGetList(NovExplanationCriteria novexplanationCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNovExplanationSearchList";

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

		public static int Save(NovExplanation myNovExplanation)
		{
			if (!myNovExplanation.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a novexplanation in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNovExplanationInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myNovExplanation.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.String, "@module_name", myNovExplanation.mModuleName);
				Helpers.CreateParameter(myCommand, DbType.String, "@explanation", myNovExplanation.mExplanation);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myNovExplanation.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myNovExplanation.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myNovExplanation);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update novexplanation as it has been updated by someone else");
				}
				//myNovExplanation.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spNovExplanationDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static NovExplanation FillDataRecord(IDataRecord myDataRecord)
		{
			NovExplanation novexplanation = new NovExplanation();

			novexplanation.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			novexplanation.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			novexplanation.mModuleName = myDataRecord.GetString(myDataRecord.GetOrdinal("module_name"));
			novexplanation.mExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("explanation"));
			novexplanation.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			novexplanation.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			novexplanation.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return novexplanation;
		}
	}
}