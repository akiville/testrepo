using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingPlanOicDB
	{
		public static RovingPlanOic GetItem(int rovingplanoicId)
		{
			RovingPlanOic rovingplanoic = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanOicSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingplanoicId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingplanoic = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingplanoic;
		}

		public static RovingPlanOicCollection GetList(RovingPlanOicCriteria rovingplanoicCriteria)
		{
			RovingPlanOicCollection tempList = new RovingPlanOicCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanOicSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "roving_plan_id", rovingplanoicCriteria.mRovingPlanId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "employee_id", rovingplanoicCriteria.mEmployeeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingPlanOicCollection();
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

		public static int SelectCountForGetList(RovingPlanOicCriteria rovingplanoicCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanOicSearchList";

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

		public static int Save(RovingPlanOic myRovingPlanOic)
		{
			if (!myRovingPlanOic.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingplanoic in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanOicInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRovingPlanOic.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_plan_id", myRovingPlanOic.mRovingPlanId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myRovingPlanOic.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@disable", myRovingPlanOic.mDisable);

                Helpers.SetSaveParameters(myCommand, myRovingPlanOic);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingplanoic as it has been updated by someone else");
				}
				//myRovingPlanOic.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRovingPlanOicDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingPlanOic FillDataRecord(IDataRecord myDataRecord)
		{
			RovingPlanOic rovingplanoic = new RovingPlanOic();

			rovingplanoic.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingplanoic.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			rovingplanoic.mRovingPlanId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_plan_id"));
			rovingplanoic.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			rovingplanoic.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return rovingplanoic;
		}
	}
}