using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RovingPlanScheduledDB
	{
		public static RovingPlanScheduled GetItem(int rovingplanscheduledId)
		{
			RovingPlanScheduled rovingplanscheduled = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rovingplanscheduledId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rovingplanscheduled = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rovingplanscheduled;
		}

		public static RovingPlanScheduledCollection GetList(RovingPlanScheduledCriteria rovingplanscheduledCriteria)
		{
			RovingPlanScheduledCollection tempList = new RovingPlanScheduledCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_plan_id", rovingplanscheduledCriteria.mRovingPlanId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_plan_oic_id", rovingplanscheduledCriteria.mRovingPlanOicId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", rovingplanscheduledCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", rovingplanscheduledCriteria.mEndDate);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RovingPlanScheduledCollection();
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

		public static int SelectCountForGetList(RovingPlanScheduledCriteria rovingplanscheduledCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledSearchList";

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

		public static int Save(RovingPlanScheduled myRovingPlanScheduled)
		{
			if (!myRovingPlanScheduled.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rovingplanscheduled in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRovingPlanScheduledInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRovingPlanScheduled.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myRovingPlanScheduled.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_request_id", myRovingPlanScheduled.mRovingRequestId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_plan_id", myRovingPlanScheduled.mRovingPlanId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_plan_dates_id", myRovingPlanScheduled.mRovingPlanDatesId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_plan_oic_id", myRovingPlanScheduled.mRovingPlanOicId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@roving_plan_scheduled_id", myRovingPlanScheduled.mRovingPlanScheduledId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_area_id", myRovingPlanScheduled.mBranchAreaId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myRovingPlanScheduled.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", myRovingPlanScheduled.mStartDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", myRovingPlanScheduled.mEndDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myRovingPlanScheduled.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@visited", myRovingPlanScheduled.mVisited);
				Helpers.CreateParameter(myCommand, DbType.String, "@visited_remarks", myRovingPlanScheduled.mVisitedRemarks);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@post", myRovingPlanScheduled.mPost);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@already_egress", myRovingPlanScheduled.mAlreadyEgress);
                Helpers.CreateParameter(myCommand, DbType.String, "@branch_id_name", myRovingPlanScheduled.mBranchIdName);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@disable", myRovingPlanScheduled.mDisable);

				Helpers.SetSaveParameters(myCommand, myRovingPlanScheduled);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rovingplanscheduled as it has been updated by someone else");
				}
				//myRovingPlanScheduled.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRovingPlanScheduledDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RovingPlanScheduled FillDataRecord(IDataRecord myDataRecord)
		{
			RovingPlanScheduled rovingplanscheduled = new RovingPlanScheduled();

			rovingplanscheduled.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rovingplanscheduled.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			rovingplanscheduled.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			rovingplanscheduled.mRovingRequestId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_request_id"));
			rovingplanscheduled.mRovingPlanId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_plan_id"));
			rovingplanscheduled.mRovingPlanDatesId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_plan_dates_id"));
			rovingplanscheduled.mRovingPlanOicId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_plan_oic_id"));
			rovingplanscheduled.mRovingPlanScheduledId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_plan_scheduled_id"));
			rovingplanscheduled.mBranchAreaId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_area_id"));
			rovingplanscheduled.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			rovingplanscheduled.mStartDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("start_date"));
			rovingplanscheduled.mEndDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("end_date"));
			rovingplanscheduled.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			rovingplanscheduled.mVisited = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("visited"));
			rovingplanscheduled.mVisitedRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("visited_remarks"));
			rovingplanscheduled.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			rovingplanscheduled.mPost = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("post"));
			rovingplanscheduled.mAlreadyEgress = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("already_egress"));
            rovingplanscheduled.mBranchIdName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_id_name"));
            rovingplanscheduled.mBackColor = myDataRecord.GetString(myDataRecord.GetOrdinal("back_color"));
            rovingplanscheduled.mForeColor = myDataRecord.GetString(myDataRecord.GetOrdinal("fore_color"));
            rovingplanscheduled.mRovingPlanOicId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_plan_oic_id_employee_id"));
            rovingplanscheduled.mRovingPlanOicEmployeeIdName = myDataRecord.GetString(myDataRecord.GetOrdinal("roving_plan_oic_id_employee_id_name"));
            rovingplanscheduled.mIsActual = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_actual"));
            rovingplanscheduled.mActualReport = myDataRecord.GetString(myDataRecord.GetOrdinal("actual_reports"));
            rovingplanscheduled.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
            rovingplanscheduled.mRovingTaskId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_task_id"));
            rovingplanscheduled.mRovingTaskName = myDataRecord.GetString(myDataRecord.GetOrdinal("roving_task_name"));
            rovingplanscheduled.mAreaName = myDataRecord.GetString(myDataRecord.GetOrdinal("area_name"));
            rovingplanscheduled.mRovingPlanActualId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("roving_plan_actual_id"));
            rovingplanscheduled.mActualRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("actual_remarks"));
            rovingplanscheduled.mActualTimeIn = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("actual_time_in"));
            rovingplanscheduled.mActualTimeOut = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("actual_time_out"));
            rovingplanscheduled.mLateExplain = myDataRecord.GetString(myDataRecord.GetOrdinal("late_explanation"));
            rovingplanscheduled.mMcOnDutyId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("mc_on_duty_id"));
            rovingplanscheduled.mMcOnDuty = myDataRecord.GetString(myDataRecord.GetOrdinal("mc_on_duty"));

            return rovingplanscheduled;
		}
	}
}