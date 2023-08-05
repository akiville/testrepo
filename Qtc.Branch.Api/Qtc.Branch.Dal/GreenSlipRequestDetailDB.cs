using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class GreenSlipRequestDetailDB
	{
		public static GreenSlipRequestDetail GetItem(int greensliprequestdetailId)
		{
			GreenSlipRequestDetail greensliprequestdetail = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenSlipRequestDetailSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", greensliprequestdetailId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						greensliprequestdetail = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return greensliprequestdetail;
		}

		public static GreenSlipRequestDetailCollection GetList(GreenSlipRequestDetailCriteria greensliprequestdetailCriteria)
		{
			GreenSlipRequestDetailCollection tempList = new GreenSlipRequestDetailCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenSlipRequestDetailSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@greenslip_id", greensliprequestdetailCriteria.mGreenSlipId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new GreenSlipRequestDetailCollection();
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

		public static int SelectCountForGetList(GreenSlipRequestDetailCriteria greensliprequestdetailCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenSlipRequestDetailSearchList";

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

		public static int Save(GreenSlipRequestDetail myGreenSlipRequestDetail)
		{
			if (!myGreenSlipRequestDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a greensliprequestdetail in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenSlipRequestDetailInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@green_slip_id", myGreenSlipRequestDetail.mGreenSlipId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myGreenSlipRequestDetail.mProductId);
				Helpers.CreateParameter(myCommand, DbType.Double, "@other_quantity", myGreenSlipRequestDetail.mOtherQuantity);
				Helpers.CreateParameter(myCommand, DbType.Double, "@quantity", myGreenSlipRequestDetail.mQuantity);
				Helpers.CreateParameter(myCommand, DbType.Double, "@approved_quantity", myGreenSlipRequestDetail.mApprovedQuantity);
				Helpers.CreateParameter(myCommand, DbType.Double, "@other_cancel", myGreenSlipRequestDetail.mOtherCancel);
				Helpers.CreateParameter(myCommand, DbType.Double, "@cancel", myGreenSlipRequestDetail.mCancel);
				Helpers.CreateParameter(myCommand, DbType.Double, "@approved_cancel", myGreenSlipRequestDetail.mApprovedCancel);
				Helpers.CreateParameter(myCommand, DbType.Double, "@dispatch", myGreenSlipRequestDetail.mDispatch);
				Helpers.CreateParameter(myCommand, DbType.Double, "@received_quantity", myGreenSlipRequestDetail.mReceivedQuantity);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myGreenSlipRequestDetail.mRemarks);

				Helpers.SetSaveParameters(myCommand, myGreenSlipRequestDetail);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update greensliprequestdetail as it has been updated by someone else");
				}
				//myGreenSlipRequestDetail.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spGreenSlipRequestDetailDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static GreenSlipRequestDetail FillDataRecord(IDataRecord myDataRecord)
		{
			GreenSlipRequestDetail greensliprequestdetail = new GreenSlipRequestDetail();

			greensliprequestdetail.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			greensliprequestdetail.mGreenSlipId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("green_slip_id"));
			greensliprequestdetail.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
			greensliprequestdetail.mOtherQuantity = myDataRecord.GetDouble(myDataRecord.GetOrdinal("other_quantity"));
			greensliprequestdetail.mQuantity = myDataRecord.GetDouble(myDataRecord.GetOrdinal("quantity"));
			greensliprequestdetail.mApprovedQuantity = myDataRecord.GetDouble(myDataRecord.GetOrdinal("approved_quantity"));
			greensliprequestdetail.mOtherCancel = myDataRecord.GetDouble(myDataRecord.GetOrdinal("other_cancel"));
			greensliprequestdetail.mCancel = myDataRecord.GetDouble(myDataRecord.GetOrdinal("cancel"));
			greensliprequestdetail.mApprovedCancel = myDataRecord.GetDouble(myDataRecord.GetOrdinal("approved_cancel"));
			greensliprequestdetail.mDispatch = myDataRecord.GetDouble(myDataRecord.GetOrdinal("dispatch"));
			greensliprequestdetail.mReceivedQuantity = myDataRecord.GetDouble(myDataRecord.GetOrdinal("received_quantity"));
			greensliprequestdetail.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			greensliprequestdetail.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return greensliprequestdetail;
		}
	}
}