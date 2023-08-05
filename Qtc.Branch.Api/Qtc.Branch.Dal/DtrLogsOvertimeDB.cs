using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DtrLogsOvertimeDB
	{
		public static DtrLogsOvertime GetItem(int dtrlogsovertimeId)
		{
			DtrLogsOvertime dtrlogsovertime = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDtrLogsOvertimeSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", dtrlogsovertimeId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						dtrlogsovertime = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return dtrlogsovertime;
		}

		public static DtrLogsOvertimeCollection GetList(DtrLogsOvertimeCriteria dtrlogsovertimeCriteria)
		{
			DtrLogsOvertimeCollection tempList = new DtrLogsOvertimeCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDtrLogsOvertimeSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "filed_by_id", dtrlogsovertimeCriteria.mFiledById);
                Helpers.CreateParameter(myCommand, DbType.Int32, "employee_id", dtrlogsovertimeCriteria.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "branch_ot", dtrlogsovertimeCriteria.mBranchOt);
                Helpers.CreateParameter( myCommand, DbType.DateTime, "start_date", dtrlogsovertimeCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "end_date", dtrlogsovertimeCriteria.mEndDate);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "is_sales", dtrlogsovertimeCriteria.mIsSales);
                Helpers.CreateParameter(myCommand, DbType.Int32, "lmm_id", dtrlogsovertimeCriteria.mLmmId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DtrLogsOvertimeCollection();
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

		public static int SelectCountForGetList(DtrLogsOvertimeCriteria dtrlogsovertimeCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDtrLogsOvertimeSearchList";

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

		public static int Save(DtrLogsOvertime myDtrLogsOvertime)
		{
			if (!myDtrLogsOvertime.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a dtrlogsovertime in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDtrLogsOvertimeInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myDtrLogsOvertime.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@number", myDtrLogsOvertime.mNumber);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myDtrLogsOvertime.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@shift_id", myDtrLogsOvertime.mShiftId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@filed_by_id", myDtrLogsOvertime.mFiledById);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@ot_date", myDtrLogsOvertime.mOtDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@ot_start", myDtrLogsOvertime.mOtStart);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@ot_end", myDtrLogsOvertime.mOtEnd);
				Helpers.CreateParameter(myCommand, DbType.Double, "@total_hours", myDtrLogsOvertime.mTotalHours);
				Helpers.CreateParameter(myCommand, DbType.String, "@reason", myDtrLogsOvertime.mReason);
				Helpers.CreateParameter(myCommand, DbType.String, "@explanation", myDtrLogsOvertime.mExplanation);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@approved", myDtrLogsOvertime.mApproved);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@approved_by", myDtrLogsOvertime.mApprovedBy);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@approved_date", myDtrLogsOvertime.mApprovedDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@approved_number", myDtrLogsOvertime.mApprovedNumber);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@cancelled", myDtrLogsOvertime.mCancelled);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@cancelled_by", myDtrLogsOvertime.mCancelledBy);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@cancelled_date", myDtrLogsOvertime.mCancelledDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_print", myDtrLogsOvertime.mDatePrint);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_ot", myDtrLogsOvertime.mBranchOt);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@verified_by", myDtrLogsOvertime.mVerifiedBy);
				Helpers.CreateParameter(myCommand, DbType.String, "@verified_by_remarks", myDtrLogsOvertime.mVerifiedByRemarks);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@verified_by_date", myDtrLogsOvertime.mVerifiedByDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myDtrLogsOvertime.mRecordId);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@from_local", myDtrLogsOvertime.mFromLocal);

				Helpers.SetSaveParameters(myCommand, myDtrLogsOvertime);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update dtrlogsovertime as it has been updated by someone else");
				}
				//myDtrLogsOvertime.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDtrLogsOvertimeDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static DtrLogsOvertime FillDataRecord(IDataRecord myDataRecord)
		{
			DtrLogsOvertime dtrlogsovertime = new DtrLogsOvertime();

			dtrlogsovertime.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			dtrlogsovertime.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			dtrlogsovertime.mNumber = myDataRecord.GetInt32(myDataRecord.GetOrdinal("number"));
			dtrlogsovertime.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			dtrlogsovertime.mShiftId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("shift_id"));
			dtrlogsovertime.mFiledById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("filed_by_id"));
			dtrlogsovertime.mOtDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ot_date"));
			dtrlogsovertime.mOtStart = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ot_start"));
			dtrlogsovertime.mOtEnd = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("ot_end"));
			dtrlogsovertime.mTotalHours = myDataRecord.GetDouble(myDataRecord.GetOrdinal("total_hours"));
			dtrlogsovertime.mReason = myDataRecord.GetString(myDataRecord.GetOrdinal("reason"));
			dtrlogsovertime.mExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("explanation"));
			dtrlogsovertime.mApproved = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("approved"));
			dtrlogsovertime.mApprovedBy = myDataRecord.GetInt32(myDataRecord.GetOrdinal("approved_by"));
			dtrlogsovertime.mApprovedDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("approved_date"));
			dtrlogsovertime.mApprovedNumber = myDataRecord.GetInt32(myDataRecord.GetOrdinal("approved_number"));
			dtrlogsovertime.mCancelled = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("cancelled"));
			dtrlogsovertime.mCancelledBy = myDataRecord.GetInt32(myDataRecord.GetOrdinal("cancelled_by"));
			dtrlogsovertime.mCancelledDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("cancelled_date"));
			dtrlogsovertime.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			dtrlogsovertime.mDatePrint = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_print"));
			dtrlogsovertime.mBranchOt = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_ot"));
			dtrlogsovertime.mVerifiedBy = myDataRecord.GetInt32(myDataRecord.GetOrdinal("verified_by"));
			dtrlogsovertime.mVerifiedByRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("verified_by_remarks"));
			dtrlogsovertime.mVerifiedByDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("verified_by_date"));
			dtrlogsovertime.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
            dtrlogsovertime.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            dtrlogsovertime.mEmployeeName = myDataRecord.GetString(myDataRecord.GetOrdinal("employee_name"));
            dtrlogsovertime.mFiledByName = myDataRecord.GetString(myDataRecord.GetOrdinal("filed_by_name"));
            dtrlogsovertime.mFromLocal = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("from_local"));
            dtrlogsovertime.mApprovedNumberComplete = myDataRecord.GetString(myDataRecord.GetOrdinal("approved_number_complete"));
            dtrlogsovertime.mPositionName = myDataRecord.GetString(myDataRecord.GetOrdinal("position_name"));

            return dtrlogsovertime;
		}
	}
}