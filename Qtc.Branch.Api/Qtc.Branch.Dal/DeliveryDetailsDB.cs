using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DeliveryDetailsDB
	{
		public static DeliveryDetails GetItem(int deliverydetailsId)
		{
			DeliveryDetails deliverydetails = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryDetailsSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", deliverydetailsId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						deliverydetails = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return deliverydetails;
		}

		public static DeliveryDetailsCollection GetList(DeliveryDetailsCriteria deliverydetailsCriteria)
		{
			DeliveryDetailsCollection tempList = new DeliveryDetailsCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryDetailsSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@dispatch_id", deliverydetailsCriteria.mDispatchId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DeliveryDetailsCollection();
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

		public static int SelectCountForGetList(DeliveryDetailsCriteria deliverydetailsCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryDetailsSearchList";

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

		public static int Save(DeliveryDetails myDeliveryDetails)
		{
			if (!myDeliveryDetails.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a deliverydetails in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryDetailsInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@dispatch_id", myDeliveryDetails.mDispatchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myDeliveryDetails.mProductId);
				Helpers.CreateParameter(myCommand, DbType.String, "@product", myDeliveryDetails.mProduct);
				Helpers.CreateParameter(myCommand, DbType.String, "@code", myDeliveryDetails.mCode);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@planned", myDeliveryDetails.mPlanned);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@additional", myDeliveryDetails.mAdditional);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@cancel", myDeliveryDetails.mCancel);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@ddir", myDeliveryDetails.mDdir);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myDeliveryDetails.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myDeliveryDetails);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update deliverydetails as it has been updated by someone else");
				}
				//myDeliveryDetails.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDeliveryDetailsDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static DeliveryDetails FillDataRecord(IDataRecord myDataRecord)
		{
			DeliveryDetails deliverydetails = new DeliveryDetails();

			deliverydetails.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			deliverydetails.mDispatchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("dispatch_id"));
			deliverydetails.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
			deliverydetails.mProduct = myDataRecord.GetString(myDataRecord.GetOrdinal("product"));
			deliverydetails.mCode = myDataRecord.GetString(myDataRecord.GetOrdinal("code"));
			deliverydetails.mPlanned = myDataRecord.GetInt32(myDataRecord.GetOrdinal("planned"));
			deliverydetails.mAdditional = myDataRecord.GetInt32(myDataRecord.GetOrdinal("additional"));
			deliverydetails.mCancel = myDataRecord.GetInt32(myDataRecord.GetOrdinal("cancel"));
			deliverydetails.mDdir = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ddir"));
			deliverydetails.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			deliverydetails.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return deliverydetails;
		}
	}
}