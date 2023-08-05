using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RfscDB
	{
		public static Rfsc GetItem(int rfscId)
		{
			Rfsc rfsc = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRfscSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", rfscId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						rfsc = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return rfsc;
		}

		public static RfscCollection GetList(RfscCriteria rfscCriteria)
		{
			RfscCollection tempList = new RfscCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRfscSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@encoder_id", rfscCriteria.mEncoderId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", rfscCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", rfscCriteria.mEndDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@status_id", rfscCriteria.mStatusId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RfscCollection();
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

		public static int SelectCountForGetList(RfscCriteria rfscCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRfscSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@encoder_id", rfscCriteria.mEncoderId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", rfscCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", rfscCriteria.mEndDate);
                Helpers.CreateParameter(myCommand, DbType.String, "@is_approved_value", rfscCriteria.mIsApprovedValue);
                Helpers.CreateParameter(myCommand, DbType.String, "@is_cancelled_value", rfscCriteria.mIsCancelledValue);
                Helpers.CreateParameter(myCommand, DbType.String, "@is_acknowledge_value", rfscCriteria.mIsAcknowledgeValue);


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

		public static int Save(Rfsc myRfsc)
		{
			if (!myRfsc.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a rfsc in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRfscInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_filed", myRfsc.mDateFiled);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@cutoff_date", myRfsc.mCutoffDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_requested_from", myRfsc.mDateRequestedFrom);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_requested_to", myRfsc.mDateRequestedTo);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@encoder_id", myRfsc.mEncoderId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myRfsc.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@type_id", myRfsc.mTypeId);
				Helpers.CreateParameter(myCommand, DbType.String, "@reason", myRfsc.mReason);
				Helpers.CreateParameter(myCommand, DbType.String, "@explanation", myRfsc.mExplanation);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@change_with", myRfsc.mChangeWith);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_from", myRfsc.mBranchFrom);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_to", myRfsc.mBranchTo);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_planned", myRfsc.mIsPlanned);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_approved", myRfsc.mIsApproved);
				Helpers.CreateParameter(myCommand, DbType.String, "@recon_number", myRfsc.mReconNumber);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_cancelled", myRfsc.mIsCancelled);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_executed", myRfsc.mIsExecuted);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_printed", myRfsc.mIsPrinted);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_acknowledge", myRfsc.mIsAcknowledge);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@acknowledge_by_user_id", myRfsc.mAcknowledgeByUserId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myRfsc.mRecordId);

                Helpers.SetSaveParameters(myCommand, myRfsc);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update rfsc as it has been updated by someone else");
				}
				//myRfsc.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRfscDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Rfsc FillDataRecord(IDataRecord myDataRecord)
		{
			Rfsc rfsc = new Rfsc();

			rfsc.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			rfsc.mDateFiled = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_filed"));
			rfsc.mCutoffDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("cutoff_date"));
			rfsc.mDateRequestedFrom = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_requested_from"));
			rfsc.mDateRequestedTo = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_requested_to"));
			rfsc.mEncoderId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("encoder_id"));
			rfsc.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			rfsc.mTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("type_id"));
			rfsc.mReason = myDataRecord.GetString(myDataRecord.GetOrdinal("reason"));
			rfsc.mExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("explanation"));
			rfsc.mChangeWith = myDataRecord.GetInt32(myDataRecord.GetOrdinal("change_with"));
			rfsc.mBranchFrom = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_from"));
			rfsc.mBranchTo = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_to"));
			rfsc.mIsPlanned = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_planned"));
			rfsc.mIsApproved = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_approved"));
			rfsc.mReconNumber = myDataRecord.GetString(myDataRecord.GetOrdinal("recon_number"));
			rfsc.mIsCancelled = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_cancelled"));
			rfsc.mIsExecuted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_executed"));
			rfsc.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			rfsc.mIsPrinted = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_printed"));
			rfsc.mIsAcknowledge = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_acknowledge"));
			rfsc.mAcknowledgeByUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("acknowledge_by_user_id"));
            rfsc.mRequestedBy = myDataRecord.GetString(myDataRecord.GetOrdinal("requested_by"));
            rfsc.mEmployeeName = myDataRecord.GetString(myDataRecord.GetOrdinal("employee"));
            rfsc.mFromBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("from_branch_name"));
            rfsc.mToBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("to_branch_name"));
            rfsc.mChangeWithName = myDataRecord.GetString(myDataRecord.GetOrdinal("change_with_name"));
            rfsc.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
            rfsc.mAcknowledgeBy = myDataRecord.GetString(myDataRecord.GetOrdinal("acknowledge_by"));
            rfsc.mType = myDataRecord.GetString(myDataRecord.GetOrdinal("type"));
            rfsc.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
            return rfsc;
		}
	}
}