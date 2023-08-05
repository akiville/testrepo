using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class LoeDetailDB
	{
		public static LoeDetail GetItem(int loedetailId)
		{
			LoeDetail loedetail = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLoeDetailSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", loedetailId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						loedetail = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return loedetail;
		}

		public static LoeDetailCollection GetList(LoeDetailCriteria loedetailCriteria)
		{
			LoeDetailCollection tempList = new LoeDetailCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLoeDetailSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@loe_id", loedetailCriteria.mLoeId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new LoeDetailCollection();
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

		public static int SelectCountForGetList(LoeDetailCriteria loedetailCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLoeDetailSearchList";

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

		public static int Save(LoeDetail myLoeDetail)
		{
			if (!myLoeDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a loedetail in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spLoeDetailInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@loe_id", myLoeDetail.mLoeId);
				Helpers.CreateParameter(myCommand, DbType.Double, "@quantity", myLoeDetail.mQuantity);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@approved_quantity", myLoeDetail.mApprovedQuantity);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myLoeDetail.mProductId);
				Helpers.CreateParameter(myCommand, DbType.Double, "@price", myLoeDetail.mPrice);
				Helpers.CreateParameter(myCommand, DbType.Double, "@discount", myLoeDetail.mDiscount);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@employee_id", myLoeDetail.mEmployeeId);
				Helpers.CreateParameter(myCommand, DbType.String, "@unit", myLoeDetail.mUnit);
				Helpers.CreateParameter(myCommand, DbType.String, "@product", myLoeDetail.mProduct);
				Helpers.CreateParameter(myCommand, DbType.String, "@edited_quantity", myLoeDetail.mEditedQuantity);
				Helpers.CreateParameter(myCommand, DbType.String, "@edited_price", myLoeDetail.mEditedPrice);
				Helpers.CreateParameter(myCommand, DbType.String, "@edited_discount", myLoeDetail.mEditedDiscount);

				Helpers.SetSaveParameters(myCommand, myLoeDetail);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update loedetail as it has been updated by someone else");
				}
				//myLoeDetail.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spLoeDetailDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static LoeDetail FillDataRecord(IDataRecord myDataRecord)
		{
			LoeDetail loedetail = new LoeDetail();

			loedetail.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			loedetail.mLoeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("loe_id"));
			loedetail.mQuantity = myDataRecord.GetDouble(myDataRecord.GetOrdinal("quantity"));
			loedetail.mApprovedQuantity = myDataRecord.GetInt32(myDataRecord.GetOrdinal("approved_quantity"));
			loedetail.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
			loedetail.mPrice = myDataRecord.GetDouble(myDataRecord.GetOrdinal("price"));
			loedetail.mDiscount = myDataRecord.GetDouble(myDataRecord.GetOrdinal("discount"));
			loedetail.mEmployeeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("employee_id"));
			loedetail.mUnit = myDataRecord.GetString(myDataRecord.GetOrdinal("unit"));
			loedetail.mProduct = myDataRecord.GetString(myDataRecord.GetOrdinal("product"));
			loedetail.mEditedQuantity = myDataRecord.GetString(myDataRecord.GetOrdinal("edited_quantity"));
			loedetail.mEditedPrice = myDataRecord.GetString(myDataRecord.GetOrdinal("edited_price"));
			loedetail.mEditedDiscount = myDataRecord.GetString(myDataRecord.GetOrdinal("edited_discount"));
			loedetail.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return loedetail;
		}
	}
}