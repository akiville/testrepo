using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class LmmCashCountDB
	{
		public static LmmCashCount GetItem(int lmmcashcountId)
		{
			LmmCashCount lmmcashcount = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmCashCountSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", lmmcashcountId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						lmmcashcount = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return lmmcashcount;
		}

        public static LmmCashCountCollection GetListCount(LmmCashCountCriteria lmmcashcountCriteria)
        {
            LmmCashCountCollection tempList = new LmmCashCountCollection();
            using (DbCommand myCommand = AppConfiguration.CreateCommand())
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "syPa_LmmCashCount";
                
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", lmmcashcountCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", lmmcashcountCriteria.mEndDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", lmmcashcountCriteria.mLmmId);
                myCommand.Connection.Open();
                using (DbDataReader myReader = myCommand.ExecuteReader())
                {
                    if (myReader.HasRows)
                    {
                        tempList = new LmmCashCountCollection();
                        while (myReader.Read())
                        {
                            tempList.Add(FillDataRecordCount(myReader));
                        }
                        myReader.Close();
                    }
                }
                myCommand.Connection.Close();
            }

            return tempList;
        }

        public static LmmCashCountCollection GetList(LmmCashCountCriteria lmmcashcountCriteria)
		{
			LmmCashCountCollection tempList = new LmmCashCountCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmCashCountSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", lmmcashcountCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@start_date", lmmcashcountCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@end_date", lmmcashcountCriteria.mEndDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", lmmcashcountCriteria.mLmmId);
                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new LmmCashCountCollection();
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

		public static int SelectCountForGetList(LmmCashCountCriteria lmmcashcountCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmCashCountSearchList";

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

		public static int Save(LmmCashCount myLmmCashCount)
		{
			if (!myLmmCashCount.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a lmmcashcount in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLmmCashCountInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myLmmCashCount.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@sales_date", myLmmCashCount.mSalesDate);
				Helpers.CreateParameter(myCommand, DbType.Double, "@cash", myLmmCashCount.mCash);
				Helpers.CreateParameter(myCommand, DbType.Double, "@grab_food", myLmmCashCount.mGrabFood);
				Helpers.CreateParameter(myCommand, DbType.Double, "@foodpanda", myLmmCashCount.mFoodpanda);
				Helpers.CreateParameter(myCommand, DbType.Double, "@total_cash", myLmmCashCount.mTotalCash);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@user_id", myLmmCashCount.mUserId);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myLmmCashCount.mRemarks);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@deposit_date", myLmmCashCount.mDepositDate);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_deposited", myLmmCashCount.mIsDeposited);
                Helpers.CreateParameter(myCommand, DbType.String, "@deposit_remarks", myLmmCashCount.mDepositRemarks);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_closed", myLmmCashCount.mIsClosed);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_deposit", myLmmCashCount.mIsDeposit);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_pickup", myLmmCashCount.mIsPickup);
                Helpers.CreateParameter(myCommand, DbType.String, "@deposit_transaction_no", myLmmCashCount.mDepositTransactionNo);
                Helpers.CreateParameter(myCommand, DbType.String, "@pickup_receipt_no", myLmmCashCount.mPickUpReceiptNo);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@pickup_date", myLmmCashCount.mPickUpDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@foodpanda_id", myLmmCashCount.mFoodPandaId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@grabfood_id", myLmmCashCount.mGrabFoodId);
                Helpers.CreateParameter(myCommand, DbType.Double, "@maya", myLmmCashCount.mMaya);
                //Helpers.CreateParameter(myCommand, DbType.Int32, "@maya_id", myLmmCashCount.mMayaId);


                Helpers.SetSaveParameters(myCommand, myLmmCashCount);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update lmmcashcount as it has been updated by someone else");
				}
				//myLmmCashCount.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spLmmCashCountDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}
        private static LmmCashCount FillDataRecordCount(IDataRecord myDataRecord)
        {
            LmmCashCount lmmcashcount = new LmmCashCount();

            lmmcashcount.mTotalItem = myDataRecord.GetInt32(myDataRecord.GetOrdinal("total_item"));
            lmmcashcount.mWithEntry = myDataRecord.GetInt32(myDataRecord.GetOrdinal("with_entry"));
            lmmcashcount.mLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_name"));
            lmmcashcount.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
            lmmcashcount.mSalesDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sales_date"));

            return lmmcashcount;
        }
        private static LmmCashCount FillDataRecord(IDataRecord myDataRecord)
		{
			LmmCashCount lmmcashcount = new LmmCashCount();

			lmmcashcount.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			lmmcashcount.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			lmmcashcount.mSalesDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("sales_date"));
			lmmcashcount.mCash = myDataRecord.GetDouble(myDataRecord.GetOrdinal("cash"));
			lmmcashcount.mGrabFood = myDataRecord.GetDouble(myDataRecord.GetOrdinal("grab_food"));
			lmmcashcount.mFoodpanda = myDataRecord.GetDouble(myDataRecord.GetOrdinal("foodpanda"));
			lmmcashcount.mTotalCash = myDataRecord.GetDouble(myDataRecord.GetOrdinal("total_cash"));
			lmmcashcount.mUserId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("user_id"));
			lmmcashcount.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			lmmcashcount.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            lmmcashcount.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
            lmmcashcount.mOnDuty = myDataRecord.GetString(myDataRecord.GetOrdinal("on_duty"));
            lmmcashcount.mLmmName = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_name"));
            lmmcashcount.mDepositDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("deposit_date"));
            lmmcashcount.mIsDeposited = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_deposited"));
            lmmcashcount.mDepositRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("deposit_remarks"));
            lmmcashcount.mIsClosed = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_closed"));
            lmmcashcount.mIsDeposit = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_deposit"));
            lmmcashcount.mIsPickup = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_pickup"));
            lmmcashcount.mDepositTransactionNo = myDataRecord.GetString(myDataRecord.GetOrdinal("deposit_transaction_no"));
            lmmcashcount.mPickUpReceiptNo = myDataRecord.GetString(myDataRecord.GetOrdinal("pickup_receipt_no"));
            lmmcashcount.mPickUpDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("pickup_date"));
            lmmcashcount.mFoodPandaId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("foodpanda_id"));
            lmmcashcount.mGrabFoodId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("grabfood_id"));
            lmmcashcount.mMaya = myDataRecord.GetDouble(myDataRecord.GetOrdinal("maya"));
            lmmcashcount.mMayaId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("maya_id"));

            return lmmcashcount;
		}
	}
}