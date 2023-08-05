using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingPlanDB
	{
		public static RovingPlan GetItem(int rovingplanId)
		{
			RovingPlan rovingplan = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingplanId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingplan = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingplan;
		}

		public static RovingPlanCollection GetList(RovingPlanCriteria rovingplanCriteria)
		{
			RovingPlanCollection tempList = new RovingPlanCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanSearchList";

                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", rovingplanCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", rovingplanCriteria.mEndDate);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingPlanCollection();
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

		public static int SelectCountForGetList(RovingPlanCriteria rovingplanCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanSearchList";

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

		public static int Save(RovingPlan myRovingPlan)
		{
			if (!myRovingPlan.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingplan in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRovingPlan.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", myRovingPlan.mStartDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", myRovingPlan.mEndDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@planned_by", myRovingPlan.mPlannedBy);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@post", myRovingPlan.mPost);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@disable", myRovingPlan.mDisable);

				Helpers.SetSaveParameters(myCommand, myRovingPlan);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingplan as it has been updated by someone else");
				}
				//myRovingPlan.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRovingPlanDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingPlan FillDataRecord(IDataRecord myDataRecord)
		{
			RovingPlan rovingplan = new RovingPlan();

			rovingplan.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingplan.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			rovingplan.mStartDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("start_date"));
			rovingplan.mEndDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("end_date"));
			rovingplan.mPlannedBy = myDataRecord.GetInt32(myDataRecord.GetOrdinal("planned_by"));
			rovingplan.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			rovingplan.mPost = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("post"));

			return rovingplan;
		}
	}
}