using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class SalesSchedulingConcernDB
	{
		public static SalesSchedulingConcern GetItem(int salesschedulingconcernId)
		{
			SalesSchedulingConcern salesschedulingconcern = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingConcernSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", salesschedulingconcernId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						salesschedulingconcern = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return salesschedulingconcern;
		}

		public static SalesSchedulingConcernCollection GetList(SalesSchedulingConcernCriteria salesschedulingconcernCriteria)
		{
			SalesSchedulingConcernCollection tempList = new SalesSchedulingConcernCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingConcernSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@sales_scheduling_id", salesschedulingconcernCriteria.mSalesSchedulingId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", salesschedulingconcernCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", salesschedulingconcernCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.String, "@status", salesschedulingconcernCriteria.mStatus);
                

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new SalesSchedulingConcernCollection();
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

		public static int SelectCountForGetList(SalesSchedulingConcernCriteria salesschedulingconcernCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingConcernSearchList";

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

		public static int Save(SalesSchedulingConcern mySalesSchedulingConcern)
		{
			if (!mySalesSchedulingConcern.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a salesschedulingconcern in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spSalesSchedulingConcernInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@sales_scheduling_id", mySalesSchedulingConcern.mSalesSchedulingId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", mySalesSchedulingConcern.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", mySalesSchedulingConcern.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.String, "@concern", mySalesSchedulingConcern.mConcern);
				Helpers.CreateParameter(myCommand, DbType.String, "@status", mySalesSchedulingConcern.mStatus);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_submitted", mySalesSchedulingConcern.mDateSubmitted);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@checked_by", mySalesSchedulingConcern.mCheckedBy);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@checked_date", mySalesSchedulingConcern.mCheckedDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", mySalesSchedulingConcern.mDatestamp);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@cutoff_id", mySalesSchedulingConcern.mCutoffId);
                Helpers.CreateParameter(myCommand, DbType.String, "@concern_date", mySalesSchedulingConcern.mConcernDate);
				Helpers.SetSaveParameters(myCommand, mySalesSchedulingConcern);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update salesschedulingconcern as it has been updated by someone else");
				}
				//mySalesSchedulingConcern.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spSalesSchedulingConcernDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static SalesSchedulingConcern FillDataRecord(IDataRecord myDataRecord)
		{
			SalesSchedulingConcern salesschedulingconcern = new SalesSchedulingConcern();

			salesschedulingconcern.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			salesschedulingconcern.mSalesSchedulingId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("sales_scheduling_id"));
			salesschedulingconcern.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			salesschedulingconcern.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			salesschedulingconcern.mConcern = myDataRecord.GetString(myDataRecord.GetOrdinal("concern"));
			salesschedulingconcern.mStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("status"));
			salesschedulingconcern.mDateSubmitted = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_submitted"));
			salesschedulingconcern.mCheckedBy = myDataRecord.GetInt32(myDataRecord.GetOrdinal("checked_by"));
			salesschedulingconcern.mCheckedDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("checked_date"));
			salesschedulingconcern.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			salesschedulingconcern.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
            salesschedulingconcern.mLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_name"));
            salesschedulingconcern.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            salesschedulingconcern.mCutoffId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("cutoff_id"));
            salesschedulingconcern.mStartDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("start_date"));
            salesschedulingconcern.mEndDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("end_date"));
            salesschedulingconcern.mConcernDate = myDataRecord.GetString(myDataRecord.GetOrdinal("concern_date"));
            salesschedulingconcern.mEmployeeName = myDataRecord.GetString(myDataRecord.GetOrdinal("employee_name"));

			return salesschedulingconcern;
		}
	}
}