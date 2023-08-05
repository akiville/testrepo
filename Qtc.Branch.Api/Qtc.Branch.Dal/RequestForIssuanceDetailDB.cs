using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RequestForIssuanceDetailDB
	{
		public static RequestForIssuanceDetail GetItem(int requestforissuancedetailId)
		{
			RequestForIssuanceDetail requestforissuancedetail = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestForIssuanceDetailSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", requestforissuancedetailId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						requestforissuancedetail = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return requestforissuancedetail;
		}

		public static RequestForIssuanceDetailCollection GetList(RequestForIssuanceDetailCriteria requestforissuancedetailCriteria)
		{
			RequestForIssuanceDetailCollection tempList = new RequestForIssuanceDetailCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestForIssuanceDetailSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@request_for_issuance_id", requestforissuancedetailCriteria.mRequestForIssuanceId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RequestForIssuanceDetailCollection();
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

		public static int SelectCountForGetList(RequestForIssuanceDetailCriteria requestforissuancedetailCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestForIssuanceDetailSearchList";

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

		public static int Save(RequestForIssuanceDetail myRequestForIssuanceDetail)
		{
			if (!myRequestForIssuanceDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a requestforissuancedetail in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestForIssuanceDetailInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@request_for_issuance_id", myRequestForIssuanceDetail.mRequestForIssuanceId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myRequestForIssuanceDetail.mProductId);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@requested_qty", myRequestForIssuanceDetail.mRequestedQty);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@approved_qty", myRequestForIssuanceDetail.mApprovedQty);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@for_purchase_quantity", myRequestForIssuanceDetail.mForPurchaseQuantity);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@approved_quantity_for_purchase", myRequestForIssuanceDetail.mApprovedQuantityForPurchase);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@released_quantity", myRequestForIssuanceDetail.mReleasedQuantity);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@used_quantity", myRequestForIssuanceDetail.mUsedQuantity);
				Helpers.CreateParameter(myCommand, DbType.Decimal, "@cost", myRequestForIssuanceDetail.mCost);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myRequestForIssuanceDetail.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@cancelled", myRequestForIssuanceDetail.mCancelled);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_serial_id_gatepass", myRequestForIssuanceDetail.mProductSerialIdGatepass);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_serial_id_ingress", myRequestForIssuanceDetail.mProductSerialIdIngress);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myRequestForIssuanceDetail.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myRequestForIssuanceDetail);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update requestforissuancedetail as it has been updated by someone else");
				}
				//myRequestForIssuanceDetail.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRequestForIssuanceDetailDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RequestForIssuanceDetail FillDataRecord(IDataRecord myDataRecord)
		{
			RequestForIssuanceDetail requestforissuancedetail = new RequestForIssuanceDetail();

			requestforissuancedetail.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			requestforissuancedetail.mRequestForIssuanceId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("request_for_issuance_id"));
			requestforissuancedetail.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
			requestforissuancedetail.mRequestedQty = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("requested_qty"));
			requestforissuancedetail.mApprovedQty = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("approved_qty"));
			requestforissuancedetail.mForPurchaseQuantity = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("for_purchase_quantity"));
			requestforissuancedetail.mApprovedQuantityForPurchase = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("approved_quantity_for_purchase"));
			requestforissuancedetail.mReleasedQuantity = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("released_quantity"));
			requestforissuancedetail.mUsedQuantity = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("used_quantity"));
			requestforissuancedetail.mCost = myDataRecord.GetDecimal(myDataRecord.GetOrdinal("cost"));
			requestforissuancedetail.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			requestforissuancedetail.mCancelled = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("cancelled"));
			requestforissuancedetail.mProductSerialIdGatepass = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_serial_id_gatepass"));
			requestforissuancedetail.mProductSerialIdIngress = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_serial_id_ingress"));
			requestforissuancedetail.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			requestforissuancedetail.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));

			return requestforissuancedetail;
		}
	}
}