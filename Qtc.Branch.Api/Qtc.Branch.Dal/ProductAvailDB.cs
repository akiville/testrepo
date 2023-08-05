using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class ProductAvailDB
	{
		public static ProductAvail GetItem(int productavailId)
		{
			ProductAvail productavail = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductAvailSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", productavailId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						productavail = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return productavail;
		}

		public static ProductAvailCollection GetList(ProductAvailCriteria productavailCriteria)
		{
			ProductAvailCollection tempList = new ProductAvailCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductAvailSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", productavailCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@id", productavailCriteria.mId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@category_id", productavailCriteria.mCategoryId);
                Helpers.CreateParameter(myCommand, DbType.Date, "@inventory_date", productavailCriteria.mInventoryDate);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new ProductAvailCollection();
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

        public static ProductAvailCollection GetListForDownload(ProductAvailCriteria productavailCriteria)
        {
            ProductAvailCollection tempList = new ProductAvailCollection();
            using (DbCommand myCommand = AppConfiguration.CreateCommand())
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Qt_spProductAvailForDownloadSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@is_ho_downloaded", productavailCriteria.mIsHoDownloaded);

                myCommand.Connection.Open();
                using (DbDataReader myReader = myCommand.ExecuteReader())
                {
                    if (myReader.HasRows)
                    {
                        tempList = new ProductAvailCollection();
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

        public static int SelectCountForGetList(ProductAvailCriteria productavailCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductAvailSearchList";

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

        
		public static int Save(ProductAvail myProductAvail)
		{
			if (!myProductAvail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a productavail in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductAvailInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myProductAvail.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@inventory_date", myProductAvail.mInventoryDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myProductAvail.mProductId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@category_id", myProductAvail.mCategoryId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@prior", myProductAvail.mPrior);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@avail", myProductAvail.mAvail);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@add_back", myProductAvail.mAddBack);
				Helpers.CreateParameter(myCommand, DbType.String, "@add_back_reason", myProductAvail.mAddBackReason);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@dispatch", myProductAvail.mDispatch);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@greenslip_add", myProductAvail.mGreenslipAdd);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@greenslip_cancel", myProductAvail.mGreenslipCancel);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@ddir_out", myProductAvail.mDdirOut);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@ddir_in", myProductAvail.mDdirIn);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@loe", myProductAvail.mLoe);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@ibw_out", myProductAvail.mIbwOut);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@ibw_in", myProductAvail.mIbwIn);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rma", myProductAvail.mRma);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myProductAvail.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@upload_date", myProductAvail.mUploadDate);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_ho_downloaded", myProductAvail.mIsHoDownloaded);
				//Helpers.CreateParameter(myCommand, DbType.Binary, "@signature", myProductAvail.mSignature);

				Helpers.SetSaveParameters(myCommand, myProductAvail);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update productavail as it has been updated by someone else");
				}
				
				result = Helpers.GetBusinessBaseId(myCommand);
				myCommand.Connection.Close();
			}
			return result;
		}
        public static bool Update(int branch_id, DateTime inventory_date)
        {
            int result = 0;

            using (DbCommand myCommand = AppConfiguration.CreateCommand())
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = "Qt_spProductAvailUpdateByBranch";
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", branch_id);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@inventory_date", inventory_date);
                myCommand.Connection.Open();
                result = myCommand.ExecuteNonQuery();
                myCommand.Connection.Close();

            }
            return result > 0;

        }
        public static bool Delete(int id)
		{
			int result = 0;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductAvailDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static ProductAvail FillDataRecord(IDataRecord myDataRecord)
		{
			ProductAvail productavail = new ProductAvail();

			productavail.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			productavail.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			productavail.mInventoryDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("inventory_date"));
			productavail.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
			productavail.mCategoryId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("category_id"));
			productavail.mPrior = myDataRecord.GetInt32(myDataRecord.GetOrdinal("prior"));
			productavail.mAvail = myDataRecord.GetInt32(myDataRecord.GetOrdinal("avail"));
			productavail.mAddBack = myDataRecord.GetInt32(myDataRecord.GetOrdinal("add_back"));
			productavail.mAddBackReason = myDataRecord.GetString(myDataRecord.GetOrdinal("add_back_reason"));
			productavail.mDispatch = myDataRecord.GetInt32(myDataRecord.GetOrdinal("dispatch"));
			productavail.mGreenslipAdd = myDataRecord.GetInt32(myDataRecord.GetOrdinal("greenslip_add"));
			productavail.mGreenslipCancel = myDataRecord.GetInt32(myDataRecord.GetOrdinal("greenslip_cancel"));
			productavail.mDdirOut = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ddir_out"));
			productavail.mDdirIn = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ddir_in"));
			productavail.mLoe = myDataRecord.GetInt32(myDataRecord.GetOrdinal("loe"));
			productavail.mIbwOut = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ibw_out"));
			productavail.mIbwIn = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ibw_in"));
			productavail.mRma = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rma"));
			productavail.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			productavail.mUploadDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("upload_date"));
            productavail.mProductAvailId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_avail_id"));
            productavail.mIsHoDownloaded = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_ho_downloaded"));
            //if (myDataRecord["signature"] != DBNull.Value)
            //    productavail.mSignature = (byte[])myDataRecord.GetValue(myDataRecord.GetOrdinal("background_logo"));
            //else
            //    productavail.mSignature = null;
            //productavail.mSignature = myDataRecord.GetByte(myDataRecord.GetOrdinal("signature"));
            productavail.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            productavail.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
            productavail.mCode = myDataRecord.GetString(myDataRecord.GetOrdinal("code"));
            productavail.mUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("unit"));

            return productavail;
		}
	}
}