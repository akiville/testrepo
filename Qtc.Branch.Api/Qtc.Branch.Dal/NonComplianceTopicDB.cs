using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class NonComplianceTopicDB
	{
		public static NonComplianceTopic GetItem(int noncompliancetopicId)
		{
			NonComplianceTopic noncompliancetopic = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNonComplianceTopicSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", noncompliancetopicId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						noncompliancetopic = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return noncompliancetopic;
		}

		public static NonComplianceTopicCollection GetList(NonComplianceTopicCriteria noncompliancetopicCriteria)
		{
			NonComplianceTopicCollection tempList = new NonComplianceTopicCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNonComplianceTopicSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new NonComplianceTopicCollection();
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

		public static int SelectCountForGetList(NonComplianceTopicCriteria noncompliancetopicCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNonComplianceTopicSearchList";

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

		public static int Save(NonComplianceTopic myNonComplianceTopic)
		{
			if (!myNonComplianceTopic.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a noncompliancetopic in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNonComplianceTopicInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@name", myNonComplianceTopic.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myNonComplianceTopic.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.String, "@code", myNonComplianceTopic.mCode);

				Helpers.SetSaveParameters(myCommand, myNonComplianceTopic);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update noncompliancetopic as it has been updated by someone else");
				}
				//myNonComplianceTopic.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spNonComplianceTopicDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static NonComplianceTopic FillDataRecord(IDataRecord myDataRecord)
		{
			NonComplianceTopic noncompliancetopic = new NonComplianceTopic();

			noncompliancetopic.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			noncompliancetopic.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			noncompliancetopic.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			noncompliancetopic.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			noncompliancetopic.mCode = myDataRecord.GetString(myDataRecord.GetOrdinal("code"));

			return noncompliancetopic;
		}
	}
}