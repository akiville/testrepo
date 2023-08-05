using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class UrgentScheduleChangeDB
	{
		public static UrgentScheduleChange GetItem(int urgentschedulechangeId)
		{
			UrgentScheduleChange urgentschedulechange = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUrgentScheduleChangeSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", urgentschedulechangeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						urgentschedulechange = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return urgentschedulechange;
		}

		public static UrgentScheduleChangeCollection GetList(UrgentScheduleChangeCriteria urgentschedulechangeCriteria)
		{
			UrgentScheduleChangeCollection tempList = new UrgentScheduleChangeCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUrgentScheduleChangeSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", urgentschedulechangeCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@affected_branch_id", urgentschedulechangeCriteria.mAffectedBranchId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", urgentschedulechangeCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", urgentschedulechangeCriteria.mEndDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@affected_personnel_id", urgentschedulechangeCriteria.mAffectedPersonnelId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_filed", urgentschedulechangeCriteria.mDateFiled);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@to_lmm_id", urgentschedulechangeCriteria.mToLmmId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", urgentschedulechangeCriteria.mEmployeeId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new UrgentScheduleChangeCollection();
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

		public static int SelectCountForGetList(UrgentScheduleChangeCriteria urgentschedulechangeCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUrgentScheduleChangeSearchList";

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

		public static int Save(UrgentScheduleChange myUrgentScheduleChange)
		{
			if (!myUrgentScheduleChange.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a urgentschedulechange in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spUrgentScheduleChangeInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myUrgentScheduleChange.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_filed", myUrgentScheduleChange.mDateFiled);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@incident_date", myUrgentScheduleChange.mIncidentDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myUrgentScheduleChange.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.String, "@concern", myUrgentScheduleChange.mConcern);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_attendance_schedule_type_id", myUrgentScheduleChange.mLmmAttendanceScheduleTypeId);
				Helpers.CreateParameter(myCommand, DbType.String, "@explanation", myUrgentScheduleChange.mExplanation);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@reason_code_id", myUrgentScheduleChange.mReasonCodeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@affected_personnel_id", myUrgentScheduleChange.mAffectedPersonnelId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@affected_branch_id", myUrgentScheduleChange.mAffectedBranchId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myUrgentScheduleChange.mDatestamp);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myUrgentScheduleChange.mUserId);
                Helpers.CreateParameter(myCommand, DbType.String, "@action", myUrgentScheduleChange.mAction);
                Helpers.CreateParameter(myCommand, DbType.String, "@status", myUrgentScheduleChange.mStatus);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myUrgentScheduleChange.mRecordId);
                //Helpers.CreateParameter(myCommand, DbType.Int32, "@to_lmm_id", myUrgentScheduleChange.mToLmmId);

				Helpers.SetSaveParameters(myCommand, myUrgentScheduleChange);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update urgentschedulechange as it has been updated by someone else");
				}
				//myUrgentScheduleChange.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spUrgentScheduleChangeDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static UrgentScheduleChange FillDataRecord(IDataRecord myDataRecord)
		{
			UrgentScheduleChange urgentschedulechange = new UrgentScheduleChange();

			urgentschedulechange.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			urgentschedulechange.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			urgentschedulechange.mDateFiled = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_filed"));
			urgentschedulechange.mIncidentDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("incident_date"));
			urgentschedulechange.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			urgentschedulechange.mConcern = myDataRecord.GetString(myDataRecord.GetOrdinal("concern"));
			urgentschedulechange.mLmmAttendanceScheduleTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_attendance_schedule_type_id"));
			urgentschedulechange.mExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("explanation"));
			urgentschedulechange.mReasonCodeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("reason_code_id"));
			urgentschedulechange.mAffectedPersonnelId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("affected_personnel_id"));
			urgentschedulechange.mAffectedBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("affected_branch_id"));
			urgentschedulechange.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			urgentschedulechange.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			urgentschedulechange.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
            urgentschedulechange.mAction = myDataRecord.GetString(myDataRecord.GetOrdinal("action"));
            urgentschedulechange.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
            urgentschedulechange.mConcernType = myDataRecord.GetString(myDataRecord.GetOrdinal("concern_type"));
            urgentschedulechange.mLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_name"));
            urgentschedulechange.mToLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("to_lmm_name"));
            urgentschedulechange.mAffectedPersonnelName = myDataRecord.GetString(myDataRecord.GetOrdinal("affected_personnel_name"));
            urgentschedulechange.mAffectedBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("affected_branch_name"));
            urgentschedulechange.mPersonnelName = myDataRecord.GetString(myDataRecord.GetOrdinal("personnel_name"));
            urgentschedulechange.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
            urgentschedulechange.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
            urgentschedulechange.mMessageId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("message_id"));
            urgentschedulechange.mMessageStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("message_status"));
            urgentschedulechange.mBorrowedPersonnelName = myDataRecord.GetString(myDataRecord.GetOrdinal("borrowed_personnel_name"));
            urgentschedulechange.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            urgentschedulechange.mToLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("to_lmm_id"));
            urgentschedulechange.mBorrowedPersonnelId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("borrowed_personnel_id"));
            urgentschedulechange.mStartDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("start_date"));
            urgentschedulechange.mEndDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("end_date"));
            return urgentschedulechange;
		}
	}
}