using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RequestRepairDB
	{
		public static RequestRepair GetItem(int requestrepairId)
		{
			RequestRepair requestrepair = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestRepairSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", requestrepairId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						requestrepair = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return requestrepair;
		}

		public static RequestRepairCollection GetList(RequestRepairCriteria requestrepairCriteria)
		{
			RequestRepairCollection tempList = new RequestRepairCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestRepairSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "branch_id", requestrepairCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "product_id", requestrepairCriteria.mProductId);
                Helpers.CreateParameter(myCommand, DbType.Int32, "requested_by_id", requestrepairCriteria.mRequestedById);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "start_date", requestrepairCriteria.mStartDate);
                Helpers.CreateParameter(myCommand, DbType.DateTime, "end_date", requestrepairCriteria.mEndDate);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RequestRepairCollection();
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

		public static int SelectCountForGetList(RequestRepairCriteria requestrepairCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestRepairSearchList";

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

		public static int Save(RequestRepair myRequestRepair)
		{
			if (!myRequestRepair.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a requestrepair in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestRepairInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@request_repair_id", myRequestRepair.mRequestRepairId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@number", myRequestRepair.mNumber);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date", myRequestRepair.mDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_incident", myRequestRepair.mDateIncident);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myRequestRepair.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@planned", myRequestRepair.mPlanned);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@contractor", myRequestRepair.mContractor);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@requested_by_id", myRequestRepair.mRequestedById);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@mc_id", myRequestRepair.mMcId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@approved_by_id", myRequestRepair.mApprovedById);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@code_id", myRequestRepair.mCodeId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_id", myRequestRepair.mProductId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_serial_id", myRequestRepair.mProductSerialId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@reason_id", myRequestRepair.mReasonId);
				Helpers.CreateParameter(myCommand, DbType.String, "@complaint", myRequestRepair.mComplaint);

				Helpers.SetSaveParameters(myCommand, myRequestRepair);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update requestrepair as it has been updated by someone else");
				}
				//myRequestRepair.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRequestRepairDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RequestRepair FillDataRecord(IDataRecord myDataRecord)
		{
			RequestRepair requestrepair = new RequestRepair();

			requestrepair.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			requestrepair.mRequestRepairId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("request_repair_id"));
			requestrepair.mNumber = myDataRecord.GetInt32(myDataRecord.GetOrdinal("number"));
			requestrepair.mDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date"));
			requestrepair.mDateIncident = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_incident"));
			requestrepair.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			requestrepair.mPlanned = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("planned"));
			requestrepair.mContractor = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("contractor"));
			requestrepair.mRequestedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("requested_by_id"));
			requestrepair.mMcId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("mc_id"));
			requestrepair.mApprovedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("approved_by_id"));
			requestrepair.mCodeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("code_id"));
			requestrepair.mProductId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_id"));
			requestrepair.mProductSerialId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_serial_id"));
			requestrepair.mReasonId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("reason_id"));
			requestrepair.mComplaint = myDataRecord.GetString(myDataRecord.GetOrdinal("complaint"));
			requestrepair.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
            requestrepair.mProductName = myDataRecord.GetString(myDataRecord.GetOrdinal("product_name"));
            requestrepair.mBranchName = myDataRecord.GetString(myDataRecord.GetOrdinal("branch_name"));
            requestrepair.mRequestedBy = myDataRecord.GetString(myDataRecord.GetOrdinal("requested_by"));
            requestrepair.mReasonName = myDataRecord.GetString(myDataRecord.GetOrdinal("reason_name"));

			return requestrepair;
		}
	}
}