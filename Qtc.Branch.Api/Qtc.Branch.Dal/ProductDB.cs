using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class ProductDB
	{
		public static Product GetItem(int productId)
		{
			Product product = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", productId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						product = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return product;
		}

		public static ProductCollection GetList(ProductCriteria productCriteria)
		{
			ProductCollection tempList = new ProductCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@rp_filing", productCriteria.mRpFilingValue);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new ProductCollection();
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

		public static int SelectCountForGetList(ProductCriteria productCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductSearchList";

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

		public static int Save(Product myProduct)
		{
			if (!myProduct.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a product in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spProductInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_group_id", myProduct.mProductGroupId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@category_id", myProduct.mCategoryId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_type_id", myProduct.mProductTypeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@category_2_id", myProduct.mCategory2Id);
				Helpers.CreateParameter(myCommand, DbType.String, "@name", myProduct.mName);
				Helpers.CreateParameter(myCommand, DbType.String, "@code", myProduct.mCode);
				Helpers.CreateParameter(myCommand, DbType.String, "@description", myProduct.mDescription);
				Helpers.CreateParameter(myCommand, DbType.String, "@size", myProduct.mSize);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@brand_id", myProduct.mBrandId);
				Helpers.CreateParameter(myCommand, DbType.String, "@brand", myProduct.mBrand);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@color_id", myProduct.mColorId);
				Helpers.CreateParameter(myCommand, DbType.String, "@color", myProduct.mColor);
				Helpers.CreateParameter(myCommand, DbType.String, "@dimension", myProduct.mDimension);
				Helpers.CreateParameter(myCommand, DbType.String, "@serial", myProduct.mSerial);
				Helpers.CreateParameter(myCommand, DbType.String, "@model", myProduct.mModel);
				Helpers.CreateParameter(myCommand, DbType.String, "@control_no", myProduct.mControlNo);
				Helpers.CreateParameter(myCommand, DbType.String, "@unit", myProduct.mUnit);
				Helpers.CreateParameter(myCommand, DbType.String, "@matching_items", myProduct.mMatchingItems);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@ordering", myProduct.mOrdering);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@perishable", myProduct.mPerishable);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@required_date", myProduct.mRequiredDate);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@chargeable", myProduct.mChargeable);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@budget", myProduct.mBudget);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@equipment", myProduct.mEquipment);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@repair_parts", myProduct.mRepairParts);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@rp_filing", myProduct.mRpFiling);
				Helpers.SetSaveParameters(myCommand, myProduct);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update product as it has been updated by someone else");
				}
				//myProduct.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spProductDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Product FillDataRecord(IDataRecord myDataRecord)
		{
			Product product = new Product();

			product.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			product.mProductGroupId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_group_id"));
			product.mCategoryId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("category_id"));
			product.mProductTypeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_type_id"));
			product.mCategory2Id = myDataRecord.GetInt32(myDataRecord.GetOrdinal("category_2_id"));
			product.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));
			product.mCode = myDataRecord.GetString(myDataRecord.GetOrdinal("code"));
			product.mDescription = myDataRecord.GetString(myDataRecord.GetOrdinal("description"));
			product.mSize = myDataRecord.GetString(myDataRecord.GetOrdinal("size"));
			product.mBrandId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("brand_id"));
			product.mBrand = myDataRecord.GetString(myDataRecord.GetOrdinal("brand"));
			product.mColorId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("color_id"));
			product.mColor = myDataRecord.GetString(myDataRecord.GetOrdinal("color"));
			product.mDimension = myDataRecord.GetString(myDataRecord.GetOrdinal("dimension"));
			product.mSerial = myDataRecord.GetString(myDataRecord.GetOrdinal("serial"));
			product.mModel = myDataRecord.GetString(myDataRecord.GetOrdinal("model"));
			product.mControlNo = myDataRecord.GetString(myDataRecord.GetOrdinal("control_no"));
			product.mUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("unit"));
			product.mMatchingItems = myDataRecord.GetString(myDataRecord.GetOrdinal("matching_items"));
			product.mOrdering = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ordering"));
			product.mPerishable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("perishable"));
			product.mRequiredDate = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("required_date"));
			product.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			product.mChargeable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("chargeable"));
			product.mBudget = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("budget"));
			product.mEquipment = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("equipment"));
			product.mRepairParts = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("repair_parts"));
            product.mRpFiling = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("rp_filing"));
			return product;
		}
	}
}