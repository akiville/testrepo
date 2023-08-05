using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class BranchRequisitionDB
	{
		public static BranchRequisition GetItem(int branchrequisitionId)
		{
			BranchRequisition branchrequisition = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchRequisitionSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", branchrequisitionId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						branchrequisition = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return branchrequisition;
		}

		public static BranchRequisitionCollection GetList(BranchRequisitionCriteria branchrequisitionCriteria)
		{
			BranchRequisitionCollection tempList = new BranchRequisitionCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchRequisitionSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", branchrequisitionCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@sales_date", branchrequisitionCriteria.mSalesDate);
                Helpers.CreateParameter(myCommand, DbType.String, "@code", branchrequisitionCriteria.mCode);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", branchrequisitionCriteria.mEmployeeId);
                Helpers.CreateParameter(myCommand, DbType.String, "@status", branchrequisitionCriteria.mStatus);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", branchrequisitionCriteria.mLmmId);


				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new BranchRequisitionCollection();
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

		public static int SelectCountForGetList(BranchRequisitionCriteria branchrequisitionCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchRequisitionSearchList";

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

		public static int Save(BranchRequisition myBranchRequisition)
		{
			if (!myBranchRequisition.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a branchrequisition in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchRequisitionInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@sales_date", myBranchRequisition.mSalesDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myBranchRequisition.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myBranchRequisition.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myBranchRequisition.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_created", myBranchRequisition.mDateCreated);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_updated", myBranchRequisition.mDateUpdated);
				Helpers.CreateParameter(myCommand, DbType.String, "@lmm_remarks", myBranchRequisition.mLmmRemarks);
				Helpers.CreateParameter(myCommand, DbType.String, "@employee_remarks", myBranchRequisition.mEmployeeRemarks);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myBranchRequisition.mDatestamp);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myBranchRequisition.mUserId);
				Helpers.CreateParameter(myCommand, DbType.String, "@code", myBranchRequisition.mCode);

				Helpers.SetSaveParameters(myCommand, myBranchRequisition);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update branchrequisition as it has been updated by someone else");
				}
				//myBranchRequisition.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spBranchRequisitionDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static BranchRequisition FillDataRecord(IDataRecord myDataRecord)
		{
			BranchRequisition branchrequisition = new BranchRequisition();

			branchrequisition.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			branchrequisition.mSalesDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sales_date"));
			branchrequisition.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			branchrequisition.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			branchrequisition.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			branchrequisition.mDateCreated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_created"));
			branchrequisition.mDateUpdated = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_updated"));
			branchrequisition.mLmmRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_remarks"));
			branchrequisition.mEmployeeRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("employee_remarks"));
			branchrequisition.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			branchrequisition.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			branchrequisition.mCode = myDataRecord.GetString(myDataRecord.GetOrdinal("code"));
			branchrequisition.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            branchrequisition.mLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_name"));
            branchrequisition.mEmployeeName = myDataRecord.GetString(myDataRecord.GetOrdinal("employee_name"));
            branchrequisition.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));

            return branchrequisition;
		}
	}
}