using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingPlanScheduledActualChecklistDB
	{
		public static RovingPlanScheduledActualChecklist GetItem(int rovingplanscheduledactualchecklistId)
		{
			RovingPlanScheduledActualChecklist rovingplanscheduledactualchecklist = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualChecklistSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingplanscheduledactualchecklistId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingplanscheduledactualchecklist = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingplanscheduledactualchecklist;
		}

		public static RovingPlanScheduledActualChecklistCollection GetList(RovingPlanScheduledActualChecklistCriteria rovingplanscheduledactualchecklistCriteria)
		{
			RovingPlanScheduledActualChecklistCollection tempList = new RovingPlanScheduledActualChecklistCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualChecklistSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_id", rovingplanscheduledactualchecklistCriteria.mRpsId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingPlanScheduledActualChecklistCollection();
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

		public static int SelectCountForGetList(RovingPlanScheduledActualChecklistCriteria rovingplanscheduledactualchecklistCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualChecklistSearchList";

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

		public static int Save(RovingPlanScheduledActualChecklist myRovingPlanScheduledActualChecklist)
		{
			if (!myRovingPlanScheduledActualChecklist.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingplanscheduledactualchecklist in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualChecklistInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRovingPlanScheduledActualChecklist.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myRovingPlanScheduledActualChecklist.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_id", myRovingPlanScheduledActualChecklist.mRpsId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rc_id", myRovingPlanScheduledActualChecklist.mRcId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_check", myRovingPlanScheduledActualChecklist.mIsCheck);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myRovingPlanScheduledActualChecklist.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@lost_sales", myRovingPlanScheduledActualChecklist.mLostSales);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@with_violation", myRovingPlanScheduledActualChecklist.mWithViolation);
				Helpers.CreateParameter(myCommand, DbType.String, "@action_taken", myRovingPlanScheduledActualChecklist.mActionTaken);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rpsa_id", myRovingPlanScheduledActualChecklist.mRpsaId);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@disable", myRovingPlanScheduledActualChecklist.mDisable);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@points", myRovingPlanScheduledActualChecklist.mPoints);

				Helpers.SetSaveParameters(myCommand, myRovingPlanScheduledActualChecklist);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingplanscheduledactualchecklist as it has been updated by someone else");
				}
				//myRovingPlanScheduledActualChecklist.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualChecklistDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingPlanScheduledActualChecklist FillDataRecord(IDataRecord myDataRecord)
		{
			RovingPlanScheduledActualChecklist rovingplanscheduledactualchecklist = new RovingPlanScheduledActualChecklist();

			rovingplanscheduledactualchecklist.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingplanscheduledactualchecklist.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			rovingplanscheduledactualchecklist.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			rovingplanscheduledactualchecklist.mRpsId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rps_id"));
			rovingplanscheduledactualchecklist.mRcId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rc_id"));
			rovingplanscheduledactualchecklist.mIsCheck = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_check"));
			rovingplanscheduledactualchecklist.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			rovingplanscheduledactualchecklist.mLostSales = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("lost_sales"));
			rovingplanscheduledactualchecklist.mWithViolation = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("with_violation"));
			rovingplanscheduledactualchecklist.mActionTaken = myDataRecord.GetString(myDataRecord.GetOrdinal("action_taken"));
			rovingplanscheduledactualchecklist.mRpsaId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rpsa_id"));
			rovingplanscheduledactualchecklist.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            rovingplanscheduledactualchecklist.mPoints = myDataRecord.GetInt32(myDataRecord.GetOrdinal("points"));
            rovingplanscheduledactualchecklist.mCategory = myDataRecord.GetString(myDataRecord.GetOrdinal("category_id_name"));
            rovingplanscheduledactualchecklist.mChecklist = myDataRecord.GetString(myDataRecord.GetOrdinal("checklist_name"));
            rovingplanscheduledactualchecklist.mDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("checklist_remarks"));
            return rovingplanscheduledactualchecklist;
		}
	}
}