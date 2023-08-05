using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DeliveryScheduleDB
	{
		public static DeliverySchedule GetItem(int deliveryscheduleId)
		{
			DeliverySchedule deliveryschedule = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", deliveryscheduleId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						deliveryschedule = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return deliveryschedule;
		}

		public static DeliveryScheduleCollection GetList(DeliveryScheduleCriteria deliveryscheduleCriteria)
		{
			DeliveryScheduleCollection tempList = new DeliveryScheduleCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", deliveryscheduleCriteria.mLmmId);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@delivery_start_date", deliveryscheduleCriteria.mDeliveryStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "@delivery_end_date", deliveryscheduleCriteria.mDeliveryEndDate);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DeliveryScheduleCollection();
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

		public static int SelectCountForGetList(DeliveryScheduleCriteria deliveryscheduleCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleSearchList";

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

		public static int Save(DeliverySchedule myDeliverySchedule)
		{
			if (!myDeliverySchedule.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a deliveryschedule in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myDeliverySchedule.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@delivery_schedule_id", myDeliverySchedule.mDeliveryScheduleId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myDeliverySchedule.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@eta", myDeliverySchedule.mEta);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@delivery_date", myDeliverySchedule.mDeliveryDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@delivery_time", myDeliverySchedule.mDeliveryTime);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@record_id", myDeliverySchedule.mRecordId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datetime", myDeliverySchedule.mDatetime);

				Helpers.SetSaveParameters(myCommand, myDeliverySchedule);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update deliveryschedule as it has been updated by someone else");
				}
				//myDeliverySchedule.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDeliveryScheduleDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static DeliverySchedule FillDataRecord(IDataRecord myDataRecord)
		{
			DeliverySchedule deliveryschedule = new DeliverySchedule();

			deliveryschedule.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			deliveryschedule.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			deliveryschedule.mDeliveryScheduleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("delivery_schedule_id"));
			deliveryschedule.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			deliveryschedule.mEta = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("eta"));
			deliveryschedule.mDeliveryDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("delivery_date"));
			deliveryschedule.mDeliveryTime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("delivery_time"));
			deliveryschedule.mRecordId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("record_id"));
			deliveryschedule.mDatetime = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datetime"));
			deliveryschedule.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            deliveryschedule.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            deliveryschedule.mConcernId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("concern_id"));
			return deliveryschedule;
		}
	}
}