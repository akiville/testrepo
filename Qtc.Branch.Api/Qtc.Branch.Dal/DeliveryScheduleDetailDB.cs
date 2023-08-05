using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DeliveryScheduleDetailDB
	{
		public static DeliveryScheduleDetail GetItem(int deliveryscheduledetailId)
		{
			DeliveryScheduleDetail deliveryscheduledetail = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleDetailSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", deliveryscheduledetailId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						deliveryscheduledetail = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return deliveryscheduledetail;
		}

		public static DeliveryScheduleDetailCollection GetList(DeliveryScheduleDetailCriteria deliveryscheduledetailCriteria)
		{
			DeliveryScheduleDetailCollection tempList = new DeliveryScheduleDetailCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleDetailSearchList";

                Helpers.CreateParameter(myCommand, DbType.DateTime, "@delivery_date", deliveryscheduledetailCriteria.mDeliveryDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", deliveryscheduledetailCriteria.mBranchId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DeliveryScheduleDetailCollection();
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

		public static int SelectCountForGetList(DeliveryScheduleDetailCriteria deliveryscheduledetailCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleDetailSearchList";

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

		public static int Save(DeliveryScheduleDetail myDeliveryScheduleDetail)
		{
			if (!myDeliveryScheduleDetail.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a deliveryscheduledetail in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleDetailInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@delivery_date", myDeliveryScheduleDetail.mDeliveryDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myDeliveryScheduleDetail.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myDeliveryScheduleDetail.mProductId);
				Helpers.CreateParameter(myCommand, DbType.String, "@product", myDeliveryScheduleDetail.mProduct);
				Helpers.CreateParameter(myCommand, DbType.String, "@code", myDeliveryScheduleDetail.mCode);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@planned", myDeliveryScheduleDetail.mPlanned);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@additional", myDeliveryScheduleDetail.mAdditional);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@cancel", myDeliveryScheduleDetail.mCancel);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@ddir", myDeliveryScheduleDetail.mDdir);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myDeliveryScheduleDetail.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myDeliveryScheduleDetail);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update deliveryscheduledetail as it has been updated by someone else");
				}
				//myDeliveryScheduleDetail.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDeliveryScheduleDetailDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static DeliveryScheduleDetail FillDataRecord(IDataRecord myDataRecord)
		{
			DeliveryScheduleDetail deliveryscheduledetail = new DeliveryScheduleDetail();

			deliveryscheduledetail.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			deliveryscheduledetail.mDeliveryDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("delivery_date"));
			deliveryscheduledetail.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			deliveryscheduledetail.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
			deliveryscheduledetail.mProduct = myDataRecord.GetString(myDataRecord.GetOrdinal("product"));
			deliveryscheduledetail.mCode = myDataRecord.GetString(myDataRecord.GetOrdinal("code"));
			deliveryscheduledetail.mPlanned = myDataRecord.GetInt32(myDataRecord.GetOrdinal("planned"));
			deliveryscheduledetail.mAdditional = myDataRecord.GetInt32(myDataRecord.GetOrdinal("additional"));
			deliveryscheduledetail.mCancel = myDataRecord.GetInt32(myDataRecord.GetOrdinal("cancel"));
			deliveryscheduledetail.mDdir = myDataRecord.GetInt32(myDataRecord.GetOrdinal("ddir"));
			deliveryscheduledetail.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
			deliveryscheduledetail.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return deliveryscheduledetail;
		}
	}
}