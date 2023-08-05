using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class BranchCashDetailsDB
	{
		public static BranchCashDetails GetItem(int branchcashdetailsId)
		{
			BranchCashDetails branchcashdetails = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchCashDetailsSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", branchcashdetailsId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						branchcashdetails = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return branchcashdetails;
		}

		public static BranchCashDetailsCollection GetList(BranchCashDetailsCriteria branchcashdetailsCriteria)
		{
			BranchCashDetailsCollection tempList = new BranchCashDetailsCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchCashDetailsSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_cash_id", branchcashdetailsCriteria.mBranchCashId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", branchcashdetailsCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@sales_date", branchcashdetailsCriteria.mSalesDate);


				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new BranchCashDetailsCollection();
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

		public static int SelectCountForGetList(BranchCashDetailsCriteria branchcashdetailsCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchCashDetailsSearchList";

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

		public static int Save(BranchCashDetails myBranchCashDetails)
		{
			if (!myBranchCashDetails.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a branchcashdetails in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchCashDetailsInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@sales_date", myBranchCashDetails.mSalesDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@denomination_id", myBranchCashDetails.mDenominationId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@qty", myBranchCashDetails.mQty);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@total_amount", myBranchCashDetails.mTotalAmount);
				Helpers.CreateParameter(myCommand, DbType.String, "@cash_explanation", myBranchCashDetails.mCashExplanation);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_cash_id", myBranchCashDetails.mBranchCashId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myBranchCashDetails.mUserId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@upload_date", myBranchCashDetails.mUploadDate);

				Helpers.SetSaveParameters(myCommand, myBranchCashDetails);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update branchcashdetails as it has been updated by someone else");
				}
				//myBranchCashDetails.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spBranchCashDetailsDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static BranchCashDetails FillDataRecord(IDataRecord myDataRecord)
		{
			BranchCashDetails branchcashdetails = new BranchCashDetails();

			branchcashdetails.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			branchcashdetails.mSalesDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sales_date"));
			branchcashdetails.mDenominationId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("denomination_id"));
			branchcashdetails.mQty = myDataRecord.GetInt32(myDataRecord.GetOrdinal("qty"));
			branchcashdetails.mTotalAmount = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("total_amount"));
			branchcashdetails.mCashExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("cash_explanation"));
			branchcashdetails.mBranchCashId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_cash_id"));
			branchcashdetails.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			branchcashdetails.mUploadDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("upload_date"));
			branchcashdetails.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            branchcashdetails.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
            //branchcashdetails.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

            return branchcashdetails;
		}
	}
}