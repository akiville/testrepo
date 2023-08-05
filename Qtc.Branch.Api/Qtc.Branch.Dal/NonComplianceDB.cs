using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class NonComplianceDB
	{
		public static NonCompliance GetItem(int noncomplianceId)
		{
			NonCompliance noncompliance = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNonComplianceSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", noncomplianceId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						noncompliance = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return noncompliance;
		}

		public static NonComplianceCollection GetList(NonComplianceCriteria noncomplianceCriteria)
		{
			NonComplianceCollection tempList = new NonComplianceCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNonComplianceSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new NonComplianceCollection();
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

		public static int SelectCountForGetList(NonComplianceCriteria noncomplianceCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNonComplianceSearchList";

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

		public static int Save(NonCompliance myNonCompliance)
		{
			if (!myNonCompliance.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a noncompliance in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spNonComplianceInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.String, "@name", myNonCompliance.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myNonCompliance.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@required_details", myNonCompliance.mRequiredDetails);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@required_explanation", myNonCompliance.mRequiredExplanation);

				Helpers.SetSaveParameters(myCommand, myNonCompliance);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update noncompliance as it has been updated by someone else");
				}
				//myNonCompliance.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spNonComplianceDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static NonCompliance FillDataRecord(IDataRecord myDataRecord)
		{
			NonCompliance noncompliance = new NonCompliance();

			noncompliance.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			noncompliance.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			noncompliance.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			noncompliance.mRequiredDetails = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("required_details"));
			noncompliance.mRequiredExplanation = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("required_explanation"));
			noncompliance.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            noncompliance.mTopicIdName = myDataRecord.GetString(myDataRecord.GetOrdinal("topic_id_name"));
			return noncompliance;
		}
	}
}