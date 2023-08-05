using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class DeliveryScheduleConcernDB
	{
		public static DeliveryScheduleConcern GetItem(int deliveryscheduleconcernId)
		{
			DeliveryScheduleConcern deliveryscheduleconcern = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleConcernSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", deliveryscheduleconcernId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						deliveryscheduleconcern = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return deliveryscheduleconcern;
		}

		public static DeliveryScheduleConcernCollection GetList(DeliveryScheduleConcernCriteria deliveryscheduleconcernCriteria)
		{
			DeliveryScheduleConcernCollection tempList = new DeliveryScheduleConcernCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleConcernSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", deliveryscheduleconcernCriteria.mLmmId);
                if (deliveryscheduleconcernCriteria.mDeliveryDate != DateTime.MinValue)
                    Helpers.CreateParameter(myCommand, DbType.DateTime, "@delivery_date", deliveryscheduleconcernCriteria.mDeliveryDate);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@delivery_schedule_id", deliveryscheduleconcernCriteria.mDeliveryScheduleId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new DeliveryScheduleConcernCollection();
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

		public static int SelectCountForGetList(DeliveryScheduleConcernCriteria deliveryscheduleconcernCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleConcernSearchList";

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

		public static int Save(DeliveryScheduleConcern myDeliveryScheduleConcern)
		{
			if (!myDeliveryScheduleConcern.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a deliveryscheduleconcern in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spDeliveryScheduleConcernInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@delivery_schedule_id", myDeliveryScheduleConcern.mDeliveryScheduleId);
				Helpers.CreateParameter(myCommand, DbType.String, "@explanation", myDeliveryScheduleConcern.mExplanation);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@delivery_date", myDeliveryScheduleConcern.mDeliveryDate);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@lmm_id", myDeliveryScheduleConcern.mLmmId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@report_date", myDeliveryScheduleConcern.mReportDate);

				Helpers.SetSaveParameters(myCommand, myDeliveryScheduleConcern);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update deliveryscheduleconcern as it has been updated by someone else");
				}
				//myDeliveryScheduleConcern.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spDeliveryScheduleConcernDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static DeliveryScheduleConcern FillDataRecord(IDataRecord myDataRecord)
		{
			DeliveryScheduleConcern deliveryscheduleconcern = new DeliveryScheduleConcern();

			deliveryscheduleconcern.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			deliveryscheduleconcern.mDeliveryScheduleId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("delivery_schedule_id"));
			deliveryscheduleconcern.mExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("explanation"));
			deliveryscheduleconcern.mDeliveryDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("delivery_date"));
			deliveryscheduleconcern.mLmmId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("lmm_id"));
			deliveryscheduleconcern.mReportDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("report_date"));
			deliveryscheduleconcern.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return deliveryscheduleconcern;
		}
	}
}