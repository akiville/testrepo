using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class RequestForIssuanceDB
	{
		public static RequestForIssuance GetItem(int requestforissuanceId)
		{
			RequestForIssuance requestforissuance = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestForIssuanceSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", requestforissuanceId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						requestforissuance = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return requestforissuance;
		}

		public static RequestForIssuanceCollection GetList(RequestForIssuanceCriteria requestforissuanceCriteria)
		{
			RequestForIssuanceCollection tempList = new RequestForIssuanceCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestForIssuanceSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", requestforissuanceCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_ho_downloaded", requestforissuanceCriteria.mIsHoDownloaded);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new RequestForIssuanceCollection();
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

		public static int SelectCountForGetList(RequestForIssuanceCriteria requestforissuanceCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestForIssuanceSearchList";

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

		public static int Save(RequestForIssuance myRequestForIssuance)
		{
			if (!myRequestForIssuance.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a requestforissuance in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spRequestForIssuanceInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@issuance_id", myRequestForIssuance.mIssuanceId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_requested", myRequestForIssuance.mDateRequested);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_needed", myRequestForIssuance.mDateNeeded);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myRequestForIssuance.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@number", myRequestForIssuance.mNumber);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@requested_by_id", myRequestForIssuance.mRequestedById);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@processed_by_id", myRequestForIssuance.mProcessedById);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@department_id", myRequestForIssuance.mDepartmentId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@purpose_id", myRequestForIssuance.mPurposeId);
				Helpers.CreateParameter(myCommand, DbType.String, "@explanation", myRequestForIssuance.mExplanation);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@product_group_id", myRequestForIssuance.mProductGroupId);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@planned", myRequestForIssuance.mPlanned);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@cancelled", myRequestForIssuance.mCancelled);
				Helpers.CreateParameter(myCommand, DbType.String, "@cancelled_reason", myRequestForIssuance.mCancelledReason);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@issued_to_id", myRequestForIssuance.mIssuedToId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@request_for_repair_id", myRequestForIssuance.mRequestForRepairId);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@datestamp", myRequestForIssuance.mDatestamp);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_ho_downloaded", myRequestForIssuance.mIsHoDownloaded);

                Helpers.SetSaveParameters(myCommand, myRequestForIssuance);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update requestforissuance as it has been updated by someone else");
				}
				//myRequestForIssuance.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spRequestForIssuanceDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static RequestForIssuance FillDataRecord(IDataRecord myDataRecord)
		{
			RequestForIssuance requestforissuance = new RequestForIssuance();

			requestforissuance.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			requestforissuance.mIssuanceId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("issuance_id"));
			requestforissuance.mDateRequested = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_requested"));
			requestforissuance.mDateNeeded = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_needed"));
			requestforissuance.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			requestforissuance.mNumber = myDataRecord.GetInt32(myDataRecord.GetOrdinal("number"));
			requestforissuance.mRequestedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("requested_by_id"));
			requestforissuance.mProcessedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("processed_by_id"));
			requestforissuance.mDepartmentId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("department_id"));
			requestforissuance.mPurposeId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("purpose_id"));
			requestforissuance.mExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("explanation"));
			requestforissuance.mProductGroupId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("product_group_id"));
			requestforissuance.mPlanned = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("planned"));
			requestforissuance.mCancelled = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("cancelled"));
			requestforissuance.mCancelledReason = myDataRecord.GetString(myDataRecord.GetOrdinal("cancelled_reason"));
			requestforissuance.mIssuedToId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("issued_to_id"));
			requestforissuance.mRequestForRepairId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("request_for_repair_id"));
			requestforissuance.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));
			requestforissuance.mDatestamp = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("datestamp"));

			return requestforissuance;
		}
	}
}