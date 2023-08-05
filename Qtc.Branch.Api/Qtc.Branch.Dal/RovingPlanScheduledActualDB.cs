using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingPlanScheduledActualDB
	{
		public static RovingPlanScheduledActual GetItem(int rovingplanscheduledactualId)
		{
			RovingPlanScheduledActual rovingplanscheduledactual = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingplanscheduledactualId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingplanscheduledactual = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingplanscheduledactual;
		}

		public static RovingPlanScheduledActualCollection GetList(RovingPlanScheduledActualCriteria rovingplanscheduledactualCriteria)
		{
			RovingPlanScheduledActualCollection tempList = new RovingPlanScheduledActualCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingPlanScheduledActualCollection();
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

		public static int SelectCountForGetList(RovingPlanScheduledActualCriteria rovingplanscheduledactualCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualSearchList";

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

		public static int Save(RovingPlanScheduledActual myRovingPlanScheduledActual)
		{
			if (!myRovingPlanScheduledActual.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingplanscheduledactual in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myRovingPlanScheduledActual.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rps_id", myRovingPlanScheduledActual.mRpsId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myRovingPlanScheduledActual.mUserId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_task_id", myRovingPlanScheduledActual.mRovingTaskId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@mc_on_duty_id", myRovingPlanScheduledActual.mMcOnDutyId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@time_in", myRovingPlanScheduledActual.mTimeIn);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@time_out", myRovingPlanScheduledActual.mTimeOut);
				Helpers.CreateParameter(myCommand, DbType.String, "@late_explain", myRovingPlanScheduledActual.mLateExplain);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myRovingPlanScheduledActual.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@nov_id", myRovingPlanScheduledActual.mNovId);
				Helpers.CreateParameter(myCommand, DbType.String, "@nov_no", myRovingPlanScheduledActual.mNovNo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rfd_id", myRovingPlanScheduledActual.mRfdId);
				Helpers.CreateParameter(myCommand, DbType.String, "@rfd_no", myRovingPlanScheduledActual.mRfdNo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rp_id", myRovingPlanScheduledActual.mRpId);
				Helpers.CreateParameter(myCommand, DbType.String, "@rp_no", myRovingPlanScheduledActual.mRpNo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rm_id", myRovingPlanScheduledActual.mRmId);
				Helpers.CreateParameter(myCommand, DbType.String, "@rm_no", myRovingPlanScheduledActual.mRmNo);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@no_deduction", myRovingPlanScheduledActual.mNoDeduction);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRovingPlanScheduledActual.mRecordId);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@disable", myRovingPlanScheduledActual.mDisable);

				Helpers.SetSaveParameters(myCommand, myRovingPlanScheduledActual);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingplanscheduledactual as it has been updated by someone else");
				}
				//myRovingPlanScheduledActual.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRovingPlanScheduledActualDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingPlanScheduledActual FillDataRecord(IDataRecord myDataRecord)
		{
			RovingPlanScheduledActual rovingplanscheduledactual = new RovingPlanScheduledActual();

			rovingplanscheduledactual.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingplanscheduledactual.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			rovingplanscheduledactual.mRpsId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rps_id"));
			rovingplanscheduledactual.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			rovingplanscheduledactual.mRovingTaskId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_task_id"));
			rovingplanscheduledactual.mMcOnDutyId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("mc_on_duty_id"));
			rovingplanscheduledactual.mTimeIn = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("time_in"));
			rovingplanscheduledactual.mTimeOut = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("time_out"));
			rovingplanscheduledactual.mLateExplain = myDataRecord.GetString(myDataRecord.GetOrdinal("late_explain"));
			rovingplanscheduledactual.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			rovingplanscheduledactual.mNovId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("nov_id"));
			rovingplanscheduledactual.mNovNo = myDataRecord.GetString(myDataRecord.GetOrdinal("nov_no"));
			rovingplanscheduledactual.mRfdId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rfd_id"));
			rovingplanscheduledactual.mRfdNo = myDataRecord.GetString(myDataRecord.GetOrdinal("rfd_no"));
			rovingplanscheduledactual.mRpId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rp_id"));
			rovingplanscheduledactual.mRpNo = myDataRecord.GetString(myDataRecord.GetOrdinal("rp_no"));
			rovingplanscheduledactual.mRmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rm_id"));
			rovingplanscheduledactual.mRmNo = myDataRecord.GetString(myDataRecord.GetOrdinal("rm_no"));
			rovingplanscheduledactual.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			rovingplanscheduledactual.mNoDeduction = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("no_deduction"));
			rovingplanscheduledactual.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));

			return rovingplanscheduledactual;
		}
	}
}