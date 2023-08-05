using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class BranchCashDB
	{
		public static BranchCash GetItem(int branchcashId)
		{
			BranchCash branchcash = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchCashSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", branchcashId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						branchcash = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return branchcash;
		}

		public static BranchCashCollection GetList(BranchCashCriteria branchcashCriteria)
		{
			BranchCashCollection tempList = new BranchCashCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchCashSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", branchcashCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.String, "@start_date", branchcashCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.String, "@end_date", branchcashCriteria.mEndDate);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new BranchCashCollection();
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

		public static int SelectCountForGetList(BranchCashCriteria branchcashCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchCashSearchList";

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

		public static int Save(BranchCash myBranchCash)
		{
			if (!myBranchCash.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a branchcash in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchCashInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myBranchCash.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@sales_date", myBranchCash.mSalesDate);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@total_amount", myBranchCash.mTotalAmount);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@is_deposited", myBranchCash.mIsDeposited);
				Helpers.CreateParameter(myCommand, DbType.String, "@cash_explanation", myBranchCash.mCashExplanation);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myBranchCash.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@deposited_by_id", myBranchCash.mDepositedById);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@deposit_date", myBranchCash.mDepositDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@deposit_validated", myBranchCash.mDepositValidated);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myBranchCash.mDatestamp);
                Helpers.CreateParameter(myCommand, DbType.String, "@deposit_slip_image_name", myBranchCash.mDepositSlipImageName);
				Helpers.SetSaveParameters(myCommand, myBranchCash);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update branchcash as it has been updated by someone else");
				}
				//myBranchCash.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spBranchCashDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static BranchCash FillDataRecord(IDataRecord myDataRecord)
		{
			BranchCash branchcash = new BranchCash();

			branchcash.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			branchcash.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			branchcash.mSalesDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sales_date"));
			branchcash.mTotalAmount = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("total_amount"));
			branchcash.mIsDeposited = myDataRecord.GetInt32(myDataRecord.GetOrdinal("is_deposited"));
			branchcash.mCashExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("cash_explanation"));
			branchcash.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			branchcash.mDepositedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("deposited_by_id"));
			branchcash.mDepositDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("deposit_date"));
			branchcash.mDepositValidated = myDataRecord.GetInt32(myDataRecord.GetOrdinal("deposit_validated"));
			branchcash.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
            branchcash.mDepositSlipImageName = myDataRecord.GetString(myDataRecord.GetOrdinal("deposit_slip_image_name"));
			//branchcash.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			//branchcash.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

			return branchcash;
		}
	}
}