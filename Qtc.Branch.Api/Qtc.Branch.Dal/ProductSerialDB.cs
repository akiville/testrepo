using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class ProductSerialDB
	{
		public static ProductSerial GetItem(int productserialId)
		{
			ProductSerial productserial = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductSerialSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", productserialId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						productserial = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return productserial;
		}

		public static ProductSerialCollection GetList(ProductSerialCriteria productserialCriteria)
		{
			ProductSerialCollection tempList = new ProductSerialCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductSerialSearchList";

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new ProductSerialCollection();
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

		public static int SelectCountForGetList(ProductSerialCriteria productserialCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductSerialSearchList";

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

		public static int Save(ProductSerial myProductSerial)
		{
			if (!myProductSerial.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a productserial in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductSerialInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myProductSerial.mProductId);
				Helpers.CreateParameter(myCommand, DbType.String, "@brand", myProductSerial.mBrand);
				Helpers.CreateParameter(myCommand, DbType.String, "@description", myProductSerial.mDescription);
				Helpers.CreateParameter(myCommand, DbType.String, "@sticker_no", myProductSerial.mStickerNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@control_no", myProductSerial.mControlNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@serial_no", myProductSerial.mSerialNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@model_no", myProductSerial.mModelNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@dimension", myProductSerial.mDimension);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@service", myProductSerial.mService);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id_location", myProductSerial.mBranchIdLocation);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myProductSerial.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Double, "@cost", myProductSerial.mCost);
				Helpers.CreateParameter(myCommand, DbType.String, "@supplier", myProductSerial.mSupplier);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rm2_id", myProductSerial.mRm2Id);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@rp_id", myProductSerial.mRpId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_model_id", myProductSerial.mProductModelId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@for_inventory", myProductSerial.mForInventory);

				Helpers.SetSaveParameters(myCommand, myProductSerial);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update productserial as it has been updated by someone else");
				}
				//myProductSerial.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spProductSerialDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static ProductSerial FillDataRecord(IDataRecord myDataRecord)
		{
			ProductSerial productserial = new ProductSerial();

			productserial.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			productserial.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
			productserial.mBrand = myDataRecord.GetString(myDataRecord.GetOrdinal("brand"));
			productserial.mDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("description"));
			productserial.mStickerNo = myDataRecord.GetString(myDataRecord.GetOrdinal("sticker_no"));
			productserial.mControlNo = myDataRecord.GetString(myDataRecord.GetOrdinal("control_no"));
			productserial.mSerialNo = myDataRecord.GetString(myDataRecord.GetOrdinal("serial_no"));
			productserial.mModelNo = myDataRecord.GetString(myDataRecord.GetOrdinal("model_no"));
			productserial.mDimension = myDataRecord.GetString(myDataRecord.GetOrdinal("dimension"));
			productserial.mService = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("service"));
			productserial.mBranchIdLocation = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id_location"));
			productserial.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			productserial.mCost = myDataRecord.GetDouble(myDataRecord.GetOrdinal("cost"));
			productserial.mSupplier = myDataRecord.GetString(myDataRecord.GetOrdinal("supplier"));
			productserial.mRm2Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rm2_id"));
			productserial.mRpId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("rp_id"));
			productserial.mProductModelId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_model_id"));
			productserial.mForInventory = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("for_inventory"));
			productserial.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return productserial;
		}
	}
}