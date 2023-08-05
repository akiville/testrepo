using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingCheckListViolationDB
	{
		public static RovingCheckListViolation GetItem(int rovingchecklistviolationId)
		{
			RovingCheckListViolation rovingchecklistviolation = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingCheckListViolationSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingchecklistviolationId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingchecklistviolation = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingchecklistviolation;
		}

		public static RovingCheckListViolationCollection GetList(RovingCheckListViolationCriteria rovingchecklistviolationCriteria)
		{
			RovingCheckListViolationCollection tempList = new RovingCheckListViolationCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingCheckListViolationSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "roving_checklist_id", rovingchecklistviolationCriteria.mRovingChecklistId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingCheckListViolationCollection();
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

		public static int SelectCountForGetList(RovingCheckListViolationCriteria rovingchecklistviolationCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingCheckListViolationSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_checklist_id", rovingchecklistviolationCriteria.mRovingChecklistId);

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

		public static int Save(RovingCheckListViolation myRovingCheckListViolation)
		{
			if (!myRovingCheckListViolation.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingchecklistviolation in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "ptApi_spRovingCheckListViolationInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_checklist_id", myRovingCheckListViolation.mRovingChecklistId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@type_of_violation_id", myRovingCheckListViolation.mTypeOfViolationId);

				Helpers.SetSaveParameters(myCommand, myRovingCheckListViolation);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingchecklistviolation as it has been updated by someone else");
				}
				//myRovingCheckListViolation.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "ptApi_spRovingCheckListViolationDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingCheckListViolation FillDataRecord(IDataRecord myDataRecord)
		{
			RovingCheckListViolation rovingchecklistviolation = new RovingCheckListViolation();

			rovingchecklistviolation.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingchecklistviolation.mRovingChecklistId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_checklist_id"));
			rovingchecklistviolation.mTypeOfViolationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("type_of_violation_id"));
			rovingchecklistviolation.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            rovingchecklistviolation.mTypeOfViolationName = myDataRecord.GetString(myDataRecord.GetOrdinal("typeof_violation_name"));
            rovingchecklistviolation.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
            return rovingchecklistviolation;
		}
	}
}