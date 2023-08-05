using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DeliveryDB
	{
		public static Delivery GetItem(int deliveryId)
		{
			Delivery delivery = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliverySelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", deliveryId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						delivery = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return delivery;
		}

		public static DeliveryCollection GetList(DeliveryCriteria deliveryCriteria)
		{
			DeliveryCollection tempList = new DeliveryCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliverySearchList";

                Helpers.CreateParameter(myCommand, DbType.String, "@branch_id", deliveryCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.String, "@date", deliveryCriteria.mDeliveryDate);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DeliveryCollection();
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

		public static int SelectCountForGetList(DeliveryCriteria deliveryCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliverySearchList";

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

		public static int Save(Delivery myDelivery)
		{
			if (!myDelivery.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a delivery in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@delivery_date", myDelivery.mDeliveryDate);
				Helpers.CreateParameter(myCommand, DbType.String, "@planned_by", myDelivery.mPlannedBy);
				Helpers.CreateParameter(myCommand, DbType.String, "@trucking", myDelivery.mTrucking);
				Helpers.CreateParameter(myCommand, DbType.String, "@driver", myDelivery.mDriver);
				Helpers.CreateParameter(myCommand, DbType.String, "@crew", myDelivery.mCrew);
				Helpers.CreateParameter(myCommand, DbType.String, "@branch", myDelivery.mBranch);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myDelivery.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.String, "@delivery_schedule", myDelivery.mDeliverySchedule);
				Helpers.CreateParameter(myCommand, DbType.String, "@eta", myDelivery.mEta);
				Helpers.CreateParameter(myCommand, DbType.String, "@crew_to_drop", myDelivery.mCrewToDrop);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myDelivery.mDatestamp);

				Helpers.SetSaveParameters(myCommand, myDelivery);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update delivery as it has been updated by someone else");
				}
				//myDelivery.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDeliveryDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static Delivery FillDataRecord(IDataRecord myDataRecord)
		{
			Delivery delivery = new Delivery();

			delivery.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			delivery.mDeliveryDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("delivery_date"));
			delivery.mPlannedBy = myDataRecord.GetString(myDataRecord.GetOrdinal("planned_by"));
			delivery.mTrucking = myDataRecord.GetString(myDataRecord.GetOrdinal("trucking"));
			delivery.mDriver = myDataRecord.GetString(myDataRecord.GetOrdinal("driver"));
			delivery.mCrew = myDataRecord.GetString(myDataRecord.GetOrdinal("crew"));
			delivery.mBranch = myDataRecord.GetString(myDataRecord.GetOrdinal("branch"));
			delivery.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			delivery.mDeliverySchedule = myDataRecord.GetString(myDataRecord.GetOrdinal("delivery_schedule"));
			delivery.mEta = myDataRecord.GetString(myDataRecord.GetOrdinal("eta"));
			delivery.mCrewToDrop = myDataRecord.GetString(myDataRecord.GetOrdinal("crew_to_drop"));
			delivery.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			delivery.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));
            delivery.mIsDelivered = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_delivered"));

			return delivery;
		}
	}
}