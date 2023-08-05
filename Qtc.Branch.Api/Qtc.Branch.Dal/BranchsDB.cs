using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class BranchsDB
	{
		public static Branchs GetItem(int branchsId)
		{
			Branchs branchs = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchsSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", branchsId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						branchs = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return branchs;
		}

		public static BranchsCollection GetList(BranchsCriteria branchsCriteria)
		{
			BranchsCollection tempList = new BranchsCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchsSearchList";

                Helpers.CreateParameter(myCommand, DbType.String, "@name", branchsCriteria.mName);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", branchsCriteria.mLmmId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new BranchsCollection();
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

		public static int SelectCountForGetList(BranchsCriteria branchsCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchsSearchList";

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

		public static int Save(Branchs myBranchs)
		{
			if (!myBranchs.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a branchs in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spBranchsInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myBranchs.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.String, "@code", myBranchs.mCode);
				Helpers.CreateParameter(myCommand, DbType.String, "@name", myBranchs.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@code2", myBranchs.mCode2);
				Helpers.CreateParameter(myCommand, DbType.String, "@classification", myBranchs.mClassification);
				Helpers.CreateParameter(myCommand, DbType.String, "@company", myBranchs.mCompany);
				Helpers.CreateParameter(myCommand, DbType.String, "@tin", myBranchs.mTin);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@area_id", myBranchs.mAreaId);
				Helpers.CreateParameter(myCommand, DbType.String, "@address", myBranchs.mAddress);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@mc_id", myBranchs.mMcId);
				Helpers.CreateParameter(myCommand, DbType.String, "@bank", myBranchs.mBank);
				Helpers.CreateParameter(myCommand, DbType.String, "@contact_no", myBranchs.mContactNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@store_hour_start", myBranchs.mStoreHourStart);
				Helpers.CreateParameter(myCommand, DbType.String, "@store_hour_end", myBranchs.mStoreHourEnd);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@delivery_time", myBranchs.mDeliveryTime);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@sunday", myBranchs.mSunday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@monday", myBranchs.mMonday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@tuesday", myBranchs.mTuesday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@wednesday", myBranchs.mWednesday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@thursday", myBranchs.mThursday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@friday", myBranchs.mFriday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@saturday", myBranchs.mSaturday);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@bank_id", myBranchs.mBankId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rp_assistant_id", myBranchs.mRpAssistantId);
                Helpers.CreateParameter(myCommand, DbType.Decimal, "@cash", myBranchs.mCash);
                Helpers.CreateParameter(myCommand, DbType.Decimal, "@grab_food", myBranchs.mGrabFood);
                Helpers.CreateParameter(myCommand, DbType.Decimal, "@foodpanda", myBranchs.mFoodPanda);
                Helpers.CreateParameter(myCommand, DbType.Decimal, "@total_cash", myBranchs.mTotalCash);

                Helpers.SetSaveParameters(myCommand, myBranchs);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update branchs as it has been updated by someone else");
				}
				//myBranchs.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spBranchsDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Branchs FillDataRecord(IDataRecord myDataRecord)
		{
			Branchs branchs = new Branchs();

			branchs.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			branchs.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			branchs.mCode = myDataRecord.GetString(myDataRecord.GetOrdinal("code"));
			branchs.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			branchs.mCode2 = myDataRecord.GetString(myDataRecord.GetOrdinal("code2"));
			branchs.mClassification = myDataRecord.GetString(myDataRecord.GetOrdinal("classification"));
			branchs.mCompany = myDataRecord.GetString(myDataRecord.GetOrdinal("company"));
			branchs.mTin = myDataRecord.GetString(myDataRecord.GetOrdinal("tin"));
			branchs.mAreaId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("area_id"));
			branchs.mAddress = myDataRecord.GetString(myDataRecord.GetOrdinal("address"));
			branchs.mMcId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("mc_id"));
			branchs.mBank = myDataRecord.GetString(myDataRecord.GetOrdinal("bank"));
			branchs.mContactNo = myDataRecord.GetString(myDataRecord.GetOrdinal("contact_no"));
			branchs.mStoreHourStart = myDataRecord.GetString(myDataRecord.GetOrdinal("store_hour_start"));
			branchs.mStoreHourEnd = myDataRecord.GetString(myDataRecord.GetOrdinal("store_hour_end"));
			branchs.mDeliveryTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("delivery_time"));
			branchs.mSunday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("sunday"));
			branchs.mMonday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("monday"));
			branchs.mTuesday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("tuesday"));
			branchs.mWednesday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("wednesday"));
			branchs.mThursday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("thursday"));
			branchs.mFriday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("friday"));
			branchs.mSaturday = myDataRecord.GetInt32(myDataRecord.GetOrdinal("saturday"));
			branchs.mBankId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("bank_id"));
			branchs.mRpAssistantId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rp_assistant_id"));
			branchs.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            branchs.mIsLoggedIn = myDataRecord.GetInt32(myDataRecord.GetOrdinal("is_logged_in"));
            branchs.mMclId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("mcl_id"));
            branchs.mPmisBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("pmis_branch_id"));
            branchs.mDeliveryId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("delivery_id"));
            branchs.mEta = myDataRecord.GetString(myDataRecord.GetOrdinal("eta"));
            branchs.mCash = myDataRecord.GetFloat(myDataRecord.GetOrdinal("cash"));
            branchs.mGrabFood = myDataRecord.GetFloat(myDataRecord.GetOrdinal("grab_food"));
            branchs.mFoodPanda = myDataRecord.GetFloat(myDataRecord.GetOrdinal("foodpanda"));
            branchs.mTotalCash = myDataRecord.GetFloat(myDataRecord.GetOrdinal("total_cash"));
            branchs.mLmmCashId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_cash_id"));
            branchs.mLmmCashRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("lmm_cash_remarks"));
            branchs.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
            //branchs.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

            return branchs;
		}
	}
}