using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DeliveryScheduleConcernDetailDB
	{
		public static DeliveryScheduleConcernDetail GetItem(int deliveryscheduleconcerndetailId)
		{
			DeliveryScheduleConcernDetail deliveryscheduleconcerndetail = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleConcernDetailSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", deliveryscheduleconcerndetailId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						deliveryscheduleconcerndetail = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return deliveryscheduleconcerndetail;
		}

		public static DeliveryScheduleConcernDetailCollection GetList(DeliveryScheduleConcernDetailCriteria deliveryscheduleconcerndetailCriteria)
		{
			DeliveryScheduleConcernDetailCollection tempList = new DeliveryScheduleConcernDetailCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleConcernDetailSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", deliveryscheduleconcerndetailCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@delivery_schedule_concern_id", deliveryscheduleconcerndetailCriteria.mDeliveryScheduleConcernId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DeliveryScheduleConcernDetailCollection();
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

		public static int SelectCountForGetList(DeliveryScheduleConcernDetailCriteria deliveryscheduleconcerndetailCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleConcernDetailSearchList";

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

		public static int Save(DeliveryScheduleConcernDetail myDeliveryScheduleConcernDetail)
		{
			if (!myDeliveryScheduleConcernDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a deliveryscheduleconcerndetail in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleConcernDetailInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@delivery_schedule_concern_id", myDeliveryScheduleConcernDetail.mDeliveryScheduleConcernId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@delivery_schedule_detail_id", myDeliveryScheduleConcernDetail.mDeliveryScheduleDetailId);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myDeliveryScheduleConcernDetail.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@actual_item_qty_received", myDeliveryScheduleConcernDetail.mActualItemQtyReceived);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myDeliveryScheduleConcernDetail.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myDeliveryScheduleConcernDetail.mProductId);
				Helpers.SetSaveParameters(myCommand, myDeliveryScheduleConcernDetail);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update deliveryscheduleconcerndetail as it has been updated by someone else");
				}
				//myDeliveryScheduleConcernDetail.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDeliveryScheduleConcernDetailDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static DeliveryScheduleConcernDetail FillDataRecord(IDataRecord myDataRecord)
		{
			DeliveryScheduleConcernDetail deliveryscheduleconcerndetail = new DeliveryScheduleConcernDetail();

			deliveryscheduleconcerndetail.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			deliveryscheduleconcerndetail.mDeliveryScheduleConcernId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("delivery_schedule_concern_id"));
			deliveryscheduleconcerndetail.mDeliveryScheduleDetailId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("delivery_schedule_detail_id"));
			deliveryscheduleconcerndetail.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			deliveryscheduleconcerndetail.mActualItemQtyReceived = myDataRecord.GetInt32(myDataRecord.GetOrdinal("actual_item_qty_received"));
			deliveryscheduleconcerndetail.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			deliveryscheduleconcerndetail.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            deliveryscheduleconcerndetail.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
            deliveryscheduleconcerndetail.mName = myDataRecord.GetString(myDataRecord.GetOrdinal("name"));

            return deliveryscheduleconcerndetail;
		}
	}
}