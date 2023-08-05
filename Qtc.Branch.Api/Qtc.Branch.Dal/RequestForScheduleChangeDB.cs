using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RequestForScheduleChangeDB
	{
		public static RequestForScheduleChange GetItem(int requestforschedulechangeId)
		{
			RequestForScheduleChange requestforschedulechange = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestForScheduleChangeSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", requestforschedulechangeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						requestforschedulechange = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return requestforschedulechange;
		}

		public static RequestForScheduleChangeCollection GetList(RequestForScheduleChangeCriteria requestforschedulechangeCriteria)
		{
			RequestForScheduleChangeCollection tempList = new RequestForScheduleChangeCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestForScheduleChangeSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", requestforschedulechangeCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", requestforschedulechangeCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", requestforschedulechangeCriteria.mEndDate);
                
				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RequestForScheduleChangeCollection();
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

		public static int SelectCountForGetList(RequestForScheduleChangeCriteria requestforschedulechangeCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestForScheduleChangeSearchList";

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

		public static int Save(RequestForScheduleChange myRequestForScheduleChange)
		{
			if (!myRequestForScheduleChange.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a requestforschedulechange in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestForScheduleChangeInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@request_for_schedule_change_id", myRequestForScheduleChange.mRequestForScheduleChangeId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_approved", myRequestForScheduleChange.mIsApproved);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_cancelled", myRequestForScheduleChange.mIsCancelled);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_postponed", myRequestForScheduleChange.mIsPostponed);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@requested_by_id", myRequestForScheduleChange.mRequestedById);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myRequestForScheduleChange.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.String, "@agency", myRequestForScheduleChange.mAgency);
				Helpers.CreateParameter(myCommand, DbType.String, "@branch", myRequestForScheduleChange.mBranch);
				Helpers.CreateParameter(myCommand, DbType.String, "@position", myRequestForScheduleChange.mPosition);
				Helpers.CreateParameter(myCommand, DbType.String, "@area", myRequestForScheduleChange.mArea);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myRequestForScheduleChange.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.String, "@lmm", myRequestForScheduleChange.mLmm);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", myRequestForScheduleChange.mStartDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", myRequestForScheduleChange.mEndDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@reason_id", myRequestForScheduleChange.mReasonId);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myRequestForScheduleChange.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@person_to_relieve_id", myRequestForScheduleChange.mPersonToRelieveId);
				Helpers.CreateParameter(myCommand, DbType.String, "@reliever_branch", myRequestForScheduleChange.mRelieverBranch);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@replacement_id", myRequestForScheduleChange.mReplacementId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@postponed_to", myRequestForScheduleChange.mPostponedTo);
				Helpers.CreateParameter(myCommand, DbType.String, "@recon_number", myRequestForScheduleChange.mReconNumber);
				Helpers.CreateParameter(myCommand, DbType.String, "@reason", myRequestForScheduleChange.mReason);
				Helpers.CreateParameter(myCommand, DbType.String, "@explanation", myRequestForScheduleChange.mExplanation);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@change_with", myRequestForScheduleChange.mChangeWith);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_from", myRequestForScheduleChange.mBranchFrom);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_to", myRequestForScheduleChange.mBranchTo);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_planned", myRequestForScheduleChange.mIsPlanned);

				Helpers.SetSaveParameters(myCommand, myRequestForScheduleChange);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update requestforschedulechange as it has been updated by someone else");
				}
				//myRequestForScheduleChange.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRequestForScheduleChangeDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RequestForScheduleChange FillDataRecord(IDataRecord myDataRecord)
		{
			RequestForScheduleChange requestforschedulechange = new RequestForScheduleChange();

			requestforschedulechange.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			requestforschedulechange.mRequestForScheduleChangeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("request_for_schedule_change_id"));
			requestforschedulechange.mIsApproved = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_approved"));
			requestforschedulechange.mIsCancelled = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_cancelled"));
			requestforschedulechange.mIsPostponed = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_postponed"));
			requestforschedulechange.mRequestedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("requested_by_id"));
			requestforschedulechange.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			requestforschedulechange.mAgency = myDataRecord.GetString(myDataRecord.GetOrdinal("agency"));
			requestforschedulechange.mBranch = myDataRecord.GetString(myDataRecord.GetOrdinal("branch"));
			requestforschedulechange.mPosition = myDataRecord.GetString(myDataRecord.GetOrdinal("position"));
			requestforschedulechange.mArea = myDataRecord.GetString(myDataRecord.GetOrdinal("area"));
			requestforschedulechange.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			requestforschedulechange.mLmm = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm"));
			requestforschedulechange.mStartDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("start_date"));
			requestforschedulechange.mEndDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("end_date"));
			requestforschedulechange.mReasonId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("reason_id"));
			requestforschedulechange.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			requestforschedulechange.mPersonToRelieveId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("person_to_relieve_id"));
			requestforschedulechange.mRelieverBranch = myDataRecord.GetString(myDataRecord.GetOrdinal("reliever_branch"));
			requestforschedulechange.mReplacementId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("replacement_id"));
			requestforschedulechange.mPostponedTo = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("postponed_to"));
			requestforschedulechange.mReconNumber = myDataRecord.GetString(myDataRecord.GetOrdinal("recon_number"));
			requestforschedulechange.mReason = myDataRecord.GetString(myDataRecord.GetOrdinal("reason"));
			requestforschedulechange.mExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("explanation"));
			requestforschedulechange.mChangeWith = myDataRecord.GetInt32(myDataRecord.GetOrdinal("change_with"));
			requestforschedulechange.mBranchFrom = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_from"));
			requestforschedulechange.mBranchTo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_to"));
			requestforschedulechange.mIsPlanned = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_planned"));
			requestforschedulechange.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return requestforschedulechange;
		}
	}
}