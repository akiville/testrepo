using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class SalesScheduleDB
	{
		public static SalesSchedule GetItem(int salesscheduleId)
		{
			SalesSchedule salesschedule = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesScheduleSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", salesscheduleId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						salesschedule = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return salesschedule;
		}

		public static SalesScheduleCollection GetList(SalesScheduleCriteria salesscheduleCriteria)
		{
			SalesScheduleCollection tempList = new SalesScheduleCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesScheduleSearchList";

                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", salesscheduleCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", salesscheduleCriteria.mEndDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", salesscheduleCriteria.mLmmId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new SalesScheduleCollection();
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

		public static int SelectCountForGetList(SalesScheduleCriteria salesscheduleCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesScheduleSearchList";

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

		public static int Save(SalesSchedule mySalesSchedule)
		{
			if (!mySalesSchedule.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a salesschedule in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesScheduleInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@schedule_id", mySalesSchedule.mScheduleId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", mySalesSchedule.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", mySalesSchedule.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date", mySalesSchedule.mDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@has_id", mySalesSchedule.mHasId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@expired_id", mySalesSchedule.mExpiredId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@schedule", mySalesSchedule.mSchedule);
				Helpers.CreateParameter(myCommand, DbType.String, "@time_in", mySalesSchedule.mTimeIn);
				Helpers.CreateParameter(myCommand, DbType.String, "@breakout", mySalesSchedule.mBreakout);
				Helpers.CreateParameter(myCommand, DbType.String, "@breakin", mySalesSchedule.mBreakin);
				Helpers.CreateParameter(myCommand, DbType.String, "@time_out", mySalesSchedule.mTimeOut);
                Helpers.CreateParameter(myCommand, DbType.String, "@attendance_status", mySalesSchedule.mAttendanceStatus);
                Helpers.CreateParameter(myCommand, DbType.String, "@attendance_remarks", mySalesSchedule.mAttendanceRemarks);


				Helpers.SetSaveParameters(myCommand, mySalesSchedule);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update salesschedule as it has been updated by someone else");
				}
				//mySalesSchedule.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spSalesScheduleDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static SalesSchedule FillDataRecord(IDataRecord myDataRecord)
		{
			SalesSchedule salesschedule = new SalesSchedule();

			salesschedule.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			salesschedule.mScheduleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("schedule_id"));
			salesschedule.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			salesschedule.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			salesschedule.mDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date"));
			salesschedule.mHasId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("has_id"));
			salesschedule.mExpiredId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("expired_id"));
			salesschedule.mSchedule = myDataRecord.GetInt32(myDataRecord.GetOrdinal("schedule"));
			salesschedule.mTimeIn = myDataRecord.GetString(myDataRecord.GetOrdinal("time_in"));
			salesschedule.mBreakout = myDataRecord.GetString(myDataRecord.GetOrdinal("breakout"));
			salesschedule.mBreakin = myDataRecord.GetString(myDataRecord.GetOrdinal("breakin"));
			salesschedule.mTimeOut = myDataRecord.GetString(myDataRecord.GetOrdinal("time_out"));
			salesschedule.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            salesschedule.mAttendanceStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("attendance_status"));
            salesschedule.mAttendanceRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("attendance_remarks"));
            salesschedule.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
            salesschedule.mBranch = myDataRecord.GetString(myDataRecord.GetOrdinal("branch"));

            //salesschedule.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

            return salesschedule;
		}
	}
}