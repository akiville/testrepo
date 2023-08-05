using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class TypeOfViolationDB
	{
		public static TypeOfViolation GetItem(int typeofviolationId)
		{
			TypeOfViolation typeofviolation = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spTypeOfViolationSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", typeofviolationId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						typeofviolation = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return typeofviolation;
		}

		public static TypeOfViolationCollection GetList(TypeOfViolationCriteria typeofviolationCriteria)
		{
			TypeOfViolationCollection tempList = new TypeOfViolationCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spTypeOfViolationSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new TypeOfViolationCollection();
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

		public static int SelectCountForGetList(TypeOfViolationCriteria typeofviolationCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spTypeOfViolationSearchList";

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

		public static int Save(TypeOfViolation myTypeOfViolation)
		{
			if (!myTypeOfViolation.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a typeofviolation in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spTypeOfViolationInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@name", myTypeOfViolation.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myTypeOfViolation.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_rfda", myTypeOfViolation.mIsRfda);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@typeofsanction_id", myTypeOfViolation.mTypeofsanctionId);
				Helpers.CreateParameter(myCommand, DbType.String, "@rule_name", myTypeOfViolation.mRuleName);
				Helpers.CreateParameter(myCommand, DbType.String, "@article_no", myTypeOfViolation.mArticleNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@section_no", myTypeOfViolation.mSectionNo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@typeofvioaltionrule_id", myTypeOfViolation.mTypeofvioaltionruleId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@offense_level", myTypeOfViolation.mOffenseLevel);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_performance", myTypeOfViolation.mIsPerformance);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myTypeOfViolation.mRecordId);

				Helpers.SetSaveParameters(myCommand, myTypeOfViolation);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update typeofviolation as it has been updated by someone else");
				}
				//myTypeOfViolation.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "ptApi_spTypeOfViolationDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static TypeOfViolation FillDataRecord(IDataRecord myDataRecord)
		{
			TypeOfViolation typeofviolation = new TypeOfViolation();

			typeofviolation.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			typeofviolation.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			typeofviolation.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			typeofviolation.mIsRfda = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_rfda"));
			typeofviolation.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			typeofviolation.mTypeofsanctionId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("typeofsanction_id"));
			typeofviolation.mRuleName = myDataRecord.GetString(myDataRecord.GetOrdinal("rule_name"));
			typeofviolation.mArticleNo = myDataRecord.GetString(myDataRecord.GetOrdinal("article_no"));
			typeofviolation.mSectionNo = myDataRecord.GetString(myDataRecord.GetOrdinal("section_no"));
			typeofviolation.mTypeofvioaltionruleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("typeofvioaltionrule_id"));
			typeofviolation.mOffenseLevel = myDataRecord.GetInt32(myDataRecord.GetOrdinal("offense_level"));
			typeofviolation.mIsPerformance = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_performance"));
			typeofviolation.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
            typeofviolation.mTypeOfSanctionIdName = myDataRecord.GetString(myDataRecord.GetOrdinal("typeofsanction_id_name"));
            typeofviolation.mTypeOfViolationRuleIdName = myDataRecord.GetString(myDataRecord.GetOrdinal("typeofvioaltionrule_id_name"));
			return typeofviolation;
		}
	}
}