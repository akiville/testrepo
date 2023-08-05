using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class GreenSlipDetailDB
	{
		public static GreenSlipDetail GetItem(int greenslipdetailId)
		{
			GreenSlipDetail greenslipdetail = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenSlipDetailSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", greenslipdetailId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						greenslipdetail = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return greenslipdetail;
		}

		public static GreenSlipDetailCollection GetList(GreenSlipDetailCriteria greenslipdetailCriteria)
		{
			GreenSlipDetailCollection tempList = new GreenSlipDetailCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenSlipDetailSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@green_slip_id", greenslipdetailCriteria.mGreenSlipId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new GreenSlipDetailCollection();
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

		public static int SelectCountForGetList(GreenSlipDetailCriteria greenslipdetailCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenSlipDetailSearchList";

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

		public static int Save(GreenSlipDetail myGreenSlipDetail)
		{
			if (!myGreenSlipDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a greenslipdetail in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenSlipDetailInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@green_slip_id", myGreenSlipDetail.mGreenSlipId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myGreenSlipDetail.mProductId);
				Helpers.CreateParameter(myCommand, DbType.Double, "@other_quantity", myGreenSlipDetail.mOtherQuantity);
				Helpers.CreateParameter(myCommand, DbType.Double, "@quantity", myGreenSlipDetail.mQuantity);
				Helpers.CreateParameter(myCommand, DbType.Double, "@approved_quantity", myGreenSlipDetail.mApprovedQuantity);
				Helpers.CreateParameter(myCommand, DbType.Double, "@other_cancel", myGreenSlipDetail.mOtherCancel);
				Helpers.CreateParameter(myCommand, DbType.Double, "@cancel", myGreenSlipDetail.mCancel);
				Helpers.CreateParameter(myCommand, DbType.Double, "@approved_cancel", myGreenSlipDetail.mApprovedCancel);
				Helpers.CreateParameter(myCommand, DbType.Double, "@dispatch", myGreenSlipDetail.mDispatch);
				Helpers.CreateParameter(myCommand, DbType.Double, "@received_quantity", myGreenSlipDetail.mReceivedQuantity);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myGreenSlipDetail.mRemarks);

				Helpers.SetSaveParameters(myCommand, myGreenSlipDetail);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update greenslipdetail as it has been updated by someone else");
				}
				//myGreenSlipDetail.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spGreenSlipDetailDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static GreenSlipDetail FillDataRecord(IDataRecord myDataRecord)
		{
			GreenSlipDetail greenslipdetail = new GreenSlipDetail();

			greenslipdetail.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			greenslipdetail.mGreenSlipId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("green_slip_id"));
			greenslipdetail.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
			greenslipdetail.mOtherQuantity = myDataRecord.GetDouble(myDataRecord.GetOrdinal("other_quantity"));
			greenslipdetail.mQuantity = myDataRecord.GetDouble(myDataRecord.GetOrdinal("quantity"));
			greenslipdetail.mApprovedQuantity = myDataRecord.GetDouble(myDataRecord.GetOrdinal("approved_quantity"));
			greenslipdetail.mOtherCancel = myDataRecord.GetDouble(myDataRecord.GetOrdinal("other_cancel"));
			greenslipdetail.mCancel = myDataRecord.GetDouble(myDataRecord.GetOrdinal("cancel"));
			greenslipdetail.mApprovedCancel = myDataRecord.GetDouble(myDataRecord.GetOrdinal("approved_cancel"));
			greenslipdetail.mDispatch = myDataRecord.GetDouble(myDataRecord.GetOrdinal("dispatch"));
			greenslipdetail.mReceivedQuantity = myDataRecord.GetDouble(myDataRecord.GetOrdinal("received_quantity"));
			greenslipdetail.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			greenslipdetail.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			//greenslipdetail.mConcurrencyId = (byte[]) myDataRecord.GetValue(myDataRecord.GetOrdinal("concurrency_id"));

			return greenslipdetail;
		}
	}
}