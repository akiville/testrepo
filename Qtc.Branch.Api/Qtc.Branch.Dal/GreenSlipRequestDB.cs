using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Validation;

namespace Qtc.Branch.Dal
{
	public class GreenSlipRequestDB
	{
		public static GreenSlipRequest GetItem(int greensliprequestId)
		{
			GreenSlipRequest greensliprequest = null;
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenSlipRequestSelectSingleItem";

				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", greensliprequestId);

				myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.Read())
					{
						greensliprequest = FillDataRecord(myReader);
					}
					myReader.Close();
				}
				myCommand.Connection.Close();
			}
			return greensliprequest;
		}


		public static GreenSlipRequestCollection GetList(GreenSlipRequestCriteria greensliprequestCriteria)
		{
			GreenSlipRequestCollection tempList = new GreenSlipRequestCollection();
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenSlipRequestSearchList";

                Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", greensliprequestCriteria.mBranchId);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@taken_action ", greensliprequestCriteria.mTakenAction);
                Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_downloaded ", greensliprequestCriteria.mIsDownloaded);
                Helpers.CreateParameter(myCommand, DbType.Int32, "@id ", greensliprequestCriteria.mId);

                myCommand.Connection.Open();
				using (DbDataReader myReader = myCommand.ExecuteReader())
				{
					if (myReader.HasRows)
					{
						tempList = new GreenSlipRequestCollection();
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

		public static int SelectCountForGetList(GreenSlipRequestCriteria greensliprequestCriteria)
		{
			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenSlipRequestSearchList";

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

		public static int Save(GreenSlipRequest myGreenSlipRequest)
		{
			if (!myGreenSlipRequest.Validate())
			{
				throw new InvalidSaveOperationException("Can't save a greensliprequest in an Invalid state. Make sure that IsValid() returns true before you call Save().");
			}
			int result = 0;

			using (DbCommand myCommand = AppConfiguration.CreateCommand())
			{
				myCommand.CommandType = CommandType.StoredProcedure;
				myCommand.CommandText = "Qt_spGreenSlipRequestInsertUpdateSingleItem";

				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date", myGreenSlipRequest.mDate);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@date_requested", myGreenSlipRequest.mDateRequested);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@branch_id", myGreenSlipRequest.mBranchId);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@requested_by_id", myGreenSlipRequest.mRequestedById);
				Helpers.CreateParameter(myCommand, DbType.String, "@type", myGreenSlipRequest.mType);
				Helpers.CreateParameter(myCommand, DbType.String, "@type2", myGreenSlipRequest.mType2);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@greenslip_request_id", myGreenSlipRequest.mGreenslipRequestId);
				Helpers.CreateParameter(myCommand, DbType.String, "@explanation", myGreenSlipRequest.mExplanation);
				Helpers.CreateParameter(myCommand, DbType.String, "@remarks", myGreenSlipRequest.mRemarks);
				Helpers.CreateParameter(myCommand, DbType.String, "@approved_by", myGreenSlipRequest.mApprovedBy);
				Helpers.CreateParameter(myCommand, DbType.Int32, "@number", myGreenSlipRequest.mNumber);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@denied", myGreenSlipRequest.mDenied);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@cancelled", myGreenSlipRequest.mCancelled);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@taken_action", myGreenSlipRequest.mTakenAction);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@delivery_date", myGreenSlipRequest.mDeliveryDate);
				Helpers.CreateParameter(myCommand, DbType.Boolean, "@is_downloaded", myGreenSlipRequest.mIsDownloaded);
				Helpers.CreateParameter(myCommand, DbType.DateTime, "@app_download_date", myGreenSlipRequest.mAppDownloadDate);

				Helpers.SetSaveParameters(myCommand, myGreenSlipRequest);
				myCommand.Connection.Open();
				int numberOfRecordsAffected = myCommand.ExecuteNonQuery();
				if (numberOfRecordsAffected == 0)
				{
					throw new DBConcurrencyException("Can't update greensliprequest as it has been updated by someone else");
				}
				//myGreenSlipRequest.mConcurrencyId = Helpers.GetConcurrencyId(myCommand);
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
				myCommand.CommandText = "Qt_spGreenSlipRequestDeleteSingleItem";
				Helpers.CreateParameter(myCommand, DbType.Int32, "@id", id);
				myCommand.Connection.Open();
				result = myCommand.ExecuteNonQuery();
				myCommand.Connection.Close();
			}
			return result > 0;
		}

		private static GreenSlipRequest FillDataRecord(IDataRecord myDataRecord)
		{
			GreenSlipRequest greensliprequest = new GreenSlipRequest();

			greensliprequest.mId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("id"));
			greensliprequest.mDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date"));
			greensliprequest.mDateRequested = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("date_requested"));
			greensliprequest.mBranchId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("branch_id"));
			greensliprequest.mRequestedById = myDataRecord.GetInt32(myDataRecord.GetOrdinal("requested_by_id"));
			greensliprequest.mType = myDataRecord.GetString(myDataRecord.GetOrdinal("type"));
			greensliprequest.mType2 = myDataRecord.GetString(myDataRecord.GetOrdinal("type2"));
			greensliprequest.mGreenslipRequestId = myDataRecord.GetInt32(myDataRecord.GetOrdinal("greenslip_request_id"));
			greensliprequest.mExplanation = myDataRecord.GetString(myDataRecord.GetOrdinal("explanation"));
			greensliprequest.mRemarks = myDataRecord.GetString(myDataRecord.GetOrdinal("remarks"));
			greensliprequest.mApprovedBy = myDataRecord.GetString(myDataRecord.GetOrdinal("approved_by"));
			greensliprequest.mNumber = myDataRecord.GetInt32(myDataRecord.GetOrdinal("number"));
			greensliprequest.mDenied = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("denied"));
			greensliprequest.mCancelled = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("cancelled"));
			greensliprequest.mTakenAction = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("taken_action"));
			greensliprequest.mDeliveryDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("delivery_date"));
			greensliprequest.mIsDownloaded = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("is_downloaded"));
			greensliprequest.mAppDownloadDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("app_download_date"));
			greensliprequest.mDisable = myDataRecord.GetBoolean(myDataRecord.GetOrdinal("disable"));

			return greensliprequest;
		}
	}
}